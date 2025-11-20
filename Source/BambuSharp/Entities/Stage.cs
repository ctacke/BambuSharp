using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Represents a single stage in a multi-stage print job with tool and material information.
/// </summary>
public class Stage
{
    /// <summary>
    /// Gets or sets a value indicating whether clock-in is enabled for this stage.
    /// </summary>
    [JsonPropertyName("clock_in")]
    public bool ClockIn { get; set; }

    /// <summary>
    /// Gets or sets the list of material colors used in this stage.
    /// </summary>
    [JsonPropertyName("color")]
    public List<string> Color { get; set; } = new();

    /// <summary>
    /// Gets or sets the list of material diameters used in this stage.
    /// </summary>
    [JsonPropertyName("diameter")]
    public List<double> Diameter { get; set; } = new();

    /// <summary>
    /// Gets or sets the estimated time in seconds for this stage.
    /// </summary>
    [JsonPropertyName("est_time")]
    public int EstTime { get; set; }

    /// <summary>
    /// Gets or sets the height of this stage in millimeters.
    /// </summary>
    [JsonPropertyName("heigh")]
    public double Heigh { get; set; }

    /// <summary>
    /// Gets or sets the index of this stage.
    /// </summary>
    [JsonPropertyName("idx")]
    public int Idx { get; set; }

    /// <summary>
    /// Gets or sets the platform type for this stage.
    /// </summary>
    [JsonPropertyName("platform")]
    public string Platform { get; set; } = "";

    /// <summary>
    /// Gets or sets a value indicating whether to print after this stage.
    /// </summary>
    [JsonPropertyName("print_then")]
    public bool PrintThen { get; set; }

    /// <summary>
    /// Gets or sets the list of processing instructions for this stage.
    /// </summary>
    [JsonPropertyName("proc_list")]
    public List<object> ProcList { get; set; } = new();

    /// <summary>
    /// Gets or sets the list of tools used in this stage.
    /// </summary>
    [JsonPropertyName("tool")]
    public List<string> Tool { get; set; } = new();

    /// <summary>
    /// Gets or sets the type of this stage.
    /// </summary>
    [JsonPropertyName("type")]
    public int Type { get; set; }
}
