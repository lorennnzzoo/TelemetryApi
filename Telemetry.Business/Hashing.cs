using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Telemetry.Business
{
    public class Hashing
    {
        public static string ComputeSha256Hash(string rawData)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
            }
        }

        public static bool Verify(string inputPassword, string storedHash)
        {
            var inputHash = ComputeSha256Hash(inputPassword); // implement with salt if possible
            return inputHash == storedHash;
        }


    }

}
