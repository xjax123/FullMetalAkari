using Crankshaft.Data;
using Crankshaft.Handlers;
using Crankshaft.Physics;
using Crankshaft.Primitives;
using System.Collections.Generic;
using System.Text;

namespace FullMetalAkari.Game.Objects.Game
{
    class Backhill : gameObject
    {
        float[] oldverts;
        public Backhill(objectData d) : base(d)
        {
            ObjectID = "tallhill";
            name = "Tall Hill";
            texPaths.Add("Game/Resources/UI/bg2.png");
            UniVector3 screenpos = physicsHandler.ConvertScreenToWorldSpaceVec3(windowHandler.ActiveWindow.Size.X, windowHandler.ActiveWindow.Size.Y, 0.0f);
            oldverts = new float[] {
                //Position         Texture coordinates
                   screenpos.X*(3 - position.Z),   screenpos.Y*(3 - position.Z), 0.0f, 0f, 0f, // top right
                   screenpos.X*(3 - position.Z),  -screenpos.Y*(3 - position.Z), 0.0f, 0f, 1f, // bottom right
                -(screenpos.X)*(3 - position.Z),  -screenpos.Y*(3 - position.Z), 0.0f, 1f, 1f, // bottom left
                -(screenpos.X)*(3 - position.Z),   screenpos.Y*(3 - position.Z), 0.0f, 1f, 0f  // top left
            };
            meshes.Add(oldverts);
        }
        public override void onResize()
        {
            UniVector3 screenpos = physicsHandler.ConvertScreenToWorldSpaceVec3(windowHandler.ActiveWindow.Size.X, windowHandler.ActiveWindow.Size.Y, 0.0f);
            int index = meshes.IndexOf(oldverts);
            meshes.Remove(oldverts);
            meshes.Insert(index,
            new float[] {
                //Position         Texture coordinates
                screenpos.X*(3 - position.Z),  0.5f, 0.0f, 1.25f, 1.0f, // top right
                screenpos.X*(3 - position.Z), -0.5f, 0.0f, 1.25f, 0.0f, // bottom right
                -(screenpos.X)*(3 - position.Z), -0.5f, 0.0f, -0.25f, 0.0f, // bottom left
                -(screenpos.X)*(3 - position.Z),  0.5f, 0.0f, -0.25f, 1.0f  // top left
            });
        }
    }
}
