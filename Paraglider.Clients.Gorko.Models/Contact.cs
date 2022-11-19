using Paraglider.GorkoClient.Models.Abstractions;

namespace Paraglider.GorkoClient.Models;

public class Contact : IHaveId
{
    public long? Id { get; set; }
    public string? Key { get; set; }
    public string? Name { get; set; }
    public string? Value { get; set; }
}