using Mapster;
using Paraglider.Application.DataTransferObjects;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.Application.MapsterRegisters;

public class ContactsRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //TODO
        config.NewConfig<Contact, ContactDTO>();
    }
}
