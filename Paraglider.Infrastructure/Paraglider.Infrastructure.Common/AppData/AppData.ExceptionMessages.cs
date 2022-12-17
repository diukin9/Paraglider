namespace Paraglider.Infrastructure.Common;

public static partial class AppData
{
    public static class ExceptionMessages
    {
        public const string ValidationError = "Validation error";

        public static string ObjectNotFound(string name) => $"Объект '{name}' не найден";
        public static string ValueNullOrEmpty(string name) => $"Значение '{name}' было пустым или null.";
        public static string ObjectIsNull(string name) => $"Объект '{name}' был null.";
        public static string UpdateError(string resourceName) => $"Ошибка: не удалось обновить ресурс: {resourceName}";
    }
}
