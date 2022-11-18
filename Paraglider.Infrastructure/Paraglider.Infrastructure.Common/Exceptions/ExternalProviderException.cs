namespace Paraglider.Infrastructure.Exceptions
{
    public class ExternalProviderException : Exception
    {
        public ExternalProviderException(string message = "Exception during request processing by external provider") : base(message) { }
    }
}
