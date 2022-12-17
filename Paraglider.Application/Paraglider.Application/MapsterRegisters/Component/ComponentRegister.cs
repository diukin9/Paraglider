using Mapster;
using Paraglider.Application.DataTransferObjects;
using Paraglider.Domain.NoSQL.Entities;

namespace Paraglider.Application.MapsterRegisters;

public class ComponentRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Component, ComponentDTO>();
    }
}
