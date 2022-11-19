using Paraglider.GorkoClient.Models.Abstractions;

namespace Paraglider.GorkoClient.Models;

public class City : IHaveId
{
    public long? Id { get; set; }
    
    public string? Name { get; set; }
    
}