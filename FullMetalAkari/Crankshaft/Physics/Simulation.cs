using System;
using System.Collections.Generic;
using System.Text;
using BulletSharp;
using OpenTK.Mathematics;

namespace Crankshaft.Physics
{
    public class Simulation
    {
        public DynamicsWorld World { get; protected set; }

        public void onUpdate()
        {

        }

        public void onLoad()
        {

        }

        public void onUnload()
        {

        }

        public void addRigidToWorld(ref RigidBody r)
        {
            //World.AddRigidBody(r);
        }
    }
}
