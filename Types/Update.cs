using Cashera.Types.Enums;
using System.Text.Json.Serialization;

namespace Cashera.Types;

public class Update
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Event { get; set; } = default!;

    [JsonIgnore]
    public UpdateType Type => Event switch
    {
        UpdateEvents.TransactionStatusUpdated => UpdateType.TransactionStatusUpdated,
        UpdateEvents.PayoutStatusUpdated => UpdateType.PayoutStatusUpdated,
        UpdateEvents.SubscriptionStatusUpdated => UpdateType.SubscriptionStatusUpdated,
        UpdateEvents.WebhookTest => UpdateType.WebhookTest,
        _ => UpdateType.Unknown,
    };

    public Transaction? Transaction { get; set; }
    public Payout? Payout { get; set; }
    public Subscription? Subscription { get; set; }
    public WebhookTest? Test { get; set; }
}

public class WebhookTest
{
    public DateTimeOffset? SentAt { get; set; }
}

public static class UpdateEvents
{
    public const string TransactionStatusUpdated = "transaction.status_updated";
    public const string PayoutStatusUpdated = "payout.status_updated";
    public const string SubscriptionStatusUpdated = "subscription.status_updated";
    public const string WebhookTest = "webhook.test";
}
