using Crankshaft.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullMetalAkari.Crankshaft.Events
{
    public static class MouseEvents
    {
        public static event EventHandler<MouseEventArgs> mouseClick;
    }
}
