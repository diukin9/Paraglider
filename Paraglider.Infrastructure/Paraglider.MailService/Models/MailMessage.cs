using MimeKit.Text;

namespace Paraglider.MailService.Models;

public class MailMessage
{
    public string RecipientMail { get; set; }
    public string Subject { get; set; }
    public TextFormat TextFormat { get; set; }
    public string Body { get; set; }

    public MailMessage(string recipientMail, string subject, string body, TextFormat textFormat)
    {
        if (StringHelper.CheckForNullOrEmpty(recipientMail, subject))
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

    public static MailMessage MailConfirmation(string recepient, string confirmationLink)
    {
        return new(recepient,
            EmailSubjects.PasswordConfirmationAfterRegistration,
            EmailTemplates.MailConfirmationTemplate(confirmationLink),
            TextFormat.Html);
    }
}
