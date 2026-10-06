using Cashera.Serialization;
using System.Text.Json.Serialization;

namespace Cashera.Types.Enums;

[JsonConverter(typeof(SnakeCaseConverter<TransactionType>))]
public enum TransactionType
{
    Unknown,
    Deposit,
    Withdraw
}
