#version 330

in vec2 TexCoord0;
in vec3 Normal0;
in vec3 NormalOrig;
in vec3 WorldPos0;

out vec4 FragColor;

struct DirectionalLight
{
    vec3 Color;
    float AmbientIntensity;
    float DiffuseIntensity;
    vec3 Direction;
};

struct Material
{
    vec3 AmbientColor;
    vec3 DiffuseColor;
    float SpecularIntensity;
    float SpecularPower;
};

uniform DirectionalLight gDirectionalLight;
uniform Material gMaterial;
uniform sampler2D gSampler;
uniform vec3 gEyeWorldPos;

void main()
{
    vec4 AmbientColor = vec4(gDirectionalLight.Color, 1.0f) *
                        gDirectionalLight.AmbientIntensity *
                        vec4(gMaterial.AmbientColor, 1.0f);

    vec3 Normal = normalize(Normal0);

    float DiffuseFactor = dot(normalize(NormalOrig), -gDirectionalLight.Direction);

    vec4 DiffuseColor = vec4(0, 0, 0, 0);
    vec4 SpecularColor = vec4(0, 0, 0, 0);

    if (DiffuseFactor > 0) {
        DiffuseColor = vec4(gDirectionalLight.Color, 1.0f) *
                       gDirectionalLight.DiffuseIntensity *
                       vec4(gMaterial.DiffuseColor, 1.0f) *
                       DiffuseFactor;

        vec3 VertexToEye = normalize(gEyeWorldPos - WorldPos0);
        vec3 LightReflect = normalize(reflect(gDirectionalLight.Direction, Normal));
        float SpecularFactor = dot(VertexToEye, LightReflect);
        if (SpecularFactor > 0) {
            SpecularFactor = pow(SpecularFactor, gMaterial.SpecularPower);
            SpecularColor = vec4(gDirectionalLight.Color *
                            gMaterial.SpecularIntensity *
//                            vec4(gMaterial.SpecularColor, 1.0f) *
                            SpecularFactor, 1.0f);
        }
    }

    FragColor = texture2D(gSampler, TexCoord0.xy) *
                (AmbientColor + DiffuseColor + SpecularColor);
}