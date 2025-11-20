using Meadow.Units;
using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Represents external tool attachment information and status.
/// </summary>
/// <remarks>
/// This class provides information about external tools attached to the printer,
/// such as engravers, cameras, or other auxiliary devices, including their type, mounting status, and calibration.
/// </remarks>
public class ExtTool
{
    /// <summary>
    /// Gets or sets the calibration status of the external tool.
    /// </summary>
    [JsonPropertyName("calib")]
    public int Calib { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the external tool is in low precision mode.
    /// </summary>
    [JsonPropertyName("low_prec")]
    public bool LowPrec { get; set; }

    /// <summary>
    /// Gets or sets the mount status of the external tool.
    /// </summary>
    [JsonPropertyName("mount")]
    public int Mount { get; set; }

    /// <summary>
    /// Gets or sets the thermal head temperature
    /// </summary>
    [JsonPropertyName("th_temp")]
    [JsonConverter(typeof(TemperatureJsonConverter))]
    public Temperature ThermalHeadTemperature { get; set; }

    /// <summary>
    /// Gets or sets the type identifier of the external tool (e.g., "laser", "engraver").
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = "";
}
