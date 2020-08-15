using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Extension
{
    public class Utilities
    {
        private readonly string PasswordHash = "P@@Sw0rd";
        private static readonly string SaltKey = "S@LT&KEY";
        private static readonly string VIKey = "@1B2c3D4e5F6g7H8";
        public string EncryptMD5(string input)
        {
            string result = "";
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                result = sb.ToString();
            }
            return result;
        }

        public string Encrypt(string plainText)
        {
            byte[] buffer3;
            if (plainText !=null)
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(plainText);
                byte[] buffer2 = new System.Security.Cryptography.Rfc2898DeriveBytes(PasswordHash, System.Text.Encoding.ASCII.GetBytes(SaltKey)).GetBytes(0x20);
                System.Security.Cryptography.RijndaelManaged managed2 = new System.Security.Cryptography.RijndaelManaged();
                managed2.Mode = System.Security.Cryptography.CipherMode.CBC;
                //                managed2.set_Mode(System.Security.Cryptography.CipherMode.CBC);
                //                managed2.set_Padding(System.Security.Cryptography.PaddingMode.Zeros);
                managed2.Padding = System.Security.Cryptography.PaddingMode.Zeros;
                System.Security.Cryptography.ICryptoTransform transform = managed2.CreateEncryptor(buffer2, System.Text.Encoding.ASCII.GetBytes(VIKey));
                using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                {
                    using (System.Security.Cryptography.CryptoStream stream2 = new System.Security.Cryptography.CryptoStream(stream, transform, System.Security.Cryptography.CryptoStreamMode.Write))
                    {
                        stream2.Write(bytes, 0, (int)bytes.Length);
                        stream2.FlushFinalBlock();
                        buffer3 = stream.ToArray();
                        stream2.Close();
                    }
                    stream.Close();
                }
                return System.Convert.ToBase64String(buffer3);
            }
           else
            {
                return "";
            }
           
        }
        public string Decrypt(string encryptedText)
        {
            byte[] buffer = System.Convert.FromBase64String(encryptedText);
            byte[] bytes = new System.Security.Cryptography.Rfc2898DeriveBytes(PasswordHash, System.Text.Encoding.ASCII.GetBytes(SaltKey)).GetBytes(0x20);
            System.Security.Cryptography.RijndaelManaged managed2 = new System.Security.Cryptography.RijndaelManaged();
            managed2.Mode = System.Security.Cryptography.CipherMode.CBC;
            managed2.Padding = System.Security.Cryptography.PaddingMode.None;
            //                managed2.set_Mode(System.Security.Cryptography.CipherMode.CBC);
            //                managed2.set_Padding(System.Security.Cryptography.PaddingMode.None);
            System.Security.Cryptography.ICryptoTransform transform = managed2.CreateDecryptor(bytes, System.Text.Encoding.ASCII.GetBytes(VIKey));
            System.IO.MemoryStream stream = new System.IO.MemoryStream(buffer);
            System.Security.Cryptography.CryptoStream stream2 = new System.Security.Cryptography.CryptoStream(stream, transform, System.Security.Cryptography.CryptoStreamMode.Read);
            byte[] buffer3 = new byte[buffer.Length];
            int num = stream2.Read(buffer3, 0, (int)buffer3.Length);
            stream.Close();
            stream2.Close();
            return System.Text.Encoding.UTF8.GetString(buffer3, 0, num).TrimEnd("\0".ToCharArray());
        }
    }
}
