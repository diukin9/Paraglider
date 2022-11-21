namespace Paraglider.Clients.Gorko.Models.Abstractions;

public interface IHaveReviews
{
    public IReadOnlyCollection<Review>? Reviews { get; set; }
}