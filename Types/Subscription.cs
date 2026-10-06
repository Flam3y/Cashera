using Cashera.Types.Enums;
using System.Text.Json.Serialization;

namespace Cashera.Types;

public class Subscription
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Guid Uuid { get; set; }

    public string? ExternalId { get; set; }
    public SubscriptionStatus? Status { get; set; }
    public long? Amount { get; set; }
    public long? GrossAmount { get; set; }
    public long? NetAmount { get; set; }
    public Currency? Currency { get; set; }
    public SubscriptionInterval? Interval { get; set; }
    public DateTimeOffset? NextChargeAt { get; set; }
    public DateTimeOffset? LastChargeAt { get; set; }
    public DateTimeOffset? ActivatedAt { get; set; }
    public DateTimeOffset? CancelledAt { get; set; }
    public DateTimeOffset? FailedAt { get; set; }
}
