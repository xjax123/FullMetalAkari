#version 330 core
//Shader Version v0.1

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec2 aTexCoord;

out vec2 texCoord;
out int sPass;

uniform mat4 translation;
uniform mat4 projection;
uniform mat4 view;
uniform mat4 zoomedView;
uniform bool UI;
uniform int pass;

void main(void)
{
    texCoord = aTexCoord;
	sPass = pass;

	if (pass == 1) 
	{
			gl_Position = vec4(aPosition, 1.0) * translation * view * projection;
			return;
	} else if (pass == 2)
	{
		if (UI)
		{
			gl_Position = vec4(aPosition, 1.0) * translation * view * projection;
			return;
		}
		gl_Position = vec4(aPosition, 1.0) * translation * zoomedView * projection;
		return;
	}
}