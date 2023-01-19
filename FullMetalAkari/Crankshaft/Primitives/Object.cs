using System;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using FullMetalAkari.Crankshaft.Interfaces;
using FullMetalAkari.Crankshaft.Handlers;

namespace FullMetalAkari.Crankshaft.Primitives
{
    class gameObject : IObject
    {
        //stored data
        private String objectID = "base";
        private int instanceID;
        private String name = "Unknown Object";

        private shaderHandler shader;
        private String shaderVert = "Crankshaft/Resources/Shaders/basicShader/basicShader.vert";
        private String shaderFrag = "Crankshaft/Resources/Shaders/basicShader/basicShader.frag";

        private textureHandler texture;
        private String texPath = "Crankshaft/Resources/Textures/error_texture.png";
        private readonly float[] vertices =
        {
           //Position           Texture coordinates
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        };
        private readonly uint[] indices =
        {
            0, 1, 3,
            1, 2, 3
        };
        private int vertexBufferObject;

        private int vertexArrayObject;

        private int elementBufferObject;

        public gameObject(int instanceID)
        {
            this.instanceID = instanceID;
            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);

            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            shader = new shaderHandler(shaderVert, shaderFrag);
            shader.Use();

            shader.SetMatrix4("translation", Matrix4.CreateScale(1));
            shader.SetMatrix4("translation", Matrix4.CreateTranslation(0.0f,0.0f,-2.0f));

            var vertexLoc = shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLoc);
            GL.VertexAttribPointer(vertexLoc, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            var texCoordLoc = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLoc);
            GL.VertexAttribPointer(texCoordLoc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture = textureHandler.LoadFromFile(texPath);
            texture.Use(TextureUnit.Texture0);
        }

        public virtual void onClick()
        {
            throw new NotImplementedException();
        }

        public virtual void onDestroy()
        {
            throw new NotImplementedException();
        }

        public virtual void onLoad()
        {
            throw new NotImplementedException();
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

        public virtual void setName(String v)
        {
            name = v;
        }

        public virtual textureHandler getTexture()
        {
            return texture;
        }

        public virtual float[] getVerts()
        {
            return vertices;
        }

        public virtual shaderHandler GetShader()
        {
            return shader;
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
