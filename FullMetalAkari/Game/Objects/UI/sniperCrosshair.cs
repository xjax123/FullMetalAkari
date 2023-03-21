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

namespace FullMetalAkari.Game.Objects.UI
{
    class sniperCrosshair : uiObject
    {
        protected int scopeFBO;
        protected string scopeFrag = "Game/Resources/Shaders/invertedShader.frag";
        protected string scopeVert = "Game/Resources/Shaders/invertedShader.vert";
        protected shaderHandler scopeShader;
        protected textureHandler frameTex;

        public sniperCrosshair(objectData d) : base(d)
        {
            objectID = "scope";
            name = "Sniper Scope";
            texPath = "Game/Resources/Texture/Scope_Duplex.png";
        }

        public override void onLoad()
        {
            base.onLoad();
            renderingHandler.basicRender(vertexArrayObject, vertexBufferObject, elementBufferObject, vertices, indices, ref scopeShader, scopeVert, scopeFrag, ref texture, texPath);

        }

        public override void onRenderFrame()
        {
            //dont activate this It will cause you to use up all of your PCs memory and crash it.
            /*
            UniVector3 worldspaceMouse = physicsHandler.ConvertScreenToWorldSpaceVec3(windowHandler.ActiveMouse.X, windowHandler.ActiveMouse.Y, -1.0f);
            renderingHandler.ViewMatrix = Matrix4.CreateTranslation(worldspaceMouse.X * (3 - position.Z), worldspaceMouse.Y * (3 - position.Z), -3.0f);
            

            scopeFBO = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, scopeFBO);

            frameTex = new textureHandler(GL.GenTexture());
            frameTex.Use(TextureUnit.Texture0);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, windowHandler.ActiveWindow.Size.X, windowHandler.ActiveWindow.Size.Y, 0, PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer,FramebufferAttachment.Color,TextureTarget.Texture2D,frameTex.Handle,0);


            
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.DeleteFramebuffer(scopeFBO);
            base.onRenderFrame();
            */
        }
    }
}
