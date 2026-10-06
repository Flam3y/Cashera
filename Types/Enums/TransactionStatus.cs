using Cashera.Serialization;
using System.Text.Json.Serialization;

namespace Cashera.Types.Enums;

[JsonConverter(typeof(SnakeCaseConverter<TransactionStatus>))]
public enum TransactionStatus
{
    Unknown,
    Pending,
    Paid,
    Failed,
    Expired,
    Refunded,
    Chargeback
}

public static class TransactionStatusExtensions
{
    public static bool IsFinal(this TransactionStatus status) =>
        status is TransactionStatus.Paid
            or TransactionStatus.Failed
            or TransactionStatus.Expired
            or TransactionStatus.Refunded
            or TransactionStatus.Chargeback;
}
