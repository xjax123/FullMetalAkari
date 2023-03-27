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
using System.Collections.Generic;

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
        protected bool visible;

        //Colider Variables
        protected List<BoundRigidBody> rigid = new List<BoundRigidBody>();
        protected float mass = 0;
        /// <summary>
        /// ScaleX, ScaleY, OffsetX, OffsetY
        /// </summary>
        private List<Matrix2> colider = new List<Matrix2>();

        //Timing Objects
        protected double appear;
        protected double disappear;

        //Empties
        protected textureHandler texture;
        protected shaderHandler shader;
        protected TextureUnit tex;
        protected float[] debugVerts;
        protected uint[] debugIndices;

        protected int vertexBufferObject;
        protected int vertexArrayObject;
        protected int elementBufferObject;
        private objectData data;

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
        public int VertexBufferObject { get => vertexBufferObject; set => vertexBufferObject = value; }
        public int VertexArrayObject { get => vertexArrayObject; set => vertexArrayObject = value; }
        public int ElementBufferObject { get => elementBufferObject; set => elementBufferObject = value; }
        public UniVector3 Position { get => position; set => position = value; }
        public bool Clickable { get => clickable; set => clickable = value; }
        public string Name { get => name; set => name = value; }
        public float Mass { get => mass; set => mass = value; }
        public List<BoundRigidBody> Rigid { get => rigid; set => rigid = value; }
        public List<Matrix2> Colider { get => colider; set => colider = value; }
        public objectData Data { get => data; set => data = value; }
        public bool Visible { get => visible; set => visible = value; }
        public double Appear { get => appear; set => appear = value; }
        public double Disappear { get => disappear; set => disappear = value; }

        public gameObject(objectData d)
        {
            Data = d;
            this.InstanceID = d.InstanceID;
            Scale = d.Position.Scale;
            CurScale = Matrix4.CreateScale(Scale);
            this.Position = new UniVector3(d.Position.X, d.Position.Y, d.Position.Z);
            Rotation = d.Position.Rotation;
            TrueRot = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(d.Position.Rotation));
            TrueTranslation = Matrix4.CreateTranslation(new UniVector3(d.Position.X, d.Position.Y, d.Position.Z))*TrueRot*CurScale;
            clickable = d.Clickable;
            mass = d.Mass;
            if (d.Timing != null)
            {
                Appear = (double)d.Timing.AppearTime;
                Disappear = (double)d.Timing.DisappearTime;
            } else
            {
                visible = true;
            }

            vertexArrayObject = GL.GenVertexArray();
            vertexBufferObject = GL.GenBuffer();
            elementBufferObject = GL.GenBuffer();
        }
        ~gameObject()
        {
            Dispose();
        }

        public virtual void onClick(int ID)
        {
            Debug.WriteLine($"Click Registered on: {name} Hotbox ID:{ID}");
        }
        //Not Implemented.
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
            if (Colider.Count == 0)
            {
                Colider.Add(new Matrix2(1,1,0,0));
            }
            renderingHandler.basicRender(vertexArrayObject, vertexBufferObject, elementBufferObject, vertices, indices, ref shader, shaderVert, shaderFrag, ref texture, texPath);
            Matrix4 combined;
            int i = 0;
            foreach (Matrix2 c in Colider)
            {
                Matrix4 RigidTranslation = Matrix4.CreateTranslation(new UniVector3((position.X) / (3 - position.Z), (position.Y) / (3 - position.Z), position.Z));
                combined = RigidTranslation * CurScale * TrueRot;

                if (Clickable != false)
                {
                    Rigid.Add(physicsHandler.createRigidBody(combined, new BoxShape(scale * (c.M11 / ((3 - position.Z) * 2)), scale * (c.M12 / ((3 - position.Z) * 2)), 0.0f), this, i, c, Mass));
                    RigidBody temp = Rigid[i];
                    windowHandler.ActiveSim.addRigidToWorld(ref temp);
                }

                if (Rigid.Count != 0)
                {
                    Rigid[i].CollisionShape.GetAabb((UniMatrix)combined, out Rigid[i].aabbmin, out Rigid[i].aabbmax);
                    
                    Rigid[i].verts = new float[] {
                    //Position           Texture coordinates
                    Rigid[i].aabbmax.X*(3 - position.Z)-position.X,  Rigid[i].aabbmax.Y*(3 - position.Z)-position.Y, 0.0f, 1.0f, 1.0f, // top right
                    Rigid[i].aabbmax.X*(3 - position.Z)-position.X,  Rigid[i].aabbmin.Y*(3 - position.Z)-position.Y, 0.0f, 1.0f, 0.0f, // bottom right
                    Rigid[i].aabbmin.X*(3 - position.Z)-position.X,  Rigid[i].aabbmin.Y*(3 - position.Z)-position.Y, 0.0f, 0.0f, 0.0f, // bottom left
                    Rigid[i].aabbmin.X*(3 - position.Z)-position.X,  Rigid[i].aabbmax.Y*(3 - position.Z)-position.Y, 0.0f, 0.0f, 1.0f  // top left
                };
                    Rigid[i].ind = new uint[]  {
                    0, 1, 3,
                    1, 2, 3
                };
                }
                i++;
            }
        }

        public virtual void onUpdateFrame(double time)
        {
            if (time > appear && visible != true && Data.Timing != null)
            {
                visible = true;
            }

            if (time > disappear && Data.Timing != null)
            {
                windowHandler.Cleanup.Add(this);
            }

            if (Rigid != null || Rigid.Count != 0)
            {
                foreach (BoundRigidBody b in Rigid)
                {
                    Matrix4 combined;
                    Matrix4 RigidTranslation = Matrix4.CreateTranslation(new UniVector3(position.X / (3 - position.Z) + b.Colider.M21/(1 + -(Position.Z + 5) * 0.12f), position.Y / (3 - position.Z) + b.Colider.M22/(1+-(Position.Z+5)*0.12f), position.Z));
                    combined = RigidTranslation * CurScale * TrueRot;
                    b.CollisionShape.GetAabb((UniMatrix)combined, out b.aabbmin, out b.aabbmax);

                    b.verts = new float[] {
                    //Position           Texture coordinates
                    b.aabbmax.X*(3 - position.Z)-position.X,  b.aabbmax.Y*(3 - position.Z)-position.Y, 0.0f, 1.0f, 1.0f, // top right
                    b.aabbmax.X*(3 - position.Z)-position.X,  b.aabbmin.Y*(3 - position.Z)-position.Y, 0.0f, 1.0f, 0.0f, // bottom right
                    b.aabbmin.X*(3 - position.Z)-position.X,  b.aabbmin.Y*(3 - position.Z)-position.Y, 0.0f, 0.0f, 0.0f, // bottom left
                    b.aabbmin.X*(3 - position.Z)-position.X,  b.aabbmax.Y*(3 - position.Z)-position.Y, 0.0f, 0.0f, 1.0f  // top left
                };
                    b.ind = new uint[]  {
                    0, 1, 3,
                    1, 2, 3
                };
                }
            }
        }
        
        public virtual void Animate(double time)
        {

        }

        public virtual void onRenderFrame()
        {
            //stops rendering if the object is invisible
            if (Visible == false)
            {
                return;
            }

            //rendering object, likely ineffcient to do it this way.
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            if (CurTranslation != Matrix4.Identity)
            {
                TrueTranslation = CurTranslation * curScale * curRot;
            }
            if (CurRot != Matrix4.Identity)
            {
                TrueRot = CurRot;
            }
            //setting shader uniforms
            Shader.Use();
            Shader.SetMatrix4("translation", TrueTranslation);
            Shader.SetMatrix4("projection", renderingHandler.ProjectionMatrix);
            Shader.SetMatrix4("view", renderingHandler.ViewMatrix);
            //binding texture & ensuring debug mode draws black lines instead of a texture.
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
            //draw calls
            if (windowHandler.DebugDraw == true && Rigid.Count != 0)
            {
                foreach (BoundRigidBody b in Rigid)
                {
                    renderingHandler.DrawScene(vertexArrayObject, vertexBufferObject, elementBufferObject, b.verts, b.ind, PrimitiveType.LineLoop);
                }
            } else if (windowHandler.DebugDraw == true && Rigid.Count == 0)
            {
                renderingHandler.DrawScene(vertexArrayObject, vertexBufferObject, elementBufferObject, vertices, indices, PrimitiveType.LineLoop);
            } else
            {
                renderingHandler.DrawScene(vertexArrayObject, vertexBufferObject, elementBufferObject, vertices, indices, PrimitiveType.Triangles);
            }

            //resetting current translation to identity.
            CurTranslation = Matrix4.Identity;
            CurRot = Matrix4.Identity;
        }

        public virtual void translateObject(Vector3 translation)
        {
            Position += (UniVector3)translation;
            UniVector3 rotTranslation = Position;

            rotTranslation.Xy *= Matrix2.Invert(Matrix2.CreateRotation(MathHelper.DegreesToRadians(Rotation)));

            CurTranslation *= Matrix4.CreateTranslation(rotTranslation * (1 / Scale));
            rotTranslation.Xy /= (3 - position.Z);
            Matrix4 rigidTranslation = Matrix4.CreateTranslation(rotTranslation * (1 / Scale));
            if (Rigid != null || Rigid.Count != 0)
            {
                foreach (BoundRigidBody b in Rigid)
                {
                    b.MotionState.WorldTransform *= (UniMatrix)rigidTranslation;
                }
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
            if (Rigid != null || Rigid.Count != 0)
            {
                foreach (BoundRigidBody b in Rigid)
                {
                    b.MotionState.WorldTransform = (UniMatrix)rigidTranslation;
                }
            }
        }

        public virtual void scaleObject(float scale)
        {
            this.Scale = scale;
            CurScale = Matrix4.CreateScale(scale);
            if (Rigid != null || Rigid.Count != 0)
            {
                foreach (BoundRigidBody b in Rigid)
                {
                    b.CollisionShape = new BoxShape((b.Colider.M11 / ((3 - position.Z) * 2)) * scale, (b.Colider.M12 / ((3 - position.Z) * 2)) * scale, 0.0f);
                    b.MotionState.WorldTransform *= CurScale;
                }
            }
        }

        public virtual void rotateObject(float rotation)
        {
            this.Rotation = rotation;
            CurRot = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation));
            if (Rigid != null || Rigid.Count != 0)
            {
                foreach (BoundRigidBody b in Rigid)
                {
                    b.MotionState.WorldTransform *= CurRot;
                }
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

        public virtual void onResize()
        {

        }
    }
}
