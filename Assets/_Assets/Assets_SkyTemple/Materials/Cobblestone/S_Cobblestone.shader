// Upgrade NOTE: upgraded instancing buffer 'S_Cobblestone' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_Cobblestone"
{
	Properties
	{
		_UVScale("UVScale", Float) = 200
		_Diffuse("Diffuse", 2D) = "white" {}
		_Hue("Hue", Range( -1 , 1)) = 0
		_Saturation("Saturation", Range( 0 , 2)) = 1
		_Lightness("Lightness", Range( 0 , 5)) = 1
		_Normal("Normal", 2D) = "bump" {}
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_Metallic("Metallic", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Normal;
		uniform sampler2D _Diffuse;

		UNITY_INSTANCING_BUFFER_START(S_Cobblestone)
			UNITY_DEFINE_INSTANCED_PROP(float, _UVScale)
#define _UVScale_arr S_Cobblestone
			UNITY_DEFINE_INSTANCED_PROP(float, _Hue)
#define _Hue_arr S_Cobblestone
			UNITY_DEFINE_INSTANCED_PROP(float, _Saturation)
#define _Saturation_arr S_Cobblestone
			UNITY_DEFINE_INSTANCED_PROP(float, _Lightness)
#define _Lightness_arr S_Cobblestone
			UNITY_DEFINE_INSTANCED_PROP(float, _Metallic)
#define _Metallic_arr S_Cobblestone
			UNITY_DEFINE_INSTANCED_PROP(float, _Smoothness)
#define _Smoothness_arr S_Cobblestone
		UNITY_INSTANCING_BUFFER_END(S_Cobblestone)


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
			float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );
			float4 appendResult6 = (float4(ase_objectScale.x , ase_objectScale.y , 0.0 , 0.0));
			float _UVScale_Instance = UNITY_ACCESS_INSTANCED_PROP(_UVScale_arr, _UVScale);
			float4 temp_output_10_0 = ( ( float4( i.uv_texcoord, 0.0 , 0.0 ) * appendResult6 ) * _UVScale_Instance );
			o.Normal = UnpackNormal( tex2D( _Normal, temp_output_10_0.xy ) );
			float _Hue_Instance = UNITY_ACCESS_INSTANCED_PROP(_Hue_arr, _Hue);
			float3 hsvTorgb13_g1 = RGBToHSV( tex2D( _Diffuse, temp_output_10_0.xy, float2( 0,0 ), float2( 0,0 ) ).rgb );
			float _Saturation_Instance = UNITY_ACCESS_INSTANCED_PROP(_Saturation_arr, _Saturation);
			float _Lightness_Instance = UNITY_ACCESS_INSTANCED_PROP(_Lightness_arr, _Lightness);
			float3 hsvTorgb17_g1 = HSVToRGB( float3(( _Hue_Instance + hsvTorgb13_g1.x ),( _Saturation_Instance * hsvTorgb13_g1.y ),( hsvTorgb13_g1.z * _Lightness_Instance )) );
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
1927;1;1266;964;2410.258;731.4503;1;True;False
Node;AmplifyShaderEditor.ObjectScaleNode;5;-1986.331,-174.1567;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DynamicAppendNode;6;-1805.535,-173.0617;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;7;-1875.185,-293.8015;Float;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-1627.185,-293.8015;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-1625.957,-176.981;Float;False;InstancedProperty;_UVScale;UVScale;0;0;Create;True;0;0;False;0;200;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-1466.966,-292.6776;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-892.8738,-640.8968;Float;False;InstancedProperty;_Hue;Hue;2;0;Create;True;0;0;False;0;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-907.1698,-568.1151;Float;False;InstancedProperty;_Saturation;Saturation;3;0;Create;True;0;0;False;0;1;1;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-901.9713,-491.4344;Float;False;InstancedProperty;_Lightness;Lightness;4;0;Create;True;0;0;False;0;1;1;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-1049.203,-364.9397;Float;True;Property;_Diffuse;Diffuse;1;0;Create;True;0;0;False;0;188a05f21ca3c4a47ab9a3f471e377fb;188a05f21ca3c4a47ab9a3f471e377fb;True;0;False;white;Auto;False;Object;-1;Derivative;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;4;-371.3508,39.39184;Float;False;InstancedProperty;_Metallic;Metallic;7;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-371.6733,119.168;Float;False;InstancedProperty;_Smoothness;Smoothness;6;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-1051.146,-174.166;Float;True;Property;_Normal;Normal;5;0;Create;True;0;0;False;0;5f39954e19f5a48428a9a294690352a5;5f39954e19f5a48428a9a294690352a5;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;11;-539.1062,-317.5195;Float;False;SF_ColorShift;-1;;1;f3651b3e85772604cb45f632c7f608e7;0;4;26;FLOAT;0;False;27;FLOAT;0;False;28;FLOAT;0;False;23;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_Cobblestone;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;6;0;5;1
WireConnection;6;1;5;2
WireConnection;8;0;7;0
WireConnection;8;1;6;0
WireConnection;10;0;8;0
WireConnection;10;1;9;0
WireConnection;1;1;10;0
WireConnection;2;1;10;0
WireConnection;11;26;12;0
WireConnection;11;27;13;0
WireConnection;11;28;14;0
WireConnection;11;23;1;0
WireConnection;0;0;11;0
WireConnection;0;1;2;0
WireConnection;0;3;4;0
WireConnection;0;4;3;0
ASEEND*/
//CHKSM=F2578C833A4C1EBA952EFE82F1AF1A5EA3F356C1