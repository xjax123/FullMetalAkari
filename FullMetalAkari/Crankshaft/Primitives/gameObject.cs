using System;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Crankshaft.Interfaces;
using Crankshaft.Handlers;

namespace Crankshaft.Primitives
{
    public class gameObject : IObject, IDisposable
    {
        //Overwrite/Hide this Data as needed, preferably in the constructor.
        protected readonly string objectID = "empty";
        protected string name = "Empty Game Object";
        protected readonly string shaderVert = "Crankshaft/Resources/Shaders/basicShader/basicShader.vert";
        protected readonly string shaderFrag = "Crankshaft/Resources/Shaders/basicShader/basicShader.frag";
        protected readonly string texPath = "Crankshaft/Resources/Textures/error_texture.png";
        protected readonly float[] vertices =
        {
           //Position           Texture coordinates
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        };
        protected readonly uint[] indices =
        {
            0, 1, 3,
            1, 2, 3
        };

        //Built in the Constructor, no need to Overwrite/Hide, Remember to set these in the constructor.
        protected int instanceID;

        protected Matrix4 curTranslation = Matrix4.Identity;
        protected Matrix4 trueTranslation;

        protected float scale;
        protected Matrix4 curScale;

        protected float rotation;
        protected Matrix4 curRot = Matrix4.Identity;
        protected Matrix4 trueRot;

        //Empties
        protected textureHandler texture;
        protected shaderHandler shader;

        protected int vertexBufferObject;
        protected int vertexArrayObject;
        protected int elementBufferObject;

        public gameObject(int instanceID, Crankshaft.Physics.Vector3 startingPos, float startingScale, float startingRot)
        {
            this.instanceID = instanceID;
            scale = startingScale;
            curScale = Matrix4.CreateScale(startingScale);
            trueTranslation = Matrix4.CreateTranslation(startingPos);
            rotation = startingRot;
            trueRot = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(startingRot));
            renderingHandler.basicRender(ref vertexArrayObject, ref vertexBufferObject, ref elementBufferObject, vertices, indices, ref shader, shaderVert, shaderFrag, ref texture, texPath);
        }
        ~gameObject()
        {
            Dispose();
        }

        public virtual void onClick()
        {
        }
        public virtual void onHover()
        {
        }

        public void Dispose()
        {
            GL.DeleteBuffer(getVertexBufferObject());
            GL.DeleteVertexArray(getVertexArrayObject());
            GL.DeleteProgram(getShader().Handle);
            GL.DeleteProgram(getTexture().Handle);
            Console.WriteLine("Disposed");
        }

        public virtual void onLoad()
        {
        }

        public virtual void onUpdateFrame()
        {
        }

        public virtual void onRenderFrame()
        {
            GL.BindVertexArray(vertexArrayObject);
            if (curTranslation != Matrix4.Identity)
            {
                trueTranslation = curTranslation;
            }
            if (curRot != Matrix4.Identity)
            {
                trueRot = curRot;
            }
            shader.Use();
            shader.SetMatrix4("translation", trueTranslation * curScale * trueRot);
            shader.SetMatrix4("projection", renderingHandler.ProjectionMatrix);
            shader.SetMatrix4("view", renderingHandler.ViewMatrix);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            curTranslation = Matrix4.Identity;
            curRot = Matrix4.Identity;
        }

        public virtual void translateObject(Vector3 translation)
        {
            Vector3 rotTranslation = translation;

            rotTranslation.Xy *= Matrix2.Invert(Matrix2.CreateRotation(MathHelper.DegreesToRadians(rotation)));

            curTranslation *= Matrix4.CreateTranslation(rotTranslation * (1 / scale));
        }

        public virtual void scaleObject(float scale)
        {
            this.scale = scale;
            curScale = Matrix4.CreateScale(scale);
        }

        public virtual void rotateObject(float rotation)
        {
            this.rotation = rotation;
            Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation));
        }

        public virtual string getObjID()
        {
            return objectID;
        }

        public virtual int getInstID()
        {
            return instanceID;
        }

        public virtual void setInstID(int v)
        {
            instanceID = v;
        }

        public virtual string getName()
        {
            return name;
        }

        public virtual void setName(string v)
        {
            name = v;
        }

        public virtual textureHandler getTexture()
        {
            return texture;
        }
        public virtual void setTexture(textureHandler texture)
        {
            this.texture = texture;
        }

        public virtual float[] getVerts()
        {
            return vertices;
        }

        public virtual shaderHandler getShader()
        {
            return shader;
        }
        public virtual void setShader(shaderHandler shader)
        {
            this.shader = shader;
        }

        public virtual uint[] getIndices()
        {
            return indices;
        }

        public virtual int getVertexBufferObject()
        {
            return vertexBufferObject;
        }

        public virtual void setVertexBufferObject(int v)
        {
            vertexBufferObject = v;
        }

        public virtual int getVertexArrayObject()
        {
            return vertexArrayObject;
        }

        public virtual void setVertexArrayObject(int v)
        {
            vertexArrayObject = v;
        }

        public virtual int getElementBufferObject()
        {
            return elementBufferObject;
        }

        public virtual void setElementBufferObject(int v)
        {
            elementBufferObject = v;
        }
    }
}
