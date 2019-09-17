using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowUITest
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
            var obj = new { IPAddress = iPAddress, ShopNature = shopNature, LanguageCode = languageCode };
            //
            IRMSApiReqParam param = new IRMSApiReqParam();
            param.Path = string.Format("{0}/irmsapi/getaccessshop/", Global.IRMSApiUrl);
            param.JsonData = JsonConvert.SerializeObject(obj);
            return param;
        }
    }
}
