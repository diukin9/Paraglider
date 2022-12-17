using MongoDB.Bson.Serialization.Attributes;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class Review
{
    [BsonElement("author")]
    public string Author { get; set; } = null!;

    [BsonElement("authorAvatarUrl")]
    public string AvatarUrl { get; set; } = null!;

    [BsonElement("text")]
    public string? Text { get; set; }

    [BsonElement("evaluation")]
    public double Evaluation { get; set; }
}
