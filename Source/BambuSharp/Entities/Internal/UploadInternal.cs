using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Contains file upload progress and status information.
/// </summary>
internal class UploadInternal
{
    /// <summary>
    /// Gets or sets the total file size in bytes.
    /// </summary>
    [JsonPropertyName("file_size")]
    public long FileSize { get; set; }

    /// <summary>
    /// Gets or sets the number of bytes successfully uploaded.
    /// </summary>
    [JsonPropertyName("finish_size")]
    public long FinishSize { get; set; }

    /// <summary>
    /// Gets or sets the upload status message.
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; set; } = "";

    /// <summary>
    /// Gets or sets the OSS (Object Storage Service) URL where the file is stored.
    /// </summary>
    [JsonPropertyName("oss_url")]
    public string OssUrl { get; set; } = "";

    /// <summary>
    /// Gets or sets the upload progress as a percentage (0-100).
    /// </summary>
    [JsonPropertyName("progress")]
    public int Progress { get; set; }

    /// <summary>
    /// Gets or sets the upload sequence identifier.
    /// </summary>
    [JsonPropertyName("sequence_id")]
    public string SequenceId { get; set; } = "";

    /// <summary>
    /// Gets or sets the upload speed in bytes per second.
    /// </summary>
    [JsonPropertyName("speed")]
    public long Speed { get; set; }

    /// <summary>
    /// Gets or sets the current upload status (e.g., "uploading", "done", "failed").
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = "";

    /// <summary>
    /// Gets or sets the task identifier for the upload operation.
    /// </summary>
    [JsonPropertyName("task_id")]
    public string TaskId { get; set; } = "";

    /// <summary>
    /// Gets or sets the estimated time remaining for the upload in seconds.
    /// </summary>
    [JsonPropertyName("time_remaining")]
    public int TimeRemaining { get; set; }

    /// <summary>
    /// Gets or sets the trouble or error identifier if the upload fails.
    /// </summary>
    [JsonPropertyName("trouble_id")]
    public string TroubleId { get; set; } = "";
}
