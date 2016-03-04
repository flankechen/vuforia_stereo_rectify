Shader "Custom/NegativeGrayscale" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Texture2 ("Texture 2 (RGB)", 2D) =  "white"{}
        _TouchX ("TouchX", Float) = 0.0
        _TouchY ("TouchY", Float) = 0.0
    }
    SubShader {

    Pass{

        CGPROGRAM

        #pragma vertex vert
        #pragma fragment frag

        #include "UnityCG.cginc"

        sampler2D _MainTex;
        sampler2D _Texture2;
        float _TouchX;
        float _TouchY;

        struct v2f {
            float4  pos : SV_POSITION;
            float2  uv : TEXCOORD0;
        };

        float4 _MainTex_ST;

        v2f vert (appdata_base v)
        {
            v2f o;
            o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
			
            float distance;
            float2 direction;
            float2 _Touch;
			// take into account homogeneous coordinate!
            float2 viewPos = (o.pos.xy/o.pos.w);
            float sinDistance;
            
            _Touch.x=_TouchX;
            _Touch.y=_TouchY;
			
        
            direction =viewPos.xy-_Touch;
            distance=sqrt(direction.x*direction.x+direction.y*direction.y);
            sinDistance = (sin(distance)+1.0);
            direction=direction/distance;

            if ((sinDistance>0.0)&&(_Touch.x != 2.0))
            {
				viewPos.xy+=(direction*(0.3/sinDistance));
				o.pos.xy = (viewPos.xy*o.pos.w);
            }
			
            o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
            return o;
        }

        
        half4 frag(v2f i) : COLOR
        {
//            half4 c = tex2D (_MainTex, i.uv);
//			
//			half color = 1.0 - ((c.r + c.g + c.b)/3);
//
//            c.r = color;
//			c.g = color;
//			c.b = color;
			
			half4 test = tex2D (_Texture2, i.uv);

            return test;
        }

        ENDCG
    }
    }



     
    FallBack "Diffuse"
}
