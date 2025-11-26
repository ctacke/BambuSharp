using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Contains cloud connectivity status information.
/// </summary>
internal class OnlineInternal
{
    /// <summary>
    /// Gets or sets a value indicating whether AHB (Bambu Lab cloud service) connectivity is active.
    /// </summary>
    [JsonPropertyName("ahb")]
    public bool Ahb { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether external cloud connectivity is active.
    /// </summary>
    [JsonPropertyName("ext")]
    public bool Ext { get; set; }

    /// <summary>
    /// Gets or sets the version number of the cloud connectivity protocol.
    /// </summary>
    [JsonPropertyName("version")]
    public int Version { get; set; }
}
