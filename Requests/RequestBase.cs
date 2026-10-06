using Cashera.Requests.Abstractions;
using Cashera.Serialization;
using System.Text.Json.Serialization;

namespace Cashera.Requests;

public abstract class RequestBase(string methodPath) : IRequest
{
    [JsonIgnore]
    public HttpMethod HttpMethod { get; set; } = HttpMethod.Post;

    [JsonIgnore]
    public string MethodPath { get; } = methodPath;

    public virtual HttpContent? ToHttpContent() =>
        System.Net.Http.Json.JsonContent.Create(this, GetType(), options: JsonApi.Options);
}

public abstract class ParameterlessRequest(string methodName) : RequestBase(methodName)
{
    public override HttpContent? ToHttpContent() => default;
}
