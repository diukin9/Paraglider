using FluentValidation.Results;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Paraglider.Application.BackgroundJobs.ReccuringJobs.Abstractions;
using Paraglider.Application.BackgroundJobs.ReccuringJobs.Gorko.Validators;
using Paraglider.Clients.Gorko;
using Paraglider.Clients.Gorko.Models.Abstractions;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.RDB.Enums;
using System.Data;
using GorkoEnums = Paraglider.Clients.Gorko.Models.Enums;
using GorkoModels = Paraglider.Clients.Gorko.Models;

namespace Paraglider.Application.BackgroundJobs.ReccuringJobs.Gorko;

public class ImportComponentsFromGorkoRecurringJob 
    : BaseReccuringJob<ImportComponentsFromGorkoRecurringJob>
{
    private const int PER_PAGE = 20;

    private readonly ICategoryRepository _categoryRepository;
    private readonly ICityRepository _cityRepository;
    private readonly IComponentRepository _componentRepository;
    private readonly GorkoClient _gorkoClient;
    private readonly IMapper _mapper;

    private readonly UserValidator _userValidator = new();
    private readonly RestaurantValidator _restaurantValidator = new();

    public ImportComponentsFromGorkoRecurringJob(
        GorkoClient gorkoClient,
        IComponentRepository componentRepository,
        ICityRepository cityRepository,
        ICategoryRepository categoryRepository,
        IMapper mapper)
    {
        _gorkoClient = gorkoClient;
        _componentRepository = componentRepository;
        _categoryRepository = categoryRepository;
        _cityRepository = cityRepository;
        _mapper = mapper;
    }

    public override async Task RunAsync()
    {
        var start = DateTime.Now;

        ClearLogs();

        LogTaskStart(start);

        await foreach (var city in GetCitiesAsync())
        {
            await foreach (var component in GetComponentsAsync(city.Id!.Value))
            {
                await SelectAndExecuteAction(component);
            }
        }

        await ArchiveExpiredComponents(start); 

        LogTaskFinish(start, DateTime.Now);
    }

    #region private methods

    private async IAsyncEnumerable<Component> GetComponentsAsync(long cityId) 
    {
        await foreach (var restaurant in GetRestaurantsAsync(cityId))
        {
            yield return restaurant;
        }

        foreach (var role in Enum.GetValues(typeof(GorkoEnums.UserRole)))
        {
            await foreach (var user in GetUsersAsync((GorkoEnums.UserRole)role, cityId))
            {
                yield return user;
            }
        }
    }

    private async IAsyncEnumerable<Component> GetRestaurantsAsync(long cityId, int perPage = PER_PAGE, int page = 1)
    {
        if (page < 1 || perPage < 1) throw new ArgumentException();

        var pagedResult = await _gorkoClient.GetRestaurantsAsync(perPage, page, cityId);

        if (pagedResult is null) LogInvalidResponse(nameof(GorkoModels.Restaurant), page, perPage);

        if (!IsDifferentHash(pagedResult!)) yield break;

        foreach (var item in pagedResult?.Items!)
        {
            var component = await BuildComponentAsync(item, item?.City?.Id?.ToString(), item?.Type?.Key);
            if (component is null) continue;

            var validationResult = await _restaurantValidator.ValidateAsync(component);
            if (validationResult.IsValid) yield return component;
            else LogValidationError(validationResult, component);
        }

        if (pagedResult?.Meta?.PagesCount > page)
        {
            await foreach (var item in GetRestaurantsAsync(cityId, perPage, page + 1)) yield return item;
        }
    }

    private async IAsyncEnumerable<Component> GetUsersAsync(GorkoEnums.UserRole role, long cityId, int perPage = PER_PAGE, int page = 1)
    {
        if (page < 1 || perPage < 1) throw new ArgumentException();

        var pagedResult = await _gorkoClient.GetUsersAsync(role, perPage, page, cityId);

        if (pagedResult is null) LogInvalidResponse(role.ToString(), page, perPage);

        if (!IsDifferentHash(pagedResult!)) yield break;

        foreach (var item in pagedResult?.Items!)
        {
            var component = await BuildComponentAsync(item, item?.City?.Id?.ToString(), item?.Role?.Key);
            if (component is null) continue;

            var validationResult = await _userValidator.ValidateAsync(component!);
            if (validationResult.IsValid) yield return component;
            else LogValidationError(validationResult, component);
        }

        if (pagedResult?.Meta?.PagesCount > page)
        {
            await foreach (var item in GetUsersAsync(role, cityId, perPage, page + 1)) yield return item;
        }
    }

    private async IAsyncEnumerable<GorkoModels.City> GetCitiesAsync(int perPage = PER_PAGE, int page = 1)
    {
        if (page < 1 || perPage < 1) throw new ArgumentException();

        var keys = await _cityRepository.GetKeysAsync(Source.Gorko);

        var pagedResult = await _gorkoClient.GetCitiesAsync(perPage, page);

        if (pagedResult is null) LogInvalidResponse(nameof(GorkoModels.City), page, perPage);

        if (!IsDifferentHash(pagedResult!)) yield break;

        var collection = pagedResult?.Items
            .Where(x => x.Id.HasValue && keys.Any(y => y == x.Id.Value.ToString()))
            .ToList() ?? new List<GorkoModels.City>();

        foreach (var item in collection) yield return item;

        if (pagedResult?.Meta?.PagesCount > page)
        {
            await foreach (var item in GetCitiesAsync(perPage, page + 1)) yield return item;
        }
    }

    private async Task<Component?> BuildComponentAsync<T>(T item, string? cityKey, string? categoryKey)
    {
        try
        {
            var component = _mapper.Map<Component>(item!);
            var category = await _categoryRepository.FindByKeyAsync(Source.Gorko, categoryKey!);
            var city = await _cityRepository.FindByKeyAsync(Source.Gorko, cityKey!);

            component.Category = _mapper.Map<Category>(category!);
            component.City = _mapper.Map<City>(city!);

            return await RestoreNecessaryData(component);
        }
        catch (Exception exception)
        {
            LogActionError(exception, nameof(BuildComponentAsync), item);
            return null;
        };
    }

    private async Task<Component> RestoreNecessaryData(Component component)
    {
        var local = await _componentRepository.FindByIdAsync(component.Id);

        component.Status = local is null
            ? ComponentStatus.Announced
            : local.Status;

        component.Popularity = local?.Popularity ?? default;

        return component;
    }

    private async Task SelectAndExecuteAction(Component component)
    {
        if (component.Status == ComponentStatus.Announced)
        {
            await CreateComponentAsync(component);
        }
        else
        {
            await UpdateComponentAsync(component);
        }
    }

    private async Task CreateComponentAsync(Component component)
    {
        component.UpdatedAt = DateTime.Now;
        component.Status = ComponentStatus.Available;
        var logMessage = $"[{DateTime.Now:HH:mm:ss}] COMPONENT WITH ID '{component.Id}' WAS CREATED";
        await InvokeActionAsync(_componentRepository.AddAsync, component, logMessage);
    }

    private async Task UpdateComponentAsync(Component component)
    {
        component.UpdatedAt = DateTime.Now;
        var logMessage = $"[{DateTime.Now:HH:mm:ss}] COMPONENT WITH ID '{component.Id}' WAS UPDATED";
        await InvokeActionAsync(_componentRepository.UpdateAsync, component, logMessage);
    }

    private async Task ArchiveExpiredComponents(DateTime frontier)
    {
        try
        { 
            var components = await _componentRepository
                .FindAsync(x => x.Provider == Source.Gorko.ToString() 
                    && x.UpdatedAt < frontier);

            foreach (var component in components)
            {
                component.Status = ComponentStatus.Archived;
                await _componentRepository.UpdateAsync(component);
            }

            await _componentRepository.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            LogActionError(exception, $"{nameof(ArchiveExpiredComponents)}");
        }
    }

    private async Task InvokeActionAsync(Func<Component, Task> action, Component item, string message)
    {
        try
        {
            await action.Invoke(item);
            await _componentRepository.SaveChangesAsync();
            _logger.LogInformation(message);
        }
        catch (Exception exception)
        {
            LogActionError(exception, action.Method.Name, item);
        };
    }

    #endregion

    #region hashes

    private long _lastOperationHash;

    private bool IsDifferentHash<T>(GorkoModels.PagedResult<T> pagedResult) where T : IHaveId
    {
        var hash = GetOperationHash(pagedResult);

        var isEquals = pagedResult != null && hash == _lastOperationHash;

        if (isEquals) LogIdenticalHash(pagedResult!);

        _lastOperationHash = hash;

        return !isEquals;
    }

    private static long GetOperationHash<T>(GorkoModels.PagedResult<T> pagedResult) where T : IHaveId
    {
        if (pagedResult?.Items is null) throw new ArgumentException();
        return pagedResult.Items.Select(x => x.Id.GetHashCode() * 24).Sum();
    }

    #endregion

    #region logging

    private void LogIdenticalHash<T>(GorkoModels.PagedResult<T> pagedResult)
    {
        if (pagedResult?.Meta?.Page is null) throw new ArgumentException();

        var message = $"\n[{DateTime.Now:HH:mm:ss}]\nIDENTICAL QUERY HASHES WERE FOUND\n" +
            $"TYPE: {typeof(T).Name}\n" +
            $"PAGES: {pagedResult.Meta.Page - 1} & {pagedResult.Meta.Page}\n";

        _logger.LogError(message);
    }

    private void LogValidationError<T>(ValidationResult validationResult, T obj)
    {
        var errors = string.Join(" ;", validationResult.Errors);

        var message = $"\n[{DateTime.Now:HH:mm:ss}]\nERROR: {errors}\n" +
            $"OBJECT: {JsonConvert.SerializeObject(obj)}\n";

        _logger.LogError(message);
    }

    private void LogActionError<T>(Exception exception, string actionName, T item)
    {
        var message = $"\n[{DateTime.Now:HH:mm:ss}]\nERROR: {exception.Message}\n" +
            $"INNER_ERROR: {exception.InnerException?.Message}\n" +
            $"ACTION: {actionName}\n" +
            $"WITH OBJECT: {JsonConvert.SerializeObject(item)}\n";

        _logger.LogError(message);
    }

    private void LogActionError(Exception exception, string actionName)
    {
        var message = $"\n[{DateTime.Now:HH:mm:ss}]\nERROR: {exception.Message}\n" +
            $"INNER_ERROR: {exception.InnerException?.Message}\n" +
            $"ACTION: {actionName}\n";

        _logger.LogError(message);
    }

    private void LogTaskStart(DateTime start)
    {
        var message = $">> [{start:HH:mm:ss}] TASK HAS STARTED\n";
        _logger.LogInformation(message);
    }

    private void LogTaskFinish(DateTime start, DateTime finish)
    {
        var executionTime = new TimeOnly(finish.Ticks - start.Ticks);

        var message = $"\n>> [{finish:HH:mm:ss}]TASK COMPLETED\n" +
            $"EXECUTION TIME: {executionTime:HH:mm:ss}.\n";

        _logger.LogInformation(message);
    }

    private void LogInvalidResponse(string typeName, int page, int perPage)
    {
        var message = $"\n[{DateTime.Now:HH:mm:ss}] RECEIVER AN " +
            $"INCORRECT RESPONSE FROM '{Source.Gorko}'\n" +
            $"TYPE: {typeName}\n" +
            $"PAGE: {page}\n" +
            $"PER_PAGE: {perPage}";

        _logger.LogError(message);
    }

    #endregion
}