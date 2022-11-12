using Paraglider.AspNetCore.Identity.Domain.Abstractions;
using Paraglider.AspNetCore.Identity.Domain.ValueObjects;

namespace Paraglider.AspNetCore.Identity.Domain.Entities
{
    public class Videographer : IHaveId, IWPItem, IHaveWeddingServices
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public ExternalInfo ExternalInfo { get; set; } = null!;
        public Contacts Contacts { get; set; } = null!;

        public Guid CityId { get; set; }
        public virtual City City { get; set; } = null!;

        public Guid AlbumId { get; set; }
        public virtual Album Album { get; set; } = null!;

        public virtual List<Review> Reviews { get; set; } = new List<Review>();
        public virtual List<WeddingService> WeddingServices { get; set; } = new List<WeddingService>();
    }
}
