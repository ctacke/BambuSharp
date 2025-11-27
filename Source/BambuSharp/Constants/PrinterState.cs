namespace BambuSharp;

/// <summary>
/// Represents the operational state of the printer.
/// </summary>
/// <remarks>
/// This is the numeric 'state' field from the printer status report.
/// Values are based on community reverse-engineering efforts.
/// </remarks>
public enum PrinterState
{
    /// <summary>
    /// Printer is idle.
    /// </summary>
    Idle = 0,

    /// <summary>
    /// Printer is preparing to print.
    /// </summary>
    Prepare = 1,

    /// <summary>
    /// Printer is actively printing.
    /// </summary>
    Running = 2,

    /// <summary>
    /// Print is paused.
    /// </summary>
    Paused = 3,

    /// <summary>
    /// Print job is completing/finishing.
    /// </summary>
    Printing = 4,

    /// <summary>
    /// Print has finished.
    /// </summary>
    Finished = 6,

    /// <summary>
    /// Print has failed or been cancelled.
    /// </summary>
    Failed = 7
}
