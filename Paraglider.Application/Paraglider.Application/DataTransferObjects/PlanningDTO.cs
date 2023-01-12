using Paraglider.Infrastructure.Common.Interfaces;
using System.Text.Json.Serialization;

namespace Paraglider.Application.DataTransferObjects;

public class PlanningDTO : IDataTransferObject
{
    [JsonPropertyName("wedding_date")]
    public DateOnly? WeddingDate { get; set; }

    [JsonPropertyName("categories")]
    public List<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();

    [JsonPropertyName("components")]
    public List<PlanningComponentDTO> Components { get; set; } = new List<PlanningComponentDTO>();
}