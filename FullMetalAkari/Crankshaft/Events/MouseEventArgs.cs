using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Crankshaft.Events
{
    public class MouseEventArgs : EventArgs
    {
        public MouseButton Button { get; set; }
        public Vector2 Position { get; set; }
    }
}
