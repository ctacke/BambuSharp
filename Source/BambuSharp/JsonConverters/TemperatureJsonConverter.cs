using Meadow.Units;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Custom JSON converter for <see cref="Temperature"/> that converts numeric values to Temperature in Celsius.
/// </summary>
public class TemperatureJsonConverter : JsonConverter<Temperature>
{
    /// <summary>
    /// Reads and converts the JSON numeric value to a <see cref="Temperature"/> object.
    /// </summary>
    public override Temperature Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.String => new Temperature(double.Parse(reader.GetString() ?? "0"), Temperature.UnitType.Celsius),
            JsonTokenType.Number => new Temperature(reader.GetDouble(), Temperature.UnitType.Celsius),
            JsonTokenType.Null => new Temperature(0, Temperature.UnitType.Celsius),
            _ => throw new JsonException($"Unexpected token type: {reader.TokenType}")
        };
    }

    /// <summary>
    /// Writes the <see cref="Temperature"/> object to JSON as a numeric value in Celsius.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, Temperature value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.Celsius);
    }
}

/// <summary>
/// Custom JSON converter for nullable <see cref="Temperature"/> that converts numeric values to Temperature in Celsius.
/// </summary>
public class NullableTemperatureJsonConverter : JsonConverter<Temperature?>
{
    /// <summary>
    /// Reads and converts the JSON numeric value to a nullable <see cref="Temperature"/> object.
    /// </summary>
    public override Temperature? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.Number => new Temperature(reader.GetDouble(), Temperature.UnitType.Celsius),
            JsonTokenType.Null => null,
            _ => throw new JsonException($"Unexpected token type: {reader.TokenType}")
        };
    }

    /// <summary>
    /// Writes the nullable <see cref="Temperature"/> object to JSON as a numeric value in Celsius.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, Temperature? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
        {
            writer.WriteNumberValue(value.Value.Celsius);
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}
