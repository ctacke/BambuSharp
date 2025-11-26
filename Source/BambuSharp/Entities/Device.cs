using Meadow.Units;
using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Contains the current state of all printer hardware components.
/// </summary>
/// <remarks>
/// This class aggregates the status and information for all hardware devices in the printer,
/// including the heated bed, extruder, nozzle, plate calibration, camera, laser, and temperature control systems.
/// </remarks>
public class Device
{
    /// <summary>
    /// Gets or sets the heated print bed status and temperature.
    /// </summary>
    [JsonPropertyName("bed")]
    public BedDevice Bed { get; set; } = new();

    /// <summary>
    /// Gets or sets the current bed temperature in degrees Celsius.
    /// </summary>
    [JsonPropertyName("bed_temp")]
    [JsonConverter(typeof(TemperatureJsonConverter))]
    public Temperature BedTemperature { get; set; }

    /// <summary>
    /// Gets or sets the camera system status.
    /// </summary>
    [JsonPropertyName("cam")]
    public CamDevice Cam { get; set; } = new();

    /// <summary>
    /// Gets or sets the CTC (Core Temperature Control) device status.
    /// </summary>
    [JsonPropertyName("ctc")]
    public CtcDevice Ctc { get; set; } = new();

    /// <summary>
    /// Gets or sets the external tool attachment information and status.
    /// </summary>
    [JsonPropertyName("ext_tool")]
    public ExtTool ExtTool { get; set; } = new();

    /// <summary>
    /// Gets or sets the extruder system status and information.
    /// </summary>
    [JsonPropertyName("extruder")]
    public ExtruderDeviceInternal Extruder { get; set; } = new();

    /// <summary>
    /// Gets or sets the cooling fan power level.
    /// </summary>
    [JsonPropertyName("fan")]
    public int Fan { get; set; }

    /// <summary>
    /// Gets or sets the laser device power and status.
    /// </summary>
    [JsonPropertyName("laser")]
    public LaserDevice Laser { get; set; } = new();

    /// <summary>
    /// Gets or sets the nozzle system status and configuration.
    /// </summary>
    [JsonPropertyName("nozzle")]
    public NozzleDevice Nozzle { get; set; } = new();

    /// <summary>
    /// Gets or sets the build plate status and calibration information.
    /// </summary>
    [JsonPropertyName("plate")]
    public PlateDevice Plate { get; set; } = new();

    /// <summary>
    /// Gets or sets the device type identifier.
    /// </summary>
    [JsonPropertyName("type")]
    public int Type { get; set; }
}
