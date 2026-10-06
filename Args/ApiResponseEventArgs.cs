namespace Cashera.Args;

public class ApiResponseEventArgs(HttpResponseMessage responseMessage, ApiRequestEventArgs apiRequestEventArgs)
{
    public HttpResponseMessage ResponseMessage { get; } = responseMessage;

    public ApiRequestEventArgs ApiRequestEventArgs { get; } = apiRequestEventArgs;
}
