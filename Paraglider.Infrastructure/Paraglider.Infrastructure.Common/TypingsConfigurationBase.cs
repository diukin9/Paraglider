using Reinforced.Typings.Fluent;
using System.Reflection;

namespace Paraglider.Infrastructure;

public static class TypingsConfiguration
{
    public static void Configure(ConfigurationBuilder builder, Assembly assembly, string typingsNamespace)
    {
        var types = assembly.GetTypes().Where(type => IsViewModel(type.Namespace, typingsNamespace));

        builder.Global(config => config.UseModules().AutoOptionalProperties().CamelCaseForProperties());

        builder.ExportAsClasses(
            types.Where(t => t.IsClass),
            conf => conf.WithAllProperties().Order(GetTypeOrder(conf.Type))
        );

        builder.ExportAsEnums(types.Where(t => t.IsEnum));
    }

    private static bool IsViewModel(string? currentNamespace, string typingsNamespace)
    {
        return currentNamespace != null
            && currentNamespace.StartsWith(typingsNamespace)
            && currentNamespace.Contains("ViewModels");
    }

    private static int GetTypeOrder(Type type)
    {
        var order = 0;
        var baseType = type.BaseType;

        while (baseType != null)
        {
            baseType = baseType.BaseType;
            order += 100;
        }

        return order;
    }
}
