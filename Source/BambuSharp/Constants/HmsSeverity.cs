namespace BambuSharp;

/// <summary>
/// Represents the severity level of a Health Management System (HMS) error or warning.
/// </summary>
public enum HmsSeverity
{
    /// <summary>
    /// Fatal error - requires immediate attention, printing cannot continue.
    /// </summary>
    Fatal = 1,

    /// <summary>
    /// Serious error - significant issue that affects print quality or safety.
    /// </summary>
    Serious = 2,

    /// <summary>
    /// Common warning - minor issue that may affect print quality.
    /// </summary>
    Common = 3,

    /// <summary>
    /// Informational message - does not indicate a problem.
    /// </summary>
    Info = 4
}
