using Paraglider.Infrastructure.Common;
using System.Reflection;
using ConfigurationBuilder = Reinforced.Typings.Fluent.ConfigurationBuilder;
using static Paraglider.Infrastructure.Common.AppData;
using Reinforced.Typings.Fluent;

namespace Paraglider.API;

public static class TypingsDefinition
{
    public static void Configure(ConfigurationBuilder builder)
    {
        //var assembly = Assembly.GetExecutingAssembly();
        //TypingsConfiguration.Configure(builder, assembly, TypingsNamespace);
        builder.Global(config => config.UseModules().DontWriteWarningComment());
    }
}
