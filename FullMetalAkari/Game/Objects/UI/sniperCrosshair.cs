using System;
using System.Collections.Generic;
using System.Text;
using Crankshaft.Handlers;
using Crankshaft.Physics;
using Crankshaft.Primitives;
using Crankshaft.Data;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FullMetalAkari.Game.Objects.UI
{
    class sniperCrosshair : uiObject
    {
        new protected readonly string objectID = "scope";
        new protected string name = "Sniper Scope";
        new protected readonly string shaderVert = "Crankshaft/Resources/Shaders/basicShader/basicShader.vert";
        new protected readonly string shaderFrag = "Crankshaft/Resources/Shaders/basicShader/basicShader.frag";
        new protected readonly string texPath = "Crankshaft/Resources/Textures/error_texture.png";
        new protected readonly float[] vertices =
        {
           //Position           Texture coordinates
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        };
        new protected readonly uint[] indices =
        {
            0, 1, 3,
            1, 2, 3
        };

        public sniperCrosshair(objectData d) : base(d)
        {
        }

        public override void onRenderFrame()
        {
            //Clamps the 
            UniVector3 worldspaceMouse = OpenTK.Mathematics.Vector3.Normalize(physicsHandler.ConvertScreenToWorldSpace(windowHandler.ActiveMouse.X, windowHandler.ActiveMouse.Y, windowHandler.ActiveWindow.Size.X, windowHandler.ActiveWindow.Size.Y, renderingHandler.InvertedProjection, renderingHandler.InvertedView));
            setTranslation(new UniVector3(worldspaceMouse.X * 3, worldspaceMouse.Y * 2, 0.0f));
            base.onRenderFrame();
        }
    }
}
