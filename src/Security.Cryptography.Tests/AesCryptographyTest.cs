using System;
using System.IO;
using Xunit;

namespace Tolitech.CodeGenerator.Security.Cryptography.Tests
{
    public class AesCryptographyTest
    {
        [Fact(DisplayName = "AesCryptography - EncryptWithIvAndKeyGenerated - Valid")]
        public void AesCryptography_EncryptWithIvAndKeyGenerated_Valid()
        {
            string key = AesCryptography.GenerateKey();
            string iv = AesCryptography.GenerateIV();

            string textEncrypted = AesCryptography.Encrypt("plainText", key, iv);
            string textDecrypted = AesCryptography.Decrypt(textEncrypted, key, iv);

            Assert.True(textDecrypted == "plainText");
        }

        [Fact(DisplayName = "AesCryptography - EncryptWithIvAndKeyHardCoded - Valid")]
        public void AesCryptography_EncryptWithIvAndKeyHardCoded_Valid()
        {
            string key = "xcy0ZtG2yQrnJMKTXVg/TFp/uYK7pD1EzcymWRsDdCg=";
            string iv = "CHdSIp4eHY8lxpNlHDYn3w==";

            string textEncrypted = AesCryptography.Encrypt("plainText", key, iv);
            string textDecrypted = AesCryptography.Decrypt(textEncrypted, key, iv);

            Assert.True(textDecrypted == "plainText");
        }

        [Fact(DisplayName = "AesCryptography - EncryptBytes - Valid")]
        public void AesCryptography_EncryptBytes_Valid()
        {
            string key = AesCryptography.GenerateKey();
            string iv = AesCryptography.GenerateIV();

            var bytes = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Assets", "image.jpeg"));

            var encrypted = AesCryptography.Encrypt(bytes, key, iv);
            var decrypted = AesCryptography.Decrypt(encrypted, key, iv);

            Assert.True(Equality(decrypted, bytes));
        }

        private bool Equality(byte[] a1, byte[] b1)
        {
            int i;
            if (a1.Length == b1.Length)
            {
                i = 0;

                while (i < a1.Length && (a1[i] == b1[i]))
                    i++;

                if (i == a1.Length)
                    return true;
            }

            return false;
        }
    }
}