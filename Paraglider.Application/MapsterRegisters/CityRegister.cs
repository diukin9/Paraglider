using Mapster;
using Paraglider.API.DataTransferObjects;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.API.MapsterRegisters;

public class CityRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<City, CityDTO>()
            .RequireDestinationMemberSource(true);
    }
}