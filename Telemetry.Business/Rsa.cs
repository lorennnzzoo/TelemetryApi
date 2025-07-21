using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Telemetry.Business
{
    public class Rsa
    {
        public static class RsaKeyGenerator
        {
            public static (string PublicKey, string PrivateKey) GenerateRsaKeyPair()
            {
                using (var rsa = RSA.Create(2048))
                {
                    // Export keys as Base64-encoded PEM strings
                    string publicKey = Convert.ToBase64String(rsa.ExportSubjectPublicKeyInfo());
                    string privateKey = Convert.ToBase64String(rsa.ExportPkcs8PrivateKey());

                    return (publicKey, privateKey);
                }
            }

            

        }
        public static string DecryptPayload(string base64Encrypted, string base64PrivateKey)
        {
            byte[] encryptedBytes = Convert.FromBase64String(base64Encrypted);
            byte[] privateKeyBytes = Convert.FromBase64String(base64PrivateKey);

            using var rsa = RSA.Create();
            rsa.ImportPkcs8PrivateKey(privateKeyBytes, out _);

            byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, RSAEncryptionPadding.OaepSHA256);
            return Encoding.UTF8.GetString(decryptedBytes);
        }

        public static string EncryptPayload(string jsonPayload, string base64PublicKey)
        {
            byte[] publicKeyBytes = Convert.FromBase64String(base64PublicKey);
            byte[] payloadBytes = Encoding.UTF8.GetBytes(jsonPayload);

            using var rsa = RSA.Create();
            rsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);

            byte[] encryptedBytes = rsa.Encrypt(payloadBytes, RSAEncryptionPadding.OaepSHA256);
            return Convert.ToBase64String(encryptedBytes);
        }

    }
}
