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
        public static gameObject? CheckClicked()
        {
            UniVector3 wsMouse = ConvertScreenToWorldSpaceVec3(windowHandler.ActiveMouse.X, windowHandler.ActiveMouse.Y);
            float x = wsMouse.X;
            float y = wsMouse.Y;
            Debug.WriteLine($"Click Position: X:{x} Y:{y}");
            for (int i = windowHandler.ActiveScene.objects.Count; i > 0; i--)
            {
                gameObject o = windowHandler.ActiveScene.objects[i-1];
                if (o.Clickable == false)
                {
                    continue;
                }
                Debug.WriteLine($"{o.Name}:");
                Debug.WriteLine($"Z:{o.Position.Z}");
                Debug.WriteLine($"X: Max:{o.Aabbmax.X} Min:{o.Aabbmin.X}");
                if(x >= o.Aabbmin.X && x <= o.Aabbmax.X)
                {

                    Debug.WriteLine($"Y: Max:{o.Aabbmax.Y} Min:{o.Aabbmin.Y}");
                    if (y >= o.Aabbmin.Y && y <= o.Aabbmax.Y)
                    {
                        Debug.WriteLine("Clicked");
                        return o;
                    }
                }
            }
            return null;
        }
    }
}
