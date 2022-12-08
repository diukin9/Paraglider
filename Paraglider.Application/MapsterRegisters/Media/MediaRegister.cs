using Mapster;
using Paraglider.API.DataTransferObjects;
using Paraglider.Domain.NoSQL.ValueObjects;

namespace Paraglider.API.MapsterRegisters;

public class MediaRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Media, MediaDTO>();
    }
}
