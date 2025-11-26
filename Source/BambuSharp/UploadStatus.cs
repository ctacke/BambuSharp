namespace BambuSharp;

/// <summary>
/// Represents the status and progress of a file upload operation to the printer.
/// </summary>
public class UploadStatus
{
    /// <summary>
    /// Gets the total file size in bytes.
    /// </summary>
    public long FileSize { get; }

    /// <summary>
    /// Gets the number of bytes successfully uploaded.
    /// </summary>
    public long FinishSize { get; }

    /// <summary>
    /// Gets the upload status message.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Gets the OSS (Object Storage Service) URL where the file is stored.
    /// </summary>
    public string OssUrl { get; }

    /// <summary>
    /// Gets the upload progress as a percentage (0-100).
    /// </summary>
    public int Progress { get; }

    /// <summary>
    /// Gets the upload sequence identifier.
    /// </summary>
    public string SequenceId { get; }

    /// <summary>
    /// Gets the upload speed in bytes per second.
    /// </summary>
    public long Speed { get; }

    /// <summary>
    /// Gets the current upload status (e.g., "uploading", "done", "failed").
    /// </summary>
    public string Status { get; }

    /// <summary>
    /// Gets the task identifier for the upload operation.
    /// </summary>
    public string TaskId { get; }

    /// <summary>
    /// Gets the estimated time remaining for the upload in seconds.
    /// </summary>
    public int TimeRemaining { get; }

    /// <summary>
    /// Gets the trouble or error identifier if the upload fails.
    /// </summary>
    public string TroubleId { get; }

    internal UploadStatus(UploadInternal upload)
    {
        FileSize = upload.FileSize;
        FinishSize = upload.FinishSize;
        Message = upload.Message;
        OssUrl = upload.OssUrl;
        Progress = upload.Progress;
        SequenceId = upload.SequenceId;
        Speed = upload.Speed;
        Status = upload.Status;
        TaskId = upload.TaskId;
        TimeRemaining = upload.TimeRemaining;
        TroubleId = upload.TroubleId;
    }
}
