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
        //カスタムライティングを自分で設定するために変更(ToonRamp関数がライティング)
        //SurfaceOutputStandard型は使えない
        #pragma surface surf ToonRamp

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        //メインのテクスチャとRampのテクスチャを外部から取り込むための変数
        sampler2D _MainTex;
        sampler2D _RampTex;

        //Inputの中にまとめる
        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        fixed4 LightingToonRamp(SurfaceOutput s, fixed3 lightDir, fixed atten)
        {
            //dの中にライトの方向とオブジェクトの法線の内積を格納
            half d = dot(s.Normal, lightDir) * 0.5 + 0.5;
            //内積をもとにそのメッシュはRampTexのどの色を強く使うかを決める
            fixed3 ramp = tex2D(_RampTex, fixed2(d, 0.5)).rgb;
            fixed4 c;
            //メッシュの描画する色を決める
            c.rgb = s.Albedo * _LightColor0.rgb * ramp;
            c.a = 0.5;
            return c;
        }


        void surf (Input IN, inout SurfaceOutput o)
        {
            //メインテクスチャの色を格納
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            //描画に格納した色を渡す
            o.Albedo = c.rgb;
            //アルファ値の設定
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
