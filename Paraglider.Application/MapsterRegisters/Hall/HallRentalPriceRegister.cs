using Mapster;
using Paraglider.API.DataTransferObjects;
using Paraglider.Domain.NoSQL.ValueObjects;

namespace Paraglider.API.MapsterRegisters;

public class HallRentalPriceRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<HallRentalPrice, HallRentalPriceDTO>();
    }
}
