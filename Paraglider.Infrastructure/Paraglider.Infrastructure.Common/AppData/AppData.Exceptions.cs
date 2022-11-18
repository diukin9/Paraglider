namespace Paraglider.Infrastructure
{
    public static partial class AppData
    {
        public static class Exceptions
        {
            public const string NotEnoughUserInfoFromExternalProvider = "Couldn't get basic user information from an external provider.";
            public const string WrongPasswordEntered = "Wrong password entered.";
            public const string FailedExternalAuth = $"Error when attempting external authorisation.";

            public static string NullOrEmptyField(string fieldName) => $"The '{fieldName}' was empty.";
            public static string ObjectIsNull(Type type) => $"'{type.Name}' was null.";
        }
    }
}
