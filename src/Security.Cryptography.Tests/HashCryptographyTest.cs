using System;
using Xunit;

namespace Tolitech.CodeGenerator.Security.Cryptography.Tests
{
    public class HashCryptographyTest
    {
        [Theory(DisplayName = "HashCryptography - EncryptPlainTextWithSalt - Valid")]
        [InlineData("plainText", "salt")]
        [InlineData("text", "@#$%")]
        [InlineData("text", "@#$%*()")]
        [InlineData("@#$%*()", "{}~`|")]
        public void HashCryptography_EncryptPlainTextWithSalt_Valid(string plainText, string salt)
        {
            string text1Encrypted = HashCryptography.Encrypt(plainText, salt);
            string text2Encrypted = HashCryptography.Encrypt(plainText, salt);

            Assert.True(text1Encrypted != null);
            Assert.True(text1Encrypted == text2Encrypted);
        }
    }
}
