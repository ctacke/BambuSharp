namespace BambuSharp;

/// <summary>
/// Represents the printer's IP camera with read-only configuration information.
/// </summary>
public class IpCamera
{
    /// <summary>
    /// Gets the IP camera recording settings.
    /// </summary>
    public string RecordingSetting { get; }

    /// <summary>
    /// Gets the camera resolution.
    /// </summary>
    public string Resolution { get; }

    /// <summary>
    /// Initializes a new instance from internal IP camera info.
    /// </summary>
    internal IpCamera(IpCamInternal ipCam)
    {
        RecordingSetting = ipCam.IpcamRecord;
        Resolution = ipCam.Resolution;
    }
}
