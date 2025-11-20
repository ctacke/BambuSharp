namespace BambuSharp;

/// <summary>
/// Represents the SD card status.
/// </summary>
public enum SdCardState
{
    /// <summary>
    /// No SD card is inserted.
    /// </summary>
    NoSdCard,

    /// <summary>
    /// SD card is present and functioning normally.
    /// </summary>
    Normal,

    /// <summary>
    /// SD card is present but has errors or is abnormal.
    /// </summary>
    Abnormal
}
