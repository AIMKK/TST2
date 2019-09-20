using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WindowUI
{
    public class HotKeyHelper
    {        
        /// <summary>
        /// 保存注册的热键信息
        /// </summary>
        private Dictionary<Key, Action> registerHotInfo = new Dictionary<Key, Action>();

        /// <summary>
        /// add hot key
        /// </summary>
        /// <param name="hotKey"></param>
        /// <param name="act"></param>
        public void RegisterHotKey(Key hotKey, Action act)
        {
            if (act == null)
            {
                throw new Exception("act 不能为空！");
            }
            if (registerHotInfo.ContainsKey(hotKey))
            {
                registerHotInfo[hotKey] = act;
            }
            else
            {
                registerHotInfo.Add(hotKey, act);
            }
        }

        /// <summary>
        /// remove hot key
        /// </summary>
        /// <param name="hotKey"></param>
        public void RemoveHotKey(Key hotKey)
        {
            if (registerHotInfo.ContainsKey(hotKey))
            {
                registerHotInfo.Remove(hotKey);
            }
        }

        /// <summary>
        /// process
        /// </summary>
        public void ProcessHotKey(Key hotKey)
        {
            if (registerHotInfo.ContainsKey(hotKey))
            {
                Action act = registerHotInfo[hotKey];
                if (act != null)
                {
                    act();
                }
            }
        }

    }
}
