using Microsoft.Extensions.DependencyInjection;
using Paraglider.Application.BackgroundJobs.ReccuringJobs.Gorko;
using Paraglider.Application.BackgroundJobs.ReccuringJobs.Gorko.Infrastructure;
using Paraglider.Clients.Gorko;

namespace Paraglider.Application.BackgroundJobs;

public static class IServiceCollectionExtension
{
    public static IServiceCollection RegisterServicesForHangfireTasks(this IServiceCollection services)
    {
        services.AddSingleton<GorkoClient>();
        services.AddTransient<ComponentBuilder>();
        services.AddTransient<ComponentStore>();

        services.AddScoped<ImportComponentsRecurringJob>();
        services.AddScoped<UpdateComponentPopularityDataRecurringJob>();
        services.AddScoped<RemoveExpiredAndUnusedComponentsReccuringJob>();

        return services;
    }
}
