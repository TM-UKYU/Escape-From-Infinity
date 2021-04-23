Shader "Custom/Normal"
{
    Properties
    {
        //�F
        _Color("Color", Color) = (1,1,1,1)
        //�g�p����e�N�X�`��
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        //�����x
        _Metallic("Metallic", Range(0,1)) = 0.0
        //�m�[�}���}�b�v�Ɏg�p����e�N�X�`��
        _BumpMap("Normal Map",2D) = "bump"{}
        //�m�[�}���̋�����ݒ�
        _BumpScale("Normal Scale",Range(0,1)) = 1.0
    }
    SubShader
    {
        //�`�揇�Ԑݒ�
        Tags 
        { 
            "Queue"="Geometry"
            "RenderType"="Opaque" 
        }

        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        struct Input
        {
            float2 uv_MainTex;
        };

        //Properties�̓��e���i�[���邽�߂̕ϐ��Q
        sampler2D _MainTex;
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        sampler2D _BumpTex;
        half _BumpScale;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            //�e�N�X�`���ƐF���������ĕ\������F��ݒ�
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            //�\������F��`��ɓn��
            o.Albedo = c.rgb;
            //���̑��̐��l���`��ɓn��
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
            //�m�[�}���}�b�v�̏���ݒ�
            fixed4 n = tex2D(_BumpTex, IN.uv_MainTex) * _Color;
            //�ݒ肵������`���Normal�ɓn��(UnpackScaleNormal��Normal�ɓn���ۂɕK�v�Ȋ֐��炵��)
            o.Normal = UnpackScaleNormal(n, _BumpScale);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
