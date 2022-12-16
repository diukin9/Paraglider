using MongoDB.Bson.Serialization.Attributes;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class HallRentalPrice
{
    [BsonIgnoreIfNull]
    [BsonElement("pricePerPerson")]
    public decimal? PricePerPerson { get; set; }

    [BsonIgnoreIfNull]
    [BsonElement("rentalPrice")]
    public decimal? RentalPrice { get; set; }
}
