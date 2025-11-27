using Meadow.Units;
using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Contains temperature reading information.
/// </summary>
/// <remarks>
/// This class provides a simple temperature data structure used to track thermal readings
/// from various components in the printer such as the bed, nozzle, and heating systems.
/// Temperature values in the JSON are raw ADC values in Q16.16 fixed-point format and must be divided by 65536 to get degrees Celsius.
/// </remarks>
internal class TemperatureInfo
{
    /// <summary>
    /// Gets or sets the temperature reading as a raw ADC value (Q16.16 fixed-point format).
    /// The JSON value is a large integer that must be divided by 65536 to get degrees Celsius.
    /// </summary>
    [JsonPropertyName("temp")]
    [JsonConverter(typeof(TemperatureJsonConverter))]
    public Temperature Temperature { get; set; }
}
