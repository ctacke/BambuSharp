namespace BambuSharp;

/// <summary>
/// Represents the cloud connectivity status of the printer.
/// </summary>
public class CloudStatus
{
    /// <summary>
    /// Gets a value indicating whether AHB (Bambu Lab cloud service) connectivity is active.
    /// </summary>
    public bool BambuLabCloudConnected { get; }

    /// <summary>
    /// Gets a value indicating whether external cloud connectivity is active.
    /// </summary>
    public bool ExternalCloudConnected { get; }

    /// <summary>
    /// Gets the version number of the cloud connectivity protocol.
    /// </summary>
    public int Version { get; }

    internal CloudStatus(OnlineInternal online)
    {
        BambuLabCloudConnected = online.Ahb;
        ExternalCloudConnected = online.Ext;
        Version = online.Version;
    }
}
