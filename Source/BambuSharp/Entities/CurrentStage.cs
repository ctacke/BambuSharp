using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Represents the currently active print job stage.
/// </summary>
public class CurrentStage
{
    /// <summary>
    /// Gets or sets the index of the current stage.
    /// </summary>
    [JsonPropertyName("idx")]
    public int Idx { get; set; }

    /// <summary>
    /// Gets or sets the state of the current stage.
    /// </summary>
    [JsonPropertyName("state")]
    public int State { get; set; }
}
