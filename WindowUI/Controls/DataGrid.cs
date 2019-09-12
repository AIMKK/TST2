using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WindowUI
{
    /// <summary>
    /// 
    /// </summary>
    public class DataGrid: System.Windows.Controls.DataGrid
    {
        /// <summary>
        /// 点击空白地方时不触发
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            ////DependencyObject source = (DependencyObject)e.OriginalSource;
            ////var row = UIHelper.TryFindParent<DataGridRow>(source);

            // Check if the user double-clicked a grid row and not something else
            DataGridRow row = ItemsControl.ContainerFromElement((DataGrid)this, e.OriginalSource as DependencyObject) as DataGridRow;

            //the user did not click on a row
            if (row == null)
                return;
            //
            base.OnMouseDoubleClick(e);
        }
    }
}
