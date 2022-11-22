using Paraglider.Domain.Common.Entities;
using Paraglider.Domain.NoSQL.Enums;
using Paraglider.Domain.NoSQL.ValueObjects;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Domain.NoSQL.Abstractions;

public interface IWeddingComponent : IIdentified
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? AvatarUrl { get; set; }
    public Contacts Contacts { get; set; }
    public City City { get; set; }
    public Album Album { get; set; }
    public List<Review> Reviews { get; set; }
}
