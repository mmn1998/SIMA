using SIMA.Framework.Common.Helper.RSA;

namespace SIMA.WebApi.Extensions;

public static class ConfigurationExtension
{
    public static string GetDecriptedValue(this IConfiguration configuration, string cipherText, string signedText)
    {
        string privateKeyPath = configuration.GetValue<string>("PrivateKeyPath") ?? "";
        string publicKeyPath = configuration.GetValue<string>("PublicKeyPath") ?? "";
        var signResult = EncryptThenSignWithRSAHelper.SingVerify(new RSAVerfySignRequest
        {
            PrivateKeyPath = privateKeyPath,
            PublicKeyPath = publicKeyPath,
            CipherText = cipherText,
            SignedText = signedText,
        });
        string DecriptedValue = string.Empty;
        if (signResult.SignResult)
        {
            DecriptedValue = signResult.DecriptedValue;
        }
        return DecriptedValue;
    }
}
