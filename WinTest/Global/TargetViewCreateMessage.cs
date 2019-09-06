using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinTest
{
    /// <summary>
    /// TargetViewCreateMessage
    /// </summary>
    public class TargetViewCreateMessage
    {
        private TargetViewCreateMessage()
        {

        }

        /// <summary>
        /// CreateInstance
        /// </summary>
        /// <param name="targetViewModelInitPropName"></param>
        /// <param name="viewParam"></param>
        /// <returns></returns>
        public static TargetViewCreateMessage CreateInstance(ShowTargetViewParam viewParam)
        {
            return CreateInstance("", viewParam);
        }

        /// <summary>
        /// CreateInstance
        /// </summary>
        /// <param name="targetViewModelInitPropName"></param>
        /// <param name="viewParam"></param>
        /// <returns></returns>
        public static TargetViewCreateMessage CreateInstance(string targetViewModelInitPropName,ShowTargetViewParam viewParam)
        {
            TargetViewCreateMessage param = new TargetViewCreateMessage();
            try
            {
                if (viewParam==null)
                {
                    return null;
                }
                //
                param.TargetViewType = viewParam.ViewType as Type;
                param.TargetViewModelInitPropName = targetViewModelInitPropName;
                param.TargetViewModelInitPropValue = viewParam.InitParamValue;
                param.TargetViewModalShow = viewParam.ModalShow;
            }
            catch (Exception ex)
            {
                param = null;
            }          

            return param;
        }
        public Type TargetViewType { get; set; }

        public bool TargetViewModalShow { get; set; }

        public string TargetViewModelInitPropName { get; set; }

        public object TargetViewModelInitPropValue { get; set; }
    }
}
