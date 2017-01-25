Shader "Toon/Water" {
  Properties {
    _Color ("Color", Color) = (0, 0, 0, 1)
    _CausticsLightTex ("Light Caustics", 2D) = "black" {}
    _LightenFactor ("Lighten Factor", Range(0, 1)) = 0.75
    _CausticsDarkTex ("Dark Caustics", 2D) = "white" {}
    _DarkenFactor ("Darken Factor", Range(0, 1)) = 0.25
    _NoiseTex ("Noise Texture", 2D) = "white" {}
    _NoiseSpeed ("Noise Speed", Range(0.1, 10)) = 2
    _DisplacementFactor ("Displacement Factor", Range(0, 1)) = 0.2
    _Glossiness ("Smoothness", Range(0,1)) = 0.5
    _Metallic ("Metallic", Range(0,1)) = 0.0
  }
  SubShader {
    Tags {
      "RenderType" = "Opaque"
    }
    LOD 200

    CGPROGRAM
    #pragma surface surf Standard fullforwardshadows
    #pragma target 3.0

    struct Input {
      float2 uv_NoiseTex;
    };

    static const float M_PI = 3.14159265358;

    sampler2D _CausticsLightTex;
    half _LightenFactor;
    sampler2D _CausticsDarkTex;
    half _DarkenFactor;
    sampler2D _NoiseTex;
    half _NoiseSpeed;
    half _DisplacementFactor;
    half _Glossiness;
    half _Metallic;
    fixed4 _Color;

    void surf (Input IN, inout SurfaceOutputStandard o) {
      float n = frac(_Time.r * _NoiseSpeed);
      float n2 = _Time.r * 0.5 * _NoiseSpeed;
      fixed4 noise = tex2D(_NoiseTex, IN.uv_NoiseTex + float2(n, n));
      fixed4 noise2 = tex2D(_NoiseTex, IN.uv_NoiseTex - float2(n2, n2));

      fixed4 caustics1 = tex2D(_CausticsLightTex, IN.uv_NoiseTex - noise.rg * _DisplacementFactor);
      fixed4 caustics2 = tex2D(_CausticsDarkTex, IN.uv_NoiseTex - cos(noise2.rg * M_PI) * _DisplacementFactor);

      fixed4 c = (caustics1 * _LightenFactor) + (_Color * max(caustics1, clamp(caustics2 + _DarkenFactor, 0, 1)));
      o.Albedo = c.rgb;
      o.Metallic = _Metallic;
      o.Smoothness = _Glossiness;
      o.Alpha = c.a;
    }
    ENDCG
  }
  FallBack "Diffuse"
}
