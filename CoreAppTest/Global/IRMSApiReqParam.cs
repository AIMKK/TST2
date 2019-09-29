using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreAppTest
{
    /// <summary>
    /// IRMSApiReqParam
    /// </summary>
    public class IRMSApiReqParam
    {
        public string Path { get; set; }

        public string JsonData { get; set; }
        
    }

    /// <summary>
    /// CreateIRMSApiReqParam
    /// </summary>
    public class CreateIRMSApiReqParam
    {
        /// <summary>
        /// CreateGetAccessShopParam
        /// </summary>
        /// <param name="iPAddress"></param>
        /// <param name="shopNature"></param>
        /// <param name="languageCode"></param>
        /// <returns></returns>
        public static IRMSApiReqParam CreateGetAccessShopParam(string iPAddress,string shopNature,string languageCode)
        {
            IRMSApiReqParam param = null;
            try
            {
                var obj = new { IPAddress = iPAddress, ShopNature = shopNature, LanguageCode = languageCode };
                //
                param = new IRMSApiReqParam();
                param.Path = string.Format("{0}/irmsapi/getaccessshop/", Global.IRMSApiUrl);
                param.JsonData = JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                CoreLibrary.NlogHelper.LogToFile(ex.ToString());
            }
           
            return param;
        }

        /// <summary>
        /// CreateUserLoginParam
        /// </summary>
        /// <param name="usercode"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static IRMSApiReqParam CreateUserLoginParam(string usercode, string pwd)
        {
            IRMSApiReqParam param = null;
            try
            {
                var obj = new { UserCode = usercode, Password = pwd };
                //
                param = new IRMSApiReqParam();
                param.Path = string.Format("{0}/irmsapi/login/", Global.IRMSApiUrl);
                param.JsonData = JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                CoreLibrary.NlogHelper.LogToFile(ex.ToString());
            }
            //
            return param;
        }
    }
}
