namespace Paraglider.Domain.NoSQL.ValueObjects;

public class Review
{
    public string Id { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string AvatarUrl { get; set; } = null!;
    public string? Text { get; set; }
    public double Evaluation { get; set; }
}
