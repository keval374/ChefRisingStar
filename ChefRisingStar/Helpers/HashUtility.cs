
using System;
using System.Security.Cryptography;
using System.Text;

namespace LTDCWebservice.Utilities
{
    public class HashUtility
    {
        const int keySize = 64;
        const int iterations = 350000;
        static HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        internal static string HashPasword(string password, out string salt)
        {
            byte[] saltBytes = CreateSalt(keySize);
            salt = Convert.ToBase64String(saltBytes);

            Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, iterations);
            byte[] hash = deriveBytes.GetBytes(512);
            return Convert.ToBase64String(hash);
        }

        internal static byte[] CreateSalt(int size)
        {
            //Generate a cryptographic random number.
            byte[] buff = new byte[size];
            RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetNonZeroBytes(buff);

            // Return a Base64 string representation of the random number.
            return buff;
        }
    }
}
