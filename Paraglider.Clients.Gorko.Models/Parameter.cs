namespace Paraglider.GorkoClient.Models;

public class Parameter<T>
{
    public string? Name { get; set; }
    public T? Value { get; set; }
}