using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowUITest
{
    public class HttpResponseResultObject<T>
    {
        public string Code { get; set; }
        //Newtonsoft.Json.Linq.JArray
        public T Data { get; set; }
    }
}
