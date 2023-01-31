using System;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Crankshaft.Handlers;
using Crankshaft.Physics;
using Crankshaft.Data;
using BulletSharp;
using System.Diagnostics;

namespace Crankshaft.Primitives
{
    public class gameObject : IDisposable
    {
        //Overwrite/Hide this Data as needed, preferably in the constructor.
        protected static readonly string objectID = "empty";
        protected static string name = "Empty Game Object";
        protected static readonly string shaderVert = "Crankshaft/Resources/Shaders/basicShader/basicShader.vert";
        protected static readonly string shaderFrag = "Crankshaft/Resources/Shaders/basicShader/basicShader.frag";
        protected static readonly string texPath = "Crankshaft/Resources/Textures/error_texture.png";
        protected static readonly float[] vertices =
        {
           //Position           Texture coordinates
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        };
        protected static readonly uint[] indices =
        {
            0, 1, 3,
            1, 2, 3
        };


        //Built in the Constructor, no need to Overwrite/Hide, Remember to set these in the constructor.
        protected int instanceID;

        protected UniVector3 position;
        protected UniMatrix curTranslation = Matrix4.Identity;
        protected UniMatrix trueTranslation;

        protected float scale;
        protected UniMatrix curScale;

        protected float rotation;
        protected UniMatrix curRot = Matrix4.Identity;
        protected UniMatrix trueRot;

        //Empties
        protected textureHandler texture;
        protected shaderHandler shader;
        protected RigidBody rigid;

        protected int vertexBufferObject;
        protected int vertexArrayObject;
        protected int elementBufferObject;

        //bunch of auto-generated properties, to replace the old java style accessors.
        public int InstanceID { get => instanceID; set => instanceID = value; }
        public UniMatrix CurTranslation { get => curTranslation; set => curTranslation = value; }
        public UniMatrix TrueTranslation { get => trueTranslation; set => trueTranslation = value; }
        public float Scale { get => scale; set => scale = value; }
        public UniMatrix CurScale { get => curScale; set => curScale = value; }
        public float Rotation { get => rotation; set => rotation = value; }
        public UniMatrix CurRot { get => curRot; set => curRot = value; }
        public UniMatrix TrueRot { get => trueRot; set => trueRot = value; }
        public textureHandler Texture { get => texture; set => texture = value; }
        public shaderHandler Shader { get => shader; set => shader = value; }
        public RigidBody Rigid { get => rigid; set => rigid = value; }
        public int VertexBufferObject { get => vertexBufferObject; set => vertexBufferObject = value; }
        public int VertexArrayObject { get => vertexArrayObject; set => vertexArrayObject = value; }
        public int ElementBufferObject { get => elementBufferObject; set => elementBufferObject = value; }
        public UniVector3 Position { get => position; set => position = value; }

        public gameObject(objectData d)
        {
            this.InstanceID = d.InstanceID;
            Scale = d.position.scale;
            CurScale = Matrix4.CreateScale(d.position.scale);
            Position = new UniVector3(d.position.X, d.position.Y, d.position.Z);
            TrueTranslation = Matrix4.CreateTranslation(new UniVector3(d.position.X,d.position.Y,d.position.Z));
            Rotation = d.position.rotation;
            TrueRot = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(d.position.rotation));
            renderingHandler.basicRender(ref vertexArrayObject, ref vertexBufferObject, ref elementBufferObject, vertices, indices, ref shader, shaderVert, shaderFrag, ref texture, texPath);
            UniMatrix comb = TrueTranslation * CurScale * TrueRot;
            Rigid = physicsHandler.createRigidBody(comb, new BoxShape(Scale/2,Scale/2,0.1f));
            windowHandler.ActiveSim.addRigidToWorld(ref rigid);
        }
        ~gameObject()
        {
            Dispose();
        }

        public virtual void onClick()
        {
            GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);
        }
        public virtual void onHover()
        {
        }

        public void Dispose()
        {
            GL.DeleteBuffer(VertexBufferObject);
            GL.DeleteVertexArray(VertexArrayObject);
            GL.DeleteProgram(Shader.Handle);
            GL.DeleteProgram(Texture.Handle);
            Debug.WriteLine($"{objectID} {InstanceID} Disposed");
        }

        public virtual void onLoad()
        {
        }

        public virtual void onUpdateFrame()
        {
        }

        public virtual void onRenderFrame()
        {
            GL.BindVertexArray(VertexArrayObject);
            if (CurTranslation != Matrix4.Identity)
            {
                TrueTranslation = CurTranslation;
            }
            if (CurRot != Matrix4.Identity)
            {
                TrueRot = CurRot;
            }
            Shader.Use();
            Matrix4 temp = (UniMatrix)Rigid.MotionState.WorldTransform;
            Shader.SetMatrix4("translation", temp);
            Shader.SetMatrix4("projection", renderingHandler.ProjectionMatrix);
            Shader.SetMatrix4("view", renderingHandler.ViewMatrix);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            CurTranslation = Matrix4.Identity;
            CurRot = Matrix4.Identity;
        }

        public virtual void translateObject(Vector3 translation)
        {
            Position += (UniVector3)translation;
            UniVector3 rotTranslation = translation;

            rotTranslation.Xy *= Matrix2.Invert(Matrix2.CreateRotation(MathHelper.DegreesToRadians(Rotation)));

            CurTranslation *= Matrix4.CreateTranslation(rotTranslation * (1 / Scale));
            Rigid.MotionState.WorldTransform *= CurTranslation;
        }
        public virtual void setTranslation(Vector3 translation)
        {
            Position = (UniVector3)translation;
            UniVector3 rotTranslation = translation;

            rotTranslation.Xy *= Matrix2.Invert(Matrix2.CreateRotation(MathHelper.DegreesToRadians(Rotation)));

            curTranslation = Matrix4.CreateTranslation(rotTranslation * (1 / Scale));
            Rigid.MotionState.WorldTransform = CurTranslation;
        }

        public virtual void scaleObject(float scale)
        {
            this.Scale = scale;
            CurScale = Matrix4.CreateScale(scale);
            Rigid.CollisionShape = new BoxShape(scale/2, scale/2, 0.1f);
            Rigid.MotionState.WorldTransform *= CurScale;
        }

        public virtual void rotateObject(float rotation)
        {
            this.Rotation = rotation;
            CurRot = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation));
            Rigid.MotionState.WorldTransform *= CurRot;
        }
    }
}
