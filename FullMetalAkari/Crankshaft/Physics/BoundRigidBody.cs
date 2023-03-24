using System;
using System.Collections.Generic;
using System.Text;
using BulletSharp;
using Crankshaft.Primitives;
using OpenTK.Mathematics;

namespace Crankshaft.Physics
{
    public class BoundRigidBody : RigidBody
    {
        private gameObject obj;
        private int iD;
        public Matrix2 Colider;
        public BulletSharp.Math.Vector3 aabbmin;
        public BulletSharp.Math.Vector3 aabbmax;
        public float[] verts;
        public uint[] ind;
        public gameObject Obj { get => obj; set => obj = value; }
        public int ID { get => iD; set => iD = value; }

        public BoundRigidBody(RigidBodyConstructionInfo constructionInfo, gameObject obj, int iD, Matrix2 colider) : base(constructionInfo)
        {
            Obj = obj;
            ID = iD;
            Colider = colider;
        }
    }
}
