namespace Paraglider.AspNetCore.Identity.Infrastructure.Responses.OperationResult.Abstractions;

/// <summary>
///     Interface for data object fluent Api implementation
/// </summary>
public interface IHaveDataObject
{
    void AddData(object data);
}