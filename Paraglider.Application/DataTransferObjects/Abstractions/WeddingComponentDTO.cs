using Paraglider.Domain.NoSQL.ValueObjects;

namespace Paraglider.API.DataTransferObjects.Abstractions;

public class WeddingComponentDTO : IWeddingComponentDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public string? AvatarUrl { get; set; }
    public Contacts Contacts { get; set; } = null!;
    public Guid CityId { get; set; }
    public Album Album { get; set; } = null!;
    public List<Review> Reviews { get; set; } = new List<Review>();
}
