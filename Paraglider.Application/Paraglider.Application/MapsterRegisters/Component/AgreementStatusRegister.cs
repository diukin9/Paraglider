using Mapster;
using Paraglider.Application.DataTransferObjects;
using Paraglider.Domain.RDB.Enums;

namespace Paraglider.Application.MapsterRegisters;

public class AgreementStatusRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AgreementStatus, AgreementStatusDTO>()
            .Map(dest => dest.Name, src => Enum.GetName(typeof(AgreementStatus), src))
            .Map(dest => dest.Value, src => (int)src);
    }
}