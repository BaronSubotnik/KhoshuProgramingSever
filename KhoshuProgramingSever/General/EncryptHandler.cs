using Khonshu.Encrypted;
namespace KhoshuProgramingSever.General;

public sealed class EncryptHandler
{
    private readonly Shifrovalshik coder;

    public EncryptHandler(String superPuperMegaSecuredKey, String superSecretSalt)
    {
        HelpKhonshu helper = new(superPuperMegaSecuredKey, superSecretSalt);
        coder = new Shifrovalshik(helper);
    }


    public String Encrypt(String text) => coder.Encrypt(text);
    public String Decrypt(String text) =>  coder.Decrypt(text);
    public async Task<String> EncryptAsync(String text) => await coder.EncryptAsync(text);
    public async Task<String> DecryptAsync(String text) => await coder.DecryptAsync(text);

}
