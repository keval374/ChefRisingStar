
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
        internal static string HashPasword(string password, out byte[] salt)
        {
            salt = CreateSalt(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return Convert.ToHexString(hash);
        }

        internal static string HashPaswordAlt(string password, out byte[] salt)
        {
            salt = CreateSalt(keySize);

            Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = deriveBytes.GetBytes(512);
            return Convert.ToHexString(hash);
            //return Convert.ToBase64String(hash);
        }
        
        internal static string HashPaswordWithSalt(string password, byte[] salt)
        {
            if (salt == null || salt.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(salt));
            }
            
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToHexString(hash);
        }

        internal static byte[] CreateSalt(int size)
        {
            //Generate a cryptographic random number.
            byte[] buff = new byte[size];
            using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetNonZeroBytes(buff);
                return buff;
            }
        }
    }
}
