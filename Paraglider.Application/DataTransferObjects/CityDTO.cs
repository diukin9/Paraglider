using Mapster;
using Paraglider.Domain.Entities;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects
{
    public class CityDTO : IDataTransferObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Key { get; set; } = null!;

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<City, CityDTO>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Key, src => src.Key)
                .RequireDestinationMemberSource(true);
        }
    }
}
