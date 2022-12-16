using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Application.DataTransferObjects;

public class MediaTypeDTO : IDataTransferObject
{
    public string Name { get; set; } = null!;
    public int Value { get; set; }
}
