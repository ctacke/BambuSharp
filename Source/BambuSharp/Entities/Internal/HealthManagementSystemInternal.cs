using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Represents a Health Management System (HMS) error or warning code.
/// </summary>
internal class HealthManagementSystemInternal
{
    /// <summary>
    /// Gets or sets the attribute flags associated with the HMS code.
    /// </summary>
    [JsonPropertyName("attr")]
    public long Attr { get; set; }

    /// <summary>
    /// Gets or sets the HMS error or warning code.
    /// </summary>
    [JsonPropertyName("code")]
    public int Code { get; set; }
}
