using Cashera.Args;
using Cashera.Requests.Abstractions;

namespace Cashera;

public delegate ValueTask AsyncEventHandler<in TArgs>(ICasheraClient apiClient, TArgs args, CancellationToken cancellationToken = default);

public interface ICasheraClient
{
    TimeSpan Timeout { get; set; }

    event AsyncEventHandler<ApiRequestEventArgs>? OnMakingApiRequest;

    event AsyncEventHandler<ApiResponseEventArgs>? OnApiResponseReceived;

    Task<TResponse> SendRequest<TResponse>(IRequest request, CancellationToken cancellationToken = default)
        where TResponse : class;

    bool ValidateWebhook(string? apiKey, string? secret);
}
