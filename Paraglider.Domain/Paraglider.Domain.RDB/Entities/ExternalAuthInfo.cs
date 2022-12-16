using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paraglider.Domain.RDB.Entities;

[Table("AspNetExternalAuthInfo")]
public class ExternalAuthInfo : IIdentified<Guid>
{
    public Guid Id { get; set; }
    public string ExternalId { get; set; } = null!;
    public AuthProvider ExternalProvider { get; set; }

    public Guid UserId { get; set; }
    public virtual ApplicationUser User { get; set; } = null!;
}
