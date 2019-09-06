using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinTest
{
    /// <summary>
    /// 
    /// </summary>
    public class ShowTargetViewParam
    {
        public Type ViewType { get; set; }

        public object InitParamValue { get; set; }

        public bool ModalShow { get; set; }
    }
}
