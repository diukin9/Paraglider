using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects;

public record PlanningDTO : IDataTransferObject
{
    public DateOnly? WeddingDate { get; set; }
    public List<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();
    public List<PlanningComponentDTO> Components { get; set; } = new List<PlanningComponentDTO>();
}