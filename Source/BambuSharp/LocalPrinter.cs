using Meadow.Units;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BambuSharp;

/// <summary>
/// Represents a connection to a Bambu Lab printer on the local network via MQTT.
/// </summary>
public class LocalPrinter : IDisposable, INotifyPropertyChanged
{
    private readonly PrinterMqttCommsManager _commsManager;
    private Report? _lastReport;

    // Backing fields for properties
    private Temperature _bedTemperature;
    private Temperature _nozzleTemperature;
    private int _printProgress;
    private string _currentFileName = string.Empty;
    private PrinterState _state = PrinterState.Idle;

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Gets a value indicating whether the object has been disposed.
    /// </summary>
    public bool IsDisposed { get; private set; }

    /// <summary>
    /// Gets the IP address of the printer on the local network.
    /// </summary>
    public string IpAddress => _commsManager.IpAddress;

    /// <summary>
    /// Gets the current heated bed temperature.
    /// </summary>
    public Temperature BedTemperature
    {
        get => _bedTemperature;
        private set => SetProperty(ref _bedTemperature, value);
    }

    /// <summary>
    /// Gets the current nozzle (hotend) temperature.
    /// </summary>
    public Temperature NozzleTemperature
    {
        get => _nozzleTemperature;
        private set => SetProperty(ref _nozzleTemperature, value);
    }

    /// <summary>
    /// Gets the print completion percentage (0-100).
    /// </summary>
    public int PrintProgress
    {
        get => _printProgress;
        private set => SetProperty(ref _printProgress, value);
    }

    /// <summary>
    /// Gets the name of the currently loaded or printing G-code file.
    /// </summary>
    public string CurrentFileName
    {
        get => _currentFileName;
        private set => SetProperty(ref _currentFileName, value);
    }

    /// <summary>
    /// Gets the current operational state of the printer.
    /// </summary>
    public PrinterState State
    {
        get => _state;
        private set => SetProperty(ref _state, value);
    }

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

        // Update properties from the report - SetProperty will raise PropertyChanged events
        BedTemperature = report.Print.BedTemperature;
        NozzleTemperature = report.Print.NozzleTemperature;
        PrintProgress = report.Print.Percent;
        CurrentFileName = report.Print.GcodeFile;
        State = (PrinterState)report.Print.State;
    }

    /// <summary>
    /// Gets the last received report from the printer.
    /// </summary>
    /// <remarks>
    /// This property exposes the raw Report object with settable properties.
    /// It will be deprecated in a future version as more typed properties are added to LocalPrinter.
    /// Consider using the strongly-typed properties like BedTemperature, NozzleTemperature, etc. instead.
    /// </remarks>
    [Obsolete("This property exposes the raw Report object with settable properties. It will be deprecated in a future version as more typed properties are added to LocalPrinter. Consider using the strongly-typed properties like BedTemperature, NozzleTemperature, etc. instead.", false)]
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

    /// <summary>
    /// Raises the PropertyChanged event for the specified property.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed.</param>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Sets the property value and raises PropertyChanged if the value changed.
    /// </summary>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <param name="field">Reference to the backing field.</param>
    /// <param name="value">The new value.</param>
    /// <param name="propertyName">The name of the property (automatically provided).</param>
    /// <returns>True if the value changed; otherwise, false.</returns>
    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return false;
        }

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
