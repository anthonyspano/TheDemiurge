Shader "Custom/SpriteOutline"
{
    Properties
    {
        _OutlineColor ("Outline Color", Color) = (1,0,0,1)
        _OutlineThickness ("Outline Thickness", Range(0.001, 0.02)) = 0.005
        _MainTex ("Sprite Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _OutlineColor;
            float _OutlineThickness;
            float4 _MainTex_TexelSize; // Holds (1/width, 1/height, width, height)

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Scale outline thickness based on texture resolution
                float2 pixelSize = float2(_MainTex_TexelSize.x, _MainTex_TexelSize.y); 
                float outlineThickness = pixelSize;

                float2 uv = i.uv;
                float alpha = tex2D(_MainTex, uv).a;

                // Sample surrounding pixels to detect edges
                float2 offsetX = float2(outlineThickness, 0);
                float2 offsetY = float2(0, outlineThickness);

                float alphaL = tex2D(_MainTex, uv - offsetX).a;
                float alphaR = tex2D(_MainTex, uv + offsetX).a;
                float alphaT = tex2D(_MainTex, uv + offsetY).a;
                float alphaB = tex2D(_MainTex, uv - offsetY).a;

                // If alpha is 0 but neighbor pixels have alpha > 0, it's an edge
                float outlineMask = (1 - alpha) * (alphaL + alphaR + alphaT + alphaB);

                // Blend outline and original sprite
                return lerp(tex2D(_MainTex, uv), _OutlineColor, saturate(outlineMask));
            }
            ENDCG
        }
    }
}
