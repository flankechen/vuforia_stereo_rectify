Shader "Custom/MonoRect" {
  Properties {
    _MainTex ("Base (RGB)", 2D) = "white" {}
  }
  SubShader {
    Pass{
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #include "UnityCG.cginc"

      uniform sampler2D _MainTex;

      struct v2f {
        float4  pos : SV_POSITION;
        float2  uv : TEXCOORD0;
      };

      float4 _MainTex_ST;

      v2f vert (appdata_base v) {
        v2f o;
        o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
        o.uv = v.texcoord.xy;
        return o;
      }

      half4 frag(v2f i) : COLOR {
        //Camera Intrinsics
        //  FocalLength: [406.5248 405.6990]
        //  PrincipalPoint: [317.9921 250.4745]
        //  Skew: 0
        //Lens Distortion
        //  RadialDistortion: [-0.3914 0.1455]
        //  TangentialDistortion: [-0.0010 4.0292e-04]
        
        //KK Inverse...
        float2 ff = float2(406.6270, 405.6615) / float2(640, 480);//Calibrated in 640*480 so norm to screen size...
        float2 oo = float2(317.9921, 250.4745) / float2(640, 480);//Calibrated in 640*480 so norm to screen size...    
        float2 xy = (i.uv - oo) / ff;
        
        //Radial Distortion...  
        float k1 = -0.3914;
        float k2 =  0.1455;
        float r = sqrt(dot(xy, xy));
        float r2 = r * r;
        float r4 = r2 * r2;
        float coeff = (k1 * r2 + k2 * r4);
        
        //KK
        xy = ((xy + xy * coeff) * ff) + oo;
        fixed4 main = tex2D(_MainTex, xy);
    
        return main;
      }
    ENDCG
    }
  }
  FallBack "Diffuse"
}
