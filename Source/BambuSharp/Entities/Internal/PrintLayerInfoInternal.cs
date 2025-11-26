using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Contains 3D printing layer progress information.
/// </summary>
internal class PrintLayerInfoInternal
{
    /// <summary>
    /// Gets or sets the current layer number being printed.
    /// </summary>
    [JsonPropertyName("layer_num")]
    public int LayerNum { get; set; }

    /// <summary>
    /// Gets or sets the total number of layers in the print job.
    /// </summary>
    [JsonPropertyName("total_layer_num")]
    public int TotalLayerNum { get; set; }
}
