using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Domain.RDB.Entities;

public class Review : IIdentified
{
    public Guid Id { get; set; }

    public string Author { get; set; } = null!;

    public string AvatarUrl { get; set; } = null!;

    public string? Text { get; set; }

    public double Evaluation { get; set; }
}
