using Cashera.Serialization;
using System.Text.Json.Serialization;

namespace Cashera.Types.Enums;

[JsonConverter(typeof(ToUpperConverter<Currency>))]
public enum Currency
{
    Unknown,
    Rub,
    Usdt
}
