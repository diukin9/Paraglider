using Mapster;
using Paraglider.Domain.Common.ValueObjects;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects;

public class ComponentDescDTO : IDataTransferObject
{
    public ComponentStatusDTO Status { get; set; } = null!;
    public TimeInterval TimeInterval { get; set; } = null!;
    public virtual List<PaymentDTO> Payments { get; set; } = new List<PaymentDTO>();

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ComponentDesc, ComponentDescDTO>()
            .RequireDestinationMemberSource(true);
    }
}
