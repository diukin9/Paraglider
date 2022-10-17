namespace Paraglider.AspNetCore.Identity.Domain.Exceptions
{
    public class AuthorizeException : Exception
    {
        public AuthorizeException(string message) : base(message) { }
    }
}
