Shader "Game/TerrainTiles" 
{
	Properties 
	{
		_MainTex ("Main texture", 2D) = "white" {}
	}

	SubShader 
	{
		ZWrite Off
		Tags { "Queue" = "Transparent" }
		Blend One OneMinusSrcAlpha 

		Pass {
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			sampler2D _MainTex;

			struct appdata {
				float4 vertex: POSITION;
				fixed4 color: COLOR;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color: COLOR; 
			};

			v2f vert(appdata v) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.color = v.color;

				// This is the tiling or the texture resolution (pixels per tile) if you want, for example:
				// if you textures are 512x512 and you do o.uv = v.vertex.xy you will have in each tile full texture.
				// But if you do v.vertex.xy/8 a 512x512 texture will cover 8x8 tiles.
				// If you use seemless textures, this is nice for rendering tile map grounds. 
				o.uv = v.vertex.xy/8;
				return o;
			}

			half4 frag (v2f i) : COLOR {
       			// We just multiply the texture color by the vertex color alpha this will be use for the blending.
				half4 texcol = tex2D(_MainTex, i.uv.xy) * i.color.a;
				return texcol;
			}

			ENDCG
		}
	}
}