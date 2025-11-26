using Meadow.Units;
using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Internal entity for JSON deserialization. Represents a single AMS unit with its humidity, temperature, and filament tray information.
/// </summary>
internal class AmsInternal
{
    /// <summary>
    /// Gets or sets the drying time for the filament in the AMS unit (in hours or minutes depending on context).
    /// </summary>
    [JsonPropertyName("dry_time")]
    public int DryTime { get; set; }

    /// <summary>
    /// Gets or sets the humidity percentage reading in the AMS unit.
    /// </summary>
    [JsonPropertyName("humidity")]
    public string Humidity { get; set; } = "";

    /// <summary>
    /// Gets or sets the raw humidity sensor reading for diagnostic purposes.
    /// </summary>
    [JsonPropertyName("humidity_raw")]
    public string HumidityRaw { get; set; } = "";

    /// <summary>
    /// Gets or sets the unique identifier for this AMS unit.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    /// <summary>
    /// Gets or sets additional information or metadata about the AMS unit.
    /// </summary>
    [JsonPropertyName("info")]
    public string Info { get; set; } = "";

    /// <summary>
    /// Gets or sets the temperature reading in the AMS unit
    /// </summary>
    [JsonPropertyName("temp")]
    [JsonConverter(typeof(TemperatureJsonConverter))]
    public Temperature Temperature { get; set; }

    /// <summary>
    /// Gets or sets the list of filament trays in this AMS unit.
    /// </summary>
    [JsonPropertyName("tray")]
    public List<TrayInternal> Tray { get; set; } = new();
}
