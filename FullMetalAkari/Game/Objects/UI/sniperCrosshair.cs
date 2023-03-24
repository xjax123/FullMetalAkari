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
        protected UniMatrix scopeView;

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

        //TODO: This still leaks alot of memory
        public override void onRenderFrame()
        {
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

            //Setting Uniforms
            Shader.Use();
            Shader.SetMatrix4("translation", TrueTranslation);
            Shader.SetMatrix4("projection", renderingHandler.ProjectionMatrix);
            Shader.SetMatrix4("view", scopeView);

            //Rendering scope picture to frame buffer
            renderingHandler.DrawScene(vertexArrayObject, vertexBufferObject,elementBufferObject, vertices, indices, PrimitiveType.Triangles);

            //binding current frame buffer image to texture.
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.Color, TextureTarget.Texture2D, frameTex.Handle, 0);

            //deleting frame buffer and rebinding to the default buffer
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);
            GL.Clear(ClearBufferMask.StencilBufferBit);
            GL.DeleteTexture(frameTex.Handle);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.DeleteFramebuffer(scopeFBO);
            base.onRenderFrame();
        }
    }
}
