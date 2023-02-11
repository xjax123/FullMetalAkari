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
        CollisionConfiguration collisionConf = new DefaultCollisionConfiguration();
        CollisionDispatcher Dispatcher = new CollisionDispatcher( new DefaultCollisionConfiguration());
        DbvtBroadphase Broadphase = new DbvtBroadphase();
        public Simulation()
        {
            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, collisionConf);
        }
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
            World.AddRigidBody(r);
        }
    }
}
