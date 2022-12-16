using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Application.DataTransferObjects;

public class ReviewDTO : IDataTransferObject
{
    public string Author { get; set; } = null!;
    public string AvatarUrl { get; set; } = null!;
    public string? Text { get; set; }
    public double Evaluation { get; set; }
}
