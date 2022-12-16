using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Application.DataTransferObjects;

public class ServiceDTO : IDataTransferObject
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public PriceDTO Price { get; set; } = null!;
}
