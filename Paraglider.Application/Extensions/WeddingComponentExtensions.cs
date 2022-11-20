using MapsterMapper;
using Paraglider.API.DataTransferObjects;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Domain.NoSQL.Enums;

namespace Paraglider.API.Extensions;

//это вообще треш
public static class WeddingComponentExtensions
{
    public static object Convert(this WeddingComponent component, IMapper mapper) => component.Type switch
    {
        WeddingComponentType.Limousine => mapper.Map<LimousineDTO>(component),
        WeddingComponentType.Photographer => mapper.Map<PhotographerDTO>(component),
        WeddingComponentType.Videographer => mapper.Map<VideographerDTO>(component),
        WeddingComponentType.Toastmaster => mapper.Map<ToastmasterDTO>(component),
        WeddingComponentType.Dj => mapper.Map<DjDTO>(component),
        WeddingComponentType.Stylist => mapper.Map<StylistDTO>(component),
        WeddingComponentType.Decorator => mapper.Map<DecoratorDTO>(component),
        WeddingComponentType.Catering => mapper.Map<CateringDTO>(component),
        WeddingComponentType.Confectioner => mapper.Map<ConfectionerDTO>(component),
        WeddingComponentType.BanquetHall => mapper.Map<BanquetHallDTO>(component),
        WeddingComponentType.Registrar => mapper.Map<RegistrarDTO>(component),
        WeddingComponentType.PhotoStudio => mapper.Map<PhotoStudioDTO>(component),
        _ => throw new NotImplementedException()
    };
}