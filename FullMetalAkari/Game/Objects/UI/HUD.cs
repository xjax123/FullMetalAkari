using Crankshaft.Animation;
using Crankshaft.Data;
using Crankshaft.Handlers;
using Crankshaft.Physics;
using Crankshaft.Primitives;
using FullMetalAkari.Game.Objects.Game;
using OpenTK.Graphics.ES20;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullMetalAkari.Game.Objects.UI
{
    public class HUD : uiObject
    {
        Phrase targetMax;
        Phrase targets;
        float[] oldverts;
        public HUD(objectData d) : base(d)
        {
            ObjectID = "hud";
            name = "HUD";

            //Akari Model [0]
            meshes.Add(new float[] {
                //Position         Texture coordinates
                1f,  0.5f, 0.0f, 1f, 1f, // top right
                1f, -0.5f, 0.0f, 1f, -0f, // bottom right
                -1f, -0.5f, 0.0f, -0f, -0f, // bottom left
                -1f,  0.5f, 0.0f, -0f, 1f  // top left
            });
            texPaths.Add(@"\Game\Resources\UI\akari-rig-setup_new.png");
            visualScale.Add(Matrix4.CreateScale(1.1f));
            offsets.Add(Matrix4.CreateTranslation(new UniVector3(-1.3f, -0.7f, 0)));

            //Breath Meter Background [1]
            meshes.Add(new float[] {
                //Position         Texture coordinates
                3f,  0.5f, 0.0f, 1f, 1f, // top right
                3f, -0.5f, 0.0f, 1f, -0f, // bottom right
                -3f, -0.5f, 0.0f, -0f, -0f, // bottom left
                -3f,  0.5f, 0.0f, -0f, 1f  // top left
            });
            texPaths.Add(@"\Game\Resources\UI\breathback.png");
            visualScale.Add(Matrix4.CreateScale(0.25f));
            offsets.Add(Matrix4.CreateTranslation(new UniVector3(-1.7f, -1.3f, 0)));

            //Breath Meter Foreground [2]
            meshes.Add(new float[] {
                //Position         Texture coordinates
                3f,  0.5f, 0.0f, 1f, 1f, // top right
                3f, -0.5f, 0.0f, 1f, -0f, // bottom right
                -3f, -0.5f, 0.0f, -0f, -0f, // bottom left
                -3f,  0.5f, 0.0f, -0f, 1f  // top left
            });
            texPaths.Add(@"\Game\Resources\UI\breath100.png");
            visualScale.Add(Matrix4.CreateScale(0.20f));
            offsets.Add(Matrix4.CreateTranslation(new UniVector3(-1.7f, -1.3f, 0)));

            //Base HUD [3]
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


            //Wind [4]
            meshes.Add(new float[] {
                //Position         Texture coordinates
                0.5f,  0.5f, 0.0f, 1f, 1f, // top right
                0.5f, -0.5f, 0.0f, 1f, -0f, // bottom right
                -0.5f, -0.5f, 0.0f, -0f, -0f, // bottom left
                -0.5f,  0.5f, 0.0f, -0f, 1f  // top left
            });
            texPaths.Add(@"\Game\Resources\Fonts\No Wind-Text.png");
            visualScale.Add(Matrix4.CreateScale(0.5f));
            offsets.Add(Matrix4.CreateTranslation(new UniVector3(1f, -1.18f, 0.1f)));

            //Bullet 1 [5]
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

            //Bullet 2 [6]
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

            //Bullet 3 [7]
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

            //Bullet 4 [8]
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

            //Bullet 5 [9]
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

            //Bullet 6 [10]
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

            //Bullet 7 [11]
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

            //Bullet 8 [12]
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

            //Bullet 9 [13]
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

            //Bullet 10 [14]
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

            //Bullet 11 [15]
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

            //Bullet 12 [16]
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

            targetMax = new Phrase();
            targetMax.changePhrase(windowHandler.MaxScore.ToString(), new Vector3(14.2f, -7.15f, 0), 0.15f, 0);
            textCreator.renderTargets.Add(targetMax);

            targets = new Phrase();
            targets.changePhrase(windowHandler.Score.ToString(), new Vector3(11.7f, -7.13f, 0), 0.15f, 0);
            textCreator.renderTargets.Add(targets);
        }

        public override void onLoad()
        {
            base.onLoad();
        }

        public override void onRenderFrame()
        {
            base.onRenderFrame();
            targets.changePhrase(windowHandler.Score.ToString(), new Vector3(11.7f, -7.13f, 0), 0.15f, 0);
        }

        public void meterChange(float breath)
        {
            offsets[2] = Matrix4.CreateTranslation(new UniVector3(-2.8f+(breath/100)*1.1f, -1.3f, 0));
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
