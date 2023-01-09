using System.Text.Json.Serialization;

namespace Paraglider.MobileApp.Models;

public class ResponseTemplate<T>
{
    [JsonPropertyName("metadata")]
    public Metadata<T> Metadata { get; set; }

    [JsonPropertyName("exception")]
    public Exception Exception { get; set; }

    [JsonPropertyName("isOk")]
    public bool IsOk { get; set; }
}

public class Metadata<T>
{
    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("dataObject")]
    public T DataObject { get; set; }
}