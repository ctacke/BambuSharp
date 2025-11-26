using Meadow.Units;

namespace BambuSharp;

/// <summary>
/// Represents a filament tray with essential material properties and status information.
/// This is a read-only wrapper around the internal tray entity.
/// </summary>
public class Tray
{
    /// <summary>
    /// Gets the unique identifier for this filament tray.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Gets the filament material type (PLA, PETG, ABS, TPU, etc.).
    /// </summary>
    public string FilamentType { get; }

    /// <summary>
    /// Gets the filament color in RGBA hex format.
    /// </summary>
    public string Color { get; }

    /// <summary>
    /// Gets the remaining filament length in millimeters.
    /// </summary>
    public int RemainingLength { get; }

    /// <summary>
    /// Gets the total filament length in millimeters.
    /// </summary>
    public int TotalLength { get; }

    /// <summary>
    /// Gets the remaining filament as a percentage (0-100).
    /// </summary>
    public int RemainingPercent { get; }

    /// <summary>
    /// Gets the minimum nozzle temperature for this filament material.
    /// </summary>
    public Temperature NozzleTempMin { get; }

    /// <summary>
    /// Gets the maximum nozzle temperature for this filament material.
    /// </summary>
    public Temperature NozzleTempMax { get; }

    /// <summary>
    /// Gets the sub-brand or manufacturer name of the filament.
    /// </summary>
    public string Brand { get; }

    /// <summary>
    /// Initializes a new instance of the Tray class from an internal tray entity.
    /// </summary>
    /// <param name="tray">The internal tray entity to wrap.</param>
    internal Tray(TrayInternal tray)
    {
        Id = tray.Id;
        FilamentType = tray.TrayType;
        Color = tray.TrayColor;
        RemainingLength = tray.Remain;
        TotalLength = tray.TotalLen;
        RemainingPercent = tray.TotalLen > 0 ? (int)((double)tray.Remain / tray.TotalLen * 100) : 0;
        NozzleTempMin = tray.NozzleTempMin;
        NozzleTempMax = tray.NozzleTempMax;
        Brand = tray.TraySubBrands;
    }
}
