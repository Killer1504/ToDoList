using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListLib.Helper
{
    public static class EncryptHelper
    {
        public static string GetHashMD5(string input, string salt = "")
        {
            using (var md5 = MD5.Create())
            {
                var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input + salt));
                return BytesToStr(bytes);
            }
        }
        public static string GetHashSHA256(string plainText, string salt = "")
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainText + salt));

                // Convert byte array to a string   
                return BytesToStr(bytes);
            }
        }
        public static string GetHashSHA512(string plainText, string salt = "")
        {
            using (var sha256 = SHA512.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainText + salt));

                // Convert byte array to a string   
                return BytesToStr(bytes);
            }
        }
        public static string GetHashSHA1(string plainText, string salt = "")
        {
            using (var sha1 = SHA1.Create())
            {
                byte[] bytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(plainText + salt));

                // Convert byte array to a string   
                return BytesToStr(bytes);
            }
        }

        private static string BytesToStr(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }

        /// <summary>
        /// ma hoa with key do dai 64 ki tu
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string EncryptString(string plainText, string key = "fOQEIP9izO5qNbC8UcZv7J4ciLRkZewc")
        {
            if(key.Length >= 32)
            {
                key = key.Substring(0,32);
            }
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = System.Text.Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return System.Convert.ToBase64String(array);
        }

        /// <summary>
        /// giai ma voi key co do dai 64
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DecryptString(string cipherText, string key = "fOQEIP9izO5qNbC8UcZv7J4ciLRkZewc")
        {
            try
            {
                if (key.Length >= 32)
                {
                    key = key.Substring(0, 32);
                }
                byte[] iv = new byte[16];
                byte[] buffer = System.Convert.FromBase64String(cipherText);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = System.Text.Encoding.UTF8.GetBytes(key);
                    aes.IV = iv;
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream(buffer))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (System.Exception)
            {
                return "";
            }
        }


    }
}
