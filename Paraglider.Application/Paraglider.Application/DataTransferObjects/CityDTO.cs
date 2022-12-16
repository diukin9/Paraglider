using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Application.DataTransferObjects;

public record CityDTO : IDataTransferObject
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
}