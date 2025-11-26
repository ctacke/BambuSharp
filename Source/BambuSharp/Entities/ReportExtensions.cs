namespace BambuSharp;

/// <summary>
/// Extension methods for the <see cref="Report"/> class to provide strongly-typed enum access.
/// </summary>
public static class ReportExtensions
{
    /// <summary>
    /// Gets the current G-code state as a strongly-typed enum.
    /// </summary>
    /// <param name="print">The print status object.</param>
    /// <returns>The <see cref="GCodeState"/> enum value.</returns>
    internal static GCodeState GetGCodeState(this PrintInternal print)
    {
        return EnumHelpers.ParseGCodeState(print.GcodeState);
    }

    /// <summary>
    /// Gets the current printer state as a strongly-typed enum.
    /// </summary>
    /// <param name="print">The print status object.</param>
    /// <returns>The <see cref="PrinterState"/> enum value, or null if the state is not recognized.</returns>
    internal static PrinterState? GetPrinterState(this PrintInternal print)
    {
        return EnumHelpers.GetPrinterState(print.State);
    }

    /// <summary>
    /// Gets the current speed profile as a strongly-typed enum.
    /// </summary>
    /// <param name="print">The print status object.</param>
    /// <returns>The <see cref="SpeedProfile"/> enum value, or null if the speed level is not recognized.</returns>
    internal static SpeedProfile? GetSpeedProfile(this PrintInternal print)
    {
        return EnumHelpers.GetSpeedProfile(print.SpdLvl);
    }

    /// <summary>
    /// Gets a value indicating whether the printer is currently printing.
    /// </summary>
    /// <param name="print">The print status object.</param>
    /// <returns>True if the printer is actively printing; otherwise, false.</returns>
    internal static bool IsPrinting(this PrintInternal print)
    {
        var state = print.GetGCodeState();
        return state == GCodeState.Running || state == GCodeState.Prepare;
    }

    /// <summary>
    /// Gets a value indicating whether the printer is idle.
    /// </summary>
    /// <param name="print">The print status object.</param>
    /// <returns>True if the printer is idle; otherwise, false.</returns>
    internal static bool IsIdle(this PrintInternal print)
    {
        var state = print.GetGCodeState();
        return state == GCodeState.Idle;
    }

    /// <summary>
    /// Gets a value indicating whether a print job has completed (successfully or failed).
    /// </summary>
    /// <param name="print">The print status object.</param>
    /// <returns>True if the print has finished or failed; otherwise, false.</returns>
    internal static bool IsComplete(this PrintInternal print)
    {
        var state = print.GetGCodeState();
        return state == GCodeState.Finish || state == GCodeState.Failed;
    }

    /// <summary>
    /// Gets the HMS error severity level as a strongly-typed enum.
    /// </summary>
    /// <param name="hms">The HMS error object.</param>
    /// <returns>The <see cref="HmsSeverity"/> enum value, or null if not recognized.</returns>
    public static HmsSeverity? GetSeverity(this Hms hms)
    {
        // HMS severity is encoded in the code
        // The exact bit pattern for extracting severity may need adjustment based on actual data
        int severityCode = (hms.Code >> 16) & 0x0F;
        return EnumHelpers.GetHmsSeverity(severityCode);
    }

    /// <summary>
    /// Gets the HMS error module as a strongly-typed enum.
    /// </summary>
    /// <param name="hms">The HMS error object.</param>
    /// <returns>The <see cref="HmsModule"/> enum value, or null if not recognized.</returns>
    public static HmsModule? GetModule(this Hms hms)
    {
        // HMS module is encoded in the code
        // The exact bit pattern for extracting module may need adjustment based on actual data
        int moduleCode = (hms.Code >> 8) & 0xFF;
        return EnumHelpers.GetHmsModule(moduleCode);
    }

    /// <summary>
    /// Gets the active network interfaces (those with configured IP addresses).
    /// </summary>
    /// <param name="network">The network configuration object.</param>
    /// <returns>A collection of network interfaces that have IP addresses configured.</returns>
    public static IEnumerable<NetworkInfo> GetActiveInterfaces(this Network network)
    {
        return network.Info.Where(i => i.IpAddress != null);
    }

    /// <summary>
    /// Gets a value indicating whether the printer has any active network connection.
    /// </summary>
    /// <param name="network">The network configuration object.</param>
    /// <returns>True if at least one network interface has an IP address; otherwise, false.</returns>
    public static bool IsConnected(this Network network)
    {
        return network.Info.Any(i => i.IpAddress != null);
    }

    /// <summary>
    /// Gets a value indicating whether this network interface is configured.
    /// </summary>
    /// <param name="networkInfo">The network interface information.</param>
    /// <returns>True if the interface has an IP address; otherwise, false.</returns>
    public static bool IsConfigured(this NetworkInfo networkInfo)
    {
        return networkInfo.IpAddress != null;
    }
}
