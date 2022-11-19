using Paraglider.Domain.NoSQL.ValueObjects;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects.Abstractions;

public interface IWeddingComponentDTO : IIdentified
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? AvatarUrl { get; set; }
    public Contacts Contacts { get; set; }
    public Guid CityId { get; set; }
    public Album Album { get; set; }
    public List<Review> Reviews { get; set; }
}
