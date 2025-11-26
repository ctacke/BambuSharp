namespace BambuSharp;

/// <summary>
/// Represents the printer's nozzle with read-only specification and status information.
/// </summary>
public class Nozzle
{
    /// <summary>
    /// Gets the nozzle identifier.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Gets the nozzle diameter in millimeters.
    /// </summary>
    public double Diameter { get; }

    /// <summary>
    /// Gets the nozzle material type (e.g., "brass", "hardened").
    /// </summary>
    public string Type { get; }

    /// <summary>
    /// Gets the nozzle wear level as a percentage (0-100), where 100 indicates new and 0 indicates fully worn.
    /// </summary>
    public int WearPercent { get; }

    /// <summary>
    /// Initializes a new instance from internal nozzle info.
    /// </summary>
    internal Nozzle(NozzleInfoInternal info)
    {
        Id = info.Id;
        Diameter = info.Diameter;
        Type = info.Type;
        WearPercent = info.Wear;
    }
}
