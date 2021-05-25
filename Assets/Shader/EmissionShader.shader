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

        float _Emission00FN; //時間係数1/fゆらぎにしてを外部から受け取る
        half4 _MainColor;    //基本色
        half4 _EmissionColor;//発光色
        sampler2D _MainTex;  //テクスチャ
        sampler2D _EmissionTex;//発光テクスチャ

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            //テクスチャを設定
            half4 c = tex2D (_MainTex, IN.uv_MainTex)*_MainColor;
            //surf関数内で発光マップテクスチャから値を取得し、時間係数を掛ける
            float e = tex2D(_EmissionTex, IN.uv_MainTex).a * _Emission00FN;
            o.Albedo = c.rgb;
            //アルファ値にも時間係数を入れる
            o.Alpha = _Emission00FN;
            //アウトプットのEmissionに発光色と発光係数をセットする
            o.Emission = _EmissionColor * e;
        }
        ENDCG
    }
    FallBack "Diffuse"
}