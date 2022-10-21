using Microsoft.AspNetCore.Mvc;

namespace Paraglider.AspNetCore.Identity.Infrastructure.Responses
{
    /// <summary>
    /// Http status code result with message
    /// </summary>
    public class StatusCodeResultWithMessage : StatusCodeResult
    {
        /// <summary>
        /// Forwarded message 
        /// </summary>
        public string Message { get; set; }

        public StatusCodeResultWithMessage(int statusCode, string message) : base(statusCode)
        {
            Message = message;
        }
    }
}
