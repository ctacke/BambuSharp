namespace BambuSharp;

/// <summary>
/// Represents the current G-code execution state of the printer.
/// </summary>
/// <remarks>
/// These values are reverse-engineered from the Bambu Lab MQTT protocol and community integrations.
/// There is no official Bambu Lab documentation for these state codes.
/// </remarks>
public enum GCodeState
{
    /// <summary>
    /// Unknown or unrecognized state.
    /// </summary>
    Unknown,

    /// <summary>
    /// Printer is idle and not performing any operation.
    /// </summary>
    Idle,

    /// <summary>
    /// Printer is initializing or starting up.
    /// </summary>
    Init,

    /// <summary>
    /// Printer is preparing to print (heating, homing, loading filament, etc.).
    /// </summary>
    Prepare,

    /// <summary>
    /// Printer is actively printing.
    /// </summary>
    Running,

    /// <summary>
    /// Print is paused.
    /// </summary>
    Pause,

    /// <summary>
    /// Print has finished successfully.
    /// </summary>
    Finish,

    /// <summary>
    /// Print has failed or encountered an error.
    /// </summary>
    Failed,

    /// <summary>
    /// Printer is slicing G-code.
    /// </summary>
    Slicing,

    /// <summary>
    /// Printer is offline or not connected.
    /// </summary>
    Offline
}
