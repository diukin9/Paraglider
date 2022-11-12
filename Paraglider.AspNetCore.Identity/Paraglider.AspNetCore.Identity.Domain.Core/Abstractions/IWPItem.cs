using Paraglider.AspNetCore.Identity.Domain.Entities;
using Paraglider.AspNetCore.Identity.Domain.ValueObjects;

namespace Paraglider.AspNetCore.Identity.Domain.Abstractions
{
    public interface IWPItem
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public ExternalInfo ExternalInfo { get; set; }
        public Contacts Contacts { get; set; }

        public List<Review> Reviews { get; set; }

        public Guid CityId { get; set; }
        public City City { get; set; }

        public Guid AlbumId { get; set; }
        public Album Album { get; set; }
    }
}
