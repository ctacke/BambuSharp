using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Contains firmware upgrade status and version information.
/// </summary>
public class UpgradeState
{
    /// <summary>
    /// Gets or sets the new version number for the AHB (Bambu Lab cloud) firmware.
    /// </summary>
    [JsonPropertyName("ahb_new_version_number")]
    public string AhbNewVersionNumber { get; set; } = "";

    /// <summary>
    /// Gets or sets the new version number for the AMS (Automatic Material Station) firmware.
    /// </summary>
    [JsonPropertyName("ams_new_version_number")]
    public string AmsNewVersionNumber { get; set; } = "";

    /// <summary>
    /// Gets or sets a value indicating whether a consistency check is requested during the upgrade.
    /// </summary>
    [JsonPropertyName("consistency_request")]
    public bool ConsistencyRequest { get; set; }

    /// <summary>
    /// Gets or sets the display state code for the upgrade process.
    /// </summary>
    [JsonPropertyName("dis_state")]
    public int DisState { get; set; }

    /// <summary>
    /// Gets or sets the error code for the upgrade operation (0 indicates no error).
    /// </summary>
    [JsonPropertyName("err_code")]
    public int ErrCode { get; set; }

    /// <summary>
    /// Gets or sets the new version number for the EXT (external) firmware.
    /// </summary>
    [JsonPropertyName("ext_new_version_number")]
    public string ExtNewVersionNumber { get; set; } = "";

    /// <summary>
    /// Gets or sets a value indicating whether the upgrade is forced.
    /// </summary>
    [JsonPropertyName("force_upgrade")]
    public bool ForceUpgrade { get; set; }

    /// <summary>
    /// Gets or sets the upgrade sequence index.
    /// </summary>
    [JsonPropertyName("idx")]
    public int Idx { get; set; }

    /// <summary>
    /// Gets or sets a secondary upgrade sequence index.
    /// </summary>
    [JsonPropertyName("idx2")]
    public long Idx2 { get; set; }

    /// <summary>
    /// Gets or sets the lower limit version number for the upgrade.
    /// </summary>
    [JsonPropertyName("lower_limit")]
    public string LowerLimit { get; set; } = "";

    /// <summary>
    /// Gets or sets the upgrade status message.
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; set; } = "";

    /// <summary>
    /// Gets or sets the firmware module being upgraded.
    /// </summary>
    [JsonPropertyName("module")]
    public string Module { get; set; } = "";

    /// <summary>
    /// Gets or sets the new version state code.
    /// </summary>
    [JsonPropertyName("new_version_state")]
    public int NewVersionState { get; set; }

    /// <summary>
    /// Gets or sets the new version number for the OTA (Over-The-Air) firmware.
    /// </summary>
    [JsonPropertyName("ota_new_version_number")]
    public string OtaNewVersionNumber { get; set; } = "";

    /// <summary>
    /// Gets or sets the upgrade progress as a percentage or status string.
    /// </summary>
    [JsonPropertyName("progress")]
    public string Progress { get; set; } = "";

    /// <summary>
    /// Gets or sets the upgrade sequence identifier.
    /// </summary>
    [JsonPropertyName("sequence_id")]
    public int SequenceId { get; set; }

    /// <summary>
    /// Gets or sets the serial number of the device being upgraded.
    /// </summary>
    [JsonPropertyName("sn")]
    public string Sn { get; set; } = "";

    /// <summary>
    /// Gets or sets the current upgrade status (e.g., "idle", "upgrading", "done").
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = "";
}
