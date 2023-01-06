using MongoDB.Bson.Serialization.Attributes;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class HallRentalPrice
{
    [BsonElement("pricePerPerson")]
    public decimal? PricePerPerson { get; set; }

    [BsonElement("rentalPrice")]
    public decimal? RentalPrice { get; set; }
}
