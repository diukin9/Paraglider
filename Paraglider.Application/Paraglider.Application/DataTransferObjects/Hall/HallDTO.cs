using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Application.DataTransferObjects;

public class HallDTO : IDataTransferObject
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public HallRentalPriceDTO RentalPrice { get; set; } = null!;

    public CapacityDTO Capacity { get; set; } = null!;

    public AlbumDTO Album { get; set; } = null!;

    public decimal? MinimalPrice { get; set; }
}
