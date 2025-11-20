namespace BambuSharp;

/// <summary>
/// Represents the module or subsystem that generated an HMS error code.
/// </summary>
public enum HmsModule
{
    /// <summary>
    /// Motion Controller (MC) module.
    /// </summary>
    MotionController = 0x03,

    /// <summary>
    /// Mainboard module.
    /// </summary>
    Mainboard = 0x05,

    /// <summary>
    /// Automatic Material System (AMS) module.
    /// </summary>
    Ams = 0x07,

    /// <summary>
    /// Toolhead module (extruder, nozzle, etc.).
    /// </summary>
    Toolhead = 0x08,

    /// <summary>
    /// XCam (AI camera system) module.
    /// </summary>
    XCam = 0x0C
}
