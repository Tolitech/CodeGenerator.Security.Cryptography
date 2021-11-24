using System;
using System.Security.Cryptography;

namespace Tolitech.CodeGenerator.Security.Cryptography
{
    public sealed class AesCryptography
    {
        /// <summary>
        /// Method to encrypt a text.
        /// </summary>
        /// <param name="plainText">plain text</param>
        /// <param name="key">key</param>
        /// <param name="iv">iv</param>
        /// <returns>Encrypted text</returns>
        public static string Encrypt(string plainText, string key, string iv)
        {
            string? data = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Convert.FromBase64String(key);
                aesAlg.IV = Convert.FromBase64String(iv);

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using MemoryStream msEncrypt = new();
                using CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write);
                using (StreamWriter swEncrypt = new(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                }

                data = Convert.ToBase64String(msEncrypt.ToArray());
            }

            return data;
        }

        /// <summary>
        /// Method to decrypt a text.
        /// </summary>
        /// <param name="encryptedText">text</param>
        /// <param name="key">key</param>
        /// <param name="iv">iv</param>
        /// <returns>Decrypted text</returns>
        public static string Decrypt(string encryptedText, string key, string iv)
        {
            string? data = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Convert.FromBase64String(key);
                aesAlg.IV = Convert.FromBase64String(iv);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using MemoryStream msDecrypt = new(Convert.FromBase64String(encryptedText));
                using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
                using StreamReader srDecrypt = new(csDecrypt);
                data = srDecrypt.ReadToEnd();
            }

            return data;
        }

        /// <summary>
        /// Method to encrypt a file.
        /// </summary>
        /// <param name="plainFile">plain file</param>
        /// <param name="key">key</param>
        /// <param name="iv">iv</param>
        /// <returns>Encrypted file</returns>
        public static byte[] Encrypt(byte[] plainFile, string key, string iv)
        {
            byte[]? encryptedFile = null;

            using (var aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(key);
                aes.IV = Convert.FromBase64String(iv);
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using MemoryStream plain = new(plainFile);
                using MemoryStream encrypted = new();
                using (CryptoStream cs = new(encrypted, encryptor, CryptoStreamMode.Write))
                {
                    using var originalByteStream = new MemoryStream(plainFile);
                    int data;

                    while ((data = originalByteStream.ReadByte()) != -1)
                        cs.WriteByte((byte)data);
                }

                encryptedFile = encrypted.ToArray();
            }

            return encryptedFile;
        }

        /// <summary>
        /// Method to decrypt a file.
        /// </summary>
        /// <param name="encryptedFile">encrypted file</param>
        /// <param name="key">key</param>
        /// <param name="iv">iv</param>
        /// <returns>Decrypted file</returns>
        public static byte[] Decrypt(byte[] encryptedFile, string key, string iv)
        {
            byte[]? plainFile = null;

            using (var aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(key);
                aes.IV = Convert.FromBase64String(iv);
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using MemoryStream encrypted = new(encryptedFile);
                using MemoryStream plain = new();
                using (CryptoStream cs = new(encrypted, decryptor, CryptoStreamMode.Read))
                {
                    int data;

                    while ((data = cs.ReadByte()) != -1)
                        plain.WriteByte((byte)data);
                }

                // reset position in prep for reading.
                plain.Position = 0;
                plainFile = ConvertToByteArray(plain);
            }

            return plainFile;
        }

        public static string GenerateKey()
        {
            string key = "";

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.GenerateKey();
                key = Convert.ToBase64String(aesAlg.Key);
            }

            return key;
        }

        public static string GenerateIV()
        {
            string IV = "";

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.GenerateIV();
                IV = Convert.ToBase64String(aesAlg.IV);
            }

            return IV;
        }

        private static byte[] ConvertToByteArray(Stream stream)
        {
            byte[] buffer = new byte[16 * 1024];

            using MemoryStream ms = new();
            int read;
            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                ms.Write(buffer, 0, read);

            return ms.ToArray();
        }
    }
}