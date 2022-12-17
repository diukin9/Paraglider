using MongoDB.Bson.Serialization.Attributes;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class Price
{
    [BsonElement("min")]
    public decimal? Min { get; set; }

    [BsonElement("max")]
    public decimal? Max { get; set; }
}
