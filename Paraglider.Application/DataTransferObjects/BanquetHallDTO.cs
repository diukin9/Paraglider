using Mapster;
using Paraglider.API.DataTransferObjects.Abstractions;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Domain.NoSQL.ValueObjects;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects;

public record BanquetHallDTO : BaseWeddingComponentDTO, IDataTransferObject
{
    public List<Hall> Halls { get; set; } = new List<Hall>();

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<BanquetHall, BanquetHallDTO>()
            .RequireDestinationMemberSource(true);
    }
}
