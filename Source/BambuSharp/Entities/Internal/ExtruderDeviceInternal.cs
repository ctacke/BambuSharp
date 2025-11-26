using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Represents the extruder system status and information.
/// </summary>
/// <remarks>
/// This class provides comprehensive status information for each extruder unit including
/// temperature, filament state, and operational metrics.
/// </remarks>
public class ExtruderDeviceInternal
{
    /// <summary>
    /// Gets or sets the detailed status information for each extruder.
    /// </summary>
    [JsonPropertyName("info")]
    public List<ExtruderInfoInternal> Info { get; set; } = new();

    /// <summary>
    /// Gets or sets the overall operational state of the extruder system.
    /// </summary>
    [JsonPropertyName("state")]
    public int State { get; set; }
}
