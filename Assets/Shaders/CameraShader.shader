Shader "Unlit/CameraShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        
        _GrainTex("Film grain texture", 2D) = "white" {}
        
        _Grain_Params1("Grain control 1", Vector) = (1,0.5,0,0)
        _Grain_Params2("Grain control 2", Vector) = (1,1,0,0)
        
        
        
        
        _Radius ("Vignette radius",Range(0,1)) = 1
        _Feather ("Vignette Feathering",Range(0,1)) = 1
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

            struct appdata
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
          //  float4 _MainTex_ST;

            float _Radius;
            float _Feather;

            
            sampler2D _GrainTex;
            float2 _Grain_Params1;
            float4 _Grain_Params2;
            

            
            
            

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }


            float3 ApplyFilmGrain(float3 color, float2 uv)
            {

                                
                float2 grainUV = uv * _Grain_Params2.xy + _Grain_Params2.zw;
                float4 grain = tex2D(_GrainTex, grainUV);
                
                float lum = dot(color,float3(0.299,0.587,0.114));
                lum = 1.0 - sqrt(saturate(lum));
                lum = lerp(1.0,lum,_Grain_Params1.x);
                return color = color * grain * _Grain_Params1.y * lum;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 col = tex2D(_MainTex,i.uv);

                col += ApplyFilmGrain(col,i.uv);
                
                
                

                
                float2 centerUVS = (float2 (i.uv * 2 - 1));
                col *= (1-smoothstep(_Radius,_Radius+_Feather,length(centerUVS)));
      
                

                
                return fixed4(col,1);
            }
            ENDCG
        }
    }
}
