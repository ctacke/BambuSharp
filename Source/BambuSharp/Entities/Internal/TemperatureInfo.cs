using Meadow.Units;
using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Contains temperature reading information.
/// </summary>
/// <remarks>
/// This class provides a simple temperature data structure used to track thermal readings
/// from various components in the printer such as the bed, nozzle, and heating systems.
/// </remarks>
internal class TemperatureInfo
{
    /// <summary>
    /// Gets or sets the temperature reading.
    /// </summary>
    [JsonPropertyName("temp")]
    [JsonConverter(typeof(TemperatureJsonConverter))]
    public Temperature Temperature { get; set; }
}
