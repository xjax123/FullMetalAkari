using Crankshaft.Data;
using Crankshaft.Handlers;
using Crankshaft.Primitives;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullMetalAkari.Game.Objects.Game
{
    class Bottle : gameObject
    {
        private int? variant;
        public int? Variant { get => variant; set => variant = value; }

        public Bottle(objectData d) : base(d)
        {
            objectID = "bottle";
            name = "Bottle";
            Variant = d.Variant;
            switch (d.Variant)
            {
                case 1:
                    texPath = "Game/Resources/Texture/bottle1.png";
                    vertices = new float[] {
                         //Position         Texture coordinates
                         0.5f,  0.5f, 0.0f, 2.0f, 1.0f, // top right
                         0.5f, -0.5f, 0.0f, 2.0f, 0.0f, // bottom right
                         -0.5f, -0.5f, 0.0f, -1.0f, 0.0f, // bottom left
                         -0.5f,  0.5f, 0.0f, -1.0f, 1.0f  // top left
                    };
                    //Stand Bottom
                    Colider.Add(new Matrix2(0.07f, 0.45f, 0.001f, -0.03f));
                    //Stand Top
                    Colider.Add(new Matrix2(0.25f, 0.06f, 0.0f, 0.003f));
                    //Bottle Borrom
                    Colider.Add(new Matrix2(0.15f, 0.26f, 0.0f, 0.0235f));
                    //Bottle Neck
                    Colider.Add(new Matrix2(0.08f, 0.15f, 0.0f, 0.05f));

                    break;
                case 4:
                    texPath = "Game/Resources/Texture/bottle2.png";
                    vertices = new float[] {
                         //Position         Texture coordinates
                         0.5f,  0.5f, 0.0f, 2.0f, 1.25f, // top right
                         0.5f, -0.5f, 0.0f, 2.0f, 0.0f, // bottom right
                         -0.5f, -0.5f, 0.0f, -1.0f, 0.0f, // bottom left
                         -0.5f,  0.5f, 0.0f, -1.0f, 1.25f  // top left
                    };
                    //Bottle Body
                    Colider.Add(new Matrix2(0.25f,0.5f,0.0f,-0.028f));
                    //Bottle Neck
                    Colider.Add(new Matrix2(0.15f, 0.23f, 0.0f, 0.018f));
                    break;
                case 3:
                    texPath = "Game/Resources/Texture/bottle3.png";

                    vertices = new float[] {
                         //Position         Texture coordinates
                         0.5f,  0.5f, 0.0f, 2.0f, 1.0f, // top right
                         0.5f, -0.5f, 0.0f, 2.0f, 0.0f, // bottom right
                         -0.5f, -0.5f, 0.0f, -1.0f, 0.0f, // bottom left
                         -0.5f,  0.5f, 0.0f, -1.0f, 1.0f  // top left
                    };
                    //Stand Bottom
                    Colider.Add(new Matrix2(0.07f, 0.75f, 0.001f, -0.011f));
                    //Stand Top
                    Colider.Add(new Matrix2(0.25f, 0.1f, 0.0f, 0.042f));
                    break;
                case 2:
                    texPath = "Game/Resources/Texture/bottle4.png";

                    vertices = new float[] {
                         //Position         Texture coordinates
                         0.5f,  0.5f, 0.0f, 2.0f, 1.0f, // top right
                         0.5f, -0.5f, 0.0f, 2.0f, 0.0f, // bottom right
                         -0.5f, -0.5f, 0.0f, -1.0f, 0.0f, // bottom left
                         -0.5f,  0.5f, 0.0f, -1.0f, 1.0f  // top left
                    };
                    //Stand Bottom
                    Colider.Add(new Matrix2(0.07f, 0.45f, 0.001f, -0.03f));
                    //Stand Top
                    Colider.Add(new Matrix2(0.25f, 0.06f, 0.0f, 0.003f));
                    break;
                default:
                    texPath = "Game/Resources/Texture/bottle2.png";
                    vertices = new float[] {
                         //Position         Texture coordinates
                         0.5f,  0.5f, 0.0f, 2.0f, 1.25f, // top right
                         0.5f, -0.5f, 0.0f, 2.0f, 0.0f, // bottom right
                         -0.5f, -0.5f, 0.0f, -1.0f, 0.0f, // bottom left
                         -0.5f,  0.5f, 0.0f, -1.0f, 1.25f  // top left
                    };
                    Colider.Add(new Matrix2(0.25f, 0.5f, 0.0f, -0.028f));
                    Colider.Add(new Matrix2(0.15f, 0.23f, 0.0f, 0.018f));
                    Variant = 2;
                    break;
            }
        }
        
        public override void onClick(int ID)
        {
            objectData temp;
            switch (Data.Variant)
            {
                case 1:
                    switch (ID)
                    {
                        case 0:
                            metalHit();
                            break;
                        case 1:
                            metalHit();
                            break;
                        case 2:
                            glassBreak();
                            windowHandler.ActiveScene.objects.Remove(this);
                            temp = Data;
                            temp.Variant = 2;
                            sceneHandler.addObjectToActiveScene(new Bottle(temp));
                            break;
                        case 3:
                            glassBreak();
                            windowHandler.ActiveScene.objects.Remove(this);
                            temp = Data;
                            temp.Variant = 2;
                            sceneHandler.addObjectToActiveScene(new Bottle(temp));
                            break;
                    }
                    break;
                case 2:
                    switch (ID)
                    {
                        case 0:
                            metalHit();
                            break;
                        case 1:
                            metalHit();
                            break;
                    }
                    break;
                case 3:
                    switch (ID)
                    {
                        case 0:
                            metalHit();
                            break;
                        case 1:
                            metalHit();
                            break;
                    }
                    break;
                case 4:
                    switch (ID)
                    {
                        case 0:
                            glassBreak();
                            break;
                        case 1:
                            glassBreak();
                            break;
                    }
                    break;
            }
        }

        private void glassBreak()
        {
        }

        private void metalHit()
        {
        }
    }
}
