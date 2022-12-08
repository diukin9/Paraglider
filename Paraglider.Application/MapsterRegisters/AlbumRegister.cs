using Mapster;
using Paraglider.API.DataTransferObjects;
using Paraglider.Domain.NoSQL.ValueObjects;

namespace Paraglider.API.MapsterRegisters;

public class AlbumRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Album, AlbumDTO>();
    }
}
