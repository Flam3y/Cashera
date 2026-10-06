using Cashera.Types.Enums;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Cashera.Types;

public class Payout
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Guid Uuid { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public PayoutStatus Status { get; set; }

    public string? ExternalId { get; set; }
    public long? Amount { get; set; }
    public Currency? Currency { get; set; }
    public PayoutDestination? Destination { get; set; }
    public JsonObject? Metadata { get; set; }
    public string? FailureReason { get; set; }
    public DateTimeOffset? CompletedAt { get; set; }
    public DateTimeOffset? FailedAt { get; set; }
}

public class PayoutDestination
{
    public string? Type { get; set; }
    public string? Number { get; set; }
}
