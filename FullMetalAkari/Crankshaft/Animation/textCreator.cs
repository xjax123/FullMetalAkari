using Crankshaft.Handlers;
using Crankshaft.Animation;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Linq;

namespace Crankshaft.Animation
{
    public static class textCreator
    {
        public static Dictionary<char, Character> Chars { get; private set; }
        public static List<Phrase> renderTargets { get; private set; } = new List<Phrase>();

        private static string frag = @"Crankshaft\Resources\Shaders\basicShader\basicShader.frag";
        private static string vert = @"Crankshaft\Resources\Shaders\basicShader\basicShader.vert"; 
        public static float[] vertices =
        {
           //Position           Texture coordinates
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        };
        public static readonly uint[] indices =
        {
            0, 1, 3,
            1, 2, 3
        };

        public static void loadDictionary()
        {
            Chars = new Dictionary<char, Character>()
            {
                { '1' , new Character() { Texture = textureHandler.LoadFromFile("Game/Resources/Fonts/1-Text.png", TextureUnit.Texture0), Shader = new shaderHandler(vert, frag), Scale = 0f, Rotation = 0 } },
                { '2' , new Character() { Texture = textureHandler.LoadFromFile("Game/Resources/Fonts/2-Text.png", TextureUnit.Texture0), Shader = new shaderHandler(vert, frag), Scale = 0f, Rotation = 0 } },
                { '3' , new Character() { Texture = textureHandler.LoadFromFile("Game/Resources/Fonts/3-Text.png", TextureUnit.Texture0), Shader = new shaderHandler(vert, frag), Scale = 0f, Rotation = 0 } },
                { '4' , new Character() { Texture = textureHandler.LoadFromFile("Game/Resources/Fonts/4-Text.png", TextureUnit.Texture0), Shader = new shaderHandler(vert, frag), Scale = 0f, Rotation = 0 } },
                { '5' , new Character() { Texture = textureHandler.LoadFromFile("Game/Resources/Fonts/5-Text.png", TextureUnit.Texture0), Shader = new shaderHandler(vert, frag), Scale = 0f, Rotation = 0 } },
                { '6' , new Character() { Texture = textureHandler.LoadFromFile("Game/Resources/Fonts/6-Text.png", TextureUnit.Texture0), Shader = new shaderHandler(vert, frag), Scale = 0f, Rotation = 0 } },
                { '7' , new Character() { Texture = textureHandler.LoadFromFile("Game/Resources/Fonts/7-Text.png", TextureUnit.Texture0), Shader = new shaderHandler(vert, frag), Scale = 0f, Rotation = 0 } },
                { '8' , new Character() { Texture = textureHandler.LoadFromFile("Game/Resources/Fonts/8-Text.png", TextureUnit.Texture0), Shader = new shaderHandler(vert, frag), Scale = 0f, Rotation = 0 } },
                { '9' , new Character() { Texture = textureHandler.LoadFromFile("Game/Resources/Fonts/9-Text.png", TextureUnit.Texture0), Shader = new shaderHandler(vert, frag), Scale = 0f, Rotation = 0 } },
                { '0' , new Character() { Texture = textureHandler.LoadFromFile("Game/Resources/Fonts/0-Text.png", TextureUnit.Texture0), Shader = new shaderHandler(vert, frag), Scale = 0f, Rotation = 0 } },
                { '%' , new Character() { Texture = textureHandler.LoadFromFile("Game/Resources/Fonts/%-Text.png", TextureUnit.Texture0), Shader = new shaderHandler(vert, frag), Scale = 0f, Rotation = 0 } },
            };
        }

        public static void renderText()
        {
            if (windowHandler.DebugDraw == true)
            {
                return;
            }
            foreach (Phrase p in renderTargets)
            {
                foreach (Character c in p.Characters)
                {
                    //setting shaders
                    c.Shader.Use();
                    c.Shader.SetMatrix4("translation", Matrix4.CreateTranslation(c.Position) * Matrix4.CreateScale(c.Scale) * Matrix4.CreateRotationZ(c.Rotation));
                    c.Shader.SetMatrix4("projection", renderingHandler.ProjectionMatrix);
                    c.Shader.SetMatrix4("view", renderingHandler.ViewMatrix);

                    //dealing with buffers
                    GL.BindVertexArray(p.Objects.VAO);
                    GL.BindBuffer(BufferTarget.ArrayBuffer, p.Objects.VBO);
                    GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
                    GL.BindBuffer(BufferTarget.ElementArrayBuffer, p.Objects.EBO);
                    GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

                    //dealing with textures
                    GL.ActiveTexture(TextureUnit.Texture0);
                    GL.BindTexture(TextureTarget.Texture2D, c.Texture.Handle);

                    //drawing
                    GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);
                }
            }
        }

        public static void registerPhrase(Phrase p){ renderTargets.Add(p); }
        public static void unregisterPhrase(Phrase p) { renderTargets.Remove(p); }
        public static Phrase buildPhrase(Vector3 Position, float Spacing, params Character[] c)
        {
            Phrase p;
            p = new Phrase() { Characters = c.ToList<Character>(), Objects = new arrayObjects()};
            return p;
        }
        public static Phrase buildPhrase(Vector3 Position, float Spacing, float Scale = 1, float Rot = 0, params char[] c)
        {
            Phrase p;
            int vao = GL.GenVertexArray();
            int vbo = GL.GenBuffer();
            int ebo = GL.GenBuffer();
            Character[] chara = new Character[c.Length];
            try {
                int i = 0;
                foreach (char ch in c)
                {
                    Character c1;
                    Chars.TryGetValue(c[i], out c1);

                    GL.BindVertexArray(vao);

                    GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
                    GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

                    GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
                    GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

                    c1.Shader.Use();

                    var vertexLoc = c1.Shader.GetAttribLocation("aPosition");
                    GL.EnableVertexAttribArray(vertexLoc);
                    GL.VertexAttribPointer(vertexLoc, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

                    var texCoordLoc = c1.Shader.GetAttribLocation("aTexCoord");
                    GL.EnableVertexAttribArray(texCoordLoc);
                    GL.VertexAttribPointer(texCoordLoc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

                    c1.Scale = Scale;
                    c1.Rotation = Rot;
                    Vector3 v = Position;
                    v.X = Position.X + i * Spacing;
                    c1.Position= v;
                    chara[i] = c1;
                    i++;
                }
            } catch
            {
                throw new NotImplementedException();
            }
            p = new Phrase() {Characters = chara.ToList<Character>(), Objects = new arrayObjects() { VAO = vao, VBO = vbo, EBO = ebo } };
            return p;
        }
    }
}
