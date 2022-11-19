namespace Paraglider.GorkoClient.Models.Abstractions;

public interface IHaveReviews
{
    public IReadOnlyCollection<Review>? Reviews { get; set; }
}