using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Represents the laser device power and status.
/// </summary>
/// <remarks>
/// This class provides information about the laser engraver or cutting device integrated into the printer,
/// including its current power level and operational state.
/// </remarks>
public class LaserDevice
{
    /// <summary>
    /// Gets or sets the laser power level as a percentage (0-100).
    /// </summary>
    [JsonPropertyName("power")]
    public int Power { get; set; }
}
