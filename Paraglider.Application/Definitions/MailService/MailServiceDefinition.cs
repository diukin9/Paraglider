using Paraglider.API.Definitions.Base;
using Paraglider.MailService;
using Paraglider.MailService.Models;

namespace Paraglider.API.Definitions.MailService;

public class MailServiceDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var mailSettings = new MailSettings();
        configuration.Bind(nameof(MailSettings), mailSettings);
        services.AddSingleton(mailSettings);
        services.AddScoped<IMailService, Paraglider.MailService.MailService>();
    }
}