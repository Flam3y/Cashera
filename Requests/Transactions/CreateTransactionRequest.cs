using Cashera.Types.Enums;
using System.Text.Json.Nodes;

namespace Cashera.Requests.Transactions;

public class CreateTransactionRequest() : RequestBase("integration/transactions")
{
    public required long Amount { get; set; }

    public required Currency Currency { get; set; }

    public PaymentMethod? PaymentMethod { get; set; }

    public required string ExternalId { get; set; }

    public required string Description { get; set; }

    public JsonObject? Metadata { get; set; }

    public Uri? CallbackUrl { get; set; }

    public Uri? SuccessUrl { get; set; }

    public Uri? FailUrl { get; set; }
}
