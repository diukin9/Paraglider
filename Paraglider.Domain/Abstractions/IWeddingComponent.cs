﻿using Paraglider.Domain.Entities;
using Paraglider.Domain.ValueObjects;

namespace Paraglider.Domain.Abstractions;

public interface IWeddingComponent : IIdentified
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? AvatarUrl { get; set; }

    public Contacts Contacts { get; set; }

    public Guid CityId { get; set; }
    public City City { get; set; }

    public Guid AlbumId { get; set; }
    public Album Album { get; set; }

    public Guid ExternalInfoId { get; set; }
    public ExternalInfo ExternalInfo { get; set; }

    public List<Review> Reviews { get; set; }
}