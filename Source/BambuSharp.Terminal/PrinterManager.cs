using BambuSharp.Terminal.Configuration;
using System.Text.Json;

namespace BambuSharp.Terminal;

/// <summary>
/// Manages printer configurations and LocalPrinter instances.
/// </summary>
public class PrinterManager : IDisposable
{
    private readonly string _configFilePath;
    private readonly Dictionary<string, LocalPrinter> _printers = new();
    private AppConfiguration _configuration = new();

    public PrinterManager()
    {
        var appDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "BambuSharp");

        Directory.CreateDirectory(appDataPath);
        _configFilePath = Path.Combine(appDataPath, "printers.json");
    }

    /// <summary>
    /// Gets the list of configured printers.
    /// </summary>
    public IReadOnlyList<PrinterConfig> ConfiguredPrinters => _configuration.Printers;

    /// <summary>
    /// Gets a connected printer instance by IP address.
    /// </summary>
    public LocalPrinter? GetPrinter(string ipAddress)
    {
        _printers.TryGetValue(ipAddress, out var printer);
        return printer;
    }

    /// <summary>
    /// Loads printer configurations from the JSON file.
    /// </summary>
    public void LoadConfigurationsAsync()
    {
        if (!File.Exists(_configFilePath))
        {
            _configuration = new AppConfiguration();
            return;
        }

        try
        {
            var json = File.ReadAllText(_configFilePath);
            _configuration = JsonSerializer.Deserialize<AppConfiguration>(json) ?? new AppConfiguration();
        }
        catch
        {
            // If config file is corrupted, start with empty configuration
            _configuration = new AppConfiguration();
        }
    }

    /// <summary>
    /// Saves the current printer configurations to the JSON file.
    /// </summary>
    public async Task SaveConfigurationsAsync()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(_configuration, options);
        await File.WriteAllTextAsync(_configFilePath, json);
    }

    /// <summary>
    /// Adds a new printer configuration and creates a LocalPrinter instance.
    /// </summary>
    public async Task<LocalPrinter> AddPrinterAsync(PrinterConfig config)
    {
        // Check if printer already exists
        if (_configuration.Printers.Any(p => p.IpAddress == config.IpAddress))
        {
            throw new InvalidOperationException($"Printer with IP {config.IpAddress} already exists.");
        }

        // Add to configuration
        _configuration.Printers.Add(config);
        await SaveConfigurationsAsync();

        // Create and store printer instance
        var printer = new LocalPrinter(config.IpAddress, config.AccessCode);
        _printers[config.IpAddress] = printer;

        return printer;
    }

    /// <summary>
    /// Removes a printer configuration and disconnects it.
    /// </summary>
    public async Task RemovePrinterAsync(string ipAddress)
    {
        var config = _configuration.Printers.FirstOrDefault(p => p.IpAddress == ipAddress);
        if (config != null)
        {
            _configuration.Printers.Remove(config);
            await SaveConfigurationsAsync();
        }

        if (_printers.TryGetValue(ipAddress, out var printer))
        {
            await printer.Disconnect();
            printer.Dispose();
            _printers.Remove(ipAddress);
        }
    }

    /// <summary>
    /// Connects to all configured printers.
    /// </summary>
    public async Task ConnectAllAsync()
    {
        foreach (var config in _configuration.Printers)
        {
            if (!_printers.ContainsKey(config.IpAddress))
            {
                var printer = new LocalPrinter(config.IpAddress, config.AccessCode);
                _printers[config.IpAddress] = printer;
            }

            try
            {
                await _printers[config.IpAddress].Connect();
            }
            catch
            {
                // Continue connecting to other printers even if one fails
                // Errors will be displayed in the UI
            }
        }
    }

    /// <summary>
    /// Disconnects all printers.
    /// </summary>
    public void DisconnectAll()
    {
        foreach (var printer in _printers.Values)
        {
            try
            {
                printer.Disconnect().Wait(1000);
            }
            catch
            {
                // Ignore disconnect errors during cleanup
            }
        }
    }

    public void Dispose()
    {
        DisconnectAll();

        foreach (var printer in _printers.Values)
        {
            printer.Dispose();
        }

        _printers.Clear();
        GC.SuppressFinalize(this);
    }
}
