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

    }
}
