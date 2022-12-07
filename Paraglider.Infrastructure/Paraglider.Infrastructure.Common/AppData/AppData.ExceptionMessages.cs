namespace Paraglider.Infrastructure.Common;

public static partial class AppData
{
    public static class ExceptionMessages
    {
        public const string ValidationError = "Validation error";

        public static string ObjectNotFound(string name) => $"Объект '{name}' не найден";
        public static string ValueNullOrEmpty(string name) => $"Значение '{name}' было пустым или null.";
        public static string ObjectIsNull(string name) => $"Объект '{name}' был null.";
        public static string CannotBeNegative(string name) => $"Значение '{name}' не может быть отрицательным.";
        public static string CannotBeHigherThan(string first, string second) => $"'{first}' не может быть больше '{second}'.";
        public static string CannotBeHigherThan(string name, double limit) => CannotBeHigherThan(name, limit.ToString());
        public static string UpdateError(string resourceName) => $"Ошибка: не удалось обновить ресурс: {resourceName}";
    }
}
