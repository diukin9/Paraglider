using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects;

public class MediaTypeDTO : IDataTransferObject
{
    public string Name { get; set; } = null!;
    public int Value { get; set; }
}
