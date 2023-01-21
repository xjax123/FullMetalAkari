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
        private readonly float[] _vertices =
        {
           //Position           Texture coordinates
             0.5f,  1.0f, 0.0f, 1.0f, 1.0f, // top right
             0.5f, -1.0f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -1,0f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  1.0f, 0.0f, 0.0f, 1.0f  // top left
        };

        //temp objects
        private gameObject gameObj;
        private gameObject tempObj;

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
            
            Vector3 worldspaceMouse = mouseHandler.ConvertScreenToWorldSpace(MouseState.X,MouseState.Y, (float)Size.X, (float)Size.Y,inv_projection,inv_view);
            gameObj.translateObject(new Vector3(worldspaceMouse.X*3, worldspaceMouse.Y*3, 0.0f));

            gameObj.onRenderFrame();
            tempObj.onRenderFrame();

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
            inv_view = Matrix4.Invert(view);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), (float)Size.X / (float)Size.Y, 0.1f, 100.0f);
            inv_projection = Matrix4.Invert(projection);

            gameObj = new gameObject(1, projection, view, new Vector3(0.0f, 0.0f, -5.0f), 1.0f, 0f);

            tempObj = new gameObject(1, projection, view, new Vector3(0.0f, 0.0f, -10.0f), 1.0f, 0f);
            gameObj.onLoad();
            tempObj.onLoad();
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            base.OnUnload();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
        }
    }
}
