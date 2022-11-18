namespace Paraglider.Infrastructure.Exceptions
{
    public class ExternalAuthException : Exception
    {
        public ExternalAuthException(string message = "Error when trying to log in through an external provider") : base(message) { } 
    }
}
