using Mapster;
using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects
{
    public class ComponentStatusDTO : IDataTransferObject
    {
        public string Name { get; set; } = null!;
        public int Value { get; set; }
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ComponentStatus, ComponentStatusDTO>()
                .Map(dest => dest.Name, src => Enum.GetName(typeof(ComponentStatus), src))
                .Map(dest => dest.Value, src => (int)src)
                .RequireDestinationMemberSource(true);
        }
    }
}
