Shader "Custom/DF-Diffuse" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_FrontTex ("Base (RGB)", 2D) = "white" {}
		_BackTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		Cull back

		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _FrontTex;
		fixed4 _Color;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_FrontTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
		
		Cull front

		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _BackTex;
		fixed4 _Color;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_BackTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}

	Fallback "VertexLit"
}

