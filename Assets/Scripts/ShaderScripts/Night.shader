Shader "Sprites/Night" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_AspectRatio("Screen Aspect Ratio", Float) = 0
		_Player("Player", Vector) = (0, 0, 0, 0)
		_Orange("Orange Color", Vector) = (0, 0, 0, 0)
	}
		SubShader{
			Pass {
				CGPROGRAM
				#pragma vertex vert_img
				#pragma fragment frag

				#include "UnityCG.cginc"

				uniform sampler2D _MainTex;

				uniform float4 _Player;

				uniform float4 _Orange;

				uniform float _AspectRatio;


				float4 frag(v2f_img i) : COLOR {
					float4 c = tex2D(_MainTex, i.uv);
					float2 ratio = float2(3, 1 / _AspectRatio);
					float delta = 0.2;

					float ray = length((_Player.xy - i.uv.xy) * ratio);
					delta += smoothstep(_Player.z, 0, ray) * _Player.w;

					c.rgb *= delta * _Orange;
					return c;
				}
				ENDCG
			}
		}
}