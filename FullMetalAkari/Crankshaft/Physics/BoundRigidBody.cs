using System;
using System.Collections.Generic;
using System.Text;
using BulletSharp;
using Crankshaft.Primitives;

namespace Crankshaft.Physics
{
    public class BoundRigidBody : RigidBody
    {
        private gameObject obj;
        public gameObject Obj { get => obj; set => obj = value; }
        public BoundRigidBody(RigidBodyConstructionInfo constructionInfo, gameObject obj) : base(constructionInfo)
        {
            this.obj = obj;
        }
    }
}
