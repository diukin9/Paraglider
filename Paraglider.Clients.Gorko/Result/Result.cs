using System.Net;

namespace Paraglider.Clients.Gorko.Result;

public class Result : IResult
{
    protected Result(HttpStatusCode? statusCode = null,
        string? errorMessage = null,
        Exception? exception = null)
    {
        StatusCode = statusCode;
        ErrorMessage = errorMessage;
        Exception = exception;
    }
    
    public HttpStatusCode? StatusCode { get; }
    public string? ErrorMessage { get; }
    public Exception? Exception { get; }
    
    public bool IsSuccessful => StatusCode != null
                                && ((int)StatusCode >= 200) 
                                && ((int)StatusCode <= 299);

    public static Result Ok() => new Result(HttpStatusCode.OK);

    public static Result Error(HttpStatusCode? statusCode,
        string? errorMessage = null,
        Exception? exception = null) 
        => new Result(statusCode, errorMessage, exception);
}

public class Result<T> : Result
{
    protected Result(T? value,
        HttpStatusCode? statusCode,
        string? errorMessage = null,
        Exception? exception = null) : base(statusCode, errorMessage, exception)
    {
        Value = value;
    }
    public T? Value { get; }

    public static Result<T> Ok(T? value) => 
        new(value, HttpStatusCode.OK);

    public new static Result<T> Error(HttpStatusCode? statusCode,
        string? errorMessage = null,
        Exception? exception = null) =>
        new Result<T>(default, statusCode, errorMessage, exception);
}