using MailKit.Security;
using Paraglider.MailService.StaticData;

namespace Paraglider.MailService.Models;

public class MailSettings
{
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public SecureSocketOptions SecureSocketOptions { get; set; }
    public string SenderMail { get; set; } = null!;

    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;

    public MailSettings(SecureSocketOptions secureSocketOptions = SecureSocketOptions.Auto)
    {
        SecureSocketOptions = secureSocketOptions;
    }
    
    public MailSettings(string host,
        int port,
        string senderMail,
        string login,
        string password,
        SecureSocketOptions secureSocketOptions = SecureSocketOptions.Auto) : this(secureSocketOptions)
    {
        if (StringHelper.CheckForNullOrEmpty(host, senderMail, login, password))
        {
            throw new ArgumentException(Exceptions.PassedEmptyParameter);
        }

        if (!senderMail.IsEmail())
        {
            throw new ArgumentException(Exceptions.WrongMailboxFormat);
        }

        Host = host;
        Port = port;
        SenderMail = senderMail;
        Login = login;
        Password = password;
    }
}
