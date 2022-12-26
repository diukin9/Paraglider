using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Paraglider.Infrastructure.Common.Attributes;
using System.Reflection;

namespace Paraglider.Infrastructure.Common.AppDefinition;

public static class AppDefinitionExtensions
{
    /// <summary>
    /// Filling the 'Configure Services' method
    /// </summary>
    /// <param name="source"></param>
    /// <param name="builder"></param>
    /// <param name="entryPointsAssembly"></param>
    public static void AddDefinitions(this IServiceCollection source, WebApplicationBuilder builder, params Type[] entryPointsAssembly)
    {
        var definitions = new List<IAppDefinition>();

        foreach (var entryPoint in entryPointsAssembly)
        {
            var types = entryPoint.Assembly.ExportedTypes.Where(x => !x.IsAbstract && typeof(IAppDefinition).IsAssignableFrom(x));
            var instances = types.Select(Activator.CreateInstance).Cast<IAppDefinition>();
            definitions.AddRange(instances);
        }

       definitions = (List<IAppDefinition>)OrderByCallingOrder(definitions);

        definitions.ForEach(app => app.ConfigureServices(source, builder.Configuration));
        source.AddSingleton(definitions as IReadOnlyCollection<IAppDefinition>);
    }

    /// <summary>
    /// Filling the 'Configure' method
    /// </summary>
    /// <param name="source"></param>
    public static void UseDefinitions(this WebApplication source)
    {
        var definitions = source.Services.GetRequiredService<IReadOnlyCollection<IAppDefinition>>();

        definitions = (IReadOnlyCollection<IAppDefinition>)OrderByCallingOrder(definitions);

        var environment = source.Services.GetRequiredService<IWebHostEnvironment>();
        foreach (var endpoint in definitions)
        {
            endpoint.ConfigureApplication(source, environment);
        }
    }

    private static IEnumerable<IAppDefinition> OrderByCallingOrder(IEnumerable<IAppDefinition> definitions)
    {
        return definitions
            .OrderBy(x => (x
                .GetType()
                .GetCustomAttribute(typeof(CallingOrderAttribute), false) as CallingOrderAttribute)!.Index)
            .ToList();
    }

}