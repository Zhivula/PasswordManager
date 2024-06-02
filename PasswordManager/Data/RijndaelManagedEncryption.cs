using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace PasswordManager.Data
{
    public class RijndaelManagedEncryption
    {
        public static byte[] EncryptData(byte[] data, string password)
        {
            SymmetricAlgorithm sa = Aes.Create();
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] IV = new byte[sha512.ComputeHash(Encoding.UTF8.GetBytes(password)).Length / 4];
                for (var i = 0; i < IV.Length; i++)
                {
                    IV[i] = sha512.ComputeHash(Encoding.UTF8.GetBytes(password))[i];
                }
                sa.IV = IV;
            }
            Rfc2898DeriveBytes hasher = new Rfc2898DeriveBytes(password,sa.IV, 1000);
            sa.Key = hasher.GetBytes(32);


            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, sa.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                }
                return ms.ToArray();
            }
        }

        public static string EncryptText(string text, string password)
        {
            return Convert.ToBase64String(EncryptData(Encoding.UTF8.GetBytes(text), password));
        }

        public static byte[] DecryptData(byte[] data, string password)
        {
            SymmetricAlgorithm sa = Aes.Create();
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] IV = new byte[sha512.ComputeHash(Encoding.UTF8.GetBytes("12345678")).Length / 4];
                for (var i = 0; i < IV.Length; i++)
                {
                    IV[i] = sha512.ComputeHash(Encoding.UTF8.GetBytes("12345678"))[i];
                }
                sa.IV = IV;
            }
            Rfc2898DeriveBytes hasher = new Rfc2898DeriveBytes("12345678", sa.IV, 1000);
            sa.Key = hasher.GetBytes(32);

            var decryptor = sa.CreateDecryptor(sa.Key, sa.IV);
            using (var dataStream = new MemoryStream(data))
            {
                using (var cryptoStream = new CryptoStream(dataStream, decryptor, CryptoStreamMode.Read))
                {
                    using (var decryptedStream = new MemoryStream())
                    {
                        cryptoStream.CopyTo(decryptedStream);
                        return decryptedStream.ToArray();
                    }
                }
            }
        }

        public static string DecryptText(string text, string password)
        {
            return Encoding.UTF8.GetString(DecryptData(Convert.FromBase64String(text), password));
        }
    }
}