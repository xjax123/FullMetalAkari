//Cody By: Jackson Maclean
//Generic
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
//OpenTK
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//Internal
using FullMetalAkari.Crankshaft.Handlers;
using FullMetalAkari.Crankshaft.Primitives;

namespace FullMetalAkari
{
    public class windowHandler : GameWindow
    {
        //Temporary Verts & Indecies.
        //TODO: Remove This
        //Storing of this data should be handled by the object, not in windowHandler.
        private gameObject gameObj;
        private mouseHandler _mouse = new mouseHandler();
        private Matrix4 view;
        private Matrix4 projection;
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
        private double _time;
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            _time += args.Time;
            textureHandler texture = gameObj.getTexture();
            shaderHandler shader = gameObj.GetShader();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.BindVertexArray(gameObj.getVertexArrayObject());
            
            Vector3 worldspaceMouse = _mouse.ConvertMouseToWorldSpace(MouseState.X,MouseState.Y, (float)Size.X, (float)Size.Y,projection,view);

            shader.Use();
            //Test, setting the object's position to the mouse.
            shader.SetMatrix4("translation", Matrix4.Identity * Matrix4.CreateTranslation((float)worldspaceMouse.X*3, worldspaceMouse.Y*3, 0.0f));
            shader.SetMatrix4("projection", projection);
            shader.SetMatrix4("view", view);

            GL.DrawElements(PrimitiveType.Triangles, gameObj.getIndices().Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.Viewport(0, 0, Size.X, Size.Y);
            GL.ClearColor(0.6f,0.6f,0.6f,1.0f);
            GL.Enable(EnableCap.DepthTest);

            //CursorState = CursorState.Hidden;

            view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), Size.X / (float)Size.Y, 0.1f, 100.0f);

            gameObj = new gameObject(1);
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            // Delete all the resources.
            GL.DeleteBuffer(gameObj.getVertexBufferObject());
            GL.DeleteVertexArray(gameObj.getVertexArrayObject());

            GL.DeleteProgram(gameObj.GetShader().Handle);
            GL.DeleteProgram(gameObj.getTexture().Handle);

            base.OnUnload();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
        }
    }
}
