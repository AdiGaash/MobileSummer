Shader "StylizedGuns/Plasm"
{
	Properties
	{
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_ScrollSpeed("Scroll Speed", Float) = 0.5
		_AnimationSpeed("Animation Speed", Float) = 6.8
		_AnimationScale("Animation Scale", Float) = 0.04
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _AnimationSpeed;
		uniform float _AnimationScale;
		uniform sampler2D _TextureSample0;
		uniform float _ScrollSpeed;
		uniform sampler2D _TextureSample1;
		uniform float _Cutoff = 0.5;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertexNormal = v.normal.xyz;
			float simplePerlin2D51 = snoise( ( ( _Time.x * _AnimationSpeed ) + ase_vertexNormal ).xy );
			simplePerlin2D51 = simplePerlin2D51*0.5 + 0.5;
			float3 temp_cast_1 = ((( _AnimationScale * -1.0 ) + (simplePerlin2D51 - 0.0) * (_AnimationScale - ( _AnimationScale * -1.0 )) / (1.0 - 0.0))).xxx;
			v.vertex.xyz += temp_cast_1;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float temp_output_5_0 = ( _Time.y * _ScrollSpeed );
			float2 temp_cast_0 = (temp_output_5_0).xx;
			float2 uv_TexCoord7 = i.uv_texcoord + temp_cast_0;
			float2 temp_cast_1 = (( 1.0 - temp_output_5_0 )).xx;
			float2 uv_TexCoord12 = i.uv_texcoord + temp_cast_1;
			float4 color14 = IsGammaSpace() ? float4(2.851895,2.013102,10.68063,1) : float4(10.02984,4.661275,183.1948,1);
			float4 temp_output_22_0 = ( ( tex2D( _TextureSample0, uv_TexCoord7 ) + tex2D( _TextureSample1, uv_TexCoord12 ) ) * color14 );
			float4 temp_output_69_0 = ( temp_output_22_0 + 0.2 );
			o.Albedo = temp_output_69_0.rgb;
			o.Alpha = 1;
			clip( temp_output_69_0.r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
}