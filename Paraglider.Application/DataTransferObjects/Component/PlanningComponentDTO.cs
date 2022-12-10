using Newtonsoft.Json;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects;

public class PlanningComponentDTO : IDataTransferObject
{
    [JsonIgnore]
    public string ComponentId { get; set; } = null!;
    public ComponentDTO? Component { get; set; }
    public ComponentDescDTO ComponentDesc { get; set; } = null!;
}
