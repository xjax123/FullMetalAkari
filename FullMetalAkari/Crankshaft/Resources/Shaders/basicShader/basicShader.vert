#version 330 core
//Shader Version v0.1

layout(location = 0) in vec3 aPosition;

layout(location = 1) in vec2 aTexCoord;

out vec2 texCoord;
out int target;

uniform mat4 translation;
uniform mat4 projection;
uniform mat4 view;
uniform int tex;

void main(void)
{
    texCoord = aTexCoord;
    target = tex;

    gl_Position = vec4(aPosition, 1.0) * translation * view * projection;
}