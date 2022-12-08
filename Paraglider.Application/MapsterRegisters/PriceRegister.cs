using Mapster;
using Paraglider.API.DataTransferObjects;
using Paraglider.Domain.NoSQL.ValueObjects;

namespace Paraglider.API.MapsterRegisters;

public class PriceRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Price, PriceDTO>();
    }
}
