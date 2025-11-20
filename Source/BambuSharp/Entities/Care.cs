using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Represents maintenance and care information.
/// </summary>
public class Care
{
    /// <summary>
    /// Gets or sets the identifier for the maintenance item.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    /// <summary>
    /// Gets or sets the maintenance information or description.
    /// </summary>
    [JsonPropertyName("info")]
    public string Info { get; set; } = "";
}
