using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cashera.Serialization;

internal class SnakeCaseConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum
{
    private static readonly Dictionary<string, TEnum> mapRead = [];
    private static readonly Dictionary<TEnum, string> mapWrite = [];

    static SnakeCaseConverter()
    {
        Type enumType = typeof(TEnum);
        string[] names = Enum.GetNames(enumType);

        TEnum[] values = Enum.GetValues<TEnum>();

        for (int i = 0; i < names.Length; i++)
        {
            string snakeName = JsonNamingPolicy.SnakeCaseLower.ConvertName(names[i]);

            mapRead[snakeName] = values[i];
            mapWrite[values[i]] = snakeName;
        }
    }

    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? str = reader.GetString();
        if (str == null)
        {
            return default;
        }

        mapRead.TryGetValue(str, out TEnum value);
        return value;
    }

    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        if (!mapWrite.TryGetValue(value, out string? str))
        {
            throw new JsonException($"Can't serialize value {value} for enum {typeof(TEnum).Name}");
        }

        writer.WriteStringValue(str);
    }
}
