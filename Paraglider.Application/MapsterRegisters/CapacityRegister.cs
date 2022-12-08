using Mapster;
using Paraglider.API.DataTransferObjects;
using Paraglider.Domain.NoSQL.ValueObjects;

namespace Paraglider.API.MapsterRegisters;

public class CapacityRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Capacity, CapacityDTO>();
    }
}
