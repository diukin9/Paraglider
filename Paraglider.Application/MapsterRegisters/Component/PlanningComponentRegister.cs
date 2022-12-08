using Mapster;
using Paraglider.API.DataTransferObjects;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.API.MapsterRegisters;

public class PlanningComponentRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PlanningComponent, PlanningComponentDTO>()
            .Ignore(dest => dest.Component!);
    }
}