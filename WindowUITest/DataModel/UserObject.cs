using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowUITest
{
    /// <summary>
    /// UserObject
    /// </summary>
    public class UserObject
    {
        public string UserCode { get; set; }

        public string ChineseName { get; set; }

        public string EnglishName { get; set; }

        public string Gender { get; set; }

        public string OfficeNo { get; set; }

        public string PhoneNo { get; set; }

        public string Email { get; set; }

        public DateTime? ChangePasswordDate { get; set; }
    }
}
