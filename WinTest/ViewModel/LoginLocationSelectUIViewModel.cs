using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinTest
{
    public class LoginLocationSelectUIViewModel: ViewModelBase
    {
        public LoginLocationSelectUIViewModel()
        {
            iniData();
        }

        private void iniData()
        {
            LoctionLst=RemoteApi.GetAccessShop(Global.CurrentLoginIP, Global.CurrentLoginType, Global.CurrentLangCode);
        }
        //
        public List<UserAccessObject> LoctionLst { get; set; }
        //
        private void btnConfirm()
        {

        }

    }
}
