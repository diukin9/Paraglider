using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Application.DataTransferObjects;

public class MediaDTO : IDataTransferObject
{
    public MediaTypeDTO Type { get; set; } = null!;

    public string Url { get; set; } = null!;
}
