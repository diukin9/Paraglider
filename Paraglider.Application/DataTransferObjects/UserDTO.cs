using Mapster;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects
{
    public class UserDTO : IDataTransferObject
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string? Email { get; set; }
        public CityDTO City { get; set; } = null!;
        public List<WeddingPlanDTO> WeddingPlans { get; set; } = null!;

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ApplicationUser, UserDTO>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Username, src => src.UserName)
                .Map(dest => dest.Firstname, src => src.FirstName)
                .Map(dest => dest.Surname, src => src.Surname)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.City, src => src.City)
                .Map(dest => dest.WeddingPlans, src => src.WeddingPlannings)
                .RequireDestinationMemberSource(true);
        }
    }
}
