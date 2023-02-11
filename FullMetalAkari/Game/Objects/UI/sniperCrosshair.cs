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
            UniVector3 worldspaceMouse = physicsHandler.ConvertScreenToWorldSpaceVec3(windowHandler.ActiveMouse.X, windowHandler.ActiveMouse.Y, -1.0f);
            setTranslation(new UniVector3(worldspaceMouse.X * 3, worldspaceMouse.Y * 3, 0.0f));
            base.onRenderFrame();
        }
    }
}
