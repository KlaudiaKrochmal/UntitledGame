uniform highp vec3 modelColor;
uniform highp int hasTexture;
uniform sampler2D texture;
varying highp vec3 fragNormal;
varying highp vec3 vertexWorldSpace;
varying highp vec2 fragUV;

struct Light {
    highp vec3 position;
    highp vec3 ambient;
    highp vec3 diffuse;
};
uniform Light light;

void main() {
    highp vec3 N = normalize(fragNormal);
    highp vec3 L = normalize(light.position - vertexWorldSpace);
    highp float cosNL = dot(N, L);
    cosNL = clamp(cosNL, 0.0, 1.0);
    highp vec3 colorAmb = modelColor * light.ambient;
    highp vec3 colorDif = modelColor * light.diffuse * cosNL;
    highp vec3 colorFull = clamp(colorAmb + colorDif, 0.0, 1.0);

    highp vec3 tex = texture2D(texture, fragUV).xyz;

    if (hasTexture == 1) {
        gl_FragColor = vec4(colorFull * tex, 1.0);
    } else {
        gl_FragColor = vec4(colorFull, 1.0);
    }
}
