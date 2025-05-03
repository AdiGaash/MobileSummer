Shader "StylizedGuns/PowerCable"
{
	Properties
	{
		_Texture("Texture", 2D) = "white" {}
		[HDR]_MainColor("Main Color", Color) = (1,1,1,0)
		_Width("Width", Float) = 7.5
		_Power("Power", Float) = 1
		_Speed("Speed", Float) = 0.2
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _Speed;
		uniform float _Width;
		uniform float _Power;
		uniform float4 _MainColor;
		uniform sampler2D _Texture;
		uniform float4 _Texture_ST;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float temp_output_1_0_g21 = ( _Speed * _Time.y );
			float2 uv_TexCoord3 = v.texcoord.xy + float2( 0,-0.15 );
			float temp_output_5_0 = ( ( (0.0 + (( ( temp_output_1_0_g21 - floor( ( temp_output_1_0_g21 + 0.5 ) ) ) * 2 ) - 0.0) * (1.0 - 0.0) / (1.0 - 0.0)) - uv_TexCoord3.y ) * _Width );
			float3 ase_vertexNormal = v.normal.xyz;
			v.vertex.xyz += ( saturate( ( ( 1.0 - ( temp_output_5_0 * temp_output_5_0 ) ) * _Power ) ) * ase_vertexNormal );
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Texture = i.uv_texcoord * _Texture_ST.xy + _Texture_ST.zw;
			o.Albedo = ( _MainColor * tex2D( _Texture, uv_Texture ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
}