using Mapster;
using Paraglider.API.DataTransferObjects;
using Paraglider.Domain.NoSQL.Entities;

namespace Paraglider.API.MapsterRegisters;

public class ComponentRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Component, ComponentDTO>();
    }
}
