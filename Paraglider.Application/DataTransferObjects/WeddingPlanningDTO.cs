using Mapster;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects;

public record WeddingPlanningDTO : IDataTransferObject
{
    public Guid Id { get; set; }
    public CityDTO City { get; set; } = null!;
    public List<object> Components { get; set; } = new List<object>();

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<WeddingPlanning, WeddingPlanningDTO>()
            .Ignore(dest => dest.Components)
            .RequireDestinationMemberSource(true);
    }
}