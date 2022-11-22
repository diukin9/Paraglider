using Paraglider.Domain.Common.Entities;
using Paraglider.Domain.NoSQL.Abstractions;
using Paraglider.Domain.NoSQL.Enums;
using Paraglider.Domain.NoSQL.ValueObjects;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Domain.NoSQL.Entities;

public class Confectioner : IDerived<CommonWeddingComponent>, IIdentified, IWeddingComponent, IOfferServices
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public string? AvatarUrl { get; set; }
    public Contacts Contacts { get; set; } = null!;
    public City City { get; set; } = null!;
    public Album Album { get; set; } = null!;
    public List<Review> Reviews { get; set; } = new List<Review>();

    public List<Service> Services { get; set; } = new List<Service>();
}
