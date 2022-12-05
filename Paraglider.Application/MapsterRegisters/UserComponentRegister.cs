using Mapster;
using Paraglider.API.DataTransferObjects;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.API.MapsterRegisters;

public class UserComponentRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserComponent, UserComponentDTO>()
            .Ignore(dest => dest.Component!);
    }
}