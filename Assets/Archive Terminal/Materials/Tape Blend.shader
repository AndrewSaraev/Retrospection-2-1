Shader "Custom/Tape Blend" {
Properties {
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_Lighting ("Lighting (RGB)", 2D) = "white" {}
	_Pattern ("Pattern (RGB)", 2D) = "white" {}
	_State ("State (RGB)", 2D) = "white" {}
}

SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 200

CGPROGRAM
#pragma surface surf Lambert alpha

sampler2D _MainTex;
sampler2D _Lighting;
sampler2D _Pattern;
sampler2D _State;

struct Input {
	float2 uv_MainTex;
	float2 uv_Lighting;
	float2 uv_Pattern;
	float2 uv_State;
};

void surf (Input IN, inout SurfaceOutput o) {
	o.Albedo = tex2D(_MainTex, IN.uv_MainTex) * tex2D(_Lighting, IN.uv_Lighting);
	fixed3 alpha = tex2D(_Pattern, IN.uv_Pattern) * tex2D(_State, IN.uv_State);
	o.Alpha = 1 - alpha.r - alpha.g - alpha.b;
}
ENDCG
}

Fallback "Transparent/VertexLit"
}