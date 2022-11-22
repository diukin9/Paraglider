using Mapster;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects;

public record UserDTO : IDataTransferObject
{
    public Guid Id { get; init; }
    public string Firstname { get; init; } = null!;
    public string Surname { get; init; } = null!;
    public string Username { get; init; } = null!;
    public string? Email { get; init; }
    public CityDTO City { get; init; } = null!;
    public WeddingPlanningDTO WeddingPlanning { get; init; } = null!;

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ApplicationUser, UserDTO>()
            .Ignore(dest => dest.WeddingPlanning)
            .RequireDestinationMemberSource(true);
    }
}