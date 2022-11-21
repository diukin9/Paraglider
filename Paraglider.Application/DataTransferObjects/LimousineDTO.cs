using Mapster;
using Paraglider.API.DataTransferObjects.Abstractions;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects;

public class LimousineDTO : WeddingComponentDTO, IDataTransferObject
{
    public DateTime? ManufactureYear { get; set; }
    public TimeSpan? MinRentLength { get; set; }
    public int? Capacity { get; set; }

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<WeddingComponent, LimousineDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.AvatarUrl, src => src.AvatarUrl)
            .Map(dest => dest.CityId, src => src.CityId)
            .Map(dest => dest.Album, src => src.Album)
            .Map(dest => dest.Contacts, src => src.Contacts)
            .Map(dest => dest.Reviews, src => src.Reviews)
            .Map(dest => dest.ManufactureYear, src => src.ManufactureYear)
            .Map(dest => dest.MinRentLength, src => src.MinRentLength)
            .Map(dest => dest.Capacity, src => src.Capacity);
    }
}
