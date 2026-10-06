using Cashera.Serialization;
using System.Text.Json.Serialization;

namespace Cashera.Types.Enums;

[JsonConverter(typeof(SnakeCaseConverter<SubscriptionInterval>))]
public enum SubscriptionInterval
{
    Unknown,
    Monthly
}
