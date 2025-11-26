using Meadow.Units;

namespace BambuSharp;

/// <summary>
/// Represents an AMS (Automatic Material System) unit with essential status and configuration information.
/// This is a read-only wrapper around the internal AMS entity.
/// </summary>
public class Ams
{
    /// <summary>
    /// Gets the unique identifier for this AMS unit.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Gets the temperature reading in the AMS unit.
    /// </summary>
    public Temperature Temperature { get; }

    /// <summary>
    /// Gets the humidity percentage in the AMS unit (0-100).
    /// </summary>
    public int Humidity { get; }

    /// <summary>
    /// Gets the number of filament trays in this AMS unit.
    /// </summary>
    public int TrayCount { get; }

    /// <summary>
    /// Gets the read-only collection of filament trays in this AMS unit.
    /// </summary>
    public IReadOnlyList<Tray> Trays { get; }

    /// <summary>
    /// Initializes a new instance of the Ams class from an internal AMS entity.
    /// </summary>
    /// <param name="ams">The internal AMS entity to wrap.</param>
    internal Ams(AmsInternal ams)
    {
        Id = ams.Id;
        Temperature = ams.Temperature;

        // Parse humidity from string, defaulting to 0 if parsing fails
        Humidity = int.TryParse(ams.Humidity, out var humidity) ? humidity : 0;

        // Build read-only collection of public Tray wrappers
        var trays = ams.Tray.Select(t => new Tray(t)).ToList();
        Trays = trays.AsReadOnly();
        TrayCount = trays.Count;
    }
}
