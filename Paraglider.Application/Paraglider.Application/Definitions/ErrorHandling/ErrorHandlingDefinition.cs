using Microsoft.AspNetCore.Diagnostics;
using Paraglider.Application.Definitions.Base;
using Serilog;
using System.Text.Json;

namespace Paraglider.Application.Definitions.ErrorHandling;

public class ErrorHandlingDefinition : AppDefinition
{
    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        app.UseExceptionHandler(error => error.Run(async context =>
        {
            context.Response.ContentType = "application/json";
            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature is not null)
            {
                Log.Error($"Something went wrong in the {contextFeature.Error}");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                if (env.IsDevelopment())
                {
                    await context.Response.WriteAsync($"INTERNAL SERVER ERROR: {contextFeature.Error}");
                }
                else
                {
                    await context.Response.WriteAsync("INTERNAL SERVER ERROR. PLEASE TRY AGAIN LATER");
                }
            }
        }));
    }
}