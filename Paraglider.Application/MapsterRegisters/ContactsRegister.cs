using Mapster;
using Paraglider.API.DataTransferObjects;
using Paraglider.Domain.NoSQL.ValueObjects;

namespace Paraglider.API.MapsterRegisters;

public class ContactsRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Contacts, ContactsDTO>();
    }
}
