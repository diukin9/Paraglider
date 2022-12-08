using MongoDB.Bson.Serialization.Attributes;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class Service
{
    [BsonElement("name")]
    public string Name { get; set; } = null!;

    [BsonElement("description")]
    public string? Description { get; set; }

    [BsonElement("price")]
    public Price Price { get; set; } = null!;
}

