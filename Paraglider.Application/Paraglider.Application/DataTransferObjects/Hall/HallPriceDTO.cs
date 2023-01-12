using Paraglider.Infrastructure.Common.Interfaces;
using System.Text.Json.Serialization;

namespace Paraglider.Application.DataTransferObjects;

public class HallPriceDTO : IDataTransferObject
{
    [JsonPropertyName("price_per_person")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? PricePerPerson { get; set; }

    [JsonPropertyName("rental_price")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? RentalPrice { get; set; }
}
