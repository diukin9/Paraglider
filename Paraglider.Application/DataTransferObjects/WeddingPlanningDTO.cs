using Mapster;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects;

public class WeddingPlanningDTO : IDataTransferObject
{
    public Guid Id { get; set; }
    public CityDTO City { get; set; } = null!;
    public List<object> Components { get; set; } = new List<object>();

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<WeddingPlanning, WeddingPlanningDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.City, src => src.City)
            .Ignore(dest => dest.Components)
            .RequireDestinationMemberSource(true);
    }
}