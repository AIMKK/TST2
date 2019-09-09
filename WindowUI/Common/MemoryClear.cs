using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowUI
{
    public class MemoryClear
    {

        ////////clearMemoryTimer = new System.Threading.Timer(new System.Threading.TimerCallback((obj) =>
        ////////    {
        ////////        WinformUI.MemoryClear.FreeMemory();
        ////////    }), null, 10000, 10000);

        ////////      try
        ////////    {
        ////////        if (clearMemoryTimer != null)
        ////////            clearMemoryTimer.Dispose();
        ////////    }
        ////////    catch (Exception ex)
        ////////    {
        ////////        WinformUI.NlogHelper.LogToFile(ex.ToString());
        ////////    }

        #region 内存回收
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);


        /// <summary>
        /// 释放内存 秒位单位
        /// </summary>
        /// <param name="second"></param>
        public static void FreeMemory()
        {
            try
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
