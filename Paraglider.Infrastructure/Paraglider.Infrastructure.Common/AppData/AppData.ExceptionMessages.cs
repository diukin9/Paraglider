namespace Paraglider.Infrastructure.Common;

public static partial class AppData
{
    public static class ExceptionMessages
    {
        public const string NotEnoughUserInfoFromExternalProvider = "Couldn't get basic user information from an external provider.";
        public const string WrongPasswordEntered = "Wrong password entered.";
        public const string FailedExternalAuth = "Error when attempting external authorisation.";
        public const string ValidationError = "Validation error";

        public const string UnconfirmedEmail = "Uncomfirmed email was passed";
        
        public static string ObjectNotFound(string name) => $"The '{name}' not foud";
        public static string NullOrEmpty(string name) => $"The '{name}' was null or empty.";
        public static string ObjectIsNull(string name) => $"The '{name}' was null.";
        public static string CannotBeNegative(string name) => $"The '{name}' cannot be negative.";
        public static string CannotBeHigherThan(string first, string second) => $"The '{first}' cannot be higher than {second}.";
        public static string CannotBeHigherThan(string name, double limit) => CannotBeHigherThan(name, limit.ToString());

        public static string UserWithEmailAlreadyExist(string email)
        {
            return $"User with {email} already exist";
        }
    }
}
