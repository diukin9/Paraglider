using MongoDB.Driver;
using Paraglider.Application.BackgroundJobs.ReccuringJobs.Abstractions;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Infrastructure.Common.MongoDB;

namespace Paraglider.Application.BackgroundJobs.ReccuringJobs.Gorko;

public class UpdateComponentPopularityDataRecurringJob
    : BaseReccuringJob<UpdateComponentPopularityDataRecurringJob>
{
    private readonly IComponentAdditionHistoryRepository _repository;
    private readonly IMongoDataAccess<Component> _components;

    public UpdateComponentPopularityDataRecurringJob(
        IComponentAdditionHistoryRepository repository,
        IMongoDataAccess<Component> components)
    {
        _repository = repository;
        _components = components;
    }

    public override async Task RunAsync()
    {
        var perPage = 100;
        var iterationCount = await _components.CountAsync() / perPage + 1;

        for (var index = 0; index < iterationCount; index++)
        {
            var components = await _components.FindAsync(
                filter: _ => true,
                sort: x => x.Id,
                sortDirection: SortDirection.Ascending,
                skip: index * perPage, 
                limit: perPage);

            foreach (var component in components)
            {
                component.Popularity = await _repository.CountAsync(component.Id);
                await _components.UpdateAsync(component);
            }
        }
    }
}
