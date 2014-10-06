Shader "Custom/Glass" {
	Properties {
//		_Color ("Color", Color) = (1,1,1,1)
		_Luminance ("Luminance", Color) = (0,0,0,0)
//		_Reflection ("Reflection", CUBE) = "" {}
	}
	SubShader {
		Tags { "Queue" = "Transparent" }
      	Pass {
			ZWrite Off
			Blend One One
 
			CGPROGRAM
			
			#include "UnityCG.cginc"
			
			#pragma vertex vert
			#pragma fragment frag

			float4 _Color;
			float4 _Luminance;
//			samplerCUBE _Reflection;
 
			struct v2f  {
				float4 pos : SV_POSITION;
				float3 normal : TEXCOORD;
				float3 viewDir : TEXCOORD1;
         	};
 
			v2f vert(appdata_base v) {
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.normal = normalize(mul(float4(v.normal, 0.0), _World2Object).xyz);
				o.viewDir = normalize(_WorldSpaceCameraPos - mul(_Object2World, v.vertex).xyz);
				return o;
			}
			
			float4 frag(v2f i) : COLOR {
				float3 normal = normalize(i.normal);
				float3 viewDir = normalize(i.viewDir);
				float opacity = abs(dot(normal, viewDir));
				if (opacity < 0.5) {
					opacity = pow(1.0 - opacity, 2.0);
				}
				else {
					opacity = pow(opacity, 2.0);
				}
//	            float3 reflectDir = reflect(-i.viewDir, i.normal);
	            
//	            return (texCUBE(_Reflection, reflectDir) * _Color + _Luminance * _Luminance.a * opacity * 4.0) * 0.5;
	            return (_Luminance * _Luminance.a * opacity * 4.0) * 0.5;
			}
			
			ENDCG
		}
	} 
	FallBack "Diffuse"
}