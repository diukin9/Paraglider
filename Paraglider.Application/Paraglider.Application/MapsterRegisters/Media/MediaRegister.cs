using Mapster;
using Paraglider.Application.DataTransferObjects;
using Paraglider.Domain.NoSQL.ValueObjects;

namespace Paraglider.Application.MapsterRegisters;

public class MediaRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Media, MediaDTO>();
    }
}
