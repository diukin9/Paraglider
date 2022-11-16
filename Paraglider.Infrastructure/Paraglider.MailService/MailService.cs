using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using Paraglider.MailService.Enums;
using Paraglider.MailService.Models;

namespace Paraglider.MailService;

public class MailService : IMailService
{
    private readonly MailSettings _settings;

    public MailService(MailSettings settings)
    {
        _settings = settings;
    }

    public async Task SendAsync(MailMessage message)
    {
        var mimeMessage = GetMimeMessage(message.RecipientMail, message.Subject, message.Body, message.TextFormat);
        await SendUsingSmtpAsync(mimeMessage);
    }

    public void Send(MailMessage message) => SendAsync(message).Wait();

    public async Task SendByTemplateAsync(EmailTemplate template, MailMessage message)
    {
        switch(template)
        {
            case EmailTemplate.MailConfirmation:
                message.Body = EmailTemplates.MailConfirmationTemplate(message.Body);
                break;
            case EmailTemplate.PasswordRecovery:
                message.Body = EmailTemplates.PasswordRecoveryTemplate(message.Body);
                break;
        }
        await SendAsync(message);
    }

    public void SendByTemplate(EmailTemplate template, MailMessage message) => SendByTemplateAsync(template, message).Wait();

    #region private methods

    private MimeMessage GetMimeMessage(
        string recipientMail,
        string subject,
        string body,
        TextFormat textFormat = TextFormat.Text)
    {
        var mimeMessage = new MimeMessage();
        mimeMessage.From.Add(MailboxAddress.Parse(_settings.SenderMail));
        mimeMessage.To.Add(MailboxAddress.Parse(recipientMail));
        mimeMessage.Subject = subject;
        mimeMessage.Body = new TextPart(textFormat) { Text = body };

        return mimeMessage;
    }

    private async Task SendUsingSmtpAsync(MimeMessage message)
    {
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_settings.Host, _settings.Port, _settings.SecureSocketOptions);
        await smtp.AuthenticateAsync(_settings.SenderMail, _settings.Password);
        await smtp.SendAsync(message);
        await smtp.DisconnectAsync(true);
    }

    #endregion
}