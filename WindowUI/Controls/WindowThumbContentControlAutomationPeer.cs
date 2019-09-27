using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;

namespace WindowUI.Controls
{
    /// <summary>
    /// The MetroThumbContentControlAutomationPeer class exposes the <see cref="T:WindowUI.Controls.WindowThumbContentControl" /> type to UI Automation.
    /// </summary>
    public class WindowThumbContentControlAutomationPeer : FrameworkElementAutomationPeer
    {
        public WindowThumbContentControlAutomationPeer(FrameworkElement owner)
            : base(owner)
        {
        }

        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Custom;
        }

        protected override string GetClassNameCore()
        {
            return "WindowThumbContentControl";
        }
    }
}
