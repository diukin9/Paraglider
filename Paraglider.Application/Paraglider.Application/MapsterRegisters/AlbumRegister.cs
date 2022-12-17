using Mapster;
using Paraglider.Application.DataTransferObjects;
using Paraglider.Domain.NoSQL.ValueObjects;

namespace Paraglider.Application.MapsterRegisters;

public class AlbumRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Album, AlbumDTO>();
    }
}
