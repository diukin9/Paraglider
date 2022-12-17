using Newtonsoft.Json;
using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Application.DataTransferObjects;

public class PlanningComponentDTO : IDataTransferObject
{
    [JsonIgnore]
    public string ComponentId { get; set; } = null!;
    public ComponentDTO? Component { get; set; }
    public ComponentDescDTO ComponentDesc { get; set; } = null!;
}
