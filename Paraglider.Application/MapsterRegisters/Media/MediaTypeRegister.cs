using Mapster;
using Paraglider.API.DataTransferObjects;
using Paraglider.Domain.NoSQL.Enums;

namespace Paraglider.API.MapsterRegisters;

public class MediaTypeRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<MediaType, MediaTypeDTO>()
            .Map(dest => dest.Name, src => Enum.GetName(typeof(MediaType), src))
            .Map(dest => dest.Value, src => (int)src);
    }
}
