using Cashera.Serialization;
using System.Text.Json.Serialization;

namespace Cashera.Types.Enums;

[JsonConverter(typeof(SnakeCaseConverter<FeePayer>))]
public enum FeePayer
{
    Unknown,
    Merchant,
    Customer
}
