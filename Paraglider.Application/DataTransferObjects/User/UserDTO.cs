using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects;

public record UserDTO : IDataTransferObject
{
    public Guid Id { get; init; }
    public string FirstName { get; init; } = null!;
    public string Surname { get; init; } = null!;
    public string UserName { get; init; } = null!;
    public string? Email { get; init; }
    public CityDTO City { get; init; } = null!;
    public PlanningDTO Planning { get; init; } = null!;
    public List<UserComponentDTO> Favourites { get; set; } = null!;
}