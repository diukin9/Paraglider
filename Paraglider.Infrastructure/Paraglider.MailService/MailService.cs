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

    public async Task SendAsync(MailMessage message, CancellationToken cancellationToken)
    {
        var mimeMessage = GetMimeMessage(message.RecipientMail, message.Subject, message.Body, message.TextFormat);
        await SendUsingSmtpAsync(mimeMessage, cancellationToken);
    }

    public async Task SendByTemplateAsync(EmailTemplate template, MailMessage message,
        CancellationToken cancellationToken)
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

        await SendAsync(message, cancellationToken);
    }

    #region private methods

    private MimeMessage GetMimeMessage(
        string recipientMail,
        string subject,
        string body,
        TextFormat textFormat)
    {
        var mimeMessage = new MimeMessage();
        mimeMessage.From.Add(MailboxAddress.Parse(_settings.SenderMail));
        mimeMessage.To.Add(MailboxAddress.Parse(recipientMail));
        mimeMessage.Subject = subject;
        mimeMessage.Body = new TextPart(textFormat) { Text = body };

        return mimeMessage;
    }

    private async Task SendUsingSmtpAsync(MimeMessage message, CancellationToken cancellationToken)
    {
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_settings.Host, _settings.Port, _settings.SecureSocketOptions, cancellationToken);
        await smtp.AuthenticateAsync(_settings.Login, _settings.Password, cancellationToken);
        await smtp.SendAsync(message, cancellationToken);
        await smtp.DisconnectAsync(true, cancellationToken);
    }

    #endregion
}