// Upgrade NOTE: upgraded instancing buffer 'S_Scrolls' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_Scrolls"
{
	Properties
	{
		_Diffuse("Diffuse", 2D) = "white" {}
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Hue("Hue", Range( -1 , 1)) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_Saturation("Saturation", Range( 0 , 2)) = 1
		_Lightness("Lightness", Range( 0 , 5)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Off
		CGPROGRAM
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _Hue;
		uniform sampler2D _Diffuse;
		uniform float4 _Diffuse_ST;

		UNITY_INSTANCING_BUFFER_START(S_Scrolls)
			UNITY_DEFINE_INSTANCED_PROP(float, _Saturation)
#define _Saturation_arr S_Scrolls
			UNITY_DEFINE_INSTANCED_PROP(float, _Lightness)
#define _Lightness_arr S_Scrolls
			UNITY_DEFINE_INSTANCED_PROP(float, _Metallic)
#define _Metallic_arr S_Scrolls
			UNITY_DEFINE_INSTANCED_PROP(float, _Smoothness)
#define _Smoothness_arr S_Scrolls
		UNITY_INSTANCING_BUFFER_END(S_Scrolls)


		float3 HSVToRGB( float3 c )
		{
			float4 K = float4( 1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0 );
			float3 p = abs( frac( c.xxx + K.xyz ) * 6.0 - K.www );
			return c.z * lerp( K.xxx, saturate( p - K.xxx ), c.y );
		}


		float3 RGBToHSV(float3 c)
		{
			float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
			float4 p = lerp( float4( c.bg, K.wz ), float4( c.gb, K.xy ), step( c.b, c.g ) );
			float4 q = lerp( float4( p.xyw, c.r ), float4( c.r, p.yzx ), step( p.x, c.r ) );
			float d = q.x - min( q.w, q.y );
			float e = 1.0e-10;
			return float3( abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Diffuse = i.uv_texcoord * _Diffuse_ST.xy + _Diffuse_ST.zw;
			float3 hsvTorgb13_g1 = RGBToHSV( tex2D( _Diffuse, uv_Diffuse ).rgb );
			float _Saturation_Instance = UNITY_ACCESS_INSTANCED_PROP(_Saturation_arr, _Saturation);
			float _Lightness_Instance = UNITY_ACCESS_INSTANCED_PROP(_Lightness_arr, _Lightness);
			float3 hsvTorgb17_g1 = HSVToRGB( float3(( _Hue + hsvTorgb13_g1.x ),( _Saturation_Instance * hsvTorgb13_g1.y ),( hsvTorgb13_g1.z * _Lightness_Instance )) );
			o.Albedo = hsvTorgb17_g1;
			float _Metallic_Instance = UNITY_ACCESS_INSTANCED_PROP(_Metallic_arr, _Metallic);
			o.Metallic = _Metallic_Instance;
			float _Smoothness_Instance = UNITY_ACCESS_INSTANCED_PROP(_Smoothness_arr, _Smoothness);
			o.Smoothness = _Smoothness_Instance;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1927;7;1266;958;1538.236;658.1473;1.465115;True;False
Node;AmplifyShaderEditor.RangedFloatNode;4;-855.9182,-590.4358;Float;False;Property;_Hue;Hue;2;0;Create;True;0;0;False;0;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-871.4906,-492.2147;Float;False;InstancedProperty;_Saturation;Saturation;4;0;Create;True;0;0;False;0;1;1;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-889.458,-378.4213;Float;False;InstancedProperty;_Lightness;Lightness;5;0;Create;True;0;0;False;0;1;1;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-854.0416,-209.7358;Float;True;Property;_Diffuse;Diffuse;0;0;Create;True;0;0;False;0;None;cc48b5e83b0833e4a81b21df3715af90;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;2;-404.3214,160.4622;Float;False;InstancedProperty;_Metallic;Metallic;1;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-381.0825,249.0605;Float;False;InstancedProperty;_Smoothness;Smoothness;3;0;Create;True;0;0;False;0;0;0.145;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;7;-510.9448,-319.7279;Float;False;SF_ColorShift;-1;;1;f3651b3e85772604cb45f632c7f608e7;0;4;26;FLOAT;0;False;27;FLOAT;0;False;28;FLOAT;0;False;23;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_Scrolls;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0.05;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;7;26;4;0
WireConnection;7;27;5;0
WireConnection;7;28;6;0
WireConnection;7;23;1;0
WireConnection;0;0;7;0
WireConnection;0;3;2;0
WireConnection;0;4;3;0
ASEEND*/
//CHKSM=FCA919371357132D2B189A4291C9615F43DA3B1A