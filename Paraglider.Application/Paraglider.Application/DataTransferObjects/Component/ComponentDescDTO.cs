using Paraglider.Domain.RDB.ValueObjects;
using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Application.DataTransferObjects;

public class ComponentDescDTO : IDataTransferObject
{
    public AgreementStatusDTO Status { get; set; } = null!;
    public TimeIntervalDTO TimeInterval { get; set; } = null!;
    public virtual List<PaymentDTO> Payments { get; set; } = new List<PaymentDTO>();
}
