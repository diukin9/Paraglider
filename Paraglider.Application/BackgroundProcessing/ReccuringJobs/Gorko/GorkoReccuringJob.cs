using MapsterMapper;
using Paraglider.Clients.Gorko;
using Paraglider.Clients.Gorko.Models.Enums;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Domain.NoSQL.ValueObjects;
using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Exceptions;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.MongoDB;
using System.ComponentModel.DataAnnotations;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.BackgroundProcessing.ReccuringJobs.Gorko;

public class GorkoReccuringJob : ReccuringJob<GorkoReccuringJob>
{
    private const int PER_PAGE = 10;
    private readonly string _source = Enum.GetName(typeof(Source), Source.Gorko)!;

    private readonly IMongoDataAccess<Component> _components;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICityRepository _cityRepository;
    private readonly GorkoClient _gorkoClient;
    private readonly IMapper _mapper;

    public GorkoReccuringJob(
        GorkoClient gorkoClient,
        ICityRepository cityRepository,
        ICategoryRepository categoryRepository,
        IMongoDataAccess<Component> components,
        IMapper mapper)
    {
        _gorkoClient = gorkoClient;
        _categoryRepository = categoryRepository;
        _cityRepository = cityRepository;
        _components = components;
        _mapper = mapper;
    }

    public override async Task RunAsync()
    {
        var start = DateTime.Now;

        _logger.LogInformation("TASK HAS STARTED".ToLogFormat(nameof(GorkoReccuringJob)));

        await foreach (var city in GetCitiesAsync())
            await foreach (var component in GetComponentsAsync(city.Id!.Value))
                await SelectAndExecuteAction(component);

        var finish = DateTime.Now;
        var executionTime = new TimeOnly(finish.Ticks - start.Ticks);

        await RemoveExpiredComponentsAsync(start);

        _logger.LogInformation(
            $"TASK COMPLETED. EXECUTION TIME: {executionTime:HH:mm:ss}.\n"
                .ToLogFormat(nameof(GorkoReccuringJob)));
    }

    private async IAsyncEnumerable<Component> GetComponentsAsync(long cityId) 
    {
        //await foreach (var component in GetRestaurantsAsync(cityId)) 
        //    if (component is not null) yield return component;

        foreach (var role in Enum.GetValues(typeof(UserRole)))
            await foreach (var component in GetUsersAsync((UserRole)role, cityId))
                if (component is not null) yield return component;
    }

    private async IAsyncEnumerable<Component?> GetRestaurantsAsync(long cityId, int page = 1)
    {
        if (page < 1) throw new ArgumentException(ExceptionMessages.CannotBeNegative(nameof(page)));

        var count = 0;

        await foreach (var item in _gorkoClient.GetRestaurantsAsync(PER_PAGE, page, cityId))
        {
            yield return await BuildComponentAsync(item, item?.City?.Id?.ToString(), item?.Type?.Key);
            count++;
        }

        if (count < PER_PAGE) yield break;

        await foreach (var item in GetRestaurantsAsync(cityId, page + 1)) yield return item;

    }

    private async IAsyncEnumerable<Component?> GetUsersAsync(UserRole role, long cityId, int page = 1)
    {
        _logger.LogInformation($"ROLE: {role}, PAGE: {page}".ToLogFormat(nameof(GorkoReccuringJob)));
        if (page < 1) throw new ArgumentException(ExceptionMessages.CannotBeNegative(nameof(page)));

        var count = 0;

        await foreach (var item in _gorkoClient.GetUsersAsync(role, PER_PAGE, page, cityId))
        {
            yield return await BuildComponentAsync(item, item?.City?.Id?.ToString(), item?.Role?.Key);
            count++;
        }

        if (count < PER_PAGE) yield break;

        await foreach (var item in GetUsersAsync(role, cityId, page + 1)) yield return item;
    }

    private async IAsyncEnumerable<Clients.Gorko.Models.City> GetCitiesAsync()
    {
        var keys = await _cityRepository.GetKeysAsync(Source.Gorko);

        await foreach (var city in _gorkoClient.GetCitiesAsync(int.MaxValue, 1))
            if (keys.Contains(city.Id.ToString())) yield return city;
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

            return component;
        }
        catch (Exception exception)
        {
            var exp = new HangfireJobException(item!, exception: exception);
            _logger.LogError(exp.ToLogFormat(nameof(GorkoReccuringJob)));
            return null;
        };
    }

    private async Task SelectAndExecuteAction(Component component)
    {
        var local = await _components.FindByIdAsync(component.Id);
        if (local is null) await CreateComponentAsync(component);
        else await UpdateComponentAsync(component);
    }

    private async Task CreateComponentAsync(Component component)
    {
        var validation = AttributeValidator.Validate(component);
        if (!validation.IsValid())
        {
            var exp = new HangfireJobException(component, validation.ErrorMessage, new ValidationException());
            _logger.LogError(exp.ToLogFormat(nameof(GorkoReccuringJob)));
        }

        component.UpdatedAt = DateTime.Now;

        var logMessage = $"COMPONENT WITH ID '{component.Id}' WAS CREATED";

        await InvokeActionAsync(_components.AddAsync, component, logMessage);
    }

    private async Task UpdateComponentAsync(Component component)
    {
        var validation = AttributeValidator.Validate(component);
        if (!validation.IsValid())
        {
            var exp = new HangfireJobException(component, validation.ErrorMessage, new ValidationException());
            _logger.LogError(exp.ToLogFormat(nameof(GorkoReccuringJob)));
        }

        component.UpdatedAt = DateTime.Now;

        var logMessage = $"COMPONENT WITH ID '{component.Id}' WAS UPDATED";

        await InvokeActionAsync(_components.UpdateAsync, component, logMessage);
    }

    private async Task RemoveExpiredComponentsAsync(DateTime frontier)
    {
        var removable = await _components.FindAsync(x => x.Provider == _source && x.UpdatedAt < frontier);

        foreach (var item in removable)
        {
            var logMessage = $"COMPONENT WITH ID '{item.Id}' WAS REMOVED";
            await InvokeActionAsync(_components.RemoveAsync, item, logMessage);
        }
    }

    private async Task InvokeActionAsync(Func<Component, Task> action, Component item, string message)
    {
        try
        {
            await action.Invoke(item);
            _logger.LogInformation(message.ToLogFormat(nameof(GorkoReccuringJob)));
        }
        catch (Exception exception)
        {
            var exp = new HangfireJobException(item!, exception: exception);
            _logger.LogError(exp.ToLogFormat(nameof(GorkoReccuringJob)));
        };
    }
}