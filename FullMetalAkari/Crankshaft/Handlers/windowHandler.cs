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
using Crankshaft.Primitives;
using FullMetalAkari.Game.Objects.UI;

namespace Crankshaft.Handlers
{
    public class windowHandler : GameWindow
    {
        //constructor passthroughs
        private GameWindowSettings gameWindowSettings;
        private NativeWindowSettings nativeWindowSettings;
        private string scenesFilePath;
        private string intialScene;


        //temp objects
        private gameObject gameObj;
        private gameObject tempObj;

        //
        // Summary:
        //     The base class to produce a functioning Gamewindow.
        //
        // Parameters:
        //   gameWindowSettings:
        //     The event arguments for this frame.
        //   nativeWindowSettings:
        //     native
        //   sceneFilePath:
        //     
        //   intialScene:
        //     
        public windowHandler(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings, string scenesFilePath, string intialScene) : base(gameWindowSettings, nativeWindowSettings)
        {
            this.gameWindowSettings = gameWindowSettings;
            this.nativeWindowSettings = nativeWindowSettings;
            this.scenesFilePath = scenesFilePath;
            this.intialScene = intialScene;
        }


        //Runs Every Frame
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            var input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            if (input.IsKeyDown(Keys.Enter)) {
                tempObj.Dispose();
                GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);
            }
        }
        private double _time;
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            _time += args.Time;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            gameObj.onRenderFrame();
            tempObj.onRenderFrame();

            SwapBuffers();
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.Viewport(0, 0, Size.X, Size.Y);
            GL.ClearColor(0.6f, 0.6f, 0.6f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            //CursorState = CursorState.Hidden;

            //Compile user-defined scenes in the directory given by the user.
            //sceneHandler.compileScenes(scenesFilePath);
            //sceneHandler.loadScene(intialScene);

            renderingHandler.ViewMatrix = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
            renderingHandler.InvertedView = Matrix4.Invert(renderingHandler.ViewMatrix);
            renderingHandler.ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), Size.X / (float)Size.Y, 0.1f, 100.0f);
            renderingHandler.InvertedProjection = Matrix4.Invert(renderingHandler.ProjectionMatrix);

            gameObj = new sniperCrosshair(1, new Vector3(0.0f, 0.0f, 0.0f), 1.0f, 0f, MouseState, this);
            tempObj = new gameObject(1, new Vector3(0.0f, 0.0f, -10.0f), 1.0f, 0f);
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
            renderingHandler.ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), Size.X / (float)Size.Y, 0.1f, 100.0f);
            renderingHandler.InvertedProjection = Matrix4.Invert(renderingHandler.ProjectionMatrix);
        }
    }
}
