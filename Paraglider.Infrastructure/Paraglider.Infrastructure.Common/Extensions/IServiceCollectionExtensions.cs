using Microsoft.Extensions.DependencyInjection;
using Paraglider.Infrastructure.Common.Repository;
using System.Reflection;

namespace Paraglider.Infrastructure.Common.Extensions;

public static class IServiceCollectionExtensions
{
    #region static fiels

    private static string IRepositoryName => typeof(IRepository<>).Name;
    private static string RepositoryName => typeof(Repository<>).Name;

    #endregion

    public static IServiceCollection AddRepositories(this IServiceCollection services, Assembly assembly)
    {
        //получаем все репозитории
        var repositories = assembly.GetTypes()
            .Where(type => type?.BaseType?.Name == RepositoryName)
            .ToList();

        foreach (var repository in repositories)
        {
            //получаем контракт репозитория
            var contract = repository.GetInterfaces()
                .Where(x => x.GetInterface(IRepositoryName) is not null)
                .SingleOrDefault();

            //пропускаем, если репозиторий не содержит нужный контракт
            if (contract is null) continue;

            //регистрируем в DI
            services.AddScoped(contract, repository);
        }

        return services;
    }
}
