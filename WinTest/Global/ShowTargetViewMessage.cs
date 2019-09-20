using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowUI;

namespace WinTest
{
    /// <summary>
    /// ShowTargetViewMessage
    /// </summary>
    public class ShowTargetViewMessage
    {
        private ShowTargetViewMessage()
        {

        }

        /// <summary>
        /// CreateInstance
        /// </summary>
        /// <param name="targetViewModelInitPropName"></param>
        /// <param name="viewParam"></param>
        /// <returns></returns>
        public static ShowTargetViewMessage CreateInstance(OpenNewViewParam viewParam)
        {
            return CreateInstance("", viewParam);
        }

        /// <summary>
        /// CreateInstance
        /// </summary>
        /// <param name="viewInitValueReceiveProp"></param>
        /// <param name="viewParam"></param>
        /// <returns></returns>
        public static ShowTargetViewMessage CreateInstance(string viewInitValueReceiveProp, OpenNewViewParam viewParam)
        {
            ShowTargetViewMessage param = new ShowTargetViewMessage();
            try
            {
                if (viewParam==null)
                {
                    return null;
                }
                //
                param.TargetViewType = viewParam.NewViewType as Type;
                param.ReceiveViewInitValueProp = viewInitValueReceiveProp;
                param.ViewInitValue = viewParam.ParamValueFromCurrentView;
            }
            catch (Exception ex)
            {
                param = null;
            }
            //
            return param;
        }

        /// <summary>
        /// 目标UI 的 Type
        /// </summary>
        public Type TargetViewType { get; set; }

        /// <summary>
        /// 显示的目标UI是否是模态显示
        /// </summary>
        public bool TargetViewIsModalShow { get; set; }

        /// <summary>
        /// 接收UI传递过来的值
        /// </summary>
        public string ReceiveViewInitValueProp { get; set; }

        /// <summary>
        /// UI 传递的参数值
        /// </summary>
        public object ViewInitValue { get; set; }
    }
}
