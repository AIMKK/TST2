using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WindowUI;

namespace WindowUITest
{
    public class MainUIViewModel : ViewModelBase
    {
        public RelayCommand<Window> FormLoadedCommand { get; set; }

        public RelayCommand<Window> FormClosedCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MainUIViewModel()
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
            
        }

        /// <summary>
        /// formClosed
        /// </summary>
        /// <param name="win"></param>
        private void formClosed(Window win)
        {
            if (win != null)
            {
                win.Close();
            }
        }

    }
}
