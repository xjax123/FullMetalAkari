using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using System;
using Crankshaft.Handlers;
using System.Diagnostics;

namespace Crankshaft.Events
{
    public static class InputEvents
    {
        public static event EventHandler<MouseEventArgs> mouseClick;
        public static event EventHandler<KeyboardEventArgs> keyboardInput;
        public static event EventHandler<KeyboardEventArgs> keyboardRelease;
        public static void invokeClick(object s, MouseButton b)
        {
            MouseEventArgs m = new MouseEventArgs();
            m.Button = b;
            m.Position = new Vector2(windowHandler.ActiveMouse.Position.X, windowHandler.ActiveMouse.Position.Y);
            mouseClick?.Invoke(s, m);
        }

        public static void invokeInput(object s, Keys k)
        {
            KeyboardEventArgs m = new KeyboardEventArgs();
            m.key = k;
            keyboardInput.Invoke(s,m);
        }
        public static void invokeRelease(object s, Keys k)
        {
            KeyboardEventArgs m = new KeyboardEventArgs();
            m.key = k;
            keyboardRelease.Invoke(s, m);
        }
    }
}
