using Paraglider.GorkoClient.Models.Abstractions;

namespace Paraglider.GorkoClient.Models;

public class CatalogMedia : IHaveId
{
    public long? Id { get; set; }
    public string? Type { get; set; }
    public string? OriginalUrl { get; set; }
    public string? PreviewUrl { get; set; }
    public string? OriginalSize { get; set; }
    public string? PreviewSize { get; set; }
}