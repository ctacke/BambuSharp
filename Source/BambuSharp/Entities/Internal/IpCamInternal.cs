using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Contains IP camera configuration and recording settings.
/// </summary>
internal class IpCamInternal
{
    /// <summary>
    /// Gets or sets the Agora service configuration.
    /// </summary>
    [JsonPropertyName("agora_service")]
    public string AgoraService { get; set; } = "";

    /// <summary>
    /// Gets or sets the BRTC service configuration.
    /// </summary>
    [JsonPropertyName("brtc_service")]
    public string BrtcService { get; set; } = "";

    /// <summary>
    /// Gets or sets the backup service state.
    /// </summary>
    [JsonPropertyName("bs_state")]
    public int BsState { get; set; }

    /// <summary>
    /// Gets or sets the IP camera device identifier.
    /// </summary>
    [JsonPropertyName("ipcam_dev")]
    public string IpcamDev { get; set; } = "";

    /// <summary>
    /// Gets or sets the IP camera recording settings.
    /// </summary>
    [JsonPropertyName("ipcam_record")]
    public string IpcamRecord { get; set; } = "";

    /// <summary>
    /// Gets or sets the laser preview resolution.
    /// </summary>
    [JsonPropertyName("laser_preview_res")]
    public int LaserPreviewRes { get; set; }

    /// <summary>
    /// Gets or sets the mode bits configuration.
    /// </summary>
    [JsonPropertyName("mode_bits")]
    public int ModeBits { get; set; }

    /// <summary>
    /// Gets or sets the camera resolution.
    /// </summary>
    [JsonPropertyName("resolution")]
    public string Resolution { get; set; } = "";

    /// <summary>
    /// Gets or sets the RTSP URL for the camera stream.
    /// </summary>
    [JsonPropertyName("rtsp_url")]
    public string RtspUrl { get; set; } = "";

    /// <summary>
    /// Gets or sets the timelapse settings.
    /// </summary>
    [JsonPropertyName("timelapse")]
    public string Timelapse { get; set; } = "";

    /// <summary>
    /// Gets or sets the timelapse storage hard disk type.
    /// </summary>
    [JsonPropertyName("tl_store_hpd_type")]
    public int TlStoreHpdType { get; set; }

    /// <summary>
    /// Gets or sets the timelapse storage path type.
    /// </summary>
    [JsonPropertyName("tl_store_path_type")]
    public int TlStorePathType { get; set; }

    /// <summary>
    /// Gets or sets the TUTK server configuration.
    /// </summary>
    [JsonPropertyName("tutk_server")]
    public string TutkServer { get; set; } = "";
}
