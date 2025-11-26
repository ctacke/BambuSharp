namespace BambuSharp;

/// <summary>
/// Represents the printer's AI camera features and monitoring settings.
/// </summary>
public class AI
{
    /// <summary>
    /// Gets a value indicating whether to allow skipping parts during printing.
    /// </summary>
    public bool AllowSkipParts { get; }

    /// <summary>
    /// Gets a value indicating whether build plate marker detection is enabled.
    /// </summary>
    public bool BuildPlateMarkerDetector { get; }

    /// <summary>
    /// Gets a value indicating whether first layer adhesion inspection is enabled.
    /// </summary>
    public bool FirstLayerInspector { get; }

    /// <summary>
    /// Gets the sensitivity level for halt detection (e.g., "low", "medium", "high").
    /// </summary>
    public string HaltPrintSensitivity { get; }

    /// <summary>
    /// Gets a value indicating whether automatic print halt on detection is enabled.
    /// </summary>
    public bool PrintHalt { get; }

    /// <summary>
    /// Gets a value indicating whether print monitoring is enabled.
    /// </summary>
    public bool PrintingMonitor { get; }

    /// <summary>
    /// Gets a value indicating whether spaghetti failure detection is enabled.
    /// </summary>
    public bool SpaghettiDetector { get; }

    /// <summary>
    /// Initializes a new instance from internal XCam settings.
    /// </summary>
    internal AI(XCamInternal xCam)
    {
        AllowSkipParts = xCam.AllowSkipParts;
        BuildPlateMarkerDetector = xCam.BuildplateMarkerDetector;
        FirstLayerInspector = xCam.FirstLayerInspector;
        HaltPrintSensitivity = xCam.HaltPrintSensitivity;
        PrintHalt = xCam.PrintHalt;
        PrintingMonitor = xCam.PrintingMonitor;
        SpaghettiDetector = xCam.SpaghettiDetector;
    }
}
