namespace Paraglider.MailService;

public static class EmailTemplates
{
    public static string MailConfirmationTemplate(string confirmationLink)
    {
        return @$"Для подтверждения почты перйдите по <a href=""{confirmationLink}"">ссылке</a>";
    }

    public static string PasswordResetTemplate(string confirmationLink)
    {
        return @$"Для смены пароля перейдите по <a href=""{confirmationLink}"">ссылке</a>";
    }
}
