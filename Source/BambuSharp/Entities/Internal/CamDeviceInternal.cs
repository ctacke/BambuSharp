using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Represents the camera system status.
/// </summary>
/// <remarks>
/// This class contains information about the camera and related sensors used for monitoring
/// the print job and detecting issues in real-time.
/// </remarks>
internal class CamDeviceInternal
{
    /// <summary>
    /// Gets or sets the laser camera sensor status.
    /// </summary>
    [JsonPropertyName("laser")]
    public LaserCamInternal Laser { get; set; } = new();
}
