using System;
using System.Collections.Generic;
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


    }
}
