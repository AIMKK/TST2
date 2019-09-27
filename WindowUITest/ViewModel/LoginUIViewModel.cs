using FirstFloor.ModernUI.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WindowUI;


namespace WindowUITest
{
    public class LoginUIViewModel : ViewModelBase
    {
        public RelayCommand<OpenNewViewParam> LoginCommand { get; set; }

        public RelayCommand<Window> FormLoadedCommand { get; set; }

        public RelayCommand<Window> FormClosedCommand { get; set; }

        public RelayCommand<OpenNewViewParam> ShowLoginLocationSelect { get; set; }

        public LoginUIViewModel()
        {
            LoginCommand = new RelayCommand<OpenNewViewParam>(loginBtnClick);
            //
            FormLoadedCommand = new RelayCommand<Window>(formLoaded);
            FormClosedCommand = new RelayCommand<Window>(formClosed);
            //
            ShowLoginLocationSelect = new RelayCommand<OpenNewViewParam>(showLoginLocationSelect);
            //
            iniData();
        }

        public List<LanguageObject>LangList { get; set; }

        /// <summary>
        /// SelectLang
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
        
        /// <summary>
        /// LoginLocationCode
        /// </summary>
        public string LoginLocationCode
        {
            get { return Global.CurrentLoginLocationCode; }
            set
            {
                Global.CurrentLoginLocationCode = value;
                RaisePropertyChanged("LoginLocationCode");
            }
        }

        /// <summary>
        /// LoginLocationType
        /// </summary>
        public string LoginLocationType
        {
            get { return Global.CurrentLoginType; }
            set
            {
                if (string.Equals(value, "INTERNET", StringComparison.OrdinalIgnoreCase)||
                    string.Equals(value, "WAREHOUSE", StringComparison.OrdinalIgnoreCase))
                {
                    Global.CurrentLoginType = LoginType.OFFICE;
                }
                else if(string.Equals(value, "RETAIL", StringComparison.OrdinalIgnoreCase))
                {
                    Global.CurrentLoginType = LoginType.SHOP;
                }
                else if (string.Equals(value, "ROADSHOW", StringComparison.OrdinalIgnoreCase))
                {
                    Global.CurrentLoginType = LoginType.ROADSHOW;
                }
                else
                {
                    Global.CurrentLoginType = "";
                }
                
                RaisePropertyChanged("LoginLocationType");
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

        private string userCode = "";
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

        /// <summary>
        /// loginBtnClick
        /// </summary>
        /// <param name="viewParam"></param>
        private void loginBtnClick(OpenNewViewParam viewParam)
        {
            if (viewParam==null)
            {
                return;
            }
            //
            try
            {
                //var v=(System.Security.SecureString)(viewParam.NewViewInitValue);
                //IntPtr p = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(v);

                //// 使用.NET内部算法把IntPtr指向处的字符集合转换成字符串  
                //string password = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(p);

                string password = "";
                System.Windows.Controls.PasswordBox pwdBox = viewParam.ParamValueFromCurrentView  as System.Windows.Controls.PasswordBox;
                if (pwdBox!=null)
                {                    
                    password = pwdBox.Password;
                }
                //
                if ((this.userCode??"").Trim().Length==0)
                {
                    ModernDialog.ShowMessage("用户名不能空白！", "notice", System.Windows.MessageBoxButton.OK, viewParam.CurrentView);
                    return;
                }
                //
                if ((this.LoginLocationCode ?? "").Trim().Length == 0|| (this.LoginLocationType ?? "").Trim().Length == 0)
                {
                    ModernDialog.ShowMessage("请选择登录店铺位置！", "notice", System.Windows.MessageBoxButton.OK, viewParam.CurrentView);
                    return;
                }
                //check user
                UserObject userobj =RemoteApi.UserLogin(this.userCode, password);
                if (userobj == null||!string.Equals(userobj.UserCode,this.userCode,StringComparison.OrdinalIgnoreCase))//说明不成功
                {
                    ModernDialog.ShowMessage("登录失败，请检查用户名和密码是否正确！", "notice", System.Windows.MessageBoxButton.OK, viewParam.CurrentView);
                    return;
                }
                //说明成功，记录数据
                OpenNewView newView = new OpenNewView(viewParam.NewViewType);
                newView.Show();
            }
            catch (Exception ex)
            {
                WindowUI.NlogHelper.LogToFile(ex.ToString());
                return;
            }
            //
            if (viewParam.CurrentView!=null)
            {
                //viewParam.CurrentView.Close();
            }
        }

        /// <summary>
        /// showLoginLocationSelect
        /// </summary>
        /// <param name="viewParam"></param>
        private void showLoginLocationSelect(OpenNewViewParam viewParam)
        {
            if (viewParam == null)
            {
                return;
            }
            //
            OpenNewView targetView = new OpenNewView(viewParam.NewViewType);
            //
            LoginLocationSelectUIViewModel loginLocSelectVM= targetView.GetViewDataContext<LoginLocationSelectUIViewModel>();
            if (loginLocSelectVM!=null)
            {
                loginLocSelectVM.LoginType = viewParam.ParamValueFromCurrentView as string;
            }
            //
            
            if (targetView.ShowDialog(viewParam.CurrentView) == true)
            {
                //
                LoginLocationCode = loginLocSelectVM.SelectedUserAccess.LocationCode;
                //
                LoginLocationType = loginLocSelectVM.SelectedUserAccess.ShopNature;
                //
                Global.CurrentLoginCompanyCode = loginLocSelectVM.SelectedUserAccess.CompanyCode;
            }
        }

        /// <summary>
        /// formLoaded
        /// </summary>
        /// <param name="win"></param>
        private void formLoaded(Window win)
        {
            if (win != null)
            {
                //Messenger.Default.Register<TargetViewCreateMessage>(win, MessageTokens.ShowLoginLocationSelectUI, showTargetView);
                
            }
        }

        /// <summary>
        /// formClosed
        /// </summary>
        /// <param name="win"></param>
        private void formClosed(Window win)
        {
            if (win != null)
            {
                //Messenger.Default.Unregister<TargetViewCreateMessage>(win, MessageTokens.ShowLoginLocationSelectUI, showTargetView);
                win.Close();
            }
        }
        
    }
}
