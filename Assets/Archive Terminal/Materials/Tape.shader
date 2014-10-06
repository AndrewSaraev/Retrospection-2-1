Shader "Custom/Tape" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_Lighting ("Lighting (RGB)", 2D) = "white" {}
	_Pattern ("Pattern (RGB)", 2D) = "white" {}
	_State ("State (RGB)", 2D) = "white" {}
	_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
}

SubShader {
	Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
	LOD 200
	
CGPROGRAM
#pragma surface surf Lambert alphatest:_Cutoff

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

Fallback "Transparent/Cutout/VertexLit"
}
