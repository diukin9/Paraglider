using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Paraglider.Infrastructure.Common.AppDefinition;

/// <summary>
/// Base implementation for <see cref="IAppDefinition"/>
/// </summary>
public abstract class AppDefinition : IAppDefinition
{
    /// <summary>
    /// Configure services for current application
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration) { }

    /// <summary>
    /// Configure application for current application
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public virtual void ConfigureApplication(WebApplication app, IWebHostEnvironment env) { }
}