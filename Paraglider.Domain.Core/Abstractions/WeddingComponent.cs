using Paraglider.Domain.Entities;
using Paraglider.Domain.ValueObjects;

namespace Paraglider.Domain.Abstractions
{
    public abstract class WeddingComponent : IWeddingComponent
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public Contacts Contacts { get; set; } = null!;

        public Guid ExternalInfoId { get; set; }
        public virtual ExternalInfo ExternalInfo { get; set; } = null!;

        public Guid CityId { get; set; }
        public virtual City City { get; set; } = null!;

        public Guid AlbumId { get; set; }
        public virtual Album Album { get; set; } = null!;

        public virtual List<Review> Reviews { get; set; } = new List<Review>();
    }
}
