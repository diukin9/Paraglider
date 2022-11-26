using Paraglider.MailService.Enums;
using Paraglider.MailService.Models;

namespace Paraglider.MailService;

public interface IMailService
{
    public Task SendAsync(MailMessage message, CancellationToken cancellationToken);
    public Task SendByTemplateAsync(EmailTemplate template, MailMessage message, CancellationToken cancellationToken);
}
