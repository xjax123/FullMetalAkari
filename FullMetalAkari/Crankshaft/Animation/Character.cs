using Crankshaft.Handlers;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crankshaft.Animation
{
    public struct Character
    {
        public textureHandler Texture { get; set; }
        public shaderHandler Shader { get; set; }
        public Vector3 Position { get; set; }
        public float Scale { get; set; }
        public float Rotation { get; set; }
    }
}
