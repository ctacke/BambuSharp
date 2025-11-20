using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Contains network configuration and connection information.
/// </summary>
public class Network
{
    /// <summary>
    /// Gets or sets the network configuration status or flag.
    /// </summary>
    [JsonPropertyName("conf")]
    public int Conf { get; set; }

    /// <summary>
    /// Gets or sets the list of network interface information.
    /// </summary>
    [JsonPropertyName("info")]
    public List<NetworkInfo> Info { get; set; } = new();
}
