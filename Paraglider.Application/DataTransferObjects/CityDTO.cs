using Mapster;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects
{
    public record CityDTO : IDataTransferObject
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
        public string Key { get; init; } = null!;

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<City, CityDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Key, src => src.Key)
            .RequireDestinationMemberSource(true);
    }
}
