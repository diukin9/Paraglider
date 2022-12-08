using Mapster;
using Paraglider.API.DataTransferObjects;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.API.MapsterRegisters;

public class PaymentRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Payment, PaymentDTO>();
    }
}