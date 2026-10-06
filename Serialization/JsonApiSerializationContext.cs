using Cashera.Requests.Transactions;
using Cashera.Types;
using System.Text.Json.Serialization;

namespace Cashera.Serialization;

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    NumberHandling = JsonNumberHandling.AllowReadingFromString,
    UseStringEnumConverter = false)]
[JsonSerializable(typeof(CreateTransactionRequest))]
[JsonSerializable(typeof(GetTransactionRequest))]
[JsonSerializable(typeof(GetTransactionByExternalIdRequest))]
[JsonSerializable(typeof(Transaction))]
[JsonSerializable(typeof(ApiError))]
[JsonSerializable(typeof(Update))]
public partial class JsonApiSerializationContext : JsonSerializerContext;
