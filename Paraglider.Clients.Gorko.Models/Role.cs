using Paraglider.GorkoClient.Models.Abstractions;

namespace Paraglider.GorkoClient.Models;

public class Role : IHaveId
{
    public long? Id { get; set; }
    public string? Key { get; set; }
    public string? Name { get; set; }
}