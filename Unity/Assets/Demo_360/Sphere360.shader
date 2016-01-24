Shader "Unlit/360"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
  		CameraFovHorz("CameraFovHorz",Range(0,360)) = 360
    	CameraFovTop("CameraFovTop", Range(0,360) ) = 0
    	CameraFovBottom("CameraFovBottom", Range(0,360) ) = 360
   	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
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
			float4 _MainTex_ST;
			float CameraFovHorz;
			float CameraFovTop;
			float CameraFovBottom;
				
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				//	insideout, so uv's are back to front
				float2 uv = float2( 1-i.uv.x, i.uv.y );

				// sample the texture
				fixed4 col = tex2D(_MainTex, uv);
				return col;
			}
			ENDCG
		}
	}
}
