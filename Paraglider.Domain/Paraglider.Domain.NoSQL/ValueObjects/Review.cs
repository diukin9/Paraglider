using MongoDB.Bson.Serialization.Attributes;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class Review
{
    [BsonElement("id")]
    public string Id { get; set; } = null!;

    [BsonElement("author")]
    public string Author { get; set; } = null!;

    [BsonElement("authorAvatarUrl")]
    public string AvatarUrl { get; set; } = null!;

    [BsonElement("text")]
    public string? Text { get; set; }

    [BsonElement("evaluation")]
    public double Evaluation { get; set; }

    public Review(string id, string author, double evaluation, string? avatarUrl = null, string? text = null)
    {
        if (evaluation < 0)
        {
            throw new ArgumentException(ExceptionMessages.CannotBeNegative(nameof(Evaluation)));
        }

        if (evaluation > 5)
        {
            throw new ArgumentException(ExceptionMessages.CannotBeHigherThan(nameof(Evaluation), 5));
        }

        Id = id ?? throw new ArgumentNullException(ExceptionMessages.NullOrEmpty(nameof(id)));
        Author = author ?? throw new ArgumentNullException(ExceptionMessages.NullOrEmpty(nameof(author)));
        AvatarUrl = avatarUrl ?? DefaultAvatarUrl;
        Evaluation = evaluation;
        Text = text;
    }
}
