namespace Paraglider.AspNetCore.Identity.Infrastructure.Exceptions
{
    public class AuthorizeException : Exception
    {
        public AuthorizeException(string message) : base(message) { }
    }
}
