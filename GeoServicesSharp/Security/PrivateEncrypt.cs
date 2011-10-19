using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace GeoServicesSharp.Security
{
    class PrivateEncrypt
    {
        private string PrivateKey
        {
            get
            {
                return Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(System.Environment.MachineName + "VVRJNWRXTXpWbk5rUnpsNVdsaE9TRk5XVGtaaWJVNTVaVmhDTUdGWE9YVlZNbFo1Wkcxc2FscFJQVDA9"));
            }
        }

        public string Encrypt(string input)
        {
            RijndaelManaged AES = new RijndaelManaged();
            MD5CryptoServiceProvider Hash_AES = new MD5CryptoServiceProvider();
            byte[] hash = new byte[31];
            byte[] temp = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(PrivateKey))));
            Array.Copy(temp,0,hash,0,16);
            Array.Copy(temp,0,hash,15,16);
            AES.Key = hash;
            AES.Mode = CipherMode.ECB;
            ICryptoTransform DESEncrypter = AES.CreateEncryptor();
            byte[] buffer = System.Text.ASCIIEncoding.ASCII.GetBytes(input);
            return Convert.ToBase64String(DESEncrypter.TransformFinalBlock(buffer, 0, buffer.Length));
        }

        public string Decrypt(string input)
        {
            RijndaelManaged AES = new RijndaelManaged();
            MD5CryptoServiceProvider Hash_AES = new MD5CryptoServiceProvider();
            try
            {
                byte[] hash = new byte[31];
                byte[] temp = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(PrivateKey))));
                Array.Copy(temp, 0, hash, 0, 16);
                Array.Copy(temp, 0, hash, 15, 16);
                AES.Key = hash;
                AES.Mode = CipherMode.ECB;
                ICryptoTransform DESDecrypter = AES.CreateDecryptor();
                byte[] Buffer = Convert.FromBase64String(input);
                return System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}