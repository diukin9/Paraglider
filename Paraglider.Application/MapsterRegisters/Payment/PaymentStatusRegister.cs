using Mapster;
using Paraglider.API.DataTransferObjects;
using Paraglider.Domain.RDB.Enums;

namespace Paraglider.API.MapsterRegisters;

public class PaymentStatusRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PaymentStatus, PaymentStatusDTO>()
            .Map(dest => dest.Name, src => Enum.GetName(typeof(PaymentStatus), src))
            .Map(dest => dest.Value, src => (int) src);
    }
}