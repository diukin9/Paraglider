using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Application.DataTransferObjects;

public class CategoryDTO : IDataTransferObject
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}
