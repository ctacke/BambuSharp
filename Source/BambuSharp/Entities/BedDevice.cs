using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Represents the heated print bed status and temperature.
/// </summary>
/// <remarks>
/// This class provides information about the bed's operational state and current temperature readings.
/// The bed is essential for adhesion and proper print quality.
/// </remarks>
public class BedDevice
{
    /// <summary>
    /// Gets or sets the temperature information for the bed.
    /// </summary>
    [JsonPropertyName("info")]
    public TemperatureInfo Info { get; set; } = new();

    /// <summary>
    /// Gets or sets the operational state of the bed (e.g., heating, cooling, idle).
    /// </summary>
    [JsonPropertyName("state")]
    public int State { get; set; }
}
