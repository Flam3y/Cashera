using Cashera.Types.Enums;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Cashera.Types;

public class Transaction
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Guid Uuid { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public TransactionStatus Status { get; set; }

    public TransactionType? Type { get; set; }
    public long? Amount { get; set; }
    public long? GrossAmount { get; set; }
    public long? NetAmount { get; set; }
    public long? FeeAmount { get; set; }
    public FeePayer? FeePayer { get; set; }
    public Currency? Currency { get; set; }
    public Currency? SettlementCurrency { get; set; }
    public long? SettlementAmount { get; set; }
    public string? SettlementAmountFormatted { get; set; }
    public decimal? FxRate { get; set; }
    public DateTimeOffset? FxRateUpdatedAt { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public string? ExternalId { get; set; }
    public string? Description { get; set; }
    public JsonObject? Metadata { get; set; }
    public string? PaymentUrl { get; set; }
    public DateTimeOffset? ExpiresAt { get; set; }
    public DateTimeOffset? PaidAt { get; set; }
    public DateTimeOffset? SettleAvailableAt { get; set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
