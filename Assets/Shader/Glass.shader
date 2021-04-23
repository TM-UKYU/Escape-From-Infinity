Shader "Custom/Glass"
{
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

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            //�F��ݒ�(��)
            o.Albedo = fixed4(1, 1, 1, 1);
            //�@���x�N�g���Ǝ����x�N�g���̓��ς�����Ă���
            float alpha = 1 - (abs(dot(IN.viewDir, IN.worldNormal)));
            //��̃x�N�g�������s�Ɍ�����Ă�Ȃ瓧���A�����Ȃ甒�ŕ`��
            o.Alpha = alpha * 0.5f;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
