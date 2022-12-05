﻿using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects;

public record CityDTO : IDataTransferObject
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
}