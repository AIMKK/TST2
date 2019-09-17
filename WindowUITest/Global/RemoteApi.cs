using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowUITest
{
    public class RemoteApi
    {
        private const string SuccessCode = "200";

        /// <summary>
        /// GetAccessShop
        /// </summary>
        /// <param name="loginIPAddress"></param>
        /// <param name="loginShopNature"></param>
        /// <param name="languageCode"></param>
        /// <returns></returns>
        public static List<UserAccessObject> GetAccessShop(string loginIPAddress, string loginShopNature, string languageCode)
        {
            List<UserAccessObject> result = null;
            string responseResult = "";
            UserAccessObject userAccessObj = null;
            HttpResponseResultObject responseObj = null;
            try
            {
                result = new List<UserAccessObject>();
              
                IRMSApiReqParam apiParam = CreateIRMSApiReqParam.CreateGetAccessShopParam(loginIPAddress, loginShopNature, languageCode);
                if (apiParam != null)
                {
                    responseResult = HttpService.Post(apiParam.JsonData, apiParam.Path, 6000);
                    responseObj = JsonConvert.DeserializeObject<HttpResponseResultObject>(responseResult);
                    if (responseObj != null && responseObj.Code == SuccessCode)
                    {
                        if (responseObj.Data != null && responseObj.Data.Count > 0)
                        {
                            foreach (Newtonsoft.Json.Linq.JObject jobj in responseObj.Data)
                            {
                                userAccessObj = jobj.ToObject<UserAccessObject>();
                                if (userAccessObj != null)
                                {
                                    result.Add(userAccessObj);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
    }
}
