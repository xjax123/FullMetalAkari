//Cody By: Jackson Maclean
//Mostly Referenced from the OpenTK Tutorial.
//Generic
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
//OpenTK
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace FullMetalAkari.Shaders
{
    public class shaderHandler
    {
        public readonly int Handle;

        private readonly Dictionary<string, int> uniformLocations;

        public shaderHandler (string vert, string frag)
        {
            // Load & Compile Vertex Shader
            var sSource = File.ReadAllText(vert);
            var vertShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertShader, sSource);
            Compile(vertShader);

            //Load & Compile Fragment Shader
            sSource = File.ReadAllText(frag);
            var fragShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragShader, sSource);
            Compile(fragShader);

            //Merging Shaders
            Handle = GL.CreateProgram();
            GL.AttachShader(Handle, vertShader);
            GL.AttachShader(Handle, fragShader);
            Link(Handle);

            //Clearing Unecessary Shaders
            GL.DetachShader(Handle, vertShader);
            GL.DetachShader(Handle, fragShader);
            GL.DeleteShader(vertShader);
            GL.DeleteShader(fragShader);

            //Caching Shader
            GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);
            uniformLocations = new Dictionary<string, int>();
            for (var i = 0; i < numberOfUniforms; i++)
            {
                var key = GL.GetActiveUniform(Handle, i, out _, out _);
                var location = GL.GetUniformLocation(Handle, key);
                uniformLocations.Add(key, location);
            }
        }

        //Linker w/ Error Reporting.
        private void Link(int program)
        {
            GL.LinkProgram(program);
            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
            if (code != (int)All.True)
            {
                throw new Exception($"Error occurred whilst linking Program({program})");
            }
        }

        //Compiler w/ Error Reporting
        private static void Compile (int shader)
        {
           GL.CompileShader(shader);
            GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
            if (code != (int)All.True)
            {
                var infoLog = GL.GetShaderInfoLog(shader);
                throw new Exception($"Error occurred whilst compiling Shader({shader}).\n\n{infoLog}");
            }
        }

        //Wraper to enable the shader
        public void Use()
        {
            GL.UseProgram(Handle);
        }

        //This Does Something, Im not exactly sure what.
        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(Handle, attribName);
        }

        //Setters
        public void SetInt(string name, int data)
        {
            GL.UseProgram(Handle);
            GL.Uniform1(uniformLocations[name], data);
        }
        public void SetFloat(string name, float data)
        {
            GL.UseProgram(Handle);
            GL.Uniform1(uniformLocations[name], data);
        }
        public void SetMatrix4(string name, Matrix4 data)
        {
            GL.UseProgram(Handle);
            GL.UniformMatrix4(uniformLocations[name], true, ref data);
        }
        public void SetVector3(string name, Vector3 data)
        {
            GL.UseProgram(Handle);
            GL.Uniform3(uniformLocations[name], data);
        }
    }
}
