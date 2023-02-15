#version 330

in vec2 fragTexCoord;
in vec4 fragColor;

uniform sampler2D texture0;

out vec4 finalColor;

void main()
{

    vec4 texelColor = texture(texture0, fragTexCoord);
    float gray = dot(texelColor.rgb, vec3(0.299, 0.587, 0.114));
    finalColor = vec4(gray, gray, gray, texelColor.a);
    //finalColor = vec4(fragTexCoord.x,fragTexCoord.y,0,1);
    //finalColor = fragColor;
}