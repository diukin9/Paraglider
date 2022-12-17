namespace Paraglider.Clients.Gorko.Models.Abstractions;

public interface IHaveReviews
{
    public ICollection<Review>? Reviews { get; set; }
}