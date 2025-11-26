using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Represents the build plate status and calibration information.
/// </summary>
/// <remarks>
/// This class provides information about the build plate including its type, calibration status,
/// and identifiers for tracking different plates and their calibration profiles.
/// </remarks>
internal class PlateDeviceInternal
{
    /// <summary>
    /// Gets or sets the base plate type or configuration identifier.
    /// </summary>
    [JsonPropertyName("base")]
    public int Base { get; set; }

    /// <summary>
    /// Gets or sets the 2D calibration profile identifier for the current plate.
    /// </summary>
    [JsonPropertyName("cali2d_id")]
    public string Cali2dId { get; set; } = "";

    /// <summary>
    /// Gets or sets the unique identifier for the currently loaded build plate.
    /// </summary>
    [JsonPropertyName("cur_id")]
    public string CurId { get; set; } = "";

    /// <summary>
    /// Gets or sets the material type of the build plate surface.
    /// </summary>
    [JsonPropertyName("mat")]
    public int Material { get; set; }

    /// <summary>
    /// Gets or sets the target build plate identifier for next use or switching.
    /// </summary>
    [JsonPropertyName("tar_id")]
    public string TarId { get; set; } = "";
}
