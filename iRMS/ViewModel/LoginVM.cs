using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace iRMS
{
    public class LoginVM : ViewModelBase
    {

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
            set { Set(ref lang, value); }
        }

        private string userCode = "abc";
        public string UserCode
        {
            get { return userCode; }
            set { Set(ref userCode, value); }
        }



        public ICommand LoginCommand { get; set; }

        public RelayCommand<object> FormLoad { get; set; }

        public LoginVM()
        {
            LoginCommand = new RelayCommand(loginBtnClick);
            FormLoad = new RelayCommand<object>(formLoad);

        }

        private void loginBtnClick()
        {
            MessageBox.Show("login btn clicked");
            //
            testweakref.starttest();
        }

        private void formLoad(object sender)
        {
            Window win = sender as Window;
            win.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    MessageBox.Show("formLoad");
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
