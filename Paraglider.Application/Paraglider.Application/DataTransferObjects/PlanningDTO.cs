using Paraglider.Application.DataTransferObjects;
using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Application.DataTransferObjects;

public class PlanningDTO : IDataTransferObject
{
    public DateOnly? WeddingDate { get; set; }
    public List<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();
    public List<PlanningComponentDTO> Components { get; set; } = new List<PlanningComponentDTO>();
}