using Newtonsoft.Json;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects;

public class PlanningComponentDTO : IDataTransferObject
{
    [JsonIgnore]
    public Guid ComponentId { get; set; }
    public ComponentDTO? Component { get; set; }
    public ComponentDescDTO ComponentDesc { get; set; } = null!;
}
