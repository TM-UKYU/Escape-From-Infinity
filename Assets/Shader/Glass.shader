Shader "Custom/Glass"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard alpha:fade

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        struct Input
        {
            //�@���x�N�g��
            float3 worldNormal;
            //�����x�N�g��
            float3 viewDir;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            //�F��ݒ�(��)
            o.Albedo = _Color;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            //�@���x�N�g���Ǝ����x�N�g���̓��ς�����Ă���
            float alpha = 1 - (abs(dot(IN.viewDir, IN.worldNormal)));
            //��̃x�N�g�������s�Ɍ�����Ă�Ȃ瓧���A�����Ȃ甒�ŕ`��
            o.Alpha = alpha * 1.0f + 0.3;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
