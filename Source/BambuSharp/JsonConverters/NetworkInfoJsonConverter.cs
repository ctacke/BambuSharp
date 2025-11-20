using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Custom JSON converter for <see cref="NetworkInfo"/> that converts numeric IP addresses to <see cref="IPAddress"/> objects.
/// </summary>
internal class NetworkInfoJsonConverter : JsonConverter<NetworkInfo>
{
    /// <summary>
    /// Reads and converts the JSON to a <see cref="NetworkInfo"/> object.
    /// </summary>
    public override NetworkInfo Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException("Expected StartObject token");
        }

        long ipValue = 0;
        long maskValue = 0;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return new NetworkInfo
                {
                    IpAddress = ConvertToIpAddress(ipValue),
                    SubnetMask = ConvertToIpAddress(maskValue)
                };
            }

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                string propertyName = reader.GetString() ?? "";
                reader.Read();

                switch (propertyName)
                {
                    case "ip":
                        ipValue = reader.GetInt64();
                        break;
                    case "mask":
                        maskValue = reader.GetInt64();
                        break;
                }
            }
        }

        throw new JsonException("Expected EndObject token");
    }

    /// <summary>
    /// Writes the <see cref="NetworkInfo"/> object to JSON.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, NetworkInfo value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("ip", ConvertToLong(value.IpAddress));
        writer.WriteNumber("mask", ConvertToLong(value.SubnetMask));
        writer.WriteEndObject();
    }

    /// <summary>
    /// Converts a numeric IP address representation to an <see cref="IPAddress"/> object.
    /// </summary>
    /// <param name="value">The numeric representation of the IP address.</param>
    /// <returns>An <see cref="IPAddress"/> object, or null if the value is 0.</returns>
    private static IPAddress? ConvertToIpAddress(long value)
    {
        if (value == 0)
            return null;

        // Convert long to bytes in little-endian order
        byte[] bytes = new byte[4];
        bytes[0] = (byte)(value & 0xFF);
        bytes[1] = (byte)((value >> 8) & 0xFF);
        bytes[2] = (byte)((value >> 16) & 0xFF);
        bytes[3] = (byte)((value >> 24) & 0xFF);

        return new IPAddress(bytes);
    }

    /// <summary>
    /// Converts an <see cref="IPAddress"/> object to its numeric representation.
    /// </summary>
    /// <param name="address">The IP address to convert.</param>
    /// <returns>The numeric representation of the IP address.</returns>
    private static long ConvertToLong(IPAddress? address)
    {
        if (address == null)
            return 0;

        byte[] bytes = address.GetAddressBytes();
        if (bytes.Length != 4)
            return 0;

        // Convert bytes to long in little-endian order
        return bytes[0] | ((long)bytes[1] << 8) | ((long)bytes[2] << 16) | ((long)bytes[3] << 24);
    }
}
