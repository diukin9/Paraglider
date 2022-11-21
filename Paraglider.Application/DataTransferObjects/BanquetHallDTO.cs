using Mapster;
using Paraglider.API.DataTransferObjects.Abstractions;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Domain.NoSQL.ValueObjects;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects;

public class BanquetHallDTO : WeddingComponentDTO, IDataTransferObject
{
    public virtual List<Hall> Halls { get; set; } = new List<Hall>();

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<WeddingComponent, BanquetHallDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.AvatarUrl, src => src.AvatarUrl)
            .Map(dest => dest.CityId, src => src.CityId)
            .Map(dest => dest.Album, src => src.Album)
            .Map(dest => dest.Contacts, src => src.Contacts)
            .Map(dest => dest.Reviews, src => src.Reviews)
            .Map(dest => dest.Halls, src => src.Halls);
    }
}
