using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WindowUI
{
    /// <summary>
    /// 
    /// </summary>
    public class OpenViewParam
    {       
        /// <summary>
        /// 新打开view 类型
        /// </summary>
        public Type NewViewType { get; set; }

        /// <summary>
        /// 新打开view时初始参数
        /// </summary>
        public object NewViewInitValue { get; set; }

        /// <summary>
        /// 当前所在的view
        /// </summary>
        public Window CurrentView { get; set; }

    }
}
