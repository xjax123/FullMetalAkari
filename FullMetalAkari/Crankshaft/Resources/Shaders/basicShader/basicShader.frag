#version 330
//Shader Version v0.1

out vec4 outputColor;

in vec2 texCoord;

flat in int target;

uniform sampler2D texture0;
uniform int debug;

void main()
{
    vec4 texel = texture(texture0, texCoord);

    if (debug == 1)
    {
    outputColor = vec4(0.0f,0.0f,0.0f,1.0f);
    }
    else
    {
    outputColor = texel;
    }
}
