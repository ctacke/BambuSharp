namespace BambuSharp;

/// <summary>
/// Represents a printer light (chamber light, work light, etc.) with read-only status information.
/// </summary>
public class Light
{
    /// <summary>
    /// Gets the identifier of the light (e.g., "chamber_light", "work_light").
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the light mode (e.g., "on", "off", "flashing").
    /// </summary>
    public string Mode { get; }

    /// <summary>
    /// Gets a value indicating whether the light is currently on.
    /// </summary>
    public bool IsOn => Mode.Equals("on", StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Initializes a new instance from internal light report.
    /// </summary>
    internal Light(LightReportInternal lightReport)
    {
        Name = lightReport.Node;
        Mode = lightReport.Mode;
    }
}
