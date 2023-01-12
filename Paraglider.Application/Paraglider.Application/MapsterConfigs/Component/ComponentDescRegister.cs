using Mapster;
using Paraglider.Application.DataTransferObjects;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.Application.MapsterRegisters;

public class ComponentDescRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ComponentDesc, ComponentDescDTO>();
    }
}