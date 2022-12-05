using Mapster;
using Paraglider.API.DataTransferObjects;
using Paraglider.Domain.RDB.Enums;

namespace Paraglider.API.MapsterRegisters;

public class ComponentStatusRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ComponentStatus, ComponentStatusDTO>()
            .Map(dest => dest.Name, src => Enum.GetName(typeof(ComponentStatus), src))
            .Map(dest => dest.Value, src => (int) src);
    }
}