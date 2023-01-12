using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paraglider.Domain.RDB.Entities;

public class ExtlAuthInfo : IIdentified
{
    public Guid Id { get; set; }

    public string ExternalId { get; set; } = null!;

    public AuthProvider Provider { get; set; }

    public Guid UserId { get; set; }

    public virtual ApplicationUser User { get; set; } = null!;
}
