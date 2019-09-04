using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinTest
{
    public class HttpResponseResultObject
    {
        public string Code { get; set; }

        public Newtonsoft.Json.Linq.JArray Data { get; set; }
    }
}
