using Cashera.Serialization;
using System.Text.Json.Serialization;

namespace Cashera.Types.Enums;

[JsonConverter(typeof(SnakeCaseConverter<PayoutStatus>))]
public enum PayoutStatus
{
    Unknown,
    Completed
}
