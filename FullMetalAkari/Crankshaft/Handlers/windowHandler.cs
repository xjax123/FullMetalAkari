//Cody By: Jackson Maclean
//Generic
using System;
using System.Collections.Generic;
using System.Text;
//OpenTK
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//Internal
using FullMetalAkari.Shaders;
using FullMetalAkari.Crankshaft.Handlers;

namespace FullMetalAkari
{
    public class windowHandler : GameWindow
    {
        //Temporary Verts & Indecies.
        //TODO: Remove This
        private readonly float[] _vertices =
        {
           //Position           Texture coordinates
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        };
        private readonly uint[] _indices =
        {
            0, 1, 3,
            1, 2, 3 
        };


        private int _vertexBufferObject;

        private int _vertexArrayObject;

        private shaderHandler shader;

        private textureHandler texture;

        private int _elementBufferObject;

        //Generic Constructor
        public windowHandler(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings) {}


        //Runs Every Frame
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            var input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.BindVertexArray(_vertexArrayObject);

            texture.Use(TextureUnit.Texture0);
            shader.Use();

            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.6f,0.6f,0.6f,1.0f);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            shader = new shaderHandler("Crankshaft/Resources/Shaders/basicShader/basicShader.vert", "Crankshaft/Resources/Shaders/basicShader/basicShader.frag");
            shader.Use();

            var vertexLoc = shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLoc);
            GL.VertexAttribPointer(vertexLoc, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float),0 );

            var texCoordLoc = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLoc);
            GL.VertexAttribPointer(texCoordLoc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture = textureHandler.LoadFromFile("Crankshaft/Resources/Textures/error_texture.png");
            texture.Use(TextureUnit.Texture0);
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            // Delete all the resources.
            GL.DeleteBuffer(_vertexBufferObject);
            GL.DeleteVertexArray(_vertexArrayObject);

            GL.DeleteProgram(shader.Handle);

            base.OnUnload();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
        }
    }
}
