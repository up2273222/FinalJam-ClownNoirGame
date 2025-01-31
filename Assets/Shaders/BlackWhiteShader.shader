// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/BlackWhiteShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ColourMode("ColourMode",Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque"}
        Cull Off



        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"

            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;

            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            int _ColourMode;

            v2f vert (MeshData v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {

               fixed4 col = tex2D(_MainTex, i.uv);
               if (_ColourMode == 1)
                {
                    return col;
                }
                else
                {
                    float grayscale = dot(col.xyz, float3(0.3,0.59,0.11) * _ColourMode);
                     col.x = grayscale;
                     col.y = grayscale;
                     col.z = grayscale;
                     col.w = 1;
                     return col;
                }
               
                
                
                
                
            }
            ENDCG
        }
            Pass
            {
                Name "ShadowCaster"
                Tags{ "LightMode" = "ShadowCaster"}
                
            }
    }
}
