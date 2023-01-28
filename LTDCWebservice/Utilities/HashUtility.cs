
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
        public static string HashPasword(string password, out string salt)
        {
            byte[] saltBytes = CreateSalt(keySize);
            salt = Convert.ToBase64String(saltBytes);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                saltBytes,
                iterations,
                hashAlgorithm,
                keySize);
            return Convert.ToBase64String(hash);
        }

        public static string HashPaswordAlt(string password, out byte[] salt)
        {
            salt = CreateSalt(keySize);

            Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = deriveBytes.GetBytes(512);
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Hash a password with a known salt
        /// </summary>
        /// <param name="password">Raw password</param>
        /// <param name="salt">Pre-computed salt</param>
        /// <returns>Hash result</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string HashPaswordWithSalt(string password, string salt)
        {
            if (salt == null || salt.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(salt));
            }

            byte[] saltBytes = Convert.FromBase64String(salt);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                saltBytes,
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToBase64String(hash);
        }

        public static byte[] CreateSalt(int size)
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
