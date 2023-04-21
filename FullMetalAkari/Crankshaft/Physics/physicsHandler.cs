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
        public static UniVector3 ConvertScreenToWorldSpaceVec3(float x, float y, float z = 0)
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
            return WCposition;
        }

        //TODO: Fix This
        /*
        public Vector3 Unproject(Vector3 source, Matrix4 world)
        {
            Matrix4 projection, view;
            projection = renderingHandler.ProjectionMatrix;
            view = renderingHandler.ViewMatrix;


            Matrix4 matrix = Matrix4.Invert(world * view * projection);
            source.X = (((source.X - this.X) / ((float)this.Width)) * 2f) - 1f;
            source.Y = -((((source.Y - this.Y) / ((float)this.Height)) * 2f) - 1f);
            source.Z = (source.Z - this.MinDepth) / (this.MaxDepth - this.MinDepth);
            Vector4 source4 = new Vector4(source, 1f);
            Vector4 vector = source4 * matrix;
            float a = (((source4.X * matrix.M14) + (source4.Y * matrix.M24)) + (source4.Z * matrix.M34)) + matrix.M44;
            if (!WithinEpsilon(a, 1f))
            {
                vector = (Vector4)(vector / a);
            }
            return vector.Xyz;
        }
        */

        private static bool WithinEpsilon(float a, float b)
        {
            float num = a - b;
            return ((-1.401298E-45f <= num) && (num <= float.Epsilon));
        }

        public static BoundRigidBody createRigidBody(UniMatrix transform, CollisionShape shape, gameObject obj, int iD, Matrix2 Col, float mass = 0)
        {
            //rigidbody is dynamic if and only if mass is non zero, otherwise static
            bool isDynamic = (mass != 0.0f);

            BulletSharp.Math.Vector3 localInertia = UniVector3.Zero;
            if (isDynamic)
                shape.CalculateLocalInertia(mass, out localInertia);

            //using motionstate is recommended, it provides interpolation capabilities, and only synchronizes 'active' objects
            DefaultMotionState myMotionState = new DefaultMotionState(transform);

            RigidBodyConstructionInfo rbInfo = new RigidBodyConstructionInfo(mass, myMotionState, shape, localInertia);
            BoundRigidBody body = new BoundRigidBody(rbInfo, obj, iD,Col);
            rbInfo.Dispose();
            return body;
        }


        #nullable enable
        public static void CheckClicked(UniVector3 wsMouse)
        {
            float x = wsMouse.X;
            float y = wsMouse.Y;
            for (int i = windowHandler.ActiveScene.objects.Count; i > 0; i--)
            {
                gameObject o = windowHandler.ActiveScene.objects[i-1];
                if (o.Clickable == false)
                {
                    continue;
                }
                foreach (BoundRigidBody b in o.Rigid)
                {
                    if (x >= b.aabbmin.X && x <= b.aabbmax.X)
                    {
                        if (y >= b.aabbmin.Y && y <= b.aabbmax.Y)
                        {
                            o.onClick(b.ID);
                            return;
                        }
                    }
                }
            }
        }
    }
}
