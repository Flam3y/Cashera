namespace Cashera;

public class CasheraClientOptions(string apiKey, string? secret = null, string? baseRequestUrl = null)
{
    private const string MainCasheraUrl = "https://api.cashera.cash/api/v1";

    public string ApiKey { get; } = apiKey ?? throw new ArgumentNullException(nameof(apiKey));

    public string? Secret { get; } = secret;

    public string BaseRequestUrl { get; } = (baseRequestUrl ?? MainCasheraUrl).TrimEnd('/');

    public TimeSpan RetryDelay { get; set; } = TimeSpan.FromSeconds(5);

    public int RetryCount { get; set; } = 3;
}
