Shader "Unlit/NPCShader"
{
     Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MinMaxRotation("Rotation value", Float) = 15
        
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Cull Off
        
        


        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float _MinMaxRotation;

            

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

            v2f vert (MeshData v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                float2 pivot = float2(0.5,0.5);

                float cosAngle = cos(_MinMaxRotation);
                float sinAngle = sin(_MinMaxRotation);
                float2x2 rot = float2x2(cosAngle, -sinAngle,sinAngle, cosAngle);

                float2 uv = v.uv.xy - pivot;
                o.uv = mul(rot, uv);
                o.uv += pivot;
                

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
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
