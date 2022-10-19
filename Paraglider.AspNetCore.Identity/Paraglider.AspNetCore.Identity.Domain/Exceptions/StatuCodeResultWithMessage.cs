using Microsoft.AspNetCore.Mvc;

namespace Paraglider.AspNetCore.Identity.Domain.Exceptions
{
    public class StatuCodeResultWithMessage : StatusCodeResult
    {
        public string Message { get; set; }

        public StatuCodeResultWithMessage(int statusCode, string message) : base(statusCode)
        {
            Message = message;
        }
    }
}
