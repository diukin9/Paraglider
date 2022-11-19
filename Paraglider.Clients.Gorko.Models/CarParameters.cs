using Newtonsoft.Json;

namespace Paraglider.GorkoClient.Models;

public class CarParameters
{
    [JsonProperty("param_capacity")] public Parameter<int>? Capacity { get; set; }

    [JsonProperty("param_color")] public Parameter<string>? ColorName { get; set; }

    [JsonProperty("param_length")] public Parameter<int>? Length { get; set; }

    [JsonProperty("param_price")] public Parameter<int>? PriceFrom { get; set; }

    [JsonProperty("param_time")] public Parameter<int>? MinTimeRentInHour { get; set; }

    [JsonProperty("param_year")] public Parameter<int>? ManufactureYear { get; set; }
}