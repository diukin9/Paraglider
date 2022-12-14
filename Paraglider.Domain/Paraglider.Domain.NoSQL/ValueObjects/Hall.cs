using MongoDB.Bson.Serialization.Attributes;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class Hall
{
    [BsonElement("name")]
    public string Name { get; set; } = null!;

    [BsonElement("description")]
    public string? Description { get; set; }

    [BsonElement("rentalPrice")]
    public HallRentalPrice RentalPrice { get; set; } = null!;

    [BsonElement("capacity")]
    public Capacity Capacity { get; set; } = null!;

    [BsonElement("album")]
    public Album Album { get; set; } = null!;

    [BsonElement("minPrice")]
    public decimal? MinimalPrice { get; set; }
}
