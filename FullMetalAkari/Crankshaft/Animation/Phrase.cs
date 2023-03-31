using Crankshaft.Animation;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Crankshaft.Animation
{
    public class Phrase
    {
        private Dictionary<char, float> Offsets = new Dictionary<char, float>() 
        {
            { '1', 0.55f },
            { '2', 0.6f },
            { '3', 0.6f },
            { '4', 0.6f },
            { '5', 0.6f },
            { '6', 0.6f },
            { '7', 0.6f },
            { '8', 0.6f },
            { '9', 0.6f },
            { '0', 0.6f },
            { '%', 0.6f }
        };
        public List<Character> Characters { get; set; }
        public char[] chars { get; set; }
        public arrayObjects Objects { get; set; }

        public Phrase()
        {
            Objects = new arrayObjects();
        }

        public void changePhrase(string s, Vector3 Position, float Scale, float Rot)
        {
            Characters = new List<Character>();
            chars = s.ToCharArray();
            int i = 0;
            float[] Offset = new float[chars.Length];
            foreach (char c in chars)
            {
                Character c1;
                textCreator.Chars.TryGetValue(c, out c1);

                GL.BindVertexArray(Objects.VAO);

                GL.BindBuffer(BufferTarget.ArrayBuffer, Objects.VBO);
                GL.BufferData(BufferTarget.ArrayBuffer, textCreator.vertices.Length * sizeof(float), textCreator.vertices, BufferUsageHint.StaticDraw);

                GL.BindBuffer(BufferTarget.ElementArrayBuffer, Objects.EBO);
                GL.BufferData(BufferTarget.ElementArrayBuffer, textCreator.indices.Length * sizeof(uint), textCreator.indices, BufferUsageHint.StaticDraw);

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
                float y = 0;
                if (i == 0)
                {
                    Offset[i] = 0f;
                } else
                {
                    float f;
                    Offsets.TryGetValue(chars[i-1], out f);
                    if (c =='%')
                    {
                        f += 0.1f;
                    }
                    Offset[i] = f;
                    for (int x = 0; x < i + 1; x++)
                    {
                        y += Offset[x];
                    }
                }
                v.X = Position.X +y;
                c1.Position = v;
                Characters.Add(c1);
                i++;
            }
        }
    }

    public struct arrayObjects
    {
        public int VAO { get; set; }
        public int VBO { get; set; }
        public int EBO { get; set; }

        public arrayObjects()
        {
            VAO = GL.GenVertexArray();
            VBO = GL.GenBuffer();
            EBO = GL.GenBuffer();
        }
    }
}
