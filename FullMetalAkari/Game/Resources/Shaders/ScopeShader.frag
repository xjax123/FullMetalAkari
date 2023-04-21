#version 330
//Shader Version v0.1

layout(origin_upper_left) in vec4 gl_FragCoord;

out vec4 outputColor;

in vec2 texCoord;
flat in int sPass;

uniform sampler2D texture0;
uniform int debug;
uniform vec2 mousePos;
uniform vec3 offset;
uniform vec2 viewportDims;
uniform float scopeRadius;

//function prototypes
float distanceBetweenPoints(vec2 pointA, vec2 pointB);

//Variables
mat4 inverseProj;
mat4 inverseView;

void main()
{
    vec4 texel = texture(texture0, texCoord);
    vec3 fragmentPos = gl_FragCoord.xyz;
    vec2 fragmentPosNorm = fragmentPos.xy / viewportDims.x;
    vec2 mousePosNorm = mousePos / viewportDims.x;

    if (sPass == 1)
    {
        if (debug == 1)
        {
            outputColor = vec4(0.0f,0.0f,0.0f,1.0f);
        } else
        {
            outputColor = texel;
        }
    } else if (sPass == 2)
    {
        if (distanceBetweenPoints(mousePosNorm, fragmentPosNorm) > scopeRadius) 
        {
            discard;
        }
        
        if (debug == 1)
        {
            outputColor = vec4(0.0f,0.0f,0.0f,1.0f);
        } else
        {
            outputColor = texel;
        }
    }
}

float distanceBetweenPoints(vec2 pointA, vec2 pointB) {
    return sqrt(pow((pointA.x - pointB.x), 2) + pow((pointA.y - pointB.y), 2));
}