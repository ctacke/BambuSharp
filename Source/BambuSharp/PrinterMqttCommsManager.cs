using MQTTnet;
using MQTTnet.Formatter;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace BambuSharp;

internal class PrinterMqttCommsManager : IDisposable
{
    public event EventHandler<ReportInternal>? ReportReceived;

    public const int DefaultConnectTimeoutSeconds = 10;

    private readonly IMqttClient _mqttClient;
    private readonly MqttClientOptions? _mqttOptions;
    private TaskCompletionSource<ReportInternal>? _connectionReportTcs;

    public bool IsDisposed { get; private set; }
    public string IpAddress { get; }

    internal PrinterMqttCommsManager(string ipAddress, string accessCode)
    {
        IpAddress = ipAddress;

        var factory = new MQTTnet.MqttClientFactory();
        _mqttClient = factory.CreateMqttClient();

        _mqttOptions = new MqttClientOptionsBuilder()
            .WithTcpServer(IpAddress, 8883)
            .WithCredentials("bblp", accessCode)
            .WithProtocolVersion(MqttProtocolVersion.V311)
            .WithTlsOptions(opts =>
            {
                opts.WithCertificateValidationHandler(_ => true);
            })
            .WithCleanSession()
            .Build();


        _mqttClient.ApplicationMessageReceivedAsync += OnApplicationMessageReceived;
    }

    public async Task Connect(CancellationToken? cancellationToken)
    {
        if (_mqttClient.IsConnected)
        {
            Debug.WriteLine("Already connected to MQTT broker");
            return;
        }

        // Create a TaskCompletionSource to wait for the first report
        _connectionReportTcs = new TaskCompletionSource<ReportInternal>();

        Debug.WriteLine($"Connecting to MQTT broker at {IpAddress}:8883...");
        var connectResult = await _mqttClient.ConnectAsync(_mqttOptions, cancellationToken ?? CancellationToken.None);
        Debug.WriteLine($"MQTT connection result: {connectResult.ResultCode}");

        if (connectResult.ResultCode != MqttClientConnectResultCode.Success)
        {
            _connectionReportTcs = null;
            throw new Exception($"Failed to connect to MQTT broker: {connectResult.ResultCode} - {connectResult.ReasonString}");
        }

        // Try subscribing to multiple topic patterns - the printer might use any of these
        var topics = new[]
        {
            "device/+/report",      // Pattern used in some Bambu printers
            "bblp/printer/report",  // Alternative pattern
            "#"                     // Wildcard to catch all topics (for debugging)
        };

        foreach (var topic in topics)
        {
            var topicFilter = new MqttTopicFilterBuilder()
                .WithTopic(topic)
                .Build();

            var subscribeResult = await _mqttClient.SubscribeAsync(topicFilter, cancellationToken ?? CancellationToken.None);
            Debug.WriteLine($"Subscribed to topic '{topic}': {subscribeResult.Items.First().ResultCode}");
        }

        // Wait for the first report to arrive (with timeout)
        var timeoutTask = Task.Delay(TimeSpan.FromSeconds(DefaultConnectTimeoutSeconds), cancellationToken ?? CancellationToken.None);
        var completedTask = await Task.WhenAny(_connectionReportTcs.Task, timeoutTask);

        if (completedTask == timeoutTask)
        {
            _connectionReportTcs = null;
            throw new TimeoutException($"Timeout waiting for initial printer status report. No messages received on any subscribed topics.");
        }

        Debug.WriteLine("Successfully received first printer report");
    }

    public async Task Disconnect()
    {
        if (_mqttClient.IsConnected)
        {
            await _mqttClient.DisconnectAsync();
        }
    }

    private Task OnApplicationMessageReceived(MqttApplicationMessageReceivedEventArgs e)
    {
        var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
        Debug.WriteLine($"Received message on topic {e.ApplicationMessage.Topic}:");
        Debug.WriteLine(payload);

        try
        {
            var report = JsonSerializer.Deserialize<ReportInternal>(payload);

            // If this is the first report and we're waiting for it, complete the task
            if (_connectionReportTcs != null && !_connectionReportTcs.Task.IsCompleted)
            {
                _connectionReportTcs.TrySetResult(report!);
                _connectionReportTcs = null;
            }

            ReportReceived?.Invoke(this, report!);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to deserialize: {ex.Message}");

            // If we're waiting for the first report and deserialization failed, set exception
            if (_connectionReportTcs != null && !_connectionReportTcs.Task.IsCompleted)
            {
                _connectionReportTcs.TrySetException(ex);
                _connectionReportTcs = null;
            }
        }

        return Task.CompletedTask;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!IsDisposed)
        {
            if (disposing)
            {
                if (_mqttClient.IsConnected)
                {
                    _mqttClient.DisconnectAsync().Wait(1000);
                }

                _mqttClient?.Dispose();
            }

            IsDisposed = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
