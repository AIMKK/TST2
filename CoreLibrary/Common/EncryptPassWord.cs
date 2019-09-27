using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary
{
    public class EncryptPassWord
    {
        //string str = EncryptPassWord.CreateSalt();
        //string password = EncryptPassWord.EncryptPwd("123", str);
        //string password2 = EncryptPassWord.EncryptPwd("456", str);
        //if (PassWord.Trim() != EncryptPassWord.EncryptPwd(this.txtPwd.Text.Trim(), userTemp.Salt))

        /// <summary>
        /// 获取密钥
        /// </summary>
        /// <returns></returns>
        public static string CreateSalt()
        {
            byte[] data = new byte[8];
            new RNGCryptoServiceProvider().GetBytes(data);
            return Convert.ToBase64String(data);
        }

        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="pwdString"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string EncryptPwd(string pwdString, string salt)
        {
            if (salt == null || salt == "")
            {
                return pwdString;
            }
            byte[] bytes = Encoding.Unicode.GetBytes(salt.ToLower().Trim() + pwdString.Trim());
            string pwd = BitConverter.ToString(((HashAlgorithm)CryptoConfig.CreateFromName("SHA1")).ComputeHash(bytes));
            if (pwd != null)
            {
                pwd = pwd.Replace("-", "");
            }
            return pwd;
        }
    }
}
