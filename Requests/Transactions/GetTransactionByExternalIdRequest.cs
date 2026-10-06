namespace Cashera.Requests.Transactions;

public class GetTransactionByExternalIdRequest : ParameterlessRequest
{
    public GetTransactionByExternalIdRequest(string externalId)
        : base($"integration/transactions/by-external-id/{Uri.EscapeDataString(externalId ?? throw new ArgumentNullException(nameof(externalId)))}")
    {
        HttpMethod = HttpMethod.Get;
    }
}
