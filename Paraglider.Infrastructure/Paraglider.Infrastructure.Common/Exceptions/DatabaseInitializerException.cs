namespace Paraglider.Infrastructure.Common.Exceptions;

public class DatabaseInitializerException : Exception 
{
    private const string MESSAGE = "Error during database initialization execution";
    public DatabaseInitializerException(string message = MESSAGE) : base(message) { }
}
