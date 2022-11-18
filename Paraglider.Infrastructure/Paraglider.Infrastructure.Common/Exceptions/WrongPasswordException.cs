namespace Paraglider.Infrastructure.Exceptions;

public class WrongPasswordException : Exception
{
    public WrongPasswordException(string message = "The entered password does not match the actual password") : base(message) { }
}
