using MongoDB.Bson.Serialization.Attributes;

namespace Paraglider.Domain.RDB.ValueObjects;

public class Price
{
    public decimal? Min { get; set; }

    public decimal? Max { get; set; }
}
