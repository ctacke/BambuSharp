using System.Net;
using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Represents network interface information including IP address and subnet mask.
/// </summary>
[JsonConverter(typeof(NetworkInfoJsonConverter))]
public class NetworkInfo
{
    /// <summary>
    /// Gets or sets the IP address.
    /// </summary>
    public IPAddress? IpAddress { get; set; }

    /// <summary>
    /// Gets or sets the subnet mask.
    /// </summary>
    public IPAddress? SubnetMask { get; set; }

    /// <summary>
    /// Gets a string representation of the network interface information.
    /// </summary>
    /// <returns>A string containing the IP address and subnet mask.</returns>
    public override string ToString()
    {
        return IpAddress != null
            ? $"{IpAddress}/{SubnetMask}"
            : "No IP Address";
    }
}
