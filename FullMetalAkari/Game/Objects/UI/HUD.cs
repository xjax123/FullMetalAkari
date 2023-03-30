using Crankshaft.Data;
using Crankshaft.Handlers;
using Crankshaft.Physics;
using Crankshaft.Primitives;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullMetalAkari.Game.Objects.UI
{
    class HUD : uiObject
    {
        float[] oldverts;
        public HUD(objectData d) : base(d)
        {
            ObjectID = "hud";
            name = "HUD";

            //Base HUD
            texPaths.Add("Game/Resources/UI/FadingSteel_HUD_top_layer.png");
            UniVector3 screenpos = physicsHandler.ConvertScreenToWorldSpaceVec3(windowHandler.ActiveWindow.Size.X, windowHandler.ActiveWindow.Size.Y, 0.0f);
            oldverts = new float[] {
                //Position         Texture coordinates
                screenpos.X*3f,  1.4f, 0.0f, 1f, 1.0f, // top right
                screenpos.X*3f, -1.4f, 0.0f, 1f, 0.0f, // bottom right
                -(screenpos.X)*3f, -1.4f, 0.0f, -0f, 0.0f, // bottom left
                -(screenpos.X)*3f,  1.4f, 0.0f, -0f, 1.0f  // top left
            };
            meshes.Add(oldverts);
            visualScale.Add(Matrix4.CreateScale(1));
            offsets.Add(Matrix4.CreateTranslation(new UniVector3(0f, 0f, 0f)));

            //Bullet 1
            meshes.Add(new float[] {
                //Position         Texture coordinates
                0.5f,  0.5f, 0.0f, 1.5f, 1.5f, // top right
                0.5f, -0.5f, 0.0f, 1.5f, -0.5f, // bottom right
                -0.5f, -0.5f, 0.0f, -0.5f, -0.5f, // bottom left
                -0.5f,  0.5f, 0.0f, -0.5f, 1.5f  // top left
            });
            texPaths.Add(@"\Game\Resources\UI\bullet1.png");
            visualScale.Add(Matrix4.CreateScale(0.65f));
            offsets.Add(Matrix4.CreateTranslation(new UniVector3(-2.19f, -1.05f, 0f)));

            //Bullet 2
            meshes.Add(new float[] {
                //Position         Texture coordinates
                0.5f,  0.5f, 0.0f, 1.5f, 1.5f, // top right
                0.5f, -0.5f, 0.0f, 1.5f, -0.5f, // bottom right
                -0.5f, -0.5f, 0.0f, -0.5f, -0.5f, // bottom left
                -0.5f,  0.5f, 0.0f, -0.5f, 1.5f  // top left
            });
            texPaths.Add(@"\Game\Resources\UI\bullet1.png");
            visualScale.Add(Matrix4.CreateScale(0.5f));
            offsets.Add(Matrix4.CreateTranslation(new UniVector3(-2.12f, -1.1f, 0f)));

            //Bullet 3
            meshes.Add(new float[] {
                //Position         Texture coordinates
                0.5f,  0.5f, 0.0f, 1.5f, 1.5f, // top right
                0.5f, -0.5f, 0.0f, 1.5f, -0.5f, // bottom right
                -0.5f, -0.5f, 0.0f, -0.5f, -0.5f, // bottom left
                -0.5f,  0.5f, 0.0f, -0.5f, 1.5f  // top left
            });
            texPaths.Add(@"\Game\Resources\UI\bullet1.png");
            visualScale.Add(Matrix4.CreateScale(0.5f));
            offsets.Add(Matrix4.CreateTranslation(new UniVector3(-2.02f, -1.1f, 0f)));

            //Bullet 4
            meshes.Add(new float[] {
                //Position         Texture coordinates
                0.5f,  0.5f, 0.0f, 1.5f, 1.5f, // top right
                0.5f, -0.5f, 0.0f, 1.5f, -0.5f, // bottom right
                -0.5f, -0.5f, 0.0f, -0.5f, -0.5f, // bottom left
                -0.5f,  0.5f, 0.0f, -0.5f, 1.5f  // top left
            });
            texPaths.Add(@"\Game\Resources\UI\bullet1.png");
            visualScale.Add(Matrix4.CreateScale(0.5f));
            offsets.Add(Matrix4.CreateTranslation(new UniVector3(-1.92f, -1.1f, 0f)));

            //Bullet 5
            meshes.Add(new float[] {
                //Position         Texture coordinates
                0.5f,  0.5f, 0.0f, 1.5f, 1.5f, // top right
                0.5f, -0.5f, 0.0f, 1.5f, -0.5f, // bottom right
                -0.5f, -0.5f, 0.0f, -0.5f, -0.5f, // bottom left
                -0.5f,  0.5f, 0.0f, -0.5f, 1.5f  // top left
            });
            texPaths.Add(@"\Game\Resources\UI\bullet1.png");
            visualScale.Add(Matrix4.CreateScale(0.5f));
            offsets.Add(Matrix4.CreateTranslation(new UniVector3(-1.82f, -1.1f, 0f)));

            //Bullet 6
            meshes.Add(new float[] {
                //Position         Texture coordinates
                0.5f,  0.5f, 0.0f, 1.5f, 1.5f, // top right
                0.5f, -0.5f, 0.0f, 1.5f, -0.5f, // bottom right
                -0.5f, -0.5f, 0.0f, -0.5f, -0.5f, // bottom left
                -0.5f,  0.5f, 0.0f, -0.5f, 1.5f  // top left
            });
            texPaths.Add(@"\Game\Resources\UI\bullet1.png");
            visualScale.Add(Matrix4.CreateScale(0.5f));
            offsets.Add(Matrix4.CreateTranslation(new UniVector3(-1.72f, -1.1f, 0f)));

            //Bullet 7
            meshes.Add(new float[] {
                //Position         Texture coordinates
                0.5f,  0.5f, 0.0f, 1.5f, 1.5f, // top right
                0.5f, -0.5f, 0.0f, 1.5f, -0.5f, // bottom right
                -0.5f, -0.5f, 0.0f, -0.5f, -0.5f, // bottom left
                -0.5f,  0.5f, 0.0f, -0.5f, 1.5f  // top left
            });
            texPaths.Add(@"\Game\Resources\UI\bullet1.png");
            visualScale.Add(Matrix4.CreateScale(0.5f));
            offsets.Add(Matrix4.CreateTranslation(new UniVector3(-1.62f, -1.1f, 0f)));

            //Bullet 8
            meshes.Add(new float[] {
                //Position         Texture coordinates
                0.5f,  0.5f, 0.0f, 1.5f, 1.5f, // top right
                0.5f, -0.5f, 0.0f, 1.5f, -0.5f, // bottom right
                -0.5f, -0.5f, 0.0f, -0.5f, -0.5f, // bottom left
                -0.5f,  0.5f, 0.0f, -0.5f, 1.5f  // top left
            });
            texPaths.Add(@"\Game\Resources\UI\bullet1.png");
            visualScale.Add(Matrix4.CreateScale(0.5f));
            offsets.Add(Matrix4.CreateTranslation(new UniVector3(-1.52f, -1.1f, 0f)));

            //Bullet 9
            meshes.Add(new float[] {
                //Position         Texture coordinates
                0.5f,  0.5f, 0.0f, 1.5f, 1.5f, // top right
                0.5f, -0.5f, 0.0f, 1.5f, -0.5f, // bottom right
                -0.5f, -0.5f, 0.0f, -0.5f, -0.5f, // bottom left
                -0.5f,  0.5f, 0.0f, -0.5f, 1.5f  // top left
            });
            texPaths.Add(@"\Game\Resources\UI\bullet1.png");
            visualScale.Add(Matrix4.CreateScale(0.5f));
            offsets.Add(Matrix4.CreateTranslation(new UniVector3(-1.42f, -1.1f, 0f)));

            //Bullet 10
            meshes.Add(new float[] {
                //Position         Texture coordinates
                0.5f,  0.5f, 0.0f, 1.5f, 1.5f, // top right
                0.5f, -0.5f, 0.0f, 1.5f, -0.5f, // bottom right
                -0.5f, -0.5f, 0.0f, -0.5f, -0.5f, // bottom left
                -0.5f,  0.5f, 0.0f, -0.5f, 1.5f  // top left
            });
            texPaths.Add(@"\Game\Resources\UI\bullet1.png");
            visualScale.Add(Matrix4.CreateScale(0.5f));
            offsets.Add(Matrix4.CreateTranslation(new UniVector3(-1.32f, -1.1f, 0f)));

            //Bullet 11
            meshes.Add(new float[] {
                //Position         Texture coordinates
                0.5f,  0.5f, 0.0f, 1.5f, 1.5f, // top right
                0.5f, -0.5f, 0.0f, 1.5f, -0.5f, // bottom right
                -0.5f, -0.5f, 0.0f, -0.5f, -0.5f, // bottom left
                -0.5f,  0.5f, 0.0f, -0.5f, 1.5f  // top left
            });
            texPaths.Add(@"\Game\Resources\UI\bullet1.png");
            visualScale.Add(Matrix4.CreateScale(0.5f));
            offsets.Add(Matrix4.CreateTranslation(new UniVector3(-1.22f, -1.1f, 0f)));

            //Bullet 12
            meshes.Add(new float[] {
                //Position         Texture coordinates
                0.5f,  0.5f, 0.0f, 1.5f, 1.5f, // top right
                0.5f, -0.5f, 0.0f, 1.5f, -0.5f, // bottom right
                -0.5f, -0.5f, 0.0f, -0.5f, -0.5f, // bottom left
                -0.5f,  0.5f, 0.0f, -0.5f, 1.5f  // top left
            });
            texPaths.Add(@"\Game\Resources\UI\bullet1.png");
            visualScale.Add(Matrix4.CreateScale(0.5f));
            offsets.Add(Matrix4.CreateTranslation(new UniVector3(-1.12f, -1.1f, 0f)));
        }

        public override void onResize()
        {
            UniVector3 screenpos = physicsHandler.ConvertScreenToWorldSpaceVec3(windowHandler.ActiveWindow.Size.X, windowHandler.ActiveWindow.Size.Y, 0.0f);
            int index = meshes.IndexOf(oldverts);
            meshes.Remove(oldverts);
            meshes.Insert(index,
            new float[] {
                //Position         Texture coordinates
                screenpos.X*3,  0.5f, 0.0f, 1.25f, 1.0f, // top right
                screenpos.X*3, -0.5f, 0.0f, 1.25f, 0.0f, // bottom right
                -(screenpos.X)*3, -0.5f, 0.0f, -0.25f, 0.0f, // bottom left
                -(screenpos.X)*3,  0.5f, 0.0f, -0.25f, 1.0f  // top left
            });
        }
    }
}
