using Mapster;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects
{
    public class PaymentDTO : IDataTransferObject
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public PaymentStatusDTO Status { get; set; }
        public decimal? Sum { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Payment, PaymentDTO>();
        }
    }
}
