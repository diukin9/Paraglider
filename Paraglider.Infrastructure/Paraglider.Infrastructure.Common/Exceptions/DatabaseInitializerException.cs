namespace Paraglider.Infrastructure.Exceptions;

public class DatabaseInitializerException : Exception 
{
    public DatabaseInitializerException(string message) : base(message) { }
}
