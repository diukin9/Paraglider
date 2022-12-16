using Newtonsoft.Json;
using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Application.DataTransferObjects;

public class PriceDTO : IDataTransferObject
{
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public decimal? Min { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public decimal? Max { get; set; }
}
