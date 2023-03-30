using Crankshaft.Data;
using Crankshaft.Handlers;
using Crankshaft.Primitives;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace FullMetalAkari.Game.Objects.Game
{
    class Target : gameObject
    {
        public Target(objectData d) : base(d)
        {
            ObjectID = "target";
            name = "Target";
            switch (d.Variant)
            {
                case 1:
                    texPaths.Add("Game/Resources/Texture/target1.png");
                    //Bullseye
                    Colider.Add(new Matrix2(0.05f, 0.05f, 0.0f, 0.022f));
                    //Outer Target
                    Colider.Add(new Matrix2(0.5f, 0.5f, 0.0f, 0.022f));
                    //Metal Edge
                    Colider.Add(new Matrix2(0.6f, 0.6f, 0.0f, 0.022f));
                    //Left Leg
                    Colider.Add(new Matrix2(0.12f, 0.35f, -0.025f, -0.038f));
                    //Right Leg
                    Colider.Add(new Matrix2(0.12f, 0.35f, 0.025f, -0.038f));
                    //Centre Bar
                    Colider.Add(new Matrix2(0.35f, 0.08f, 0.0f, -0.025f));
                    break;
                case 2:
                    texPaths.Add("Game/Resources/Texture/target2.png");
                    //Outer Target
                    Colider.Add(new Matrix2(0.5f, 0.5f, 0.0f, 0.022f));
                    //Metal Edge
                    Colider.Add(new Matrix2(0.6f, 0.6f, 0.0f, 0.022f));
                    //Left Leg
                    Colider.Add(new Matrix2(0.12f, 0.35f, -0.025f, -0.038f));
                    //Right Leg
                    Colider.Add(new Matrix2(0.12f, 0.35f, 0.025f, -0.038f));
                    //Centre Bar
                    Colider.Add(new Matrix2(0.35f, 0.08f, 0.0f, -0.025f));
                    break;
            }
            meshes.Add(new float[] {
                //Position         Texture coordinates
                0.5f,  0.5f, 0.0f, 1.25f, 1.0f, // top right
                0.5f, -0.5f, 0.0f, 1.25f, 0.0f, // bottom right
                -0.5f, -0.5f, 0.0f, -0.25f, 0.0f, // bottom left
                -0.5f,  0.5f, 0.0f, -0.25f, 1.0f  // top left
            });
        }

        public override void onClick(int ID)
        {
            switch (ID)
            {
                case 0:
                    if (Data.Variant == 1)
                    {
                        bullseyeHit();
                        windowHandler.ActiveScene.objects.Remove(this);
                        objectData temp = Data;
                        temp.Variant = 2;
                        sceneHandler.addObjectToActiveScene(new Target(temp));
                    }
                    else
                    {
                        targetHit();
                    }
                    break;
                case 1:
                    if (Data.Variant == 1)
                    {
                        targetHit();
                    } else
                    {
                        metalHit();
                    }
                    break;
                case 2:
                    metalHit();
                    break;
                case 3:
                    metalHit();
                    break;
                case 4:
                    metalHit();
                    break;
                case 5:
                    metalHit();
                    break;
            }
        }

        private void bullseyeHit()
        {
            Debug.WriteLine("Bullseye");
        }

        private void targetHit()
        {
            Debug.WriteLine("Target");
        }

        private void metalHit()
        {
            Debug.WriteLine("Metal");
        }
    }
}
