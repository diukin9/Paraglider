using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Application.DataTransferObjects;

public class AlbumDTO : IDataTransferObject
{
    public List<MediaDTO> Media { get; set; } = new List<MediaDTO>();
}
