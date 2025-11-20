namespace BambuSharp;

/// <summary>
/// Represents a connection to a Bambu Lab printer on the local network via MQTT.
/// </summary>
public class LocalPrinter : IDisposable
{
    private readonly PrinterMqttCommsManager _commsManager;
    private Report? _lastReport;

    /// <summary>
    /// Gets a value indicating whether the object has been disposed.
    /// </summary>
    public bool IsDisposed { get; private set; }

    /// <summary>
    /// Gets the IP address of the printer on the local network.
    /// </summary>
    public string IpAddress => _commsManager.IpAddress;

    /// <summary>
    /// Initializes a new instance of the <see cref="LocalPrinter"/> class.
    /// </summary>
    /// <param name="ipAddress">The IP address of the printer on the local network.</param>
    /// <param name="accessCode">The access code for authenticating with the printer.</param>
    public LocalPrinter(string ipAddress, string accessCode)
    {
        _commsManager = new PrinterMqttCommsManager(ipAddress, accessCode);
        _commsManager.ReportReceived += OnPrinterReportReceived;
    }

    public Task Connect(CancellationToken? cancellationToken = null)
    {
        // TODO: we need a state manager for online/offline state, connection retries, etc.

        return _commsManager.Connect(cancellationToken);
    }

    public Task Disconnect()
    {
        return _commsManager.Disconnect();
    }

    private void OnPrinterReportReceived(object? sender, Report report)
    {
        _lastReport = report;

        // TODO: raise events for interesting properties?  Or maybe just implement IPropertyChanged?
    }

    // TODO: this needs to get hidden/removed as we expand printer props
    public Report LastReport => _lastReport ?? throw new Exception("No report received from printer yet.");

    // TODO: should we (probably yes) expose more app-friendly entities here instead of just the raw result from the Report?
    //       this also would hide the settability of the Report objects from the user of this class.

    public AmsSystem AmsSystem => _lastReport?.Print.Ams ?? throw new Exception("No report received from printer yet.");
    public PrintLayerInfo LayerProgress => _lastReport?.Print.LayerInfo ?? throw new Exception("No report received from printer yet.");

    protected virtual void Dispose(bool disposing)
    {
        if (!IsDisposed)
        {
            if (disposing)
            {
                _commsManager.Dispose();
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
