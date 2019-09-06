using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WinTest
{
    public class LoginUIViewModel : ViewModelBase
    {
        public ICommand LoginCommand { get; set; }

        public RelayCommand<object> FormLoad { get; set; }

        public RelayCommand<ShowTargetViewParam> SelectLocation { get; set; }

        public LoginUIViewModel()
        {
            LoginCommand = new RelayCommand(loginBtnClick);
            FormLoad = new RelayCommand<object>(formLoad);
            //
            SelectLocation = new RelayCommand<ShowTargetViewParam>(selectLocation);
            //
            iniData();
        }

        public List<LanguageObject>LangList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SelectLang
        {
            get
            {
                return Global.CurrentLangCode;
            }
            set
            {
                Global.CurrentLangCode = value;
            }
        }
        
        public bool RoadShowTypeSelected
        {
            get
            {
                return string.Equals(Global.CurrentLoginType, LoginType.ROADSHOW, StringComparison.OrdinalIgnoreCase) ? true : false;
            }
            set
            {
                if (value)
                {
                    if (!string.Equals(Global.CurrentLoginType, LoginType.ROADSHOW, StringComparison.OrdinalIgnoreCase))
                    {
                        Global.CurrentLoginType = LoginType.ROADSHOW;
                    }
                }
                else
                {
                    Global.CurrentLoginType = "";
                }
                RaisePropertyChanged("RoadShowSelected");
            }
        }

        public bool ShopTypeSelected
        {
            get
            {
                return string.Equals(Global.CurrentLoginType, LoginType.SHOP, StringComparison.OrdinalIgnoreCase) ? true : false;
            }
            set
            {
                if (value)
                {
                    if (!string.Equals(Global.CurrentLoginType, LoginType.SHOP, StringComparison.OrdinalIgnoreCase))
                    {
                        Global.CurrentLoginType = LoginType.SHOP;
                    }
                }
                else
                {
                    Global.CurrentLoginType = "";
                }
                RaisePropertyChanged("ShopTypeSelected");
            }
        }

        public bool OfficeTypeSelected
        {
            get
            {
                return string.Equals(Global.CurrentLoginType, LoginType.OFFICE, StringComparison.OrdinalIgnoreCase)?true:false;
            }
            set
            { 
                if (value)
                {
                    if (!string.Equals(Global.CurrentLoginType, LoginType.OFFICE,StringComparison.OrdinalIgnoreCase))
                    {
                        Global.CurrentLoginType = LoginType.OFFICE;
                    }                    
                }
                else
                {
                    Global.CurrentLoginType = "";
                }
                RaisePropertyChanged("OfficeTypeSelected");
            }
        }

        public string LoginLocationCode
        {
            get { return Global.CurrentLoginLocationCode; }
            set
            {
                Global.CurrentLoginLocationCode = value;
                RaisePropertyChanged("LoginLocationCode");
            }
        }


        private string irmsVersion = "版本:V1.1.1.1";
        public string IrmsVersion
        {
            get { return irmsVersion; }
            set { irmsVersion = value; }
        }

        private string loginIP = "网络位置:192.168.122.246";
        public string LoginIP
        {
            get { return loginIP; }
            set { loginIP = value; }
        }

        private string dbIP = "数据服务:192.168.122.246";
        public string DBIP
        {
            get { return dbIP; }
            set { dbIP = value; }
        }

        private string lang = "CHS";
        public string Lang
        {
            get { return lang; }
            set
            {
                Set(ref lang, value);
            }
        }

        private string userCode = "USER";
        public string UserCode
        {
            get { return userCode; }
            set
            {
                Set(ref userCode, value);
            }
        }

        /// <summary>
        /// iniData
        /// </summary>
        private void iniData()
        {

            LangList = new List<LanguageObject>();
            LangList.Add(new LanguageObject { LanguageCode = "CHS", LanguageName = "简体中文" });
            LangList.Add(new LanguageObject { LanguageCode = "CHI", LanguageName = "繁體中文" });
            LangList.Add(new LanguageObject { LanguageCode = "ENG", LanguageName = "英文" });
        }

        private void loginBtnClick()
        {
            System.Windows.MessageBox.Show("login btn clicked");
            //
            testweakref.starttest();
        }

        private void selectLocation(ShowTargetViewParam viewParam)
        {
            if (viewParam == null)
            {
                viewParam = new ShowTargetViewParam();
            }
            //
            TargetViewCreateMessage param = TargetViewCreateMessage.CreateInstance("LoginType", viewParam);
            //
            Messenger.Default.Send(param, MessageTokens.ShowLoginLocationSelectUI);            
        }

        private void formLoad(object sender)
        {
            Window win = sender as Window;
            win.Dispatcher.BeginInvoke((Action)delegate ()
            {
                //MessageBox.Show("formLoad");
            }, null);
            //System.Threading.Thread th = new System.Threading.Thread(() =>
            //{
            //    win.Dispatcher.BeginInvoke((Action)delegate ()
            //    {
            //        MessageBox.Show("formLoad");
            //    }, null);

            //});
            //th.IsBackground = true;
            //th.Start();
        }
    }
}
