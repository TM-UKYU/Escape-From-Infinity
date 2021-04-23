Shader "Custom/Outline"
{
	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
			//���ʂ̂ݕ`��
			Cull Front

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include"UnityCG.cginc"

			struct appdata
			{
				//���W
				float4 vertex:POSITION;
				//�@��
				float3 normal:NORMAL;
			};

			struct v2f
			{
				//���W
				float4 vertex:SV_POSITION;
			};

			//�@�������Ƀ��f����c��܂�
			v2f vert(appdata v)
			{
				v2f o;
				v.vertex += float4(v.normal * 0.04f, 0);
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			//�����h��Ԃ��ĕ`��
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
				//���W
				float4 vertex:POSITION;
				//�@��
				float3 normal:NORMAL;
			};

			struct v2f
			{
				//���W
				float4 vertex:SV_POSITION;
				//�@��
				float3 normal:NORMAL;
			};

			//�`��ʒu��ݒ肵��
			v2f vert(appdate v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.normal = UnityObjectToWorldNormal(v.normal);
				return o;
			}

			//���f���̕\�ʂ̂ݕ`�悷��
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
