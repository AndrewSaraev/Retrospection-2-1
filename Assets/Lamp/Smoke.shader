Shader "Lamp/Smoke" {
Properties {
	_MainTex ("Texture", 2D) = "white" {}
	_Color ("Color", Color) = (1,1,1,1)
}

Category {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend SrcAlpha One
	AlphaTest Greater .01
	ColorMask RGB
	Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
	
	SubShader {
		Pass {
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_particles

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			fixed4 _Color;
			
			struct v2f {
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
				float3 normal : TEXCOORD1;
				float3 viewDir : TEXCOORD2;
			};
			
			float4 _MainTex_ST;

			v2f vert (appdata_base v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				o.normal = normalize(mul(float4(v.normal, 0.0), _World2Object).xyz);
				o.viewDir = normalize(_WorldSpaceCameraPos - mul(_Object2World, v.vertex).xyz);
				return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{
				float3 normal = normalize(i.normal);
				float3 viewDir = normalize(i.viewDir);
				float opacity = abs(dot(normal, viewDir));
				
				return _Color * tex2D(_MainTex, i.texcoord).a * opacity;
			}
			ENDCG 
		}
	}	
}
}