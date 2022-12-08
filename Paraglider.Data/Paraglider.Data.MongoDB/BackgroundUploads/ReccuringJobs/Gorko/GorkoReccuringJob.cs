using MapsterMapper;
using Paraglider.Clients.Gorko;
using Paraglider.Clients.Gorko.Models;
using Paraglider.Clients.Gorko.Models.Enums;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Data.MongoDB.BackgroundUploads.ReccuringJobs.Abstractions;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Infrastructure.Common.Helpers;
using Paraglider.Infrastructure.Common.MongoDB;
using System.Security.Cryptography;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Data.MongoDB.BackgroundUploads.ReccuringJobs.Gorko;

public class GorkoReccuringJob : IReccuringJob
{
    private readonly string _source = Enum.GetName(typeof(Source), Source.Gorko)!;

    private readonly IMongoDataAccess<Component> _components;
    private readonly ICityRepository _cityRepository;
    private readonly GorkoClient _gorkoClient;
    private readonly IMapper _mapper;

    public GorkoReccuringJob(
        GorkoClient gorkoClient,
        ICityRepository cityRepository,
        IMongoDataAccess<Component> components,
        IMapper mapper)
    {
        _gorkoClient = gorkoClient;
        _cityRepository = cityRepository;
        _components = components;
        _mapper = mapper;
    }

    public async Task RunAsync()
    {
        var start = DateTime.Now;

        await foreach (var city in GetCitiesAsync())
            await foreach (var component in GetAllComponentsAsync(city.Id!.Value))
                await SelectAndExecuteAction(component);

        await RemoveExpiredComponentsAsync(start);
    }

    private async IAsyncEnumerable<Component> GetAllComponentsAsync(long cityId)
    {
        //await foreach (var component in GetRestaurantsAsync(cityId)) yield return component;
        foreach (var role in Enum.GetValues(typeof(UserRole)))
            await foreach (var component in GetUsersAsync((UserRole)role, cityId)) yield return component;
    }

    private async IAsyncEnumerable<Component> GetRestaurantsAsync(long cityId, int page = 1)
    {
        const int perPage = 1000;
        if (page < 1) throw new ArgumentException(ExceptionMessages.CannotBeNegative(nameof(page)));

        var enumerable = _gorkoClient.GetRestaurantsAsync(perPage, page, cityId);
        var enumerator = enumerable.GetAsyncEnumerator();
        if (enumerator.Current is null) yield break;

        await foreach (var item in enumerable) yield return _mapper.Map<Component>(item);
        await foreach (var item in GetRestaurantsAsync(cityId, page + 1)) yield return _mapper.Map<Component>(item);
    }

    private async IAsyncEnumerable<Component> GetUsersAsync(UserRole role, long cityId, int page = 1)
    {
        const int perPage = 10;
        if (page < 1) throw new ArgumentException(ExceptionMessages.CannotBeNegative(nameof(page)));

        var count = 0;
        await foreach(var item in _gorkoClient.GetUsersAsync(role, perPage, page, cityId))
        {
            yield return _mapper.Map<Component>(item);
            count++;
        }

        if (count < perPage) yield break;

        await foreach (var item in GetUsersAsync(role, cityId, page + 1)) yield return _mapper.Map<Component>(item);
    }

    private async Task SelectAndExecuteAction(Component component)
    {
        var local = await _components.FindAsync(x => x.ExternalId == component.Id.ToString() && x.Provider == _source);
        if (local.Count() > 1) throw new InvalidOperationException();
        else if (local.SingleOrDefault() is null) await CreateComponentAsync(component);
        else await UpdateComponentAsync(component);
    }

    private async Task CreateComponentAsync(Component component)
    {
        //TODO валидация необходимых данных
        //TODO не забыть проставить UpdatedAt и CreatedAt
        if (component is null) throw new ArgumentException($"{nameof(Component)} was null");
        await _components.AddAsync(component);
    }

    private async Task UpdateComponentAsync(Component component)
    {
        //TODO валидация необходимых данных
        //TODO не забыть проставить UpdatedAt
        if (component is null) throw new ArgumentException($"{nameof(Component)} was null");
        await _components.UpdateAsync(component);
    }

    private async Task RemoveExpiredComponentsAsync(DateTime start)
    {
        var removable = await _components.FindAsync(x => x.Provider == _source && x.UpdatedAt < start);
        foreach (var item in removable) await _components.RemoveAsync(item);
    }

    private async IAsyncEnumerable<City> GetCitiesAsync()
    {
        var localCities = await _cityRepository.GetAll(CancellationToken.None);
        await foreach (var city in _gorkoClient.GetCitiesAsync(int.MaxValue, 1))
            if (localCities.Any(local => StringHelper.CompareLetters(local.Name, city.Name))) yield return city;
    }
}