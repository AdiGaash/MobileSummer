Shader "StylizedGuns/Holograph"
{
	Properties
	{
		[HDR]_BaseColor("Base Color", Color) = (0,2.802872,2.996078,1)
		_HolographTexture("Holograph Texture", 2D) = "white" {}
		_Scrollspeed("Scroll speed", Float) = 0.0002
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Custom"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Back
		ZTest Always
		Blend SrcAlpha OneMinusSrcAlpha
		
		GrabPass{ }
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED)
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex);
		#else
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex)
		#endif
		#pragma surface surf Unlit keepalpha noshadow exclude_path:deferred 
		struct Input
		{
			float4 screenPos;
			float3 worldPos;
			float3 worldNormal;
		};

		ASE_DECLARE_SCREENSPACE_TEXTURE( _GrabTexture )
		uniform sampler2D _HolographTexture;
		uniform float _Scrollspeed;
		uniform float4 _BaseColor;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float4 screenColor67 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_GrabTexture,ase_screenPosNorm.xy);
			float4 color49 = IsGammaSpace() ? float4(0,0.8314719,3.849057,1) : float4(0,0.658552,19.39901,1);
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNdotV50 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode50 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV50, 6.0 ) );
			float4 temp_output_52_0 = ( color49 * fresnelNode50 );
			float2 temp_cast_1 = (( _Time.y * _Scrollspeed )).xx;
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float4 transform36 = mul(unity_ObjectToWorld,float4( ase_vertex3Pos , 0.0 ));
			float4 appendResult37 = (float4(transform36.x , transform36.y , 0.0 , 0.0));
			float2 panner43 = ( 1.0 * _Time.y * temp_cast_1 + ( float4( float2( 1,1 ), 0.0 , 0.0 ) * appendResult37 ).xy);
			float4 Lines41 = tex2D( _HolographTexture, panner43 );
			float4 temp_output_44_0 = ( Lines41 * 1.0 );
			float2 temp_cast_5 = (( _Time.y * 1E-05 )).xx;
			float dotResult4_g1 = dot( temp_cast_5 , float2( 12.9898,78.233 ) );
			float lerpResult10_g1 = lerp( 0.0 , 1.0 , frac( ( sin( dotResult4_g1 ) * 43758.55 ) ));
			o.Emission = ( ( ( ( screenColor67 + ( ( temp_output_52_0 * temp_output_44_0 ) * ( lerpResult10_g1 > 0.2 ? 0.9 : 0.0 ) ) ) * ( temp_output_44_0 * _BaseColor ) ) * 2.0 ) + ( temp_output_52_0 * 50.0 ) ).rgb;
			o.Alpha = temp_output_44_0.r;
		}

		ENDCG
	}

}