using System;
using System.Collections.Generic;
using System.Text;
using Crankshaft.Handlers;
using Crankshaft.Physics;
using Crankshaft.Primitives;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FullMetalAkari.Game.Objects.UI
{
    class sniperCrosshair : uiObject
    {
        public sniperCrosshair(int instanceID, UniVector3 startingPos, float startingScale, float startingRot, MouseState mouseState, NativeWindow window) : base(instanceID, startingPos, startingScale, startingRot, mouseState, window)
        {
        }

        public override void onRenderFrame()
        {
            //Clamps the 
            UniVector3 worldspaceMouse = OpenTK.Mathematics.Vector3.Normalize(physicsHandler.ConvertScreenToWorldSpace(mouseState.X, mouseState.Y, window.Size.X, window.Size.Y, renderingHandler.InvertedProjection, renderingHandler.InvertedView));
            translateObject(new UniVector3(worldspaceMouse.X * 3, worldspaceMouse.Y * 2, 0.0f));
            base.onRenderFrame();
        }
    }
}
