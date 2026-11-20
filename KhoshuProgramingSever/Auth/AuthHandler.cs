using System.Text;
using Microsoft.IdentityModel.Tokens;
namespace KhoshuProgramingSever.Auth;

public static class AuthHandler
{
    public const String superPuperMegaSecuredKey = "AdolfNigger1488_putinBombaBahChechna";
    public const String validUser = "Khonshu";
    public const String validAud = "KhonshuAi";

    public static SymmetricSecurityKey GetKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(superPuperMegaSecuredKey));
}
