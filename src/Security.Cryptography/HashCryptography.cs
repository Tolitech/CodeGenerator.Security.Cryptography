using System;
using System.Security.Cryptography;
using System.Text;

namespace Tolitech.CodeGenerator.Security.Cryptography
{
    public sealed class HashCryptography
    {
        /// <summary>
        /// Method to encrypt a text.
        /// </summary>
        /// <param name="plainText">plain text</param>
        /// <param name="salt">salt</param>
        /// <returns>Encrypted text</returns>
        public static string Encrypt(string plainText, string salt)
        {
            string? data = null;

            using (var provider = SHA512.Create())
            {
                byte[] cryptoByte = provider.ComputeHash(Encoding.ASCII.GetBytes(plainText + salt));
                data = BitConverter.ToString(cryptoByte).Replace("-", "").ToLower();
            }

            return data;
        }
    }
}