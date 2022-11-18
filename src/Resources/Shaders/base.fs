#version 330

// Input vertex attributes (from vertex shader)
in vec2 fragTexCoord;
in vec4 fragColor;
in vec3 fragPosition;

// Input uniform values
uniform sampler2D texture0;
uniform vec4 colDiffuse;
uniform vec3 viewPos;
uniform vec4 fogColor;
uniform float fogDensity;

// Output fragment color
out vec4 finalColor;

void main()
{
    vec4 texelColor = texture(texture0, fragTexCoord);
    if (texelColor.a == 0.0) discard;
    finalColor = texelColor * fragColor * colDiffuse;

    // apply fog
    float dist = length(viewPos - fragPosition);
    // const vec4 fogColor = vec4(0.5, 0.5, 0.5, 1.0);
    // const float fogDensity = 0.025;
    float fogFactor = 1.0/exp((dist*fogDensity)*(dist*fogDensity));
    fogFactor = clamp(fogFactor, 0.0, 1.0);
    finalColor = mix(fogColor, finalColor, fogFactor);
}