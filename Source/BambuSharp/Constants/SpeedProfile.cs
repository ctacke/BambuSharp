namespace BambuSharp;

/// <summary>
/// Represents the speed profile setting for print operations.
/// </summary>
public enum SpeedProfile
{
    /// <summary>
    /// Silent mode - quietest operation with slower print speeds.
    /// </summary>
    Silent = 1,

    /// <summary>
    /// Standard mode - balanced speed and noise levels.
    /// </summary>
    Standard = 2,

    /// <summary>
    /// Sport mode - faster printing with increased noise.
    /// </summary>
    Sport = 3,

    /// <summary>
    /// Ludicrous mode - maximum print speed.
    /// </summary>
    Ludicrous = 4
}
