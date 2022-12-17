using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Application.DataTransferObjects;

public record UserPatchDTO : IDataTransferObject
{
    public string? FirstName { get; init; }
    public string? Surname { get; init; }
    public string? Email { get; init; }
    public Guid? CityId { get; init; }
}