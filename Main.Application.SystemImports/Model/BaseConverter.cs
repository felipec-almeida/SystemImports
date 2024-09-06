using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Main.Application.SystemImports.Model
{
    public static class BaseConverter
    {
        public static string Encrypt(string inputText, byte[] key, byte[] iv)
        {
            using Aes aesEncryption = Aes.Create();
            aesEncryption.IV = iv;
            aesEncryption.Key = key;

            using var encryptor = aesEncryption.CreateEncryptor(aesEncryption.Key, aesEncryption.IV);
            using MemoryStream memoryStream = new MemoryStream();
            using CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
            {
                streamWriter.Write(inputText);
            }

            return Convert.ToBase64String(memoryStream.ToArray());
        }

        public static string Decrypt(string encryptedText, byte[] key, byte[] iv)
        {
            // byte[] byteEncryptedText = Convert.ToByte(encryptedText);

            using Aes aesDecryption = Aes.Create();
            aesDecryption.IV = iv;
            aesDecryption.Key = key;

            using var decryptor = aesDecryption.CreateDecryptor(aesDecryption.Key, aesDecryption.IV);
            using MemoryStream memoryStream = new MemoryStream();
            using CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using StreamReader streamReader = new StreamReader(cryptoStream);
            return streamReader.ReadToEnd();
        }
    }
}
