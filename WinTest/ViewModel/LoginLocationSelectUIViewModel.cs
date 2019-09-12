using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowUI;

namespace WinTest
{
    public class LoginLocationSelectUIViewModel: ViewModelBase
    {
        public RelayCommand<object> FormUnloaded { get; set; }

        public RelayCommand<object> FormLoaded { get; set; }

        public RelayCommand<CurrentViewParam> GridItemSelectCommand { get; set; }

        public RelayCommand<ExCommandParameter> GridItemSelectCommand2 { get; set; }
        
        public string LoginType { get; set; }

        /// <summary>
        /// SelectedUserAccess
        /// </summary>
        public UserAccessObject SelectedUserAccess { get; private set; }

        /// <summary>
        /// LoginLocationSelectUIViewModel
        /// </summary>
        public LoginLocationSelectUIViewModel()
        {
            FormUnloaded = new RelayCommand<object>(formUnloaded);
            //
            FormLoaded = new RelayCommand<object>(formLoaded);
            //
            GridItemSelectCommand = new RelayCommand<CurrentViewParam>(gridItemSelectCommand);
            GridItemSelectCommand2 = new RelayCommand<ExCommandParameter>(gridItemSelectCommand2);
            //
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
        /// <summary>
        /// gridDoubleClickCommand
        /// </summary>
        /// <param name="selectedItem"></param>
        private void gridItemSelectCommand2(ExCommandParameter viewCmdParam)
        {
            if (viewCmdParam != null)
            {
                System.Windows.Input.MouseButtonEventArgs args = viewCmdParam.EventArgs as System.Windows.Input.MouseButtonEventArgs;
                var v= args.OriginalSource;
               
            }
        }
        
        /// <summary>
        /// gridDoubleClickCommand
        /// </summary>
        /// <param name="selectedItem"></param>
        private void gridItemSelectCommand(CurrentViewParam viewCmdParam)
        {
            if (viewCmdParam != null)
            {
                UserAccessObject userAccess = viewCmdParam.CommandParamValue as UserAccessObject;
                if (userAccess!=null)
                {
                    SelectedUserAccess = userAccess;
                }
                else
                {
                    return;
                }
                //
                if (viewCmdParam.CurrentView!=null)
                {
                    viewCmdParam.CurrentView.DialogResult = true;
                }
            }
        }

        public override void Cleanup()
        {
            base.Cleanup();
        }
    }
}
