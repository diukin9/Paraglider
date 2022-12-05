using Mapster;
using Paraglider.API.DataTransferObjects;
using Paraglider.API.Features.Users.Commands;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.API.MapsterRegisters;

public class UserRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ApplicationUser, UserDTO>();
        config.NewConfig<ApplicationUser, UserPatchDTO>();
        config.NewConfig<UserPatchDTO, ChangeUserCityCommand>();
    }
}