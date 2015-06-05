Shader "Tauri Interactive/Advanced Glow" 
  {
    Properties 
    {
     _MainColor ("Main Color", Color) = (0.227,0.227,0.612,1)
     _MainTex ("Texture", 2D) = "white" {}
     _BumpMap ("Bumpmap", 2D) = "bump" {}
     _GlowColor ("Glow Color", Color) = (0,0.38,1,1)
     _GlowPower ("Glow Power", Range(10,0.5)) = 10
     _Albedo ("Albedo", Range(0,1)) = 0
     _Emission("Emission", Range(0,2)) = 1.3
    }
    
    SubShader 
    {
     Tags 
     { 
     	"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"
     }
     		
     
     CGPROGRAM
      #pragma surface surf Lambert alpha
      struct Input {
          float2 uv_MainTex;
          float2 uv_BumpMap;
          float3 viewDir;
      };
      float4 _MainColor;
      sampler2D _MainTex;
      sampler2D _BumpMap;
      float4 _GlowColor;
      float _GlowPower;
      float _Albedo;
      float _Emission;
     
      void surf (Input IN, inout SurfaceOutput o) {      
      	  float4 tex = tex2D (_MainTex, IN.uv_MainTex) * _MainColor.rgba;
          o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgba * _Albedo + tex.rgba;
          o.Alpha = tex.a;
          o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
          half rim = _Emission - saturate(dot (normalize(IN.viewDir), o.Normal));
          o.Emission = _GlowColor.rgb * pow (rim, _GlowPower);
      }
      ENDCG
     
    } 
  Fallback "Diffuse"
} 