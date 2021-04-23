Shader "Custom/Outline"
{
	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
			//裏面のみ描画
			Cull Front

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include"UnityCG.cginc"

			struct appdata
			{
				//座標
				float4 vertex:POSITION;
				//法線
				float3 normal:NORMAL;
			};

			struct v2f
			{
				//座標
				float4 vertex:SV_POSITION;
			};

			//法線方向にモデルを膨らませ
			v2f vert(appdata v)
			{
				v2f o;
				v.vertex += float4(v.normal * 0.04f, 0);
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			//黒く塗りつぶして描画
			fixed4 frag(v2f i) :SV_Target
			{
				fixed4 col = fixed4(0.1,0.1,0.1,1);
				return col;
			}

			ENDCG
		}

		Pass
		{
			Cull Back

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdate
			{
				//座標
				float4 vertex:POSITION;
				//法線
				float3 normal:NORMAL;
			};

			struct v2f
			{
				//座標
				float4 vertex:SV_POSITION;
				//法線
				float3 normal:NORMAL;
			};

			//描画位置を設定して
			v2f vert(appdate v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.normal = UnityObjectToWorldNormal(v.normal);
				return o;
			}

			//モデルの表面のみ描画する
			fixed4 frag(v2f i) :SV_Target
			{
				half nl = max(0,dot(i.normal,_WorldSpaceLightPos0.xyz));

				if (nl <= 0.01f)
				{
					nl = 0.1f;
				}
				else if (nl <= 0.3f)
				{
					nl = 0.3f;
				}
				else
				{
					nl = 1;
				}

				fixed4 col = fixed4(nl, nl, nl, 1);
				return col;
			}

			ENDCG
		}
	}
}
