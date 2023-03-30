using Crankshaft.Data;
using Crankshaft.Primitives;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullMetalAkari.Game.Objects.Game
{
    class Bullet : gameObject
    {
        public Bullet(objectData d) : base(d)
        {
            ObjectID = "bullet";
            name = "Bulletmark";
            texPaths.Add(@"\Game\Resources\Texture\gunshot.png");
            visualScale.Add(Matrix4.CreateScale(0.1f));
        }
    }
}
