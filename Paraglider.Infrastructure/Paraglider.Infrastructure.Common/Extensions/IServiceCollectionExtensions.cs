using Microsoft.Extensions.DependencyInjection;
using Paraglider.Infrastructure.Common.Abstractions;
using System.Reflection;

namespace Paraglider.Infrastructure.Common.Extensions;

public static class IServiceCollectionExtensions
{
    #region static fiels

    private static string IDataAccessName => typeof(IMongoDataAccess<>).Name;
    private static string DataAccessName => typeof(MongoDataAccess<,>).Name;

    private static string IRepositoryName => typeof(IRepository<>).Name;
    private static string RDbRepositoryName => typeof(RDbRepository<>).Name;
    private static string NoSqlRepositoryName => typeof(NoSqlRepository<>).Name;
    private static string DerivedNoSqlRepositoryName => typeof(NoSqlRepository<,>).Name;

    #endregion

    private static IEnumerable<Type> GetRepositories(Assembly assembly, string baseTypeName)
    {
        return assembly.GetTypes()
            .Where(type => type?.BaseType?.Name == baseTypeName)
            .ToList();
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services, Assembly assembly)
    {
        //получаем все репозитории
        var repositories = GetRepositories(assembly, RDbRepositoryName)
            .Union(GetRepositories(assembly, NoSqlRepositoryName))
            .Union(GetRepositories(assembly, DerivedNoSqlRepositoryName))
            .Distinct();

        foreach(var repository in repositories)
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

    public static IServiceCollection AddMongoDataAccess(this IServiceCollection services, Assembly assembly) 
    {
        //получаем все классы, предоставляющие доступ к данным mongoDB
        var mongoDataAccess = assembly.GetTypes()
            .Where(type => type?.BaseType?.Name == DataAccessName)
            .ToList();

        foreach (var dataAccess in mongoDataAccess)
        {
            //получаем контракт классов, предоставляющих доступ к данным mongoDB
            var contract = dataAccess.BaseType!.GetInterfaces()
                .Where(type => type.Name == IDataAccessName)
                .Single();

            //регистрируем в DI
            services.AddScoped(contract, dataAccess);
        }

        return services;
    }
}
