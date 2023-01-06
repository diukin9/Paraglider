using System.Net.Sockets;
using System.Net;

namespace Paraglider.MobileApp;

public static class Constants
{
    public static string REST_URL => "http://192.168.0.146:8099/api/v1";

    public const string REFRESH_TOKEN_KEY = "refresh_token";
    public const string REFRESH_TOKEN_EXPIRY_TIME_KEY = "refresh_token_expiry_time";

    public const string ACCESS_TOKEN_KEY = "access_token";
    public const string ACCESS_TOKEN_EXPIRY_TIME_KEY = "access_token_expiry_time";
}
