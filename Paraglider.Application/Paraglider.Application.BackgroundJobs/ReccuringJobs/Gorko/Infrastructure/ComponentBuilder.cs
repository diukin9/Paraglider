using MapsterMapper;
using Paraglider.Clients.Gorko.Models;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.RDB.Enums;
using static Paraglider.Application.BackgroundJobs.ReccuringJobs.Gorko.Infrastructure.StaticData;

namespace Paraglider.Application.BackgroundJobs.ReccuringJobs.Gorko.Infrastructure;

public class ComponentBuilder
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICityRepository _cityRepository;
    private readonly IComponentRepository _componentRepository;

    public ComponentBuilder(
        ICityRepository cityRepository, 
        ICategoryRepository categoryRepository, 
        IComponentRepository componentRepository,
        IMapper mapper)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
        _cityRepository = cityRepository;
        _componentRepository = componentRepository;
    }

    public async Task<Component> BuildAsync<T>(T item, string categoryKey, string cityKey)
    {
        var component = _mapper.Map<Component>(item!);

        var category = await _categoryRepository.FindByKeyAsync(Source.Gorko, categoryKey);
        var city = await _cityRepository.FindByKeyAsync(Source.Gorko, cityKey);

        component = RemoveInvalidData(component);

        component.Category = category ?? throw new ArgumentException();
        component.City = city ?? throw new ArgumentException();

        component = await RestoreNecessaryDataAsync(component);

        return component;
    }

    private static Component RemoveInvalidData(Component component)
    {
        var keys = new[] { PhoneNumber, Email, Telegram, WhatsApp };
        component.Contacts = component.Contacts.Where(x => keys.Contains(x.Key)).ToList();

        if (component.Album?.Media is not null)
        {
            component.Album.Media = component.Album.Media.Where(x => x.Url is not null).ToList();
            if (!component.Album.Media.Any()) component.Album = null;
            component.AlbumId = null;
        }

        foreach (var hall in component.Halls ?? new List<Hall>())
        {
            if (hall?.Album?.Media is not null)
            {
                hall.Album.Media = hall.Album.Media.Where(x => x.Url is not null).ToList();
                if (!hall.Album.Media.Any()) hall.Album = null;
                hall.AlbumId = null;
            }
        }

        return component;
    }

    private async Task<Component> RestoreNecessaryDataAsync(Component component)
    {
        component.Rating = component.Reviews?.Any() ?? false
            ? component.Reviews.Sum(x => x.Evaluation) / component.Reviews.Count
            : 0.0;

        component.AlbumId = component.Album?.Id;

        component.Halls = component.Halls?.Where(x => x.Album is not null).ToList();

        foreach (var hall in component.Halls ?? new HashSet<Hall>())
        {
            hall.AlbumId = hall.Album!.Id;
        }

        component.CityId = component.City.Id;
        component.CategoryId = component.Category.Id;

        var fromDb = (await _componentRepository
            .FindAsync(x => x.Provider == $"{Source.Gorko}" && x.ExternalId == component.ExternalId))
            .SingleOrDefault();

        component.Status = fromDb is null ? ComponentStatus.Announced : fromDb.Status;
        component.Popularity = fromDb?.Popularity ?? default;
        component.Id = fromDb?.Id ?? component.Id;

        return component;
    }
}
