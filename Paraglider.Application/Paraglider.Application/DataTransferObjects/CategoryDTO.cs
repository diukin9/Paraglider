using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Application.DataTransferObjects;

public record CategoryDTO : IDataTransferObject
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}
