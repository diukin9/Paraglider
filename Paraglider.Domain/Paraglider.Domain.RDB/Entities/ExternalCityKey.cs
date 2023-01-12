using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paraglider.Domain.RDB.Entities;

public class ExternalCityKey : IIdentified
{
    public Guid Id { get; set; }

    public Source Source { get; set; }

    public string Key { get; set; } = null!;
}
