using Meadow.Units;
using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Contains detailed extruder status including temperatures and filament state.
/// </summary>
/// <remarks>
/// This class provides comprehensive information about a specific extruder's current state,
/// including nozzle temperature readings, target temperatures, filament position, and status flags.
/// </remarks>
internal class ExtruderInfoInternal
{
    /// <summary>
    /// Gets or sets the backup filament information.
    /// </summary>
    [JsonPropertyName("filam_bak")]
    public List<object> FilamBak { get; set; } = new();

    /// <summary>
    /// Gets or sets the current hot-end nozzle temperature in degrees Celsius.
    /// </summary>
    [JsonPropertyName("hnow")]
    public int Hnow { get; set; }

    /// <summary>
    /// Gets or sets the previous hot-end nozzle temperature in degrees Celsius.
    /// </summary>
    [JsonPropertyName("hpre")]
    public int Hpre { get; set; }

    /// <summary>
    /// Gets or sets the target hot-end nozzle temperature in degrees Celsius.
    /// </summary>
    [JsonPropertyName("htar")]
    public int Htar { get; set; }

    /// <summary>
    /// Gets or sets the extruder identifier.
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets additional extruder information flags.
    /// </summary>
    [JsonPropertyName("info")]
    public int Info { get; set; }

    /// <summary>
    /// Gets or sets the current servo motor position.
    /// </summary>
    [JsonPropertyName("snow")]
    public int Snow { get; set; }

    /// <summary>
    /// Gets or sets the previous servo motor position.
    /// </summary>
    [JsonPropertyName("spre")]
    public int Spre { get; set; }

    /// <summary>
    /// Gets or sets the target servo motor position.
    /// </summary>
    [JsonPropertyName("star")]
    public int Star { get; set; }

    /// <summary>
    /// Gets or sets the current status state of the extruder.
    /// </summary>
    [JsonPropertyName("stat")]
    public int Stat { get; set; }

    /// <summary>
    /// Gets or sets the current filament temperature
    /// </summary>
    [JsonPropertyName("temp")]
    [JsonConverter(typeof(TemperatureJsonConverter))]
    public Temperature Temperature { get; set; }
}
