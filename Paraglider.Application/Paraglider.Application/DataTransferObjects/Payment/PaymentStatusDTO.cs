using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Application.DataTransferObjects;

public class PaymentStatusDTO : IDataTransferObject
{
    public string Name { get; set; } = null!;
    public int Value { get; set; }
}
