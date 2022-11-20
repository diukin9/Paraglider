using MongoDB.Bson.Serialization.Attributes;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class Service
{
    [BsonElement("id")]
    public string Id { get; set; } = null!;

    [BsonElement("name")]
    public string Name { get; set; } = null!;

    [BsonElement("description")]
    public string? Description { get; set; }

    [BsonElement("price")]
    public Price Price { get; set; } = null!;

    public Service(string id, string name, Price price, string? description = null)
    {
        Id = id ?? throw new ArgumentNullException(ExceptionMessages.NullOrEmpty(nameof(id)));
        Name = name ?? throw new ArgumentNullException(ExceptionMessages.NullOrEmpty(nameof(name)));
        Description = description;
        Price = price ?? throw new ArgumentNullException(ExceptionMessages.NullOrEmpty(nameof(price)));
    }
}

