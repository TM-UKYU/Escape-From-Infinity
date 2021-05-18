Shader "Custom/LimOutline"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)//中心の色味
        _OutlineColor("OutlineColor",Color) = (1,1,1,1)//外枠の色
        _MainTex("Albedo (RGB)", 2D) = "white" {}//中心の中心
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

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldNormal;
            float3 viewDir;
        };

        fixed4 _OutlineColor;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;

            //視線と法線の内積を取る
            float rim = 1 - saturate(dot(IN.viewDir, o.Normal));
            //視線と法線の内積をもとにエミッション値を変更し外枠のみ色を変える
            o.Emission = _OutlineColor * pow(rim, 3);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
