using Paraglider.Clients.Gorko.Models.Abstractions;

namespace Paraglider.Clients.Gorko.Models;

public class City : IHaveId
{
    public long? Id { get; set; }
    
    public string? Name { get; set; }
    
}