using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Internal entity for JSON deserialization. Contains configuration and status information for the Automatic Material System (AMS) that manages multiple filament spools.
/// </summary>
public class AmsSystemInternal
{
    /// <summary>
    /// Gets or sets the list of individual AMS units in the system.
    /// </summary>
    [JsonPropertyName("ams")]
    public List<AmsInternal> AmsList { get; set; } = new();

    /// <summary>
    /// Gets or sets the AMS existence bits indicating which AMS slots contain active units.
    /// </summary>
    [JsonPropertyName("ams_exist_bits")]
    public string AmsExistBits { get; set; } = "";

    /// <summary>
    /// Gets or sets the raw AMS existence bits for debugging and extended status information.
    /// </summary>
    [JsonPropertyName("ams_exist_bits_raw")]
    public string AmsExistBitsRaw { get; set; } = "";

    /// <summary>
    /// Gets or sets the calibration ID for the AMS system.
    /// </summary>
    [JsonPropertyName("cali_id")]
    public int CaliId { get; set; }

    /// <summary>
    /// Gets or sets the calibration status code for the AMS system.
    /// </summary>
    [JsonPropertyName("cali_stat")]
    public int CaliStat { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the insert flag is set.
    /// </summary>
    [JsonPropertyName("insert_flag")]
    public bool InsertFlag { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the AMS system is powered on.
    /// </summary>
    [JsonPropertyName("power_on_flag")]
    public bool PowerOnFlag { get; set; }

    /// <summary>
    /// Gets or sets the tray existence bits indicating which filament trays are present.
    /// </summary>
    [JsonPropertyName("tray_exist_bits")]
    public string TrayExistBits { get; set; } = "";

    /// <summary>
    /// Gets or sets the bits indicating which trays are Bambu Lab branded filament.
    /// </summary>
    [JsonPropertyName("tray_is_bbl_bits")]
    public string TrayIsBblBits { get; set; } = "";

    /// <summary>
    /// Gets or sets the currently active filament tray identifier.
    /// </summary>
    [JsonPropertyName("tray_now")]
    public string TrayNow { get; set; } = "";

    /// <summary>
    /// Gets or sets the previously active filament tray identifier.
    /// </summary>
    [JsonPropertyName("tray_pre")]
    public string TrayPre { get; set; } = "";

    /// <summary>
    /// Gets or sets the tray read done bits indicating which tray reads have been completed.
    /// </summary>
    [JsonPropertyName("tray_read_done_bits")]
    public string TrayReadDoneBits { get; set; } = "";

    /// <summary>
    /// Gets or sets the tray reading bits indicating which trays are currently being read.
    /// </summary>
    [JsonPropertyName("tray_reading_bits")]
    public string TrayReadingBits { get; set; } = "";

    /// <summary>
    /// Gets or sets the target filament tray identifier.
    /// </summary>
    [JsonPropertyName("tray_tar")]
    public string TrayTar { get; set; } = "";

    /// <summary>
    /// Gets or sets the unbind AMS status code.
    /// </summary>
    [JsonPropertyName("unbind_ams_stat")]
    public int UnbindAmsStat { get; set; }

    /// <summary>
    /// Gets or sets the AMS system version number.
    /// </summary>
    [JsonPropertyName("version")]
    public int Version { get; set; }
}
