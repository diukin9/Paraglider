using Paraglider.Application.BackgroundJobs.ReccuringJobs.Gorko.Validators;
using Paraglider.Clients.Gorko;
using Paraglider.Clients.Gorko.Models.Abstractions;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.RDB.Enums;
using GorkoEnums = Paraglider.Clients.Gorko.Models.Enums;
using GorkoModels = Paraglider.Clients.Gorko.Models;

namespace Paraglider.Application.BackgroundJobs.ReccuringJobs.Gorko.Infrastructure;

public class ComponentStore
{
    private const int PER_PAGE = 20;

    private readonly ICityRepository _cityRepository;

    private readonly GorkoClient _gorkoClient;
    private readonly ComponentBuilder _builder;

    private readonly UserValidator _userValidator = new();
    private readonly RestaurantValidator _restaurantValidator = new();

    public ComponentStore(GorkoClient gorkoClient, ComponentBuilder builder, ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
        _gorkoClient = gorkoClient;
        _builder = builder;
    }

    public async IAsyncEnumerable<Component> GetComponentsAsync(long cityId)
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

    public async IAsyncEnumerable<GorkoModels.City> GetCitiesAsync(int perPage = PER_PAGE, int page = 1)
    {
        if (page < 1 || perPage < 1) throw new ArgumentException();

        var keys = await _cityRepository.GetKeysAsync(Source.Gorko);

        var pagedResult = await _gorkoClient.GetCitiesAsync(perPage, page);

        if (pagedResult is null || !IsDifferentHash(pagedResult!)) yield break;

        var collection = pagedResult?.Items
            .Where(x => x.Id.HasValue && keys.Any(y => y == x.Id.Value.ToString()))
            .ToList() ?? new List<GorkoModels.City>();

        foreach (var item in collection) yield return item;

        if (pagedResult?.Meta?.PagesCount > page)
        {
            await foreach (var item in GetCitiesAsync(perPage, page + 1)) yield return item;
        }
    }

    private async IAsyncEnumerable<Component> GetRestaurantsAsync(long cityId, int perPage = PER_PAGE, int page = 1)
    {
        if (page < 1 || perPage < 1) throw new ArgumentException();

        var pagedResult = await _gorkoClient.GetRestaurantsAsync(perPage, page, cityId);

        if (pagedResult is null || !IsDifferentHash(pagedResult!)) yield break;

        foreach (var item in pagedResult?.Items!)
        {
            if (item?.City?.Id is null || item?.Type?.Key is null) continue;
            var component = await BuildComponentAsync(item, $"{item.City.Id}", item.Type.Key);
            if (component is null) continue;

            var validationResult = await _restaurantValidator.ValidateAsync(component);
            if (validationResult.IsValid) yield return component;
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

        if (pagedResult is null || !IsDifferentHash(pagedResult!)) yield break;

        foreach (var item in pagedResult?.Items!)
        {
            if (item?.City?.Id is null || item?.Role?.Key is null) continue;
            var component = await BuildComponentAsync(item, $"{item.City.Id}", item.Role.Key);
            if (component is null) continue;

            var validationResult = await _userValidator.ValidateAsync(component!);
            if (validationResult.IsValid) yield return component;
        }

        if (pagedResult?.Meta?.PagesCount > page)
        {
            await foreach (var item in GetUsersAsync(role, cityId, perPage, page + 1)) yield return item;
        }
    }

    private async Task<Component?> BuildComponentAsync<T>(T item, string? cityKey, string? categoryKey)
    {
        try { return await _builder.BuildAsync(item, categoryKey!, cityKey!); }
        catch { return null; };
    }

    #region hashes

    private long _lastOperationHash;

    private bool IsDifferentHash<T>(GorkoModels.PagedResult<T> pagedResult) where T : IHaveId
    {
        var hash = GetOperationHash(pagedResult);
        var isEquals = pagedResult != null && hash == _lastOperationHash;
        _lastOperationHash = hash;
        return !isEquals;
    }

    private static long GetOperationHash<T>(GorkoModels.PagedResult<T> pagedResult) where T : IHaveId
    {
        if (pagedResult?.Items is null) throw new ArgumentException();
        return pagedResult.Items.Select(x => x.Id.GetHashCode() * 24).Sum();
    }

    #endregion
}
