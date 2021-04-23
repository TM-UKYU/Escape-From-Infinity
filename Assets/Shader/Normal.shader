Shader "Custom/Normal"
{
    Properties
    {
        //色
        _Color("Color", Color) = (1,1,1,1)
        //使用するテクスチャ
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        //金属度
        _Metallic("Metallic", Range(0,1)) = 0.0
        //ノーマルマップに使用するテクスチャ
        _BumpMap("Normal Map",2D) = "bump"{}
        //ノーマルの強さを設定
        _BumpScale("Normal Scale",Range(0,1)) = 1.0
    }
    SubShader
    {
        //描画順番設定
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

        //Propertiesの内容を格納するための変数群
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
            //テクスチャと色情報をかけて表示する色を設定
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            //表示する色を描画に渡す
            o.Albedo = c.rgb;
            //その他の数値も描画に渡す
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
            //ノーマルマップの情報を設定
            fixed4 n = tex2D(_BumpTex, IN.uv_MainTex) * _Color;
            //設定した情報を描画のNormalに渡す(UnpackScaleNormalはNormalに渡す際に必要な関数らしい)
            o.Normal = UnpackScaleNormal(n, _BumpScale);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
