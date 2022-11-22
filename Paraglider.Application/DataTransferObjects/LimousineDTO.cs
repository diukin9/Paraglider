using Mapster;
using Paraglider.API.DataTransferObjects.Abstractions;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects;

public record LimousineDTO : BaseWeddingComponentDTO, IDataTransferObject
{
    public DateTime? ManufactureYear { get; set; }
    public TimeSpan? MinRentLength { get; set; }
    public int? Capacity { get; set; }

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Limousine, LimousineDTO>()
            .RequireDestinationMemberSource(true);
    }
}
