using Paraglider.Application.BackgroundJobs.ReccuringJobs.Abstractions;
using Paraglider.Application.BackgroundJobs.ReccuringJobs.Gorko.Infrastructure;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.RDB.Enums;

namespace Paraglider.Application.BackgroundJobs.ReccuringJobs.Gorko;

public class ImportComponentsRecurringJob : IReccuringJob
{
    private readonly IComponentRepository _componentRepository;
    private readonly ComponentStore _store;

    public ImportComponentsRecurringJob(
        IComponentRepository componentRepository,
        ComponentStore store)
    {
        _store = store;
        _componentRepository = componentRepository;
    }

    public async Task RunAsync()
    {
        var start = DateTime.UtcNow;

        await foreach (var city in _store.GetCitiesAsync())
        {
            await foreach (var component in _store.GetComponentsAsync(city.Id!.Value))
            {
                await SelectAndExecuteAction(component);
            }
        }

        await ArchiveExpiredComponents(start); 
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
        component.Status = ComponentStatus.Available;
        await InvokeActionAsync(_componentRepository.AddAsync, component);
    }

    private async Task UpdateComponentAsync(Component component)
    {
        await InvokeActionAsync(_componentRepository.UpdateAsync, component);
    }

    private async Task ArchiveExpiredComponents(DateTime frontier)
    {
        try
        {
            var components = await _componentRepository
                .FindAsync(x => x.Provider == $"{Source.Gorko}" && x.UpdatedAt < frontier);

            foreach (var component in components)
            {
                component.Status = ComponentStatus.Archived;
                await _componentRepository.UpdateAsync(component);
            }

            await _componentRepository.SaveChangesAsync();
        }
        catch { }
    }

    private async Task InvokeActionAsync(Func<Component, Task> action, Component item)
    {
        try
        {
            await action.Invoke(item);
            await _componentRepository.SaveChangesAsync();
        }
        catch { };
    }
}