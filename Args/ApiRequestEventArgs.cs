using Cashera.Requests.Abstractions;

namespace Cashera.Args;

public class ApiRequestEventArgs(IRequest request, HttpRequestMessage? httpRequestMessage = default) : EventArgs
{
    public IRequest Request { get; } = request;

    public HttpRequestMessage? HttpRequestMessage { get; } = httpRequestMessage;
}
