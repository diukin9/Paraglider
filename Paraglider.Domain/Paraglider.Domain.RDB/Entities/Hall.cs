using Paraglider.Domain.RDB.ValueObjects;
using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Domain.RDB.Entities;

public class Hall : IIdentified
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public HallRentalPrice RentalPrice { get; set; } = null!;

    public Capacity Capacity { get; set; } = null!;

    public Album Album { get; set; } = null!;

    public decimal? MinimalPrice { get; set; }
}
