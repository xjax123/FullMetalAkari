using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using BulletSharp;
using Crankshaft.Physics;
using Crankshaft.Primitives;

namespace Crankshaft.Handlers
{
    public static class renderingHandler
    {
        //Projection matrixes are stored here.
        //They used to be stored locally where needed, but in generaly the whole game should only have 1 view and projection matrix due to only having 1 camera.
        //Passing them around where they were needed became a pain in the ass, so i moved them here.
        private static Matrix4 projectionMatrix;
        private static Matrix4 invertedProjection;
        private static Matrix4 viewMatrix;
        private static Matrix4 invertedView;
        private static UniVector3 viewPosition;
        private static Matrix4 orthoProjection;

        public static int debugHandle;

        public static Matrix4 ProjectionMatrix { get => projectionMatrix; set => projectionMatrix = value; }
        public static Matrix4 ViewMatrix { get => viewMatrix; set => viewMatrix = value; }
        public static Matrix4 InvertedProjection { get => invertedProjection; set => invertedProjection = value; }
        public static Matrix4 InvertedView { get => invertedView; set => invertedView = value; }
        public static Matrix4 OrthoProjection { get => orthoProjection; set => orthoProjection = value; }
        public static UniVector3 ViewPosition { get => viewPosition; set => viewPosition = value; }

        public static void basicRender(int vertexArrayObject, int vertexBufferObject, int elementBufferObject, float[] vertices, uint[] indices, ref shaderHandler shader, string shaderVert, string shaderFrag, ref textureHandler texture, string texPath)
        {

            GL.BindVertexArray(vertexArrayObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            shader = new shaderHandler(shaderVert, shaderFrag);
            shader.Use();

            var vertexLoc = shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLoc);
            GL.VertexAttribPointer(vertexLoc, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            var texCoordLoc = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLoc);
            GL.VertexAttribPointer(texCoordLoc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            texture = textureHandler.LoadFromFile(texPath, TextureUnit.Texture0);
            texture.Use(TextureUnit.Texture0);
        }


        public static void DrawScene(int vao, int vbo, int ebo, float[] verts, uint[] indices, PrimitiveType type)
        {
            GL.BindVertexArray(vao);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, verts.Length * sizeof(float), verts, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
            GL.DrawElements(type, indices.Length, DrawElementsType.UnsignedInt, 0);
        }
    }
}
