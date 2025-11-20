using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Contains AI camera feature settings including failure detection and monitoring.
/// </summary>
public class XCam
{
    /// <summary>
    /// Gets or sets a value indicating whether to allow skipping parts during printing.
    /// </summary>
    [JsonPropertyName("allow_skip_parts")]
    public bool AllowSkipParts { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether build plate marker detection is enabled.
    /// </summary>
    [JsonPropertyName("buildplate_marker_detector")]
    public bool BuildplateMarkerDetector { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether first layer adhesion inspection is enabled.
    /// </summary>
    [JsonPropertyName("first_layer_inspector")]
    public bool FirstLayerInspector { get; set; }

    /// <summary>
    /// Gets or sets the sensitivity level for halt detection.
    /// </summary>
    [JsonPropertyName("halt_print_sensitivity")]
    public string HaltPrintSensitivity { get; set; } = "";

    /// <summary>
    /// Gets or sets a value indicating whether automatic print halt on detection is enabled.
    /// </summary>
    [JsonPropertyName("print_halt")]
    public bool PrintHalt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether print monitoring is enabled.
    /// </summary>
    [JsonPropertyName("printing_monitor")]
    public bool PrintingMonitor { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether spaghetti failure detection is enabled.
    /// </summary>
    [JsonPropertyName("spaghetti_detector")]
    public bool SpaghettiDetector { get; set; }
}
