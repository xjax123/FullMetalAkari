using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crankshaft.Primitives
{
    class uiObject : gameObject
    {
        //UI specific variables
        protected MouseState mouseState;
        protected NativeWindow window;
        public uiObject(int instanceID, Vector3 startingPos, float startingScale, float startingRot, MouseState mouseState, NativeWindow window) : base(instanceID, startingPos, startingScale, startingRot)
        {
            this.mouseState = mouseState;
            this.window = window;
        }
    }
}
