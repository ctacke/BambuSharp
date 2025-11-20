using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Represents the camera system status.
/// </summary>
/// <remarks>
/// This class contains information about the camera and related sensors used for monitoring
/// the print job and detecting issues in real-time.
/// </remarks>
public class CamDevice
{
    /// <summary>
    /// Gets or sets the laser camera sensor status.
    /// </summary>
    [JsonPropertyName("laser")]
    public LaserCam Laser { get; set; } = new();
}
