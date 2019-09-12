using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WindowUI
{
    /// <summary>
    /// ViewCommandParam
    /// </summary>
    public class CurrentViewParam
    {
        public Window CurrentView { get; set; }

        public object CommandParamValue { get; set; }
    }
}
