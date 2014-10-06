Shader "Lamp/Flare" {
Properties {
	_MainTex ("Particle Texture", 2D) = "black" {}
	_Color ("Color", Color) = (1,1,1,1)
}
SubShader {
	Tags {
		"Queue"="Transparent"
		"IgnoreProjector"="True"
		"RenderType"="Transparent"
		"PreviewType"="Plane"
	}
	Cull Off Lighting Off ZWrite Off Ztest Always Fog { Mode Off }
	Blend SrcAlpha One

	Pass {
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag

		#include "UnityCG.cginc"

		sampler2D _MainTex;
		float4 _Color;
		
		struct appdata_t {
			float4 vertex : POSITION;
			float2 texcoord : TEXCOORD0;
		};

		struct v2f {
			float4 vertex : SV_POSITION;
			float2 texcoord : TEXCOORD0;
		};

		float4 _MainTex_ST;
		
		v2f vert (appdata_t v)
		{
			v2f o;
			o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
			o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
			return o;
		}

		fixed4 frag (v2f i) : COLOR
		{
			half4 col = _Color * tex2D(_MainTex, i.texcoord);
			return col;
		}
		ENDCG 
	}
} 	

}
