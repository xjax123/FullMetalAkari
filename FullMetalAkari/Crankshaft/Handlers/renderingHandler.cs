using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace FullMetalAkari.Crankshaft.Handlers
{
    public class renderingHandler
    {
        public void basicRender(ref int vertexArrayObject, ref int vertexBufferObject, ref int elementBufferObject, float[] vertices, uint[] indices, ref shaderHandler shader, String shaderVert, String shaderFrag, ref textureHandler texture, String texPath, Vector3 startingPos, float startingScale)
        {
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

            shader.SetMatrix4("translation", Matrix4.CreateScale(startingScale));
            shader.SetMatrix4("translation", Matrix4.CreateTranslation(startingPos));

            var vertexLoc = shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLoc);
            GL.VertexAttribPointer(vertexLoc, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            var texCoordLoc = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLoc);
            GL.VertexAttribPointer(texCoordLoc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture = textureHandler.LoadFromFile(texPath);
            texture.Use(TextureUnit.Texture0);
        }
    }
}
