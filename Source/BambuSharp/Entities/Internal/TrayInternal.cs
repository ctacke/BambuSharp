using Meadow.Units;
using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Internal entity for JSON deserialization. Represents a filament tray/spool with material properties and status information.
/// </summary>
internal class TrayInternal
{
    /// <summary>
    /// Gets or sets the heated bed temperature in degrees Celsius for this filament material.
    /// </summary>
    [JsonPropertyName("bed_temp")]
    public string BedTemp { get; set; } = "";

    /// <summary>
    /// Gets or sets the heated bed temperature type or profile identifier.
    /// </summary>
    [JsonPropertyName("bed_temp_type")]
    public string BedTempType { get; set; } = "";

    /// <summary>
    /// Gets or sets the calibration index for this tray.
    /// </summary>
    [JsonPropertyName("cali_idx")]
    public int CaliIdx { get; set; }

    /// <summary>
    /// Gets or sets the list of filament colors available in this tray (in RGBA hex format).
    /// </summary>
    [JsonPropertyName("cols")]
    public List<string> Cols { get; set; } = new();

    /// <summary>
    /// Gets or sets the color type code for the filament.
    /// </summary>
    [JsonPropertyName("ctype")]
    public int Ctype { get; set; }

    /// <summary>
    /// Gets or sets the filament drying temperature
    /// </summary>
    [JsonPropertyName("drying_temp")]
    [JsonConverter(typeof(TemperatureJsonConverter))]
    public Temperature DryingTemp { get; set; }

    /// <summary>
    /// Gets or sets the filament drying time duration.
    /// </summary>
    [JsonPropertyName("drying_time")]
    public string DryingTime { get; set; } = "";

    /// <summary>
    /// Gets or sets the unique identifier for this filament tray.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    /// <summary>
    /// Gets or sets the maximum nozzle temperature for this filament material.
    /// </summary>
    [JsonPropertyName("nozzle_temp_max")]
    [JsonConverter(typeof(TemperatureJsonConverter))]
    public Temperature NozzleTempMax { get; set; }

    /// <summary>
    /// Gets or sets the minimum nozzle temperature for this filament material.
    /// </summary>
    [JsonPropertyName("nozzle_temp_min")]
    [JsonConverter(typeof(TemperatureJsonConverter))]
    public Temperature NozzleTempMin { get; set; }

    /// <summary>
    /// Gets or sets the remaining filament length in the tray.
    /// </summary>
    [JsonPropertyName("remain")]
    public int Remain { get; set; }

    /// <summary>
    /// Gets or sets the tray state code indicating its current status (empty, loaded, printing, etc.).
    /// </summary>
    [JsonPropertyName("state")]
    // TODO: reverse engineer possible states and make an enum
    public int State { get; set; }

    /// <summary>
    /// Gets or sets the unique tag UID for RFID or NFC identification of this tray.
    /// </summary>
    [JsonPropertyName("tag_uid")]
    public string TagUid { get; set; } = "";

    /// <summary>
    /// Gets or sets the total filament length in the tray.
    /// </summary>
    [JsonPropertyName("total_len")]
    public int TotalLen { get; set; }

    /// <summary>
    /// Gets or sets the filament color in RGBA hex format.
    /// </summary>
    [JsonPropertyName("tray_color")]
    public string TrayColor { get; set; } = "";

    /// <summary>
    /// Gets or sets the filament diameter in millimeters.
    /// </summary>
    [JsonPropertyName("tray_diameter")]
    public string TrayDiameter { get; set; } = "";

    /// <summary>
    /// Gets or sets the filament material name or identifier.
    /// </summary>
    [JsonPropertyName("tray_id_name")]
    public string TrayIdName { get; set; } = "";

    /// <summary>
    /// Gets or sets the tray information index or lookup identifier.
    /// </summary>
    [JsonPropertyName("tray_info_idx")]
    public string TrayInfoIdx { get; set; } = "";

    /// <summary>
    /// Gets or sets the sub-brand or manufacturer name of the filament.
    /// </summary>
    [JsonPropertyName("tray_sub_brands")]
    public string TraySubBrands { get; set; } = "";

    /// <summary>
    /// Gets or sets the filament material type (PLA, PETG, ABS, TPU, etc.).
    /// </summary>
    [JsonPropertyName("tray_type")]
    public string TrayType { get; set; } = "";

    /// <summary>
    /// Gets or sets the universally unique identifier for this tray.
    /// </summary>
    [JsonPropertyName("tray_uuid")]
    public string TrayUuid { get; set; } = "";

    /// <summary>
    /// Gets or sets the weight of the filament in the tray.
    /// </summary>
    [JsonPropertyName("tray_weight")]
    public string TrayWeight { get; set; } = "";

    /// <summary>
    /// Gets or sets additional camera or sensor information about this tray.
    /// </summary>
    [JsonPropertyName("xcam_info")]
    public string XcamInfo { get; set; } = "";
}
