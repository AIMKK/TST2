using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WindowUI
{
    /// <summary>
    /// OpenNewViewParam
    /// </summary>
    public class OpenNewViewParam
    {       
        /// <summary>
        /// 新打开view 类型
        /// </summary>
        public Type NewViewType { get; set; }

        /// <summary>
        /// 从当前view传递过来的参数值
        /// </summary>
        public object ParamValueFromCurrentView { get; set; }

        /// <summary>
        /// 当前所在的view
        /// </summary>
        public Window CurrentView { get; set; }

    }
}
