using MailKit.Security;

namespace Paraglider.MailService.Models;

public class MailSettings
{
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public SecureSocketOptions SecureSocketOptions { get; set; }
    public string SenderMail { get; set; } = null!;
    public string Password { get; set; } = null!;

    public MailSettings(string host,
        int port,
        string senderMail,
        string password,
        SecureSocketOptions secureSocketOptions = SecureSocketOptions.StartTls)
    {
        if (StringHelper.CheckForNull(host, senderMail, password))
        {
            throw new ArgumentException(Exceptions.PassedEmptyParameter);
        }

        if (!senderMail.IsEmail())
        {
            throw new ArgumentException(Exceptions.WrongMailboxFormat);
        }

        Host = host;
        Port = port;
        SecureSocketOptions = secureSocketOptions;
        SenderMail = senderMail;
        Password = password;
    }
}
