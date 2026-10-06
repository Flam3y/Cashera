using Cashera.Serialization;
using System.Text.Json.Serialization;

namespace Cashera.Types.Enums;

[JsonConverter(typeof(SnakeCaseConverter<SubscriptionStatus>))]
public enum SubscriptionStatus
{
    Unknown,
    Active
}
