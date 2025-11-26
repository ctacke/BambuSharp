using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Root object containing the complete printer status report from a Bambu Lab printer.
/// </summary>
internal class Report
{
    /// <summary>
    /// Gets or sets the main print data container with all printer status information.
    /// </summary>
    /// <remarks>
    /// IMPORTANT: This property MUST be public for JSON deserialization to work.
    /// System.Text.Json only deserializes public properties by default.
    /// </remarks>
    [JsonPropertyName("print")]
    public PrintInternal Print { get; set; } = new();
}
