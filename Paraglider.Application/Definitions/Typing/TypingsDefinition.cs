using Paraglider.Infrastructure.Common;
using System.Reflection;
using ConfigurationBuilder = Reinforced.Typings.Fluent.ConfigurationBuilder;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API;

public static class TypingsDefinition
{
    public static void Configure(ConfigurationBuilder builder)
    {
        var assembly = Assembly.GetExecutingAssembly();
        TypingsConfiguration.Configure(builder, assembly, TypingsNamespace);
    }
}
