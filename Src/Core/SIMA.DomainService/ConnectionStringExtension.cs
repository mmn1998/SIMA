using Microsoft.Extensions.Configuration;
using SIMA.DomainService;
using SIMA.Framework.Common.Helper.RSA;

namespace SIMA.DomainService;

public static class ConnectionStringExtension
{
    public static string GetConnectionString(this IConfiguration configuration)
    {
        string CipherConnectionString = configuration.GetConnectionString("UserManagementCipher") ?? "";
        string SignConnectionString = configuration.GetConnectionString("UserManagementSign") ?? "";
        string privateKeyPath = configuration.GetValue<string>("PrivateKeyPath") ?? "";
        string publicKeyPath = configuration.GetValue<string>("PublicKeyPath") ?? "";
        var signResult = EncryptThenSignWithRSAHelper.SingVerify(new RSAVerfySignRequest
        {
            PrivateKeyPath = privateKeyPath,
            PublicKeyPath = publicKeyPath,
            CipherText = CipherConnectionString,
            SignedText = SignConnectionString,
        });
        string connectionString = string.Empty;
        if (signResult.SignResult)
        {
            connectionString = signResult.DecriptedValue;
        }
        return connectionString;
    }
}
