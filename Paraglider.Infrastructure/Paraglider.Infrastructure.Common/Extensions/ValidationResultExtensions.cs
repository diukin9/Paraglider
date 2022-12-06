using System.ComponentModel.DataAnnotations;

namespace Paraglider.Infrastructure.Common.Extensions;

public static class ValidationResultExtensions
{
    public static bool IsValid(this ValidationResult result)
    {
        return string.IsNullOrEmpty(result.ErrorMessage); 
    }
}
