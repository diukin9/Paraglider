using Mapster;
using Paraglider.API.DataTransferObjects;
using Paraglider.Domain.NoSQL.ValueObjects;

namespace Paraglider.API.MapsterRegisters;

public class HallRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Hall, HallDTO>();
    }
}
