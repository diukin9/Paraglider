using Newtonsoft.Json;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Application.DataTransferObjects;

public class HallRentalPriceDTO : IDataTransferObject
{
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public decimal? PricePerPerson { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public decimal? RentalPrice { get; set; }
}
