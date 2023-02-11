using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Mathematics;
using BulletSharp;
using Crankshaft.Physics;
using Crankshaft.Handlers;
using Crankshaft.Primitives;
using System.Diagnostics;

namespace Crankshaft.Physics
{
    public static class physicsHandler
    {
        public static UniVector3 ConvertScreenToWorldSpaceVec3(float x, float y, float z)
        {
            //Translating to 3D Normalized Device Coordinates
            //Translating to 4D Homogeneous Clip Coordinates
            Vector4 HCCposition = new Vector4(2.0f * x / windowHandler.ActiveWindow.Size.X - 1.0f, 1.0f - 2.0f * y / windowHandler.ActiveWindow.Size.Y, z, 1.0f);

            //Translating to 4D Camera Coordinates
            Vector4 CCposition = renderingHandler.InvertedProjection * HCCposition;
            CCposition.Zw = new Vector2(z, 0.0f);

            //Translating to 4D World Coordinates
            UniVector3 WCposition = (renderingHandler.InvertedView * CCposition).Xyz;

            //Returning in 3D World Coordinates
            //needs to be trippled to clamp default UI (scale 1, Z = 0) objects to the mouse.
            return WCposition;
        }

        public static Vector4 ConvertScreenToWorldSpaceVec4(float x, float y, float z)
        {
            //Translating to 3D Normalized Device Coordinates
            //Translating to 4D Homogeneous Clip Coordinates
            Vector4 HCCposition = new Vector4(2.0f * x / windowHandler.ActiveWindow.Size.X - 1.0f, 1.0f - 2.0f * y / windowHandler.ActiveWindow.Size.Y, z, 1.0f);

            //Translating to 4D Camera Coordinates
            Vector4 CCposition = renderingHandler.InvertedProjection * HCCposition;
            CCposition.Zw = new Vector2(z, 0.0f);

            //Translating to 4D World Coordinates
            Vector4 WCposition = renderingHandler.InvertedView * CCposition;
            CCposition.Zw = new Vector2(z, 0.0f);

            //Returning in 3D World Coordinates
            //needs to be trippled to clamp default UI (scale 1, Z = 0) objects to the mouse.
            return WCposition;
        }

        public static BoundRigidBody createRigidBody(UniMatrix transform, CollisionShape shape, gameObject obj, float mass = 0)
        {
            //rigidbody is dynamic if and only if mass is non zero, otherwise static
            bool isDynamic = (mass != 0.0f);

            BulletSharp.Math.Vector3 localInertia = UniVector3.Zero;
            if (isDynamic)
                shape.CalculateLocalInertia(mass, out localInertia);

            //using motionstate is recommended, it provides interpolation capabilities, and only synchronizes 'active' objects
            DefaultMotionState myMotionState = new DefaultMotionState(transform);

            RigidBodyConstructionInfo rbInfo = new RigidBodyConstructionInfo(mass, myMotionState, shape, localInertia);
            BoundRigidBody body = new BoundRigidBody(rbInfo, obj);
            rbInfo.Dispose();
            return body;
        }


        #nullable enable
        public static int? CheckClicked()
        {
            Vector4 rayStart = ConvertScreenToWorldSpaceVec4(windowHandler.ActiveMouse.X,windowHandler.ActiveMouse.Y, -1.0f);
            Vector4 rayEnd = ConvertScreenToWorldSpaceVec4(windowHandler.ActiveMouse.X, windowHandler.ActiveMouse.Y, 0.0f);
            BulletSharp.Math.Vector3 rayDir = (UniVector3) Vector3.Normalize(new Vector3(rayStart - rayEnd));

            BulletSharp.Math.Vector3 out_origin = new UniVector3(rayStart.Xyz);
            BulletSharp.Math.Vector3 out_end = out_origin + rayDir*1000.0f;
            ClosestRayResultCallback rayResult = new ClosestRayResultCallback(ref out_origin, ref out_end);

            windowHandler.ActiveSim.World.RayTest(out_origin, out_end, rayResult);

            if (rayResult.HasHit)
            {
                BoundRigidBody rigid = (BoundRigidBody) rayResult.CollisionObject;
                return rigid.Obj.InstanceID;
            }

            return null;
        }
    }
}
