using Mapster;
using Paraglider.Application.DataTransferObjects;
using Paraglider.Application.Features.Users.Commands;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.Application.MapsterRegisters;

public class UserRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ApplicationUser, UserDTO>();
        config.NewConfig<ApplicationUser, UserPatchDTO>();
        config.NewConfig<UserPatchDTO, ChangeUserCityRequest>();
    }
}