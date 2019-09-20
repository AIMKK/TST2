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
            HttpResponseResultObject<object> responseObj = null;
            try
            {
                result = new List<UserAccessObject>();

                IRMSApiReqParam apiParam = CreateIRMSApiReqParam.CreateGetAccessShopParam(loginIPAddress, loginShopNature, languageCode);
                if (apiParam != null)
                {
                    responseResult = HttpService.Post(apiParam.JsonData, apiParam.Path, 6000);
                    responseObj = JsonConvert.DeserializeObject<HttpResponseResultObject<object>>(responseResult);
                    if (responseObj != null && responseObj.Code == SuccessCode)
                    {
                        Newtonsoft.Json.Linq.JArray arrData = responseObj.Data  as Newtonsoft.Json.Linq.JArray;
                        if (arrData != null && arrData.Count > 0)
                        {
                            foreach (Newtonsoft.Json.Linq.JObject jobj in arrData)
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
                WindowUI.NlogHelper.LogToFile(ex.ToString());
            }
            return result;
        }

        /// <summary>
        /// UserLogin
        /// </summary>
        /// <param name="usercode"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static UserObject UserLogin(string usercode,string pwd)
        {
            UserObject userObj = null;
            string responseResult = "";
            HttpResponseResultObject<object> responseObj = null;            
            try
            {
                IRMSApiReqParam apiParam = CreateIRMSApiReqParam.CreateUserLoginParam(usercode, pwd);
                if (apiParam != null)
                {
                    responseResult = HttpService.Post(apiParam.JsonData, apiParam.Path, 6000);
                    responseObj = JsonConvert.DeserializeObject<HttpResponseResultObject<object>>(responseResult);
                    if (responseObj != null && responseObj.Code == SuccessCode)
                    {
                        Newtonsoft.Json.Linq.JObject jObj=responseObj.Data as Newtonsoft.Json.Linq.JObject;
                        if (jObj != null)
                        {
                            userObj = jObj.ToObject<UserObject>();                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                userObj = null;
                WindowUI.NlogHelper.LogToFile(ex.ToString());
            }
            //
            return userObj;
        }
    }
}
