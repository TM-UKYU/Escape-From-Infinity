Shader "Custom/Toom"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _RampTex("Ramp",2D) = "white"{}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        //�J�X�^�����C�e�B���O�������Őݒ肷�邽�߂ɕύX(ToonRamp�֐������C�e�B���O)
        //SurfaceOutputStandard�^�͎g���Ȃ�
        #pragma surface surf ToonRamp

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        //���C���̃e�N�X�`����Ramp�̃e�N�X�`�����O�������荞�ނ��߂̕ϐ�
        sampler2D _MainTex;
        sampler2D _RampTex;

        //Input�̒��ɂ܂Ƃ߂�
        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        fixed4 LightingToonRamp(SurfaceOutput s, fixed3 lightDir, fixed atten)
        {
            //d�̒��Ƀ��C�g�̕����ƃI�u�W�F�N�g�̖@���̓��ς��i�[
            half d = dot(s.Normal, lightDir) * 0.5 + 0.5;
            //���ς����Ƃɂ��̃��b�V����RampTex�̂ǂ̐F�������g���������߂�
            fixed3 ramp = tex2D(_RampTex, fixed2(d, 0.5)).rgb;
            fixed4 c;
            //���b�V���̕`�悷��F�����߂�
            c.rgb = s.Albedo * _LightColor0.rgb * ramp;
            c.a = 0.5;
            return c;
        }


        void surf (Input IN, inout SurfaceOutput o)
        {
            //���C���e�N�X�`���̐F���i�[
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            //�`��Ɋi�[�����F��n��
            o.Albedo = c.rgb;
            //�A���t�@�l�̐ݒ�
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
