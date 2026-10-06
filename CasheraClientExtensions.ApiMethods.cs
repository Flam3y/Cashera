using Cashera.Requests.Transactions;
using Cashera.Types;
using Cashera.Types.Enums;
using System.Text.Json.Nodes;

namespace Cashera;

public static partial class CasheraClientExtensions
{
    public static async Task<Transaction> CreateTransaction(
        this ICasheraClient apiClient,
        long amount,
        Currency currency,
        string externalId,
        string description,
        PaymentMethod? paymentMethod = default,
        JsonObject? metadata = default,
        Uri? callbackUri = default,
        Uri? successUri = default,
        Uri? failUri = default,
        CancellationToken cancellationToken = default)
    {
        return await apiClient.SendRequest<Transaction>(
            new CreateTransactionRequest()
            {
                Amount = amount,
                Currency = currency,
                PaymentMethod = paymentMethod,
                ExternalId = externalId,
                Description = description,
                Metadata = metadata,
                CallbackUrl = callbackUri,
                SuccessUrl = successUri,
                FailUrl = failUri,
            }, cancellationToken);
    }

    public static async Task<Transaction> GetTransaction(
        this ICasheraClient apiClient,
        Guid uuid,
        CancellationToken cancellationToken = default)
    {
        return await apiClient.SendRequest<Transaction>(
            new GetTransactionRequest(uuid), cancellationToken);
    }

    public static async Task<Transaction> GetTransactionByExternalId(
        this ICasheraClient apiClient,
        string externalId,
        CancellationToken cancellationToken = default)
    {
        return await apiClient.SendRequest<Transaction>(
            new GetTransactionByExternalIdRequest(externalId), cancellationToken);
    }
}
