using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreAppTest
{
    public class Global
    {
        public static string CurrentLangCode = "CHS";

        public static string CurrentLoginLocationCode = "HK992";

        public static string CurrentLoginCompanyCode = "";

        public static string CurrentLoginIP = "192.168.122.244";//backup db

        public static string CurrentLoginType = LoginType.OFFICE;

        public const string IRMSApiUrl = "http://172.18.100.27:8000";
    }
}
