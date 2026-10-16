Shader "Cartoon FX/Alpha Blended + Additive Soft" {
Properties {
	_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
	_MainTex ("Particle Texture", 2D) = "white" {}
	_InvFade ("Soft Particles Factor", Range(0.01,3)) = 1
}
SubShader {
	Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" "PreviewType"="Plane" }
	Blend SrcAlpha OneMinusSrcAlpha
	ColorMask RGB
	Cull Off
	Lighting Off
	ZWrite Off
	Pass {
		CGPROGRAM
		#include "UnityCG.cginc"
		#pragma vertex vert
		#pragma fragment frag
		sampler2D _MainTex;
		float4 _MainTex_ST;
		fixed4 _TintColor;
		struct appdata { float4 vertex : POSITION; fixed4 color : COLOR; float2 uv : TEXCOORD0; };
		struct v2f { float4 pos : SV_POSITION; fixed4 color : COLOR; float2 uv : TEXCOORD0; };
		v2f vert(appdata v) {
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.color = v.color * _TintColor;
			o.uv = TRANSFORM_TEX(v.uv, _MainTex);
			return o;
		}
		fixed4 frag(v2f i) : SV_Target {
			return tex2D(_MainTex, i.uv) * i.color;
		}
		ENDCG
	}
}
}
