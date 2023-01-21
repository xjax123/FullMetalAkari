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
        //temp objects
        private gameObject gameObj;
        private gameObject tempObj;

        private mouseHandler _mouse = new mouseHandler(); //Not sure why this needs to be a new object, it doesnt store any data, but it casues NullReferences otherwise.
        private Matrix4 view;
        private Matrix4 inv_view;
        private Matrix4 projection;
        private Matrix4 inv_projection;
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

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            Vector3 worldspaceMouse = _mouse.ConvertScreenToWorldSpace(MouseState.X,MouseState.Y, (float)Size.X, (float)Size.Y,inv_projection,inv_view);
            gameObj.translateObject(new Vector3(worldspaceMouse.X, worldspaceMouse.Y , 0.0f));

            tempObj.onRenderFrame();
            gameObj.onRenderFrame();
            SwapBuffers();
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.Viewport(0, 0, Size.X, Size.Y);
            GL.ClearColor(0.6f,0.6f,0.6f,1.0f);
            GL.Enable(EnableCap.DepthTest);

            CursorState = CursorState.Hidden;

            view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
            inv_view = Matrix4.Invert(view);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), Size.X / (float)Size.Y, 0.1f, 100.0f);
            inv_projection = Matrix4.Invert(projection);

            gameObj = new gameObject(1, projection, view, new Vector3(0.0f,0.0f,-2.0f), 1.0f);
            tempObj = new gameObject(1, projection, view, new Vector3(0.0f, 0.0f, -3.0f), 0.5f);
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            // Delete all the resources.
            GL.DeleteBuffer(gameObj.getVertexBufferObject());
            GL.DeleteVertexArray(gameObj.getVertexArrayObject());

            GL.DeleteProgram(gameObj.getShader().Handle);
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
