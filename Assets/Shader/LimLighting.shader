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

        //変数宣言群
        
        //テクスチャを格納する変数
        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldNormal;
            float3 viewDir;
        };

        //粗さ
        half _Glossiness;
        //メタリック度
        half _Metallic;
        //色
        fixed4 _Color;

        //サーフェスシェーダーの中身
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            //設定したテクスチャと輪郭部分の色を格納(輪郭の色 白)
            fixed4 baseColor = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            fixed4 rimColor = fixed4(1, 1, 1, 1);

            //テクスチャの色を描画する
            o.Albedo = baseColor;
            //視線ベクトルと法線ベクトルの角度を計算
            float rim = 1 - saturate(dot(IN.viewDir, o.Normal));
            //ベクトルの計算結果をもとに放射を設定
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
