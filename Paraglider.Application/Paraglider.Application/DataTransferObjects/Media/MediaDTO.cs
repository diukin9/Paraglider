using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Application.DataTransferObjects;

public class MediaDTO : IDataTransferObject
{
    public MediaTypeDTO Type { get; set; } = null!;

    public string Url { get; set; } = null!;
}
