using Mapster;
using Paraglider.Application.DataTransferObjects;
using Paraglider.Domain.NoSQL.ValueObjects;

namespace Paraglider.Application.MapsterRegisters;

public class HallRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Hall, HallDTO>();
    }
}
