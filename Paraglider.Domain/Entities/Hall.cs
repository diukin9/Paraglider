﻿using Paraglider.Domain.Abstractions;
using Paraglider.Domain.ValueObjects;

namespace Paraglider.Domain.Entities;

public class Hall : IIdentified
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public HallRentalPrice RentalPrice { get; set; } = null!;
    public Capacity Capacity { get; set; } = null!;

    public Guid AlbumId { get; set; }
    public virtual Album Album { get; set; } = null!;

    private decimal? minimalPrice;
    public decimal? MinimalPrice 
    { 
        get => minimalPrice; 
        set
        {
            if (value.HasValue && value.Value < 0)
            {
                throw new ArgumentException("Price cannot be negative");
            }
            minimalPrice = value;
        }
    }
}