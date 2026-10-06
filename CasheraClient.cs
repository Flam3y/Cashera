using Cashera.Args;
using Cashera.Exceptions;
using Cashera.Requests.Abstractions;
using Cashera.Serialization;
using Cashera.Types;
using System.Net;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Cashera;

public class CasheraClient : ICasheraClient
{
    private const string ApiKeyHeader = "X-Api-Key";

    private readonly CasheraClientOptions _options;

    private readonly HttpClient _httpClient;

    public string ApiKey => _options.ApiKey;

    public TimeSpan Timeout
    {
        get => _httpClient.Timeout;
        set => _httpClient.Timeout = value;
    }

    public CancellationToken GlobalCancellationToken { get; }

    public event AsyncEventHandler<ApiRequestEventArgs>? OnMakingApiRequest;
    public event AsyncEventHandler<ApiResponseEventArgs>? OnApiResponseReceived;

    public CasheraClient(CasheraClientOptions options, HttpClient? httpClient = default, CancellationToken cancellationToken = default)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));

        _httpClient = httpClient ?? new HttpClient(new SocketsHttpHandler { PooledConnectionLifetime = TimeSpan.FromMinutes(3) });

        GlobalCancellationToken = cancellationToken;
    }

    public CasheraClient(string apiKey, string? secret = null, HttpClient? httpClient = null, CancellationToken cancellationToken = default)
        : this(new CasheraClientOptions(apiKey, secret), httpClient, cancellationToken)
    { }

    public virtual async Task<TResponse> SendRequest<TResponse>(IRequest request, CancellationToken cancellationToken = default)
        where TResponse : class
    {
        ArgumentNullException.ThrowIfNull(request);

        using CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(GlobalCancellationToken, cancellationToken);
        cancellationToken = cts.Token;

        string url = $"{_options.BaseRequestUrl}/{request.MethodPath}";

        using HttpContent? httpContent = request.ToHttpContent();

        for (int attempt = 1; ; attempt++)
        {
            using HttpRequestMessage httpRequest = new(request.HttpMethod, url) { Content = httpContent };
            httpRequest.Headers.Add(ApiKeyHeader, ApiKey);
            httpRequest.Headers.Accept.ParseAdd("application/json");

            ApiRequestEventArgs? requestEventArgs = null;

            if (OnMakingApiRequest is not null)
            {
                requestEventArgs ??= new(request, httpRequest);
                await OnMakingApiRequest.Invoke(this, requestEventArgs, cancellationToken).ConfigureAwait(false);
            }

            if (httpContent != null && _options.RetryCount > 1 && httpContent.Headers.ContentLength == null)
            {
                await httpContent.LoadIntoBufferAsync(cancellationToken).ConfigureAwait(false);
            }

            HttpResponseMessage httpResponse;

            try
            {
                httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
            }
            catch (TaskCanceledException exception)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    throw;
                }

                throw new RequestException("Cashera API Request timed out", exception);
            }
            catch (Exception exception)
            {
                throw new RequestException($"Cashera API Service Failure: {exception.GetType().Name}: {exception.Message}", exception);
            }

            using (httpResponse)
            {
                if (OnApiResponseReceived is not null)
                {
                    requestEventArgs ??= new(request, httpRequest);
                    ApiResponseEventArgs responseEventArgs = new(httpResponse, requestEventArgs);

                    await OnApiResponseReceived.Invoke(this, responseEventArgs, cancellationToken).ConfigureAwait(false);
                }

                if (!httpResponse.IsSuccessStatusCode)
                {
                    if (attempt < _options.RetryCount && IsTransient(httpResponse.StatusCode))
                    {
                        await Task.Delay(_options.RetryDelay, cancellationToken).ConfigureAwait(false);
                        continue;
                    }

                    throw await CreateApiException(httpResponse, cancellationToken).ConfigureAwait(false);
                }

                TResponse apiResponse = await DeserializeContent<TResponse>(
                    httpResponse,
                    cancellationToken).ConfigureAwait(false);
                return apiResponse;
            }
        }
    }

    public bool ValidateWebhook(string? apiKey, string? secret)
    {
        if (_options.Secret is null)
        {
            throw new InvalidOperationException($"{nameof(CasheraClientOptions.Secret)} must be set to validate webhooks");
        }

        if (apiKey is null || secret is null)
        {
            return false;
        }

        bool apiKeyMatches = FixedTimeEquals(apiKey, ApiKey);
        bool secretMatches = FixedTimeEquals(secret, _options.Secret);

        return apiKeyMatches & secretMatches;
    }

    private static bool FixedTimeEquals(string left, string right)
    {
        return CryptographicOperations.FixedTimeEquals(Encoding.UTF8.GetBytes(left), Encoding.UTF8.GetBytes(right));
    }

    private static bool IsTransient(HttpStatusCode statusCode) =>
        statusCode == HttpStatusCode.TooManyRequests || (int)statusCode >= 500;

    private static async Task<ApiRequestException> CreateApiException(HttpResponseMessage httpResponse,
        CancellationToken cancellationToken = default)
    {
        ApiError? error = null;

        try
        {
            string content = await httpResponse.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(content))
            {
                error = JsonSerializer.Deserialize<ApiError>(content, JsonApi.Options);
            }
        }
        catch (JsonException) 
        {
            //Ignore
        }

        string message = error?.Message ?? httpResponse.ReasonPhrase ?? "Unexpected error";

        return new ApiRequestException(message, httpResponse.StatusCode, error?.Errors);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static async Task<T> DeserializeContent<T>(HttpResponseMessage httpResponse,
        CancellationToken cancellationToken = default) where T : class
    {
        if (httpResponse.Content is null)
        {
            throw new RequestException("Response doesn't contain any content", httpResponse.StatusCode);
        }

        T? deserializedObject;

        try
        {
            deserializedObject = await httpResponse.Content.ReadFromJsonAsync<T>(JsonApi.Options, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            throw new RequestException("There was an exception during deserialization of the response", httpResponse.StatusCode, exception);
        }

        if (deserializedObject is null)
        {
            throw new RequestException("Required properties not found in response", httpResponse.StatusCode);
        }

        return deserializedObject;
    }
}
