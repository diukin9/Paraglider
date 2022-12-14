using Paraglider.Domain.RDB.Enums;
using Paraglider.Domain.RDB.ValueObjects;
using Paraglider.Infrastructure.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paraglider.Domain.RDB.Entities;

public class ComponentDesc : IIdentified
{
    public Guid Id { get; set; }

    public AgreementStatus Status { get; set; } = AgreementStatus.PreSelection;

    public TimeInterval? TimeInterval { get; set; }

    public Guid PlanningComponentId { get; set; }

    public virtual PlanningComponent PlanningComponent { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
