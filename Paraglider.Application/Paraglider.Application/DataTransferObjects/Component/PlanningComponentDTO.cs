using Paraglider.Infrastructure.Common.Interfaces;
using System.Text.Json.Serialization;

namespace Paraglider.Application.DataTransferObjects;

public class PlanningComponentDTO : IDataTransferObject
{
    [JsonPropertyName("component")]
    public ComponentDTO? Component { get; set; }

    [JsonPropertyName("component_desc")]
    public ComponentDescDTO ComponentDesc { get; set; } = null!;
}
