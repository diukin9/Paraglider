using Paraglider.Application.BackgroundJobs.ReccuringJobs.Abstractions;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;

namespace Paraglider.Application.BackgroundJobs.ReccuringJobs.Gorko;

public class UpdateComponentPopularityDataRecurringJob : IReccuringJob
{
    private readonly IComponentAddHistoryRepository _historyRepository;
    private readonly IComponentRepository _componentRepository;

    public UpdateComponentPopularityDataRecurringJob(
        IComponentAddHistoryRepository historyRepository,
        IComponentRepository componentRepository)
    {
        _historyRepository = historyRepository;
        _componentRepository = componentRepository;
        
    }

    public async Task RunAsync()
    {
        var perPage = 100;
        var iterationCount = await _componentRepository.CountAsync() / perPage + 1;

        for (var index = 0; index < iterationCount; index++)
        {
            var components = await _componentRepository.FindAsync(
                filter: _ => true,
                orderBy: x => x.Id,
                isAscending: true,
                skip: index * perPage, 
                limit: perPage);

            foreach (var component in components)
            {
                component.Popularity = await _historyRepository
                    .CountAsync(x => x.ComponentId == component.Id);

                await _componentRepository.UpdateAsync(component);
            }
        }
    }
}
