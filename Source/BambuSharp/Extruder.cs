using Meadow.Units;

namespace BambuSharp;

/// <summary>
/// Represents the printer's extruder with read-only status information.
/// </summary>
public class Extruder
{
    /// <summary>
    /// Gets the extruder identifier.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Gets the current nozzle temperature.
    /// </summary>
    public Temperature CurrentTemperature { get; }

    /// <summary>
    /// Gets the target nozzle temperature.
    /// </summary>
    public Temperature TargetTemperature { get; }

    /// <summary>
    /// Gets the filament temperature.
    /// </summary>
    public Temperature FilamentTemperature { get; }

    /// <summary>
    /// Gets the extruder status state code.
    /// </summary>
    public int Status { get; }

    /// <summary>
    /// Initializes a new instance from internal extruder info.
    /// </summary>
    internal Extruder(ExtruderInfoInternal info)
    {
        Id = info.Id;
        CurrentTemperature = new Temperature(info.Hnow, Temperature.UnitType.Celsius);
        TargetTemperature = new Temperature(info.Htar, Temperature.UnitType.Celsius);
        FilamentTemperature = info.Temperature;
        Status = info.Stat;
    }
}
