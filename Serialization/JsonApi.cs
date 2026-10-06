using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cashera.Serialization;

public static class JsonApi
{
    public static JsonSerializerOptions Options { get; } = new();

    static JsonApi()
    {
        Configure(Options);
    }

    public static void Configure(JsonSerializerOptions options)
    {
        options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.NumberHandling = JsonNumberHandling.AllowReadingFromString;

        if (!JsonSerializer.IsReflectionEnabledByDefault) 
        {
            options.TypeInfoResolverChain.Add(JsonApiSerializationContext.Default);
        }
    }
}
