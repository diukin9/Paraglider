using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Application.DataTransferObjects;

public class ComponentStatusDTO : IDataTransferObject
{
    public string Name { get; set; } = null!;
    public int Value { get; set; }
}
