#version 330
//Shader Version v0.1

out vec4 outputColor;

in vec2 texCoord;

flat in int target;

uniform sampler2D texture0;

void main()
{
    outputColor = texture(texture0, texCoord);
}
