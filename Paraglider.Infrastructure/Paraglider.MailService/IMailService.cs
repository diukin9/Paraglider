using Paraglider.MailService.Enums;
using Paraglider.MailService.Models;

namespace Paraglider.MailService;

public interface IMailService
{
    public Task SendAsync(MailMessage message);
    public Task SendByTemplateAsync(EmailTemplate template, MailMessage message);

    public void Send(MailMessage message);
    public void SendByTemplate(EmailTemplate template, MailMessage message);
}
