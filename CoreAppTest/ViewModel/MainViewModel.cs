using CoreLibrary;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace CoreAppTest
{
    public class MainViewModel: ViewModelBase
    {
        public RelayCommand<Window> FormLoadedCommand { get; set; }

        public RelayCommand<Window> FormClosedCommand { get; set; }

        public MainViewModel()
        {
            //
            FormLoadedCommand = new RelayCommand<Window>(formLoaded);
            FormClosedCommand = new RelayCommand<Window>(formClosed);
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
