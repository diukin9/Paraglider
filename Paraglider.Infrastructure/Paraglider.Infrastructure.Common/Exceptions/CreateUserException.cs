namespace Paraglider.Infrastructure.Exceptions
{
    public class CreateUserException : Exception
    {
        public CreateUserException(string message = "Error when creating a user") : base(message) { }
    }
}
