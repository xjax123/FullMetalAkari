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
    public class gameObject : IComparable<gameObject>
    {
        //Overwrite/Hide this Data as needed, preferably in the constructor.
        protected string objectID = "empty";
        protected string name = "Empty Game Object";
        protected string shaderVert = "Crankshaft/Resources/Shaders/basicShader/basicShader.vert";
        protected string shaderFrag = "Crankshaft/Resources/Shaders/basicShader/basicShader.frag";
        protected string texPath = "Crankshaft/Resources/Textures/error_texture.png";
        protected float[] vertices =
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

        //Temp
        private BulletSharp.Math.Vector3 aabbmin;
        private BulletSharp.Math.Vector3 aabbmax;
        UniMatrix comb;


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

        protected bool clickable;

        //Colider Variables
        protected RigidBody rigid;
        protected float mass = 0;
        protected float coliderX = 1;
        protected float coliderY = 1;

        //Empties
        protected textureHandler texture;
        protected shaderHandler shader;
        protected TextureUnit tex;
        protected float[] debugVerts;
        protected uint[] debugIndices;

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
        public bool Clickable { get => clickable; set => clickable = value; }
        public BulletSharp.Math.Vector3 Aabbmax { get => aabbmax; set => aabbmax = value; }
        public BulletSharp.Math.Vector3 Aabbmin { get => aabbmin; set => aabbmin = value; }
        public string Name { get => name; set => name = value; }
        public float Mass { get => mass; set => mass = value; }
        public float ColiderX { get => coliderX; set => coliderX = value; }
        public float ColiderY { get => coliderY; set => coliderY = value; }

        public gameObject(objectData d)
        {
            this.InstanceID = d.InstanceID;
            Scale = d.Position.Scale;
            CurScale = Matrix4.CreateScale(d.Position.Scale);
            this.Position = new UniVector3(d.Position.X, d.Position.Y, d.Position.Z);
            TrueTranslation = Matrix4.CreateTranslation(new UniVector3(d.Position.X, d.Position.Y, d.Position.Z));
            Rotation = d.Position.Rotation;
            TrueRot = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(d.Position.Rotation));
            clickable = d.Clickable;
            mass = d.Mass;

            vertexArrayObject = GL.GenVertexArray();
            vertexBufferObject = GL.GenBuffer();
            elementBufferObject = GL.GenBuffer();
        }
        ~gameObject()
        {
            Dispose();
        }

        public virtual void onClick()
        {
            Debug.WriteLine($"Click Registered on: {name} ID:{InstanceID}");
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
            renderingHandler.basicRender(vertexArrayObject, vertexBufferObject, elementBufferObject, vertices, indices, ref shader, shaderVert, shaderFrag, ref texture, texPath);

            Matrix4 RigidTranslation = Matrix4.CreateTranslation(new UniVector3(position.X/(3 - position.Z), position.Y/(3 - position.Z), position.Z));
            comb = RigidTranslation * CurScale * TrueRot;

            if (Clickable != false)
            {
                Rigid = physicsHandler.createRigidBody(comb, new BoxShape(coliderX/(6 + -(2 * position.Z)), coliderY/(6 + -(2 * position.Z)), 0.0f), this, Mass);
                windowHandler.ActiveSim.addRigidToWorld(ref rigid);
            }
            else
            {
                Rigid = null;
            }

            if (rigid != null)
            {
                Rigid.CollisionShape.GetAabb(comb, out aabbmin, out aabbmax);
                UniVector3 rposition = Rigid.CenterOfMassPosition * (3 - position.Z);
                Debug.WriteLine($"{name} {instanceID} Max: {Aabbmax.X * (3 - position.Z)} {Aabbmax.Y * (3 - position.Z)} Min: {Aabbmin.X * (3 - position.Z)} {Aabbmin.Y * (3 - position.Z)}");

                debugVerts = new float[] {
                    //Position           Texture coordinates
                    Aabbmax.X*(3 - position.Z)-rposition.X,  Aabbmax.Y*(3 - position.Z)-rposition.Y, 0.0f, 1.0f, 1.0f, // top right
                    Aabbmax.X*(3 - position.Z)-rposition.X,  Aabbmin.Y*(3 - position.Z)-rposition.Y, 0.0f, 1.0f, 0.0f, // bottom right
                    Aabbmin.X*(3 - position.Z)-rposition.X,  Aabbmin.Y*(3 - position.Z)-rposition.Y, 0.0f, 0.0f, 0.0f, // bottom left
                    Aabbmin.X*(3 - position.Z)-rposition.X,  Aabbmax.Y*(3 - position.Z)-rposition.Y, 0.0f, 0.0f, 1.0f  // top left
                };
                debugIndices = new uint[]  {
                    0, 1, 3,
                    1, 2, 3
                };
            }
        }

        public virtual void onUpdateFrame()
        {
            if (rigid != null)
                Rigid.CollisionShape.GetAabb(comb, out aabbmin, out aabbmax);
        }

        public virtual void onRenderFrame()
        {
            if (CurTranslation != Matrix4.Identity)
            {
                TrueTranslation = CurTranslation;
            }
            if (CurRot != Matrix4.Identity)
            {
                TrueRot = CurRot;
            }
            Shader.Use();
            Shader.SetMatrix4("translation", TrueTranslation);
            Shader.SetMatrix4("projection", renderingHandler.ProjectionMatrix);
            Shader.SetMatrix4("view", renderingHandler.ViewMatrix);
            if (textureHandler.ActiveHandle != texture.Handle && windowHandler.DebugDraw == false)
            {
                texture.Use(TextureUnit.Texture0);
                Shader.SetBool("debug", false);
            } else if (windowHandler.DebugDraw == true)
            {
                Shader.SetBool("debug", true);
                GL.ActiveTexture(TextureUnit.Texture0);
                GL.BindTexture(TextureTarget.Texture2D, renderingHandler.debugHandle);
            }
            if (windowHandler.DebugDraw == true && rigid != null)
            {
                GL.BindVertexArray(vertexArrayObject);

                GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
                GL.BufferData(BufferTarget.ArrayBuffer, debugVerts.Length * sizeof(float), debugVerts, BufferUsageHint.StaticDraw);

                GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
                GL.DrawElements(PrimitiveType.LineLoop, indices.Length, DrawElementsType.UnsignedInt, 0);
            } else if (windowHandler.DebugDraw == true && rigid == null)
            {
                GL.BindVertexArray(vertexArrayObject);

                GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
                GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

                GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
                GL.DrawElements(PrimitiveType.LineLoop, indices.Length, DrawElementsType.UnsignedInt, 0);
            } else
            {
                GL.BindVertexArray(vertexArrayObject);

                GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
                GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

                GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
                GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            }

            CurTranslation = Matrix4.Identity;
            CurRot = Matrix4.Identity;
        }

        public virtual void translateObject(Vector3 translation)
        {
            Position += (UniVector3)translation;
            UniVector3 rotTranslation = Position - (UniVector3)translation;

            rotTranslation.Xy *= Matrix2.Invert(Matrix2.CreateRotation(MathHelper.DegreesToRadians(Rotation)));

            CurTranslation *= Matrix4.CreateTranslation(rotTranslation * (1 / Scale));
            rotTranslation.Xy /= (3 - position.Z);
            Matrix4 rigidTranslation = Matrix4.CreateTranslation(rotTranslation * (1 / Scale));
            if (rigid != null)
            {
                Rigid.MotionState.WorldTransform *= (UniMatrix)rigidTranslation;
            }
        }
        public virtual void setTranslation(Vector3 translation)
        {
            Position = (UniVector3)translation;
            UniVector3 rotTranslation = translation;

            rotTranslation.Xy *= Matrix2.Invert(Matrix2.CreateRotation(MathHelper.DegreesToRadians(Rotation)));

            curTranslation = Matrix4.CreateTranslation(rotTranslation * (1 / Scale));
            rotTranslation.Xy /= (3 - position.Z);
            Matrix4 rigidTranslation = Matrix4.CreateTranslation(rotTranslation * (1 / Scale));
            if (rigid != null)
            {
                Rigid.MotionState.WorldTransform = (UniMatrix)rigidTranslation;
            }
        }

        public virtual void scaleObject(float scale)
        {
            this.Scale = scale;
            CurScale = Matrix4.CreateScale(scale);
            Rigid.CollisionShape = new BoxShape(scale/16, scale/16, 0.1f);
            if (rigid != null)
            {
                Rigid.MotionState.WorldTransform *= CurScale;
            }
        }

        public virtual void rotateObject(float rotation)
        {
            this.Rotation = rotation;
            CurRot = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation));
            if (rigid != null)
            {
                Rigid.MotionState.WorldTransform *= CurRot;
            }
        }

        //important for rendering, otherwise things render all out of order depending on their instanceID, rather than their z cord.
        public int CompareTo(gameObject compare)
        {
            if (position.Z > compare.position.Z)
            {
                return 1;
            } else if (position.Z < compare.position.Z)
            {
                return -1;
            } else if (position.Z == compare.position.Z)
            {
                return 0;
            } else
            {
                return this.CompareTo(compare);
            }
        }
    }
}
