using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace WindowUI.Controls
{
    public class WindowThumbContentControlDragCompletedEventArgs : DragCompletedEventArgs
    {
        public WindowThumbContentControlDragCompletedEventArgs(double horizontalOffset, double verticalOffset, bool canceled)
            : base(horizontalOffset, verticalOffset, canceled)
        {
            this.RoutedEvent = WindowThumbContentControl.DragCompletedEvent;
        }
    }
}
