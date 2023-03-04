using System;
using System.Collections.Generic;
using System.Text;
using Crankshaft.Data;
using Crankshaft.Primitives;

namespace FullMetalAkari.Game.Objects.Game
{
    class Barrel : gameObject
    {
        public Barrel(objectData d) : base(d)
        {
            objectID = "barrel";
            name = "Barrel";
            texPath = "Game/Resources/Texture/barrel.png";
            vertices = new float[] {
                //Position         Texture coordinates
                0.5f,  0.5f, 0.0f, 1.25f, 1.0f, // top right
                0.5f, -0.5f, 0.0f, 1.25f, 0.0f, // bottom right
                -0.5f, -0.5f, 0.0f, -0.25f, 0.0f, // bottom left
                -0.5f,  0.5f, 0.0f, -0.25f, 1.0f  // top left
            }; 
        }
    }
}
