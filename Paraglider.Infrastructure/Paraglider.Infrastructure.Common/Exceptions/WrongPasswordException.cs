namespace Paraglider.Infrastructure.Exceptions;

public class WrongPasswordException : Exception
{
    public WrongPasswordException() : base("The entered password does not match the actual password") { }
}
