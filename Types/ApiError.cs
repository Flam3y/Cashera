namespace Cashera.Types;

public class ApiError
{
    public string? Message { get; set; }

    public Dictionary<string, string[]>? Errors { get; set; }
}
