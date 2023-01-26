using System;
using System.Collections.Generic;
using System.Text;
using Crankshaft.Handlers;
using Crankshaft.Primitives;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FullMetalAkari.Game.Objects.UI
{
    class sniperCrosshair : uiObject
    {
        public sniperCrosshair(int instanceID, Vector3 startingPos, float startingScale, float startingRot, MouseState mouseState, NativeWindow window) : base(instanceID, startingPos, startingScale, startingRot, mouseState, window)
        {
        }

        public override void onRenderFrame()
        {
            //Clamps the 
            Vector3 worldspaceMouse = Vector3.Normalize(mouseHandler.ConvertScreenToWorldSpace(mouseState.X, mouseState.Y, window.Size.X, window.Size.Y, renderingHandler.InvertedProjection, renderingHandler.InvertedView));
            translateObject(new Vector3(worldspaceMouse.X * 3, worldspaceMouse.Y * 2, 0.0f));
            base.onRenderFrame();
        }
    }
}
