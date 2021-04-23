Shader "Custom/LimLighting"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        //�ϐ��錾�Q
        
        //�e�N�X�`�����i�[����ϐ�
        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldNormal;
            float3 viewDir;
        };

        //�e��
        half _Glossiness;
        //���^���b�N�x
        half _Metallic;
        //�F
        fixed4 _Color;

        //�T�[�t�F�X�V�F�[�_�[�̒��g
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            //�ݒ肵���e�N�X�`���Ɨ֊s�����̐F���i�[(�֊s�̐F ��)
            fixed4 baseColor = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            fixed4 rimColor = fixed4(1, 1, 1, 1);

            //�e�N�X�`���̐F��`�悷��
            o.Albedo = baseColor;
            //�����x�N�g���Ɩ@���x�N�g���̊p�x���v�Z
            float rim = 1 - saturate(dot(IN.viewDir, o.Normal));
            //�x�N�g���̌v�Z���ʂ����Ƃɕ��˂�ݒ�
            o.Emission = rimColor * pow(rim, 3);

            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = baseColor.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
