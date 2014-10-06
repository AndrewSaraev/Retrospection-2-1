Shader "Custom/Simple Gloss" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
	_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
}

SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 300
	
CGPROGRAM
#pragma surface surf BlinnPhong

fixed4 _Color;
half _Shininess;

struct Input {
	float3 worldPos;
};

void surf (Input IN, inout SurfaceOutput o) {
	o.Albedo = _Color.rgb;
	o.Gloss = _Color.a;
	o.Specular = _Shininess;
}
ENDCG
}

Fallback "VertexLit"
}
