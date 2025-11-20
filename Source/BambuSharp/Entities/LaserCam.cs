using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Represents the laser camera sensor status.
/// </summary>
/// <remarks>
/// This class provides information about the laser-based camera sensor used for optical detection
/// and monitoring during print operations.
/// </remarks>
public class LaserCam
{
    /// <summary>
    /// Gets or sets the condition status of the laser camera sensor.
    /// </summary>
    [JsonPropertyName("cond")]
    public int Cond { get; set; }

    /// <summary>
    /// Gets or sets the operational state of the laser camera sensor.
    /// </summary>
    [JsonPropertyName("state")]
    public int State { get; set; }
}
