using Mapster;
using Paraglider.Application.DataTransferObjects;
using Paraglider.Domain.NoSQL.ValueObjects;

namespace Paraglider.Application.MapsterRegisters
{
    public class ServiceRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Service, ServiceDTO>();
        }
    }
}
