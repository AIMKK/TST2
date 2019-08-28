using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace iRMS
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : MetroWindow//Window
    {
        //LoginVM vm = new LoginVM(DialogCoordinator.Instance);
        //
        public Login()
        {
            InitializeComponent();
            //var grid = this.Content as Grid ;


            ////
            //Button btn = new Button() { Width = grid.Width, Height = grid.Height };
            //btn.SetValue(Grid.RowProperty, 0);
            //btn.SetValue(Grid.ColumnProperty, 0);
            //btn.SetValue(Grid.ColumnSpanProperty, grid.RowDefinitions.Count);
            //btn.SetValue(Grid.RowSpanProperty, grid.ColumnDefinitions.Count);
            //btn.Opacity = 0.0;

            ////
            //grid.Children.Add(btn);

            //var grid = this.Content as Grid;
            //var v = grid.Children;
            //var control = this.FindName("loginUsercode") as TextBox ;
            //control.Height=40;
            //control.ClearValue(FrameworkElement.HeightProperty);

            this.Loaded += Login_Loaded;

        }

        private void Login_Loaded(object sender, RoutedEventArgs e)
        {
            //this.Dispatcher.BeginInvoke((Action)delegate ()
            //      {
            //          MessageBox.Show("formLoad2");
            //      }, null);

            //System.Threading.Thread th = new System.Threading.Thread(() =>
            //  {
            //      this.Dispatcher.BeginInvoke((Action)delegate()
            //      {
            //          MessageBox.Show("formLoad2");
            //      },null);

            //  });
            //th.IsBackground = true;
            //th.Start();
        }

        private void MetroWindow_Initialized(object sender, EventArgs e)
        {

        }
    }
}
