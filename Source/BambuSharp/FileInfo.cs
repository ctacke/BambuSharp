namespace BambuSharp;

/// <summary>
/// Represents a print file stored on the printer's SD card or internal storage.
/// </summary>
public class FileInfo
{
    /// <summary>
    /// Gets the file name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the file path.
    /// </summary>
    public string Path { get; }

    /// <summary>
    /// Gets the file size in bytes.
    /// </summary>
    public long Size { get; }

    /// <summary>
    /// Gets the timestamp when the file was last modified (Unix timestamp in seconds).
    /// </summary>
    public long Timestamp { get; }

    /// <summary>
    /// Gets the last modified date/time as a DateTime object.
    /// </summary>
    public DateTime LastModified { get; }

    internal FileInfo(PrintFileInternal file)
    {
        Name = file.Name;
        Path = file.Path;
        Size = file.Size;
        Timestamp = file.Timestamp;
        LastModified = file.LastModified;
    }
}
