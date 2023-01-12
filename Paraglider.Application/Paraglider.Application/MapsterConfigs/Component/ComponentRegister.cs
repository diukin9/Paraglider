using Mapster;
using Paraglider.Application.DataTransferObjects;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.Application.MapsterRegisters;

public class ComponentRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Component, ComponentDTO>()
            .Map(x => x.Services, y => y.Services != null && y.Services.Any() ? y.Services : null);
    }
}
