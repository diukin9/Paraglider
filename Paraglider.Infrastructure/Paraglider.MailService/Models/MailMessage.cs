using MimeKit.Text;

namespace Paraglider.MailService.Models;

public class MailMessage
{
    public string RecipientMail { get; set; }
    public string Subject { get; set; }
    public TextFormat TextFormat { get; set; }
    public string Body { get; set; }

    public MailMessage(string recipientMail, string subject, string body, TextFormat textFormat = TextFormat.Text)
    {
        if (StringHelper.CheckForNull(recipientMail, subject, body))
        {
            throw new ArgumentException(Exceptions.PassedEmptyParameter);
        }

        if (!recipientMail.IsEmail())
        {
            throw new ArgumentException(Exceptions.WrongMailboxFormat);
        }

        RecipientMail = recipientMail;
        Subject = subject;
        TextFormat = textFormat;
        Body = body;
    }
}
