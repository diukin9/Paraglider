using Paraglider.Infrastructure.Responses.OperationResult;
using System.Runtime.CompilerServices;
using System.Text.Json;
using static Paraglider.Infrastructure.AppData;

namespace Paraglider.Infrastructure.Extensions
{
    public static class ValidateExtensions
    {
        public static void ValidateForNull<T1, T2>(this T1 item, ref OperationResult<T2> operation, [CallerArgumentExpression("item")] string? variableName = null)
        {
            if (item?.GetType() == typeof(string)) StringValidation(item.ToString()!, ref operation, variableName ?? "parameter");
            else ObjectValidation(item, ref operation);
        }

        public static void Validate<T1>(this T1 item, ref OperationResult operation, [CallerArgumentExpression("item")] string? variableName = null)
        {
            if (item?.GetType() == typeof(string)) StringValidation(item.ToString()!, ref operation, variableName ?? "parameter");
            else ObjectValidation(item, ref operation);
        }

        private static void ObjectValidation<T>(this T? obj, ref OperationResult operation)
        {
            if (obj == null) operation.AddError(Messages.EmptyObject(typeof(T).Name));
        }

        private static void ObjectValidation<T1, T2>(this T1? obj, ref OperationResult<T2> operation)
        {
            if (obj == null) operation.AddError(Messages.EmptyObject(typeof(T1).Name));
        }

        private static void StringValidation(string value, ref OperationResult operation, string variableName)
        {
            variableName = JsonNamingPolicy.CamelCase.ConvertName(variableName);
            if (string.IsNullOrEmpty(value)) operation.AddError(Messages.EmptyObject(variableName));
        }

        private static void StringValidation<T>(string value, ref OperationResult<T> operation, string variableName)
        {
            variableName = JsonNamingPolicy.CamelCase.ConvertName(variableName);
            if (string.IsNullOrEmpty(value)) operation.AddError(Messages.EmptyObject(variableName));
        }
    }
}
