using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinTest
{
    public class LoginLocationSelectUIViewModel: ViewModelBase
    {
        public RelayCommand<object> FormUnloaded { get; set; }

        public RelayCommand<object> FormLoaded { get; set; }

        public string LoginType { get; set; }

        public LoginLocationSelectUIViewModel()
        {
            FormUnloaded = new RelayCommand<object>(formUnloaded);

            FormLoaded = new RelayCommand<object>(formLoaded);
            LoginType = Global.CurrentLoginType;
        }

        /// <summary>
        /// formLoaded
        /// </summary>
        /// <param name="sender"></param>
        private void formLoaded(object sender)
        {
            iniData();
        }

        private void iniData()
        {
            LoctionLst = RemoteApi.GetAccessShop(Global.CurrentLoginIP, LoginType, Global.CurrentLangCode);
        }
        //ObservableCollection
        private List<UserAccessObject> loctionLst;
        public List<UserAccessObject> LoctionLst
        {
            get
            {
                return loctionLst;
            }
            set
            {
                loctionLst = value;
                RaisePropertyChanged("LoctionLst");
            }
        }
        //
        private void btnConfirm()
        {

        }

        /// <summary>
        /// formUnloaded
        /// </summary>
        /// <param name="sender"></param>
        private void formUnloaded(object sender)
        {
            if (SimpleIoc.Default.IsRegistered<LoginLocationSelectUIViewModel>())
            {
                SimpleIoc.Default.Unregister<LoginLocationSelectUIViewModel>();
            }
            //
            SimpleIoc.Default.Register<LoginLocationSelectUIViewModel>();
        }

        public override void Cleanup()
        {
            base.Cleanup();
        }
    }
}
