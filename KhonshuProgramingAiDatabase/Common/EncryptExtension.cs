using Khonshu.Encrypted;
namespace KhonshuProgramingAiDatabase.Common;

public static class EncryptExtension
{
    private static readonly Shifrovalshik Coder;

    static EncryptExtension()
    {
        HelpKhonshu helper = new(Constant.Key, Constant.Salt);
        Coder =  new Shifrovalshik(helper);
    }
    extension(String text)
    {
        public String Encrypt() => Coder.Encrypt(text);
        public String Decrypt() => Coder.Decrypt(text);
        public async Task<String> EncryptAsync() =>  await Coder.EncryptAsync(text);
        public async Task<String> DecryptAsync() =>  await Coder.DecryptAsync(text);
    }
}
