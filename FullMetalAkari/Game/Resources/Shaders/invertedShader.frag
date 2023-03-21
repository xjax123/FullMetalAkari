#version 330 core
out vec4 outputColor;
  
in vec2 TexCoords;

uniform sampler2D texture0;

void main()
{ 
	outputColor = vec4(vec3(1.0 - texture(texture0, TexCoords)), 1.0);
}