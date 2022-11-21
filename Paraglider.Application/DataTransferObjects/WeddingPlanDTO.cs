using Mapster;
using Paraglider.Domain.Entities;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects
{
    public record WeddingPlanDTO : IDataTransferObject
    {
        public Guid Id { get; init; }
        public CityDTO City { get; init; } = null!;

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<WeddingPlanning, WeddingPlanDTO>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.City, src => src.City)
                .RequireDestinationMemberSource(true);
        }
    }
}
