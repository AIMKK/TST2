using CoreLibrary;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace CoreAppTest
{
    public class LoginLocationSelectViewModel : ViewModelBase
    {
        public RelayCommand<object> FormUnloadedCommand { get; set; }

        public RelayCommand<object> FormLoadedCommand { get; set; }

        public RelayCommand<Window> FormClosedCommand { get; set; }

        public RelayCommand<CurrentViewParam> GridItemSelectCommand { get; set; }

        public RelayCommand<object> GridItemSelectCommand2 { get; set; }

        public string LoginType { get; set; }

        /// <summary>
        /// SelectedUserAccess
        /// </summary>
        public UserAccessObject SelectedUserAccess { get; private set; }

        /// <summary>
        /// LoginLocationSelectUIViewModel
        /// </summary>
        public LoginLocationSelectViewModel()
        {
            FormUnloadedCommand = new RelayCommand<object>(formUnloaded);
            //
            FormLoadedCommand = new RelayCommand<object>(formLoaded);
            //
            FormClosedCommand = new RelayCommand<Window>(formClosed);
            //
            GridItemSelectCommand = new RelayCommand<CurrentViewParam>(gridItemSelectCommand);

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
            if (SimpleIoc.Default.IsRegistered<LoginLocationSelectViewModel>())
            {
                SimpleIoc.Default.Unregister<LoginLocationSelectViewModel>();
            }
            //
            SimpleIoc.Default.Register<LoginLocationSelectViewModel>();
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
                if (userAccess != null)
                {
                    SelectedUserAccess = userAccess;
                }
                else
                {
                  
                    //ModernDialog.ShowMessage("请选择所需要的行！", "message", System.Windows.MessageBoxButton.OK, viewCmdParam.CurrentView);
                    return;
                }
                //
                if (viewCmdParam.CurrentView != null)
                {
                    viewCmdParam.CurrentView.DialogResult = true;
                }
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
