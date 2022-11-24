using Mapster;
using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects
{
    public class PaymentStatusDTO : IDataTransferObject
    {
        public string Name { get; set; } = null!;
        public int Value { get; set; }
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PaymentStatus, PaymentStatusDTO>()
                .Map(dest => dest.Name, src => Enum.GetName(typeof(PaymentStatus), src))
                .Map(dest => dest.Value, src => (int)src)
                .RequireDestinationMemberSource(true);
        }
    }
}
