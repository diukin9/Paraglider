using Mapster;
using Paraglider.Application.DataTransferObjects;
using Paraglider.Domain.RDB.ValueObjects;

namespace Paraglider.Application.MapsterRegisters;

public class CapacityRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Capacity, CapacityDTO>();
    }
}
