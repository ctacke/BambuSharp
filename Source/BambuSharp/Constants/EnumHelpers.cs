namespace BambuSharp;

/// <summary>
/// Helper class for converting between enum values and their string/numeric representations.
/// </summary>
public static class EnumHelpers
{
    /// <summary>
    /// Parses a G-code state string into a <see cref="GCodeState"/> enum value.
    /// </summary>
    /// <param name="state">The state string (e.g., "IDLE", "RUNNING").</param>
    /// <returns>The corresponding <see cref="GCodeState"/> enum value.</returns>
    public static GCodeState ParseGCodeState(string state)
    {
        return state?.ToUpperInvariant() switch
        {
            "IDLE" => GCodeState.Idle,
            "INIT" => GCodeState.Init,
            "PREPARE" => GCodeState.Prepare,
            "RUNNING" => GCodeState.Running,
            "PAUSE" => GCodeState.Pause,
            "FINISH" => GCodeState.Finish,
            "FAILED" => GCodeState.Failed,
            "SLICING" => GCodeState.Slicing,
            "OFFLINE" => GCodeState.Offline,
            _ => GCodeState.Unknown
        };
    }

    /// <summary>
    /// Gets the HMS severity level from a severity code.
    /// </summary>
    /// <param name="severity">The severity code (1-4).</param>
    /// <returns>The corresponding <see cref="HmsSeverity"/> enum value, or null if invalid.</returns>
    public static HmsSeverity? GetHmsSeverity(int severity)
    {
        return severity switch
        {
            1 => HmsSeverity.Fatal,
            2 => HmsSeverity.Serious,
            3 => HmsSeverity.Common,
            4 => HmsSeverity.Info,
            _ => null
        };
    }

    /// <summary>
    /// Gets the HMS module from a module code.
    /// </summary>
    /// <param name="moduleCode">The module code (e.g., 0x05, 0x0C).</param>
    /// <returns>The corresponding <see cref="HmsModule"/> enum value, or null if not recognized.</returns>
    public static HmsModule? GetHmsModule(int moduleCode)
    {
        return moduleCode switch
        {
            0x03 => HmsModule.MotionController,
            0x05 => HmsModule.Mainboard,
            0x07 => HmsModule.Ams,
            0x08 => HmsModule.Toolhead,
            0x0C => HmsModule.XCam,
            _ => null
        };
    }

    /// <summary>
    /// Gets the speed profile from a speed level code.
    /// </summary>
    /// <param name="speedLevel">The speed level (1-4).</param>
    /// <returns>The corresponding <see cref="SpeedProfile"/> enum value, or null if invalid.</returns>
    public static SpeedProfile? GetSpeedProfile(int speedLevel)
    {
        return speedLevel switch
        {
            1 => SpeedProfile.Silent,
            2 => SpeedProfile.Standard,
            3 => SpeedProfile.Sport,
            4 => SpeedProfile.Ludicrous,
            _ => null
        };
    }

    /// <summary>
    /// Gets the printer state from a state code.
    /// </summary>
    /// <param name="stateCode">The printer state code.</param>
    /// <returns>The corresponding <see cref="PrinterState"/> enum value, or null if not recognized.</returns>
    public static PrinterState? GetPrinterState(int stateCode)
    {
        return stateCode switch
        {
            0 => PrinterState.Idle,
            1 => PrinterState.Prepare,
            2 => PrinterState.Running,
            3 => PrinterState.Paused,
            4 => PrinterState.Finishing,
            6 => PrinterState.Finished,
            7 => PrinterState.Failed,
            _ => null
        };
    }
}
