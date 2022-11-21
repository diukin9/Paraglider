using Mapster;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects
{
    public record UserDTO : IDataTransferObject
    {
        public Guid Id { get; init; }
        public string Firstname { get; init; } = null!;
        public string Surname { get; init; } = null!;
        public string Username { get; init; } = null!;
        public string? Email { get; init; }
        public CityDTO City { get; init; } = null!;
        public List<WeddingPlanDTO> WeddingPlans { get; init; } = null!;

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ApplicationUser, UserDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Username, src => src.UserName)
            .Map(dest => dest.Firstname, src => src.FirstName)
            .Map(dest => dest.Surname, src => src.Surname)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.City, src => src.City)
            .Ignore(dest => dest.WeddingPlanning)
            .RequireDestinationMemberSource(true);
    }
}
