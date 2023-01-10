using Mapster;
using Paraglider.Application.DataTransferObjects;
using Paraglider.Domain.RDB.Enums;

namespace Paraglider.Application.MapsterRegisters;

public class MediaTypeRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<MediaType, MediaTypeDTO>()
            .Map(dest => dest.Name, src => Enum.GetName(typeof(MediaType), src))
            .Map(dest => dest.Value, src => (int)src);
    }
}
