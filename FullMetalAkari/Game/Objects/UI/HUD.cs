using Crankshaft.Data;
using Crankshaft.Handlers;
using Crankshaft.Physics;
using Crankshaft.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullMetalAkari.Game.Objects.UI
{
    class HUD : uiObject
    {
        public HUD(objectData d) : base(d)
        {
            objectID = "hud";
            name = "HUD";
            texPath = "Game/Resources/UI/FadingSteel_HUD_top_layer.png";
            UniVector3 screenpos = physicsHandler.ConvertScreenToWorldSpaceVec3(windowHandler.ActiveWindow.Size.X,windowHandler.ActiveWindow.Size.Y,0.0f);
            vertices = new float[] {
                //Position         Texture coordinates
                screenpos.X*3f,  1.4f, 0.0f, 1f, 1.0f, // top right
                screenpos.X*3f, -1.4f, 0.0f, 1f, 0.0f, // bottom right
                -(screenpos.X)*3f, -1.4f, 0.0f, -0f, 0.0f, // bottom left
                -(screenpos.X)*3f,  1.4f, 0.0f, -0f, 1.0f  // top left
            };
        }

        public override void onResize()
        {
            UniVector3 screenpos = physicsHandler.ConvertScreenToWorldSpaceVec3(windowHandler.ActiveWindow.Size.X, windowHandler.ActiveWindow.Size.Y, 0.0f);
            vertices = new float[] {
                //Position         Texture coordinates
                screenpos.X*3,  0.5f, 0.0f, 1.25f, 1.0f, // top right
                screenpos.X*3, -0.5f, 0.0f, 1.25f, 0.0f, // bottom right
                -(screenpos.X)*3, -0.5f, 0.0f, -0.25f, 0.0f, // bottom left
                -(screenpos.X)*3,  0.5f, 0.0f, -0.25f, 1.0f  // top left
            };
        }
    }
}
