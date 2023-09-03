using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FileSafe.Handlers
{
    internal class EncryptionFiles
    {

        private static readonly string passphrase = Settings.Passphrase; // Replace this with your secure passphrase
        private static readonly byte[] salt = new byte[16]; // Replace this with a randomly generated salt

        private static readonly int iterations = 10000; // Number of iterations for the KDF
        private static readonly int keySize = 256; // Key size in bits (32 bytes)
        private static readonly int ivSize = 128; // IV size in bits (16 bytes)

        public static byte[] GenerateKey()
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(passphrase, salt, iterations))
            {
                return deriveBytes.GetBytes(keySize / 8);
            }
        }

        public static byte[] GenerateIV()
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(passphrase, salt, iterations))
            {
                return deriveBytes.GetBytes(ivSize / 8);
            }
        }

        public static void EncryptFile(string inputFile, string outputFile)
        {
            try
            {
                byte[] key = GenerateKey();
                byte[] iv = GenerateIV();

                using (var aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;

                    using (var inputStream = new FileStream(inputFile, FileMode.Open))
                    using (var outputStream = new FileStream(outputFile, FileMode.Create))
                    using (var encryptor = aes.CreateEncryptor())
                    using (var cryptoStream = new CryptoStream(outputStream, encryptor, CryptoStreamMode.Write))
                    {
                        inputStream.CopyTo(cryptoStream);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Encryption failed: {ex.Message}");
            }
        }

        public static void DecryptFile(string inputFile, string outputFile)
        {
            try
            {
                byte[] key = GenerateKey();
                byte[] iv = GenerateIV();

                using (var aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;

                    using (var inputStream = new FileStream(inputFile, FileMode.Open))
                    using (var outputStream = new FileStream(outputFile, FileMode.Create))
                    using (var decryptor = aes.CreateDecryptor())
                    using (var cryptoStream = new CryptoStream(inputStream, decryptor, CryptoStreamMode.Read))
                    {
                        cryptoStream.CopyTo(outputStream);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Decryption failed: {ex.Message}");
            }
        }

    }
}
