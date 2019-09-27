using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace WindowUI.Controls
{
    public class WindowThumbContentControlDragStartedEventArgs : DragStartedEventArgs
    {
        public WindowThumbContentControlDragStartedEventArgs(double horizontalOffset, double verticalOffset)
            : base(horizontalOffset, verticalOffset)
        {
            this.RoutedEvent = WindowThumbContentControl.DragStartedEvent;
        }
    }
}
