using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paraglider.Domain.RDB.Entities;

public class Media : IIdentified
{
    public Guid Id { get; set; }

    public MediaType Type { get; set; }

    public string Url { get; set; } = null!;

    public Guid AlbumId { get; set; }
}