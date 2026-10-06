using Cashera.Serialization;
using System.Text.Json.Serialization;

namespace Cashera.Types.Enums;

[JsonConverter(typeof(SnakeCaseConverter<PaymentMethod>))]
public enum PaymentMethod
{
    Unknown,
    Sbp,
    Card,
    Mastercard,
    Crypto,
    Cryptobot,
    SbpRecurring
}
