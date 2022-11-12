using Microsoft.AspNetCore.Identity;
using Paraglider.AspNetCore.Identity.Infrastructure.Responses.OperationResult;
namespace Paraglider.AspNetCore.Identity.Infrastructure.Extensions;

/// <summary>
///     OperationResult extension
/// </summary>
public static class OperationResultExtensions
{
    /// <summary>
    ///     Create or Replace special type of metadata
    /// </summary>
    /// <param name="source"></param>
    /// <param name="message"></param>
    public static OperationResult AddInfo(this OperationResult source, string message)
    {
        source.Metadata = new Metadata(source, message);
        return source;
    }

    /// <summary>
    ///     Create or Replace special type of metadata
    /// </summary>
    /// <param name="source"></param>
    /// <param name="message"></param>
    public static OperationResult AddSuccess(this OperationResult source, string message)
    {
        source.Metadata = new Metadata(source, message, MetadataType.Success);
        return source;
    }

    public static OperationResult<T> AddSuccess<T>(this OperationResult<T> source, string message)
    {
        source.Metadata = new Metadata(source, message, MetadataType.Success);
        return source;
    }

    /// <summary>
    ///     Create or Replace special type of metadata
    /// </summary>
    /// <param name="source"></param>
    /// <param name="message"></param>
    public static OperationResult AddWarning(this OperationResult source, string message)
    {
        source.Metadata = new Metadata(source, message, MetadataType.Warning);
        return source;
    }

    public static OperationResult<T> AddWarning<T>(this OperationResult<T> source, string message)
    {
        source.Metadata = new Metadata(source, message, MetadataType.Warning);
        return source;
    }

    /// <summary>
    ///     Create or Replace special type of metadata
    /// </summary>
    /// <param name="source"></param>
    /// <param name="message"></param>
    public static OperationResult AddError(this OperationResult source, string message)
    {
        source.Metadata = new Metadata(source, message, MetadataType.Error);
        return source;
    }

    public static OperationResult<T> AddError<T>(this OperationResult<T> source, string message)
    {
        source.Metadata = new Metadata(source, message, MetadataType.Error);
        return source;
    }

    /// <summary>
    ///     Create or Replace special type of metadata
    /// </summary>
    /// <param name="source"></param>
    /// <param name="exception"></param>
    public static OperationResult AddError(this OperationResult source, Exception exception)
    {
        source.Exception = exception;
        source.Metadata = new Metadata(source, exception?.Message!, MetadataType.Error);
        return source;
    }

    public static OperationResult<T> AddError<T>(this OperationResult<T> source, Exception exception)
    {
        source.Exception = exception;
        source.Metadata = new Metadata(source, exception?.Message!, MetadataType.Error);
        return source;
    }

    /// <summary>
    ///     Create or Replace special type of metadata
    /// </summary>
    /// <param name="source"></param>
    /// <param name="message"></param>
    /// <param name="exception"></param>
    public static OperationResult AddError(this OperationResult source, string message, Exception exception)
    {
        source.Exception = exception;
        source.Metadata = new Metadata(source, message, MetadataType.Error);
        return source;
    }

    public static OperationResult<T> AddError<T>(this OperationResult<T> source, string message, Exception exception)
    {
        source.Exception = exception;
        source.Metadata = new Metadata(source, message, MetadataType.Error);
        return source;
    }

    /// <summary>
    ///     Gather information from result metadata
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string? GetMessages(this OperationResult source) => source?.Metadata?.Message;

    public static OperationResult RescheduleResult(this OperationResult minor, ref OperationResult main)
    {
        main.Metadata = minor.Metadata;
        main.Exception = minor.Exception;
        return main;
    }

    public static OperationResult RescheduleResult<T>(this OperationResult<T> minor, ref OperationResult main)
    {
        main.Metadata = minor.Metadata;
        main.Exception = minor.Exception;
        return main;
    }

    public static OperationResult<T> RescheduleResult<T>(this OperationResult minor, ref OperationResult<T> main)
    {
        main.Metadata = minor.Metadata;
        main.Exception = minor.Exception;
        return main;
    }

    public static OperationResult<T> RescheduleResult<T>(this OperationResult<T> minor, ref OperationResult<T> main)
    {
        main.Metadata = minor.Metadata;
        main.Exception = minor.Exception;
        main.Result = minor.Result;
        return main;
    }

    public static OperationResult<T> AddResult<T>(this OperationResult<T> operation, T value)
    {
        operation.Result = value;
        return operation;
    }
}