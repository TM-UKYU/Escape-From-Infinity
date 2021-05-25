Shader "Custom/EmissionShader"
{
    Properties
    {
        _MainColor("MainColor",Color) = (1.0,1.0,1.0,1.0)
        _EmissionColor("Emission Color",Color) = (1.0,1.0,1.0,1.0)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _EmissionTex("Emissio Tex",2D) = "white"{}
    }

        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard alpha:fade

        float _Emission00FN; //���ԌW��1/f��炬�ɂ��Ă��O������󂯎��
        half4 _MainColor;    //��{�F
        half4 _EmissionColor;//�����F
        sampler2D _MainTex;  //�e�N�X�`��
        sampler2D _EmissionTex;//�����e�N�X�`��

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            //�e�N�X�`����ݒ�
            half4 c = tex2D (_MainTex, IN.uv_MainTex)*_MainColor;
            //surf�֐����Ŕ����}�b�v�e�N�X�`������l���擾���A���ԌW�����|����
            float e = tex2D(_EmissionTex, IN.uv_MainTex).a * _Emission00FN;
            o.Albedo = c.rgb;
            //�A���t�@�l�ɂ����ԌW��������
            o.Alpha = _Emission00FN;
            //�A�E�g�v�b�g��Emission�ɔ����F�Ɣ����W�����Z�b�g����
            o.Emission = _EmissionColor * e;
        }
        ENDCG
    }
    FallBack "Diffuse"
}