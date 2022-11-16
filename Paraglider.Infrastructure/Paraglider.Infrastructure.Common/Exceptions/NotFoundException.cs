namespace Paraglider.Infrastructure.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(Type type) : base($"The object of type: '{type.Name}' was not found in the system") { }
}
