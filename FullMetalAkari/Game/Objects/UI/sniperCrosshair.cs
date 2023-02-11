using System;
using System.Collections.Generic;
using System.Text;
using Crankshaft.Handlers;
using Crankshaft.Physics;
using Crankshaft.Primitives;
using Crankshaft.Data;
using OpenTK.Mathematics;
using BulletSharp;

namespace FullMetalAkari.Game.Objects.UI
{
    class sniperCrosshair : uiObject
    {
        public sniperCrosshair(objectData d) : base(d)
        {
            objectID = "scope";
            name = "Sniper Scope";
            texPath = "Game/Resources/Texture/Scope_Duplex.png";
        }

        public override void onRenderFrame()
        {
            //Clamps the scope to the mouse loosely.
            UniVector3 worldspaceMouse = OpenTK.Mathematics.Vector3.Normalize(physicsHandler.ConvertScreenToWorldSpace(windowHandler.ActiveMouse.X, windowHandler.ActiveMouse.Y, windowHandler.ActiveWindow.Size.X, windowHandler.ActiveWindow.Size.Y, renderingHandler.InvertedProjection, renderingHandler.InvertedView));
            setTranslation(new UniVector3(worldspaceMouse.X * 3, worldspaceMouse.Y * 2, 0.0f));
            base.onRenderFrame();
        }
        public override void onLoad()
        {
            base.onLoad();
        }
    }
}
