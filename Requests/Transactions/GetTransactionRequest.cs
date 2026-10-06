namespace Cashera.Requests.Transactions;

public class GetTransactionRequest : ParameterlessRequest
{
    public GetTransactionRequest(Guid uuid) : base($"integration/transactions/{uuid}")
    {
        HttpMethod = HttpMethod.Get;
    }
}
