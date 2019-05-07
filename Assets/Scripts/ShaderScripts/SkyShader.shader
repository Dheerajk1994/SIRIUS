Shader "Nature/Sky"{
	Properties{
		_MainTex     ("Sprite Texture", 2D) = "white"{}
		_TopColor    ("Color Top", Color) = (1, 1, 1, 1)
		_BotColor	 ("Color Bot", Color) = (1, 1, 1, 1)
	}

	SubShader{
		 
		Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			fixed4 _TopColor;
			fixed4 _BotColor;

			struct v2f{
				float4 pos      :SV_POSITION;
				float4 texcoord :TEXCOORD0;
			};

			v2f vert (appdata_full v) {
             v2f o;
             o.pos = UnityObjectToClipPos (v.vertex);
             o.texcoord = v.texcoord;
             return o;
         }
 
         fixed4 frag (v2f i) : COLOR {
			 fixed4 c = lerp(_BotColor, _TopColor, i.texcoord.y);
             c.a = 1;
             return c;
         }
			ENDCG
		}
	}
}