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

#pragma warning disable IDE0044
#pragma warning disable CS0649

    [JsonProperty("name_edit")] private string? nameEdit;
    [JsonProperty("value_edit")] private T? valueEdit;

#pragma warning restore IDE0044
#pragma warning restore CS0649
}