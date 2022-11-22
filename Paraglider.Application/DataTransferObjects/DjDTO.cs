using Mapster;
using Paraglider.API.DataTransferObjects.Abstractions;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Domain.NoSQL.ValueObjects;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects;

public record DjDTO : BaseWeddingComponentDTO, IOfferServices, IDataTransferObject
{
    public List<Service> Services { get; set; } = new List<Service>();

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Dj, DjDTO>()
            .RequireDestinationMemberSource(true);
    }
}
