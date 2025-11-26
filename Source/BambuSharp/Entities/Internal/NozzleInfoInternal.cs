using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Contains nozzle specifications including diameter, type, and wear information.
/// </summary>
/// <remarks>
/// This class provides detailed specifications and status information for a specific nozzle,
/// including its size, material compatibility, remaining lifespan, and operational parameters.
/// </remarks>
internal class NozzleInfoInternal
{
    /// <summary>
    /// Gets or sets the nozzle diameter in millimeters.
    /// </summary>
    [JsonPropertyName("diameter")]
    public double Diameter { get; set; }

    /// <summary>
    /// Gets or sets the nozzle identifier.
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the nozzle temperature coefficient or thermal management parameter.
    /// </summary>
    [JsonPropertyName("tm")]
    public int Tm { get; set; }

    /// <summary>
    /// Gets or sets the material type supported by this nozzle (e.g., "brass", "hardened").
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = "";

    /// <summary>
    /// Gets or sets the nozzle wear level as a percentage (0-100), indicating remaining lifespan.
    /// </summary>
    [JsonPropertyName("wear")]
    public int Wear { get; set; }
}
