namespace BambuSharp.Terminal.Configuration;

/// <summary>
/// Represents the configuration for a single Bambu Lab printer.
/// </summary>
public class PrinterConfig
{
    /// <summary>
    /// Gets or sets the user-friendly name for the printer.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the IP address of the printer on the local network.
    /// </summary>
    public string IpAddress { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the access code for authenticating with the printer.
    /// </summary>
    public string AccessCode { get; set; } = string.Empty;
}

/// <summary>
/// Represents the application's configuration containing all configured printers.
/// </summary>
public class AppConfiguration
{
    /// <summary>
    /// Gets or sets the list of configured printers.
    /// </summary>
    public List<PrinterConfig> Printers { get; set; } = new();
}
