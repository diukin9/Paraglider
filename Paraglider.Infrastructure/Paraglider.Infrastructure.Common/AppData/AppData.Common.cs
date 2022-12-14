﻿namespace Paraglider.Infrastructure.Common;

public static partial class AppData
{
    public const string ServiceName = "Paraglider";

    public const string TypingsNamespace = "Paraglider.Web.Endpoints";

    public const string DefaultRuleSetName = "default";
    public const string ExternalAuthHandlerRelativePath = "/external-auth-handler";

    //TODO: указать url'ы
    public const string RedirectOnSuccessfulMailConfirmation = "/";
    public const string RedirectPasswordResetPath = "/";

    public const string DefaultCityName = "Москва";

    //TODO найти базовый аватар
    public const string DefaultAvatarUrl = "https://ie.wampi.ru/2022/12/14/Avatarc84d942c6fb6b199.png";

    public const string MongoIdName = "_id";
}