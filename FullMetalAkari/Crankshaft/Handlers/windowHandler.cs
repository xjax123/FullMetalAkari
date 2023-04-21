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
using OpenTK.Audio;
//Internal
using Crankshaft.Primitives;
using Crankshaft.Physics;
using FullMetalAkari.Game.Objects.UI;
using Crankshaft.Data;
using System.Media;
using FullMetalAkari.Game.Objects.Sounds;
using Crankshaft.Events;
using System.Threading;
using Crankshaft.Animation;
using OpenTK.Audio.OpenAL;

namespace Crankshaft.Handlers
{
    public class windowHandler : GameWindow
    {
        //constructor passthroughs
        private GameWindowSettings gameWindowSettings;
        private NativeWindowSettings nativeWindowSettings;
        private string scenesFilePath;
        private string soundsFilePath;
        private string intialScene;

        //Static Accessors
        public static NativeWindow ActiveWindow { get; private set; }
        public static MouseState ActiveMouse { get; private set; }
        public static Scene ActiveScene { get; set; }
        public static Simulation ActiveSim { get; set; }
        public static HUD ActiveHUD { get; set; }
        public static sniperCrosshair ActiveScope { get; set; }
        public static bool DebugDraw { get; set; }
        public static List<gameObject> Cleanup { get; set; } = new List<gameObject>();
        public static List<gameObject> SafeAdd { get; set; } = new List<gameObject>();
        public static gameMusic ActiveMusic { get; set; }
        public static double Time { get; set; }
        public static int Score { get; set; } = 0;
        public static int MaxScore { get; set; } = 27;

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
        public windowHandler(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings, string scenesFilePath, string intialSceneID, string soundsFilePath) : base(gameWindowSettings, nativeWindowSettings)
        {
            this.gameWindowSettings = gameWindowSettings;
            this.nativeWindowSettings = nativeWindowSettings;
            this.scenesFilePath = scenesFilePath;
            this.intialScene = intialSceneID;
            this.soundsFilePath = soundsFilePath;
        }


        private double _time;
        //Runs 30 Times A Second.
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            _time += args.Time;
            base.OnUpdateFrame(args);

            //Input Handling
            var input = KeyboardState;
            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            if (input.IsKeyPressed(Keys.Enter))
            {
                if (DebugDraw == false)
                {
                    DebugDraw = true;
                } else
                {
                    DebugDraw = false;
                }
            }
            if (input.IsKeyPressed(Keys.Right))
            {
                ActiveScene.objects[0].translateObject(new Vector3(1,0,0));
            }
            if (input.IsKeyPressed(Keys.Left))
            {
                ActiveScene.objects[0].translateObject(new Vector3(-1, 0, 0));
            }
            if (input.IsKeyPressed(Keys.Up))
            {
                ActiveScene.objects[0].translateObject(new Vector3(0, 1, 0));
            }
            if (input.IsKeyPressed(Keys.Down))
            {
                ActiveScene.objects[0].translateObject(new Vector3(0, -1, 0));
            }
            if (ActiveMouse.IsButtonPressed(MouseButton.Left))
            {
                MouseButton button = MouseButton.Left;
                InputEvents.invokeClick(this, button);
            }
            if(input.IsKeyPressed(Keys.Space))
            {
                Keys key = Keys.Space;
                InputEvents.invokeInput(this, key);
            }
            if (input.IsKeyReleased(Keys.Space))
            {
                Keys key = Keys.Space;
                InputEvents.invokeRelease(this, key);
            }
            //Simulation Updates
            ActiveSim.onUpdate();

            //Object Updates
            foreach (gameObject g in ActiveScene.objects)
            {
                g.onUpdateFrame(args.Time);
            }

            //Cleanup Deleted Objects.
            foreach (gameObject g in Cleanup)
            {
                windowHandler.ActiveScene.objects.Remove(g);
            }

            //Call Music Onupdate Functions for active song
            //ActiveMusic.onUpdate();

            foreach (gameObject g in SafeAdd)
            {
                windowHandler.ActiveScene.objects.Add(g);
                int index = windowHandler.ActiveScene.objects.IndexOf(g);
                windowHandler.ActiveScene.objects[index].onLoad();
                windowHandler.ActiveScene.objects.Sort();
            }

            SafeAdd = new List<gameObject>();
            Cleanup = new List<gameObject>();
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            Time += args.Time;
            GL.Clear(ClearBufferMask.ColorBufferBit);

            //Invoke objects to render themsevles
            foreach (gameObject g in ActiveScene.objects)
            {
                g.Animate(args.Time);
                g.onRenderFrame();
            }

            textCreator.renderText();

            SwapBuffers();
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            //Set the Static Accessors
            ActiveWindow = this;
            ActiveMouse = this.MouseState;
            ActiveSim = new Simulation();
            CursorState = CursorState.Hidden;

            //loading a debug texture
            renderingHandler.debugHandle = new Error(new objectData()).textures[0].Handle;

            //Enabling a bunch of openGL nonsense
            GL.Viewport(0, 0, Size.X, Size.Y);
            GL.ClearColor(0.6f, 0.6f, 0.6f, 1.0f);
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.Multisample);
            GL.Enable(EnableCap.LineSmooth);
            GL.Hint(HintTarget.LineSmoothHint, HintMode.Fastest);
            GL.BlendEquation(BlendEquationMode.FuncAdd);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            //Setting Matricies
            renderingHandler.ViewPosition = new UniVector3(0.0f, 0.0f, -3.0f);
            renderingHandler.ViewMatrix = Matrix4.CreateTranslation(renderingHandler.ViewPosition);
            renderingHandler.InvertedView = Matrix4.Invert(renderingHandler.ViewMatrix);
            renderingHandler.ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), Size.X / (float)Size.Y, 0.1f, 100.0f);
            renderingHandler.InvertedProjection = Matrix4.Invert(renderingHandler.ProjectionMatrix);

            //Load The Text Dictionary
            textCreator.loadDictionary();

            //Create the current sound context
            var deviceName = ALC.GetString(ALDevice.Null, AlcGetString.DefaultDeviceSpecifier);
            var deivce = ALC.OpenDevice(deviceName);
            var context = ALC.CreateContext(deivce, (int[])null);
            ALC.MakeContextCurrent(context);
            soundHandler.CheckALError("Context Created");

            //Compile user-defined sound effects in the directory given by the user.
            soundHandler.compileSounds(soundsFilePath);
            soundHandler.CheckALError("Sounds Compiled");

            //Load the music (Temp)
            //ActiveMusic = new gameMusic(@"\Game\Resources\Music\Loopable Game lvl music v2.wav", @"\Game\Resources\Music\Fade intro Game lvl music.wav", "Game Music", gameMusic.loopState.Loop);
            //Thread m = new Thread(new ThreadStart(ActiveMusic.startSong));
            //m.Start();

            //Compile user-defined scenes in the directory given by the user.
            sceneHandler.compileScenes(scenesFilePath);
            sceneHandler.loadScene(intialScene);

            //Load the HUD
            foreach (gameObject g in windowHandler.ActiveScene.objects)
            {
                if (g.ObjectID == "hud")
                {
                    ActiveHUD = (HUD) g;
                    continue;
                }
                if (g.ObjectID == "scope")
                {
                    ActiveScope = (sniperCrosshair)g;
                    continue;
                }
            }

            //Load the Simulation
            ActiveSim.onLoad();

            //Subscribe to events
            foreach (gameObject g in ActiveScene.objects)
            {
                if (g.subscription.MouseEvents == true)
                {
                    InputEvents.mouseClick += g.c_MouseEvents;
                }
                if (g.subscription.InputEvents == true)
                {
                    InputEvents.keyboardInput += g.c_PressEvents;
                    InputEvents.keyboardRelease += g.c_ReleaseEvents;
                }
            }
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            ActiveSim.onUnload();

            foreach (gameObject g in ActiveScene.objects)
            {
                g.Dispose();
            }

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
