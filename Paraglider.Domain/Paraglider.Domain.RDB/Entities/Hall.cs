using Paraglider.Domain.RDB.ValueObjects;
using Paraglider.Infrastructure.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paraglider.Domain.RDB.Entities;

public class Hall : IIdentified
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public HallPrice Price { get; set; } = null!;

    public Capacity Capacity { get; set; } = null!;

    public Guid? AlbumId { get; set; }

    public virtual Album? Album { get; set; } = null!;

    public decimal? MinimalPrice { get; set; }
}
