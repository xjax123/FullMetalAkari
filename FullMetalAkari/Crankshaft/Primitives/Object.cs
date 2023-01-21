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
        readonly renderingHandler render = new renderingHandler();
        private readonly String objectID = "base";
        private int instanceID;
        private String name = "Unknown Object";
        private float scale;
        private Matrix4 curScale;

        private shaderHandler shader;
        private readonly String shaderVert = "Crankshaft/Resources/Shaders/basicShader/basicShader.vert";
        private readonly String shaderFrag = "Crankshaft/Resources/Shaders/basicShader/basicShader.frag";

        private Matrix4 projectionMat;
        private Matrix4 viewMat;

        private textureHandler texture;
        private readonly String texPath = "Crankshaft/Resources/Textures/error_texture.png";
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

        private Matrix4 currentTranslation = Matrix4.Identity;

        public gameObject(int instanceID, Matrix4 projectionMat, Matrix4 viewMat, Vector3 startingPos, float startingScale)
        {
            this.instanceID = instanceID;
            this.projectionMat = projectionMat;
            this.viewMat = viewMat;
            this.scale = startingScale;
            this.curScale = Matrix4.CreateScale(startingScale);

            render.basicRender(ref vertexArrayObject, ref vertexBufferObject, ref elementBufferObject, vertices, indices, ref shader, shaderVert, shaderFrag, ref texture, texPath, startingPos, startingScale);
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

        public virtual void onUpdateFrame()
        {
            return;
        }

        public virtual void onRenderFrame()
        {
            GL.BindVertexArray(vertexArrayObject);
            shader.Use();
            shader.SetMatrix4("translation", currentTranslation * curScale);
            shader.SetMatrix4("projection", projectionMat);
            shader.SetMatrix4("view", viewMat);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            currentTranslation = Matrix4.Identity;
        }

        public virtual void translateObject(Vector3 translation)
        {
            currentTranslation *= Matrix4.CreateTranslation(translation*(1/scale));
        }

        public virtual void scaleObject(float scale)
        {
            this.scale = scale;
            this.curScale = Matrix4.CreateScale(scale);
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

        public virtual Matrix4 getProjectionMatrix()
        {
            return projectionMat;
        }

        public virtual void setProjectionMatrix(Matrix4 projection)
        {
            projectionMat = projection;
        }

        public virtual Matrix4 getViewMatrix()
        {
            return viewMat;
        }

        public virtual void setViewMatrix(Matrix4 view)
        {
            viewMat = view;
        }
    }
}
