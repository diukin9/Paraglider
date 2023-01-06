using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Application.DataTransferObjects;

public class UserDTO : IDataTransferObject
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