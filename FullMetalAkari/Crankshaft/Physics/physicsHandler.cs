using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Mathematics;
using BulletSharp;
using Crankshaft.Physics;
using Crankshaft.Handlers;
using Crankshaft.Primitives;

namespace Crankshaft.Physics
{
    public static class physicsHandler
    {

        //TODO: Optimize (Low Priority)
        //Relatively efficient now, the only per-call Operations are 4 multiplication, 2 divison, still room for improvment.
        //Not sure what performance impact creating new vectors has, but in theory shouldnt increase the performance impact by more than a factor of 4.

        //A method to convert screen space into world space
        //for some arcane reason you need to tripple it if you are trying to clamp an object to your mouse
        //not sure why, but it is perfectly accurate once trippled, so im not complinaing.
        public static UniVector3 ConvertScreenToWorldSpace(float x, float y, float width, float height, Matrix4 inv_projection_matrix, Matrix4 inv_view_matrix)
        {
            //Version 3

            //Translating to 3D Normalized Device Coordinates
            //Translating to 4D Homogeneous Clip Coordinates
            Vector4 HCCposition = new Vector4(2.0f * x / width - 1.0f, 1.0f - 2.0f * y / height, -1.0f, 1.0f);

            //Translating to 4D Camera Coordinates
            Vector4 CCposition = inv_projection_matrix * HCCposition;
            CCposition.Zw = new Vector2(-1.0f, 0.0f);

            //Translating to 4D World Coordinates
            UniVector3 WCposition = (inv_view_matrix * CCposition).Xyz;

            //Returning in 3D World Coordinates
            //needs to be trippled to clamp default UI (scale 1, Z = 0) objects to the mouse.
            return WCposition;
        }

        public static RigidBody createRigidBody(UniMatrix transform, CollisionShape shape, float mass = 0)
        {
            //rigidbody is dynamic if and only if mass is non zero, otherwise static
            bool isDynamic = (mass != 0.0f);

            BulletSharp.Math.Vector3 localInertia = UniVector3.Zero;
            if (isDynamic)
                shape.CalculateLocalInertia(mass, out localInertia);

            //using motionstate is recommended, it provides interpolation capabilities, and only synchronizes 'active' objects
            DefaultMotionState myMotionState = new DefaultMotionState(transform);

            RigidBodyConstructionInfo rbInfo = new RigidBodyConstructionInfo(mass, myMotionState, shape, localInertia);
            RigidBody body = new RigidBody(rbInfo);
            rbInfo.Dispose();
            return body;
        }


        #nullable enable
        public static gameObject? CheckClicked()
        {
            return null;
        }
    }
}
