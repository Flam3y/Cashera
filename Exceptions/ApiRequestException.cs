using System.Net;

namespace Cashera.Exceptions;

public class ApiRequestException : RequestException
{
    public HttpStatusCode ErrorCode { get; }

    public IReadOnlyDictionary<string, string[]>? Errors { get; }

    public ApiRequestException(string message) : base(message) { }

    public ApiRequestException(string message, HttpStatusCode errorCode) : base(message, errorCode) => ErrorCode = errorCode;

    public ApiRequestException(string message, HttpStatusCode errorCode, IReadOnlyDictionary<string, string[]>? errors)
        : base(message, errorCode)
    {
        ErrorCode = errorCode;
        Errors = errors;
    }

    public ApiRequestException(string message, Exception innerException) : base(message, innerException) { }

    public ApiRequestException(string message, HttpStatusCode errorCode, Exception innerException)
        : base(message, errorCode, innerException) => ErrorCode = errorCode;

    public override string ToString()
    {
        return $"Cashera API error {(int)ErrorCode} {ErrorCode}: {Message}";
    }
}
