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
    public class gameObject : IObject
    {
        //Overwrite/Hide this Data as needed, preferably in the constructor.
        private readonly String objectID = "empty";
        private String name = "Empty Game Object";
        private readonly String shaderVert = "Crankshaft/Resources/Shaders/basicShader/basicShader.vert";
        private readonly String shaderFrag = "Crankshaft/Resources/Shaders/basicShader/basicShader.frag";
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

        //Built in the Constructor, no need to Overwrite/Hide, Remember to set these in the constructor.
        private int instanceID;

        private Matrix4 projectionMat;
        private Matrix4 viewMat;

        private Matrix4 curTranslation = Matrix4.Identity;
        private Matrix4 trueTranslation;

        private float scale;
        private Matrix4 curScale;

        private float rotation;
        private Matrix4 curRot = Matrix4.Identity;
        private Matrix4 trueRot;

        //Empties
        private textureHandler texture;
        private shaderHandler shader;

        private int vertexBufferObject;
        private int vertexArrayObject;
        private int elementBufferObject;

        public gameObject(int instanceID, Matrix4 projectionMat, Matrix4 viewMat, Vector3 startingPos, float startingScale, float startingRot)
        {
            this.instanceID = instanceID;
            this.projectionMat = projectionMat;
            this.viewMat = viewMat;
            this.scale = startingScale;
            this.curScale = Matrix4.CreateScale(startingScale);
            this.trueTranslation = Matrix4.CreateTranslation(startingPos);
            this.rotation = startingRot;
            this.trueRot = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(startingRot));
        }
        ~gameObject()
        {
            onDestroy();
        }

        public virtual void onClick()
        {
        }
        public virtual void onHover()
        {
        }

        public virtual void onDestroy()
        {
            GL.DeleteBuffer(getVertexBufferObject());
            GL.DeleteVertexArray(getVertexArrayObject());
            GL.DeleteProgram(getShader().Handle);
            GL.DeleteProgram(getTexture().Handle);
        }

        public virtual void onLoad()
        {
            renderingHandler.basicRender(ref vertexArrayObject, ref vertexBufferObject, ref elementBufferObject, vertices, indices, ref shader, shaderVert, shaderFrag, ref texture, texPath);
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
            shader.SetMatrix4("projection", projectionMat);
            shader.SetMatrix4("view", viewMat);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            curTranslation = Matrix4.Identity;
            curRot = Matrix4.Identity;
        }

        public virtual void translateObject(Vector3 translation)
        {
            Vector3 rotTranslation = translation;

            rotTranslation.Xy *= Matrix2.Invert(Matrix2.CreateRotation(MathHelper.DegreesToRadians(rotation)));

            curTranslation *= Matrix4.CreateTranslation(rotTranslation*(1/scale));
        }

        public virtual void scaleObject(float scale)
        {
            this.scale = scale;
            this.curScale = Matrix4.CreateScale(scale);
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
