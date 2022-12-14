using Newtonsoft.Json;

namespace Paraglider.Clients.Gorko.Models;

public class Parameter<T>
{
    [JsonIgnore]
    public string? Name
    {
        get => _name ?? nameEdit;
        set => _name = value;
    }

    [JsonIgnore]
    public T? Value
    {
        get => _value ?? valueEdit;
        set => _value = value;
    }

    [JsonProperty("name")] private string? _name;
    [JsonProperty("value")] private T? _value;

    [JsonProperty("name_edit")] private string? nameEdit;
    [JsonProperty("value_edit")] private T? valueEdit;
}