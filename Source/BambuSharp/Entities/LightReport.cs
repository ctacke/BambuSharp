using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Represents the status of a printer light (chamber light, work light, etc.).
/// </summary>
public class LightReport
{
    /// <summary>
    /// Gets or sets the light mode (e.g., "on", "off", "flashing").
    /// </summary>
    [JsonPropertyName("mode")]
    public string Mode { get; set; } = "";

    /// <summary>
    /// Gets or sets the identifier of the light node (e.g., "chamber_light", "work_light").
    /// </summary>
    [JsonPropertyName("node")]
    public string Node { get; set; } = "";
}
