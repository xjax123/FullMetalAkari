using Crankshaft.Data;
using Crankshaft.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullMetalAkari.Game.Objects.Game
{
    class Target : gameObject
    {
        public Target(objectData d) : base(d)
        {
            objectID = "target";
            name = "Target";
            switch (d.Variant)
            {
                case 1:
                    texPath = "Game/Resources/Texture/target1.png";
                    break;
                case 2:
                    texPath = "Game/Resources/Texture/target2.png";
                    break;
            }
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
