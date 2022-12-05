namespace Paraglider.Infrastructure.Common.Extensions;

public static class OperationResultExtensions
{
    public static string? GetMessage<T>(this OperationResult<T> source) => source?.Metadata?.Message;

    public static T? GetDataObject<T>(this OperationResult<T> source) => source?.Metadata is not null ? source!.Metadata!.DataObject : default;
}