using Mapster;
using Paraglider.Application.DataTransferObjects;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.Application.MapsterRegisters;

public class PlanningRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Planning, PlanningDTO>()
            .Map(x => x.Components, src => src.PlanningComponents);
    }
}