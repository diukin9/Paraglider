using System.ComponentModel;

namespace Paraglider.Domain.Enums
{
    /// <summary>
    /// External auth providers
    /// </summary>
    public enum ExternalAuthProvider
    {
        [Description("None")]
        None = 0,

        [Description("Yandex")]
        Yandex,

        [Description("Vkontakte")]
        Vkontakte
    }
}
