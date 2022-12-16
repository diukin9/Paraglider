using Newtonsoft.Json;

namespace Paraglider.Infrastructure.Common.Exceptions;

public class HangfireJobException : Exception
{
    public string? JsonObject { get; set; } = null!;

    public HangfireJobException(
        object item,
        string? message = null,
        Exception? exception = null) 
        : base(message, exception)
    {
        JsonObject = JsonConvert.SerializeObject(item);
    }
}


public static class LoggingExtensions
{
    public static string ToLogFormat(this string message, string header)
    {
        return $"{header.ToUpper()} [{DateTime.Now}] | " +
            $"MESSAGE: {message.ToUpper()}";
    }

    public static string ToLogFormat(this HangfireJobException exp, string header)
    {
        return $"{header.ToUpper()} [{DateTime.Now}] " +
            $"MESSAGE: '{exp.Message.ToUpper()}', " +
            $"OBJECT: '{exp.JsonObject}'";
    }
}