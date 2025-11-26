using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Contains print job stage information and progress.
/// </summary>
internal class JobInternal
{
    /// <summary>
    /// Gets or sets the currently active print job stage.
    /// </summary>
    [JsonPropertyName("cur_stage")]
    public CurrentStageInternal CurStage { get; set; } = new();

    /// <summary>
    /// Gets or sets the list of stages in the print job.
    /// </summary>
    [JsonPropertyName("stage")]
    public List<StageInternal> Stage { get; set; } = new();
}
