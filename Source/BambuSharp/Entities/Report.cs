using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Root object containing the complete printer status report from a Bambu Lab printer.
/// </summary>
public class Report
{
    /// <summary>
    /// Gets or sets the main print data container with all printer status information.
    /// </summary>
    [JsonPropertyName("print")]
    public Print Print { get; set; } = new();
}
