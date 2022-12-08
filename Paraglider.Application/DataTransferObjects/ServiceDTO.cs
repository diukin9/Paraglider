using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects;

public class ServiceDTO : IDataTransferObject
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public PriceDTO Price { get; set; } = null!;
}
