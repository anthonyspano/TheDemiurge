Shader "Custom/2D Dissolve Effect"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _NoiseTex ("Noise Texture", 2D) = "white" {}
        _DissolveThreshold ("Dissolve Threshold", Range(0, 1)) = 0.5
        _EdgeWidth ("Edge Width", Range(0, 0.2)) = 0.05
        _EdgeColor ("Edge Color", Color) = (1, 0.5, 0, 1)
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            float _DissolveThreshold;
            float _EdgeWidth;
            float4 _EdgeColor;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;

                // Sample the main texture and noise texture
                fixed4 mainColor = tex2D(_MainTex, uv);
                float noiseValue = tex2D(_NoiseTex, uv).r;

                // Calculate dissolve factor
                float dissolveFactor = smoothstep(_DissolveThreshold - _EdgeWidth, _DissolveThreshold, noiseValue);

                // Add edge effect
                float edgeFactor = step(_DissolveThreshold - _EdgeWidth, noiseValue) - step(_DissolveThreshold, noiseValue);
                fixed4 edgeColor = _EdgeColor * edgeFactor;

                // Apply dissolve effect
                fixed4 finalColor = lerp(edgeColor, mainColor, dissolveFactor);

                // Apply alpha discard
                if (finalColor.a < 0.01)
                    discard;

                return finalColor;
            }
            ENDCG
        }
    }

    FallBack "Transparent/VertexLit"
}
