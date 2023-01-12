using Paraglider.Application.BackgroundJobs.ReccuringJobs.Abstractions;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Enums;

namespace Paraglider.Application.BackgroundJobs.ReccuringJobs.Gorko;

public class RemoveExpiredAndUnusedComponentsReccuringJob : IReccuringJob
{
    private readonly IComponentRepository _componentRepository;
    private readonly IPlanningComponentRepository _planningComponentRepository;

    public RemoveExpiredAndUnusedComponentsReccuringJob(
        IComponentRepository componentRepository,
        IPlanningComponentRepository planningComponentRepository)
    {
        _componentRepository = componentRepository;
        _planningComponentRepository = planningComponentRepository;
    }

    public async Task RunAsync()
    {
        var expireds = await _componentRepository.FindAsync(x => x.Status == ComponentStatus.Archived);

        foreach (var component in expireds)
        {
            if (await _planningComponentRepository.CountAsync(x => x.ComponentId == component.Id) == 0)
            {
                await _componentRepository.RemoveAsync(component);
                await _componentRepository.SaveChangesAsync();
            }
        }
    }
}
