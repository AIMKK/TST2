using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WindowUI
{

    /// <summary>
    /// DES
    /// </summary>
    public class DesDisturb
    {
        private readonly string rgbKey;
        private readonly string rgbIV;

        /// <summary>
        /// DesDisturb
        /// </summary>
        /// <param name="byKey"></param>
        /// <param name="byIV"></param>
        public DesDisturb(string byKey, string byIV)
        {
            rgbKey = byKey;
            rgbIV = byIV;
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string DesEncrypt(string data)
        {

            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(rgbKey);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(rgbIV);

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            int i = cryptoProvider.KeySize;
            MemoryStream ms = new MemoryStream();
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

            StreamWriter sw = new StreamWriter(cst);
            sw.Write(data);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);

        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string DesDecrypt(string data)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(rgbKey);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(rgbIV);

            byte[] byEnc;
            try
            {
                byEnc = Convert.FromBase64String(data);
            }
            catch
            {
                return null;
            }

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(byEnc);
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cst);
            return sr.ReadToEnd();
        }

    }
}
