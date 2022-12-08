using Mapster;
using Paraglider.API.DataTransferObjects;
using Paraglider.Domain.NoSQL.ValueObjects;

namespace Paraglider.API.MapsterRegisters
{
    public class ServiceRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Service, ServiceDTO>();
        }
    }
}
