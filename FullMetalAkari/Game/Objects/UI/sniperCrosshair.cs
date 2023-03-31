using System;
using System.Collections.Generic;
using System.Text;
using Crankshaft.Handlers;
using Crankshaft.Physics;
using Crankshaft.Primitives;
using Crankshaft.Data;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using BulletSharp;
using Crankshaft.Events;
using System.Diagnostics;
using Crankshaft.Animation;
using FullMetalAkari.Game.Objects.Game;
using System.Threading;

namespace FullMetalAkari.Game.Objects.UI
{
    class sniperCrosshair : uiObject
    {
        public static int bulletMax = 12;
        public static int bullets = bulletMax;
        UniVector3 offset = new UniVector3(0, 0, 0);

        private enum BreathStatus
        {
            Holding,
            Released,
            Choking
        };

        BreathStatus holdingBreath = BreathStatus.Released;
        float breath = 100;
        float breathMax = 2f;
        Phrase breathPercent;

        /*
        protected int scopeFBO;
        protected string scopeFrag = "Game/Resources/Shaders/invertedShader.frag";
        protected string scopeVert = "Game/Resources/Shaders/invertedShader.vert";
        protected shaderHandler scopeShader;
        protected textureHandler frameTex;
        protected UniMatrix scopeView;
        */
        Sound shot;
        Sound load;
        Animations recoil;
        Animations sway;
        double lastShot = 1;
        float swayScale = 0.2f;
        private bool reloading;

        public sniperCrosshair(objectData d) : base(d)
        {
            ObjectID = "scope";
            name = "Sniper Scope";
            texPaths.Add("Game/Resources/UI/Scope_Duplex.png");
            //Subscribing to Events
            subscription.MouseEvents = true;
            subscription.InputEvents = true;

            //Setting Sounds
            shot = new Sound(@"\Game\Resources\SFX\Shoot.wav", "Shoot", 60);
            load = new Sound(@"\Game\Resources\SFX\Loading.wav", "Load", 60);

            //Setting Animations
            Keyframe[] r = { new Keyframe(0.1f,new UniVector3(0,1,0)), new Keyframe(0.3f, new UniVector3(0, 0, 0)) };
            recoil = new Animations(r);
            animations.Add(recoil);
            Keyframe[] s =
            {
                //top right
                new Keyframe(0.2f,new UniVector3(0.1f,0.1f,0)),
                //top middle right
                new Keyframe(0.3f,new UniVector3(0.15f,0.08f,0)),
                //far right 
                new Keyframe(0.4f,new UniVector3(0.2f,0,0)),
                //bottom middle right
                new Keyframe(0.5f,new UniVector3(0.15f,-0.08f,0)),
                //bottom right
                new Keyframe(0.6f,new UniVector3(0.1f,-0.1f,0)),
                //top left
                new Keyframe(1f,new UniVector3(-0.1f,0.1f,0)),
                //top middle right
                new Keyframe(1.1f,new UniVector3(-0.15f,0.08f,0)),
                //far left
                new Keyframe(1.2f,new UniVector3(-0.2f,0,0)),
                //bottom middle right
                new Keyframe(1.3f,new UniVector3(-0.15f,-0.08f,0)),
                //bottom left
                new Keyframe(1.4f,new UniVector3(-0.1f,-0.1f,0)),
                //neutral
                new Keyframe(1.6f,new UniVector3(0,0,0)),
            };
            sway = new Animations(s);
            sway.Loop = true;
            animations.Add(sway);
            sway.Scale = swayScale;

            breathPercent = new Phrase();
            breathPercent.changePhrase("100%", new Vector3(-8.8f,-11.13f,0), 0.1f,0);
            textCreator.renderTargets.Add(breathPercent);
        }

        public override void onLoad()
        {
            base.onLoad();
            sway.playAnimation();
        }

        public override void Animate(double time)
        {
            base.Animate(time);
            lastShot += (float)time;

            offset = new UniVector3(0, 0, 0);
            UniVector3 worldspaceMouse = physicsHandler.ConvertScreenToWorldSpaceVec3(windowHandler.ActiveMouse.X, windowHandler.ActiveMouse.Y, -1.0f);
            UniVector3 temp = new UniVector3(worldspaceMouse.X * (3 - Position.Z), worldspaceMouse.Y * (3 - Position.Z), Position.Z);
            offset += temp;
            foreach (Animations a in animations)
            {
                offset += a.Position;
            }
            setTranslation(offset);
        }

        public override void onRenderFrame()
        {
            //Scope Zoom (Not Working)
            /*
            //calculating view matrix for the scope picture
            UniVector3 worldspaceMouse = physicsHandler.ConvertScreenToWorldSpaceVec3(windowHandler.ActiveMouse.X, windowHandler.ActiveMouse.Y, -1.0f);
            scopeView = Matrix4.CreateTranslation(worldspaceMouse.X * (3 - position.Z), worldspaceMouse.Y * (3 - position.Z), -0.0f);
            renderingHandler.ViewPosition = new UniVector3(worldspaceMouse.X * (3 - position.Z), worldspaceMouse.Y * (3 - position.Z), -3.0f);
            renderingHandler.ViewMatrix = Matrix4.CreateTranslation(renderingHandler.ViewPosition);

            //setting up frame buffers
            scopeFBO = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, scopeFBO);

            frameTex = new textureHandler(GL.GenTexture());
            frameTex.Use(TextureUnit.Texture0);

            //Creating Empty texture to write to
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, windowHandler.ActiveWindow.Size.X, windowHandler.ActiveWindow.Size.Y, 0, PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            //binding current frame buffer image to texture.
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.Color, TextureTarget.Texture2D, frameTex.Handle, 0);

            //Setting Uniforms
            Shader.Use();
            Shader.SetMatrix4("translation", TrueTranslation);
            Shader.SetMatrix4("projection", renderingHandler.ProjectionMatrix);
            Shader.SetMatrix4("view", scopeView);

            //Rendering scope picture to frame buffer
            renderingHandler.DrawScene(vertexArrayObject, vertexBufferObject, elementBufferObject, vertices, indices, PrimitiveType.Triangles);

            //Displaying texture to screen
            GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer,0);
            GL.BlitFramebuffer(0,0, windowHandler.ActiveWindow.Size.X, windowHandler.ActiveWindow.Size.Y,0, 0, windowHandler.ActiveWindow.Size.X, windowHandler.ActiveWindow.Size.Y,ClearBufferMask.ColorBufferBit,BlitFramebufferFilter.Nearest);

            //deleting frame buffer and rebinding to the default buffer
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);
            GL.Clear(ClearBufferMask.StencilBufferBit);
            GL.DeleteTexture(frameTex.Handle);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.DeleteFramebuffer(scopeFBO);
            */

            base.onRenderFrame();
        }

        public override void onUpdateFrame(double time)
        {
            if ( holdingBreath == BreathStatus.Holding)
            {
                if ((breath - (time / breathMax) * 100) > 0)
                {
                    breath -= (float)(time / breathMax) * 100;
                } else
                {
                    breath = 0;
                    holdingBreath = BreathStatus.Choking;
                    sway.Scale = swayScale*3;
                    sway.Speed = 1.75f;
                }
            } else
            {
                 if ( breath < 100)
                {
                    if (holdingBreath != BreathStatus.Choking)
                    {
                        breath += (float)(time/(breathMax)) * 100;
                    } else
                    {
                        breath += (float)(time/(breathMax*1.5)) * 100;
                    }
                } else if ( breath > 100)
                {
                    breath = 100;
                }
                if (holdingBreath == BreathStatus.Choking && breath == 100)
                {
                    holdingBreath = BreathStatus.Released;
                    sway.Scale = swayScale;
                    sway.Speed = 1;
                }
            }
            if (breath == 100)
            {
                breathPercent.changePhrase($"{Math.Ceiling(breath)}%", new Vector3(-8.8f, -11.13f, 0), 0.1f, 0);
            } else if (breath < 100 && breath > 9)
            {
                breathPercent.changePhrase($"{Math.Ceiling(breath)}%", new Vector3(-8.6f, -11.13f, 0), 0.1f, 0);
            } else if ( breath <= 9)
            {
                breathPercent.changePhrase($"{Math.Ceiling(breath)}%", new Vector3(-8.4f, -11.13f, 0), 0.1f, 0);
            }
            windowHandler.ActiveHUD.meterChange(breath);
        }

        public override void c_MouseEvents(object sender, MouseEventArgs e)
        {
            if (e.Button == OpenTK.Windowing.GraphicsLibraryFramework.MouseButton.Left)
            {
                if (lastShot > 1 && bullets > 0)
                {
                    bullets -= 1;
                    Thread s = new Thread(new ThreadStart(shot.Play));
                    s.Start();
                    Thread r = new Thread(new ThreadStart(recoil.playAnimation));
                    r.Start();
                    lastShot = 0;
                    UniVector3 temp = offset;
                    temp.Xy /= (3-position.Z);
                    physicsHandler.CheckClicked(temp);
                }
                if (bullets >= 0)
                {
                    windowHandler.ActiveHUD.hide[bullets+5] = true;
                }
                if (bullets <= 0 && reloading == false)
                {
                    Thread t = new Thread(new ThreadStart(reload));
                    t.Start();
                }
            }
        }

        private void reload()
        {
            reloading = true;
            Thread.Sleep(1000);
            load.Play();
            Thread.Sleep(1000);
            bullets = bulletMax;
            for (int i = 0; i < bulletMax; i++)
            {
                windowHandler.ActiveHUD.hide[i+5] = false;
            }
            reloading = false;
        }

        public override void c_PressEvents(object sender, KeyboardEventArgs e)
        {
            if (e.key == OpenTK.Windowing.GraphicsLibraryFramework.Keys.Space)
            {
                if (holdingBreath != BreathStatus.Choking)
                {
                    sway.Scale = 0.02f;
                    holdingBreath = BreathStatus.Holding;
                }
            }
        }
        public override void c_ReleaseEvents(object sender, KeyboardEventArgs e)
        {
            if (e.key == OpenTK.Windowing.GraphicsLibraryFramework.Keys.Space)
            {
                if (holdingBreath != BreathStatus.Choking)
                {
                    sway.Scale = swayScale;
                    holdingBreath = BreathStatus.Released;
                }
            }
        }
    }
}
