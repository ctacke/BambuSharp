using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Represents a print file stored on the printer's SD card or internal storage.
/// </summary>
internal class PrintFileInternal
{
    /// <summary>
    /// Gets or sets the file name.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    /// <summary>
    /// Gets or sets the file path.
    /// </summary>
    [JsonPropertyName("path")]
    public string Path { get; set; } = "";

    /// <summary>
    /// Gets or sets the file size in bytes.
    /// </summary>
    [JsonPropertyName("size")]
    public long Size { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the file was last modified.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Gets the last modified date/time as a DateTime object.
    /// </summary>
    [JsonIgnore]
    public DateTime LastModified => DateTimeOffset.FromUnixTimeSeconds(Timestamp).DateTime;
}

/// <summary>
/// Represents the response from a file list request.
/// </summary>
internal class PrintFileListResponse
{
    /// <summary>
    /// Gets or sets the list of print files.
    /// </summary>
    [JsonPropertyName("files")]
    public List<PrintFileInternal> Files { get; set; } = new();
}
