using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Represents the CTC (Core Temperature Control) device status.
/// </summary>
/// <remarks>
/// This class provides information about the core temperature control system and its operational state.
/// The CTC system manages temperature regulation across various heating elements in the printer.
/// </remarks>
public class CtcDevice
{
    /// <summary>
    /// Gets or sets the temperature information for the CTC system.
    /// </summary>
    [JsonPropertyName("info")]
    public TemperatureInfo Info { get; set; } = new();

    /// <summary>
    /// Gets or sets the operational state of the CTC system.
    /// </summary>
    [JsonPropertyName("state")]
    public int State { get; set; }
}
