using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Represents the nozzle system status and configuration.
/// </summary>
/// <remarks>
/// This class provides detailed information about installed nozzles, their specifications,
/// wear status, and overall operational state.
/// </remarks>
internal class NozzleDeviceInternal
{
    /// <summary>
    /// Gets or sets a value indicating whether a nozzle exists or is installed.
    /// </summary>
    [JsonPropertyName("exist")]
    public int Exist { get; set; }

    /// <summary>
    /// Gets or sets the detailed information for each installed nozzle.
    /// </summary>
    [JsonPropertyName("info")]
    public List<NozzleInfoInternal> Info { get; set; } = new();

    /// <summary>
    /// Gets or sets the operational state of the nozzle system.
    /// </summary>
    // TODO: reverse engineer possible states and make an enum
    [JsonPropertyName("state")]
    public int State { get; set; }
}
