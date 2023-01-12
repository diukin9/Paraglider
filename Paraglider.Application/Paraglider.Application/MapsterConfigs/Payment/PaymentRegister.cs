using Mapster;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Application.DataTransferObjects;

namespace Paraglider.Application.MapsterRegisters;

public class PaymentRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Payment, PaymentDTO>();
    }
}