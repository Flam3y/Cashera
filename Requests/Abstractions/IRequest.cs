namespace Cashera.Requests.Abstractions;

public interface IRequest
{
    HttpMethod HttpMethod { get; }

    string MethodPath { get; }

    HttpContent? ToHttpContent();
}
