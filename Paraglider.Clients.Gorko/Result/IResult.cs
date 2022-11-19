using System.Net;

namespace Paraglider.GorkoClient.Result;

internal interface IResult
{
    public HttpStatusCode? StatusCode { get; }
    
    public string? ErrorMessage { get; }
    
    public Exception? Exception { get; }
    
    public bool IsSuccessful { get; }
}

internal interface IResult<T> : IResult
{
    public T? Value { get; }
    
    public bool HasValue => Value is not null;
}