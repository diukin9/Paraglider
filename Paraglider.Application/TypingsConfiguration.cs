using System.Reflection;
using Paraglider.Infrastructure.Reinforced;
using ConfigurationBuilder = Reinforced.Typings.Fluent.ConfigurationBuilder;

namespace Paraglider.API
{
    public static class TypingsConfiguration
    {
        private const string TypingsNamespace = "Paraglider.Web.Endpoints";

        public static void Configure(ConfigurationBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            TypingsConfigurationBase.Configure(builder, assembly, TypingsNamespace);
        }
    }
}
