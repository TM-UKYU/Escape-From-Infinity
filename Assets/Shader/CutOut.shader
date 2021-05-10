Shader "Custom/CutOut"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType" = "Geometry-1" }

        Pass
        {
            Zwrite On
            ColorMask 0
        }
    }
}
