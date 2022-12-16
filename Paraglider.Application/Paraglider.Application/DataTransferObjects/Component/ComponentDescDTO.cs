using Paraglider.Domain.RDB.ValueObjects;
using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Application.DataTransferObjects;

public class ComponentDescDTO : IDataTransferObject
{
    public ComponentStatusDTO Status { get; set; } = null!;
    public TimeInterval TimeInterval { get; set; } = null!;
    public virtual List<PaymentDTO> Payments { get; set; } = new List<PaymentDTO>();
}
