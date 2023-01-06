using Mapster;
using Paraglider.Application.DataTransferObjects;
using Paraglider.Domain.NoSQL.Enums;

namespace Paraglider.Application.MapsterRegisters;

public class ComponentStatusRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ComponentStatus, ComponentStatusDTO>()
            .Map(dest => dest.Name, src => Enum.GetName(typeof(ComponentStatus), src))
            .Map(dest => dest.Value, src => (int)src);
    }
}
