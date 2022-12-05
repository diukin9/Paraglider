using Mapster;
using Paraglider.API.DataTransferObjects;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.API.MapsterRegisters;

public class PlanningRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Planning, PlanningDTO>()
            .Map(x => x.Components, src => src.PlanningComponents);
    }
}