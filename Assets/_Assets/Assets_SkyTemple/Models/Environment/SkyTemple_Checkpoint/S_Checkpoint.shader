// Upgrade NOTE: upgraded instancing buffer 'S_Checkpoint' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_Checkpoint"
{
	Properties
	{
		_Diff("Diff", 2D) = "white" {}
		_Norm("Norm", 2D) = "white" {}
		_OSM("OSM", 2D) = "white" {}
		_Mask("Mask", 2D) = "white" {}
		[Toggle(_ACTIVE_ON)] _Active("Active", Float) = 0
		[Toggle(_CURRENTCHECKPOINT_ON)] _CurrentCheckpoint("CurrentCheckpoint", Float) = 0
		_InactiveColor("InactiveColor", Color) = (0,0,0,0)
		_ActiveEmissive("ActiveEmissive", Color) = (0,0,0,0)
		_CurrentCheckpointColor("CurrentCheckpointColor", Color) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma shader_feature _ACTIVE_ON
		#pragma shader_feature _CURRENTCHECKPOINT_ON
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Norm;
		uniform float4 _Norm_ST;
		uniform sampler2D _Diff;
		uniform float4 _Diff_ST;
		uniform sampler2D _Mask;
		uniform float4 _Mask_ST;
		uniform sampler2D _OSM;
		uniform float4 _OSM_ST;

		UNITY_INSTANCING_BUFFER_START(S_Checkpoint)
			UNITY_DEFINE_INSTANCED_PROP(float4, _InactiveColor)
#define _InactiveColor_arr S_Checkpoint
			UNITY_DEFINE_INSTANCED_PROP(float4, _ActiveEmissive)
#define _ActiveEmissive_arr S_Checkpoint
			UNITY_DEFINE_INSTANCED_PROP(float4, _CurrentCheckpointColor)
#define _CurrentCheckpointColor_arr S_Checkpoint
		UNITY_INSTANCING_BUFFER_END(S_Checkpoint)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Norm = i.uv_texcoord * _Norm_ST.xy + _Norm_ST.zw;
			o.Normal = UnpackNormal( tex2D( _Norm, uv_Norm ) );
			float2 uv_Diff = i.uv_texcoord * _Diff_ST.xy + _Diff_ST.zw;
			o.Albedo = tex2D( _Diff, uv_Diff ).rgb;
			float2 uv_Mask = i.uv_texcoord * _Mask_ST.xy + _Mask_ST.zw;
			float4 _InactiveColor_Instance = UNITY_ACCESS_INSTANCED_PROP(_InactiveColor_arr, _InactiveColor);
			float4 _ActiveEmissive_Instance = UNITY_ACCESS_INSTANCED_PROP(_ActiveEmissive_arr, _ActiveEmissive);
			float4 _CurrentCheckpointColor_Instance = UNITY_ACCESS_INSTANCED_PROP(_CurrentCheckpointColor_arr, _CurrentCheckpointColor);
			#ifdef _CURRENTCHECKPOINT_ON
				float4 staticSwitch7 = _CurrentCheckpointColor_Instance;
			#else
				float4 staticSwitch7 = _ActiveEmissive_Instance;
			#endif
			#ifdef _ACTIVE_ON
				float4 staticSwitch6 = staticSwitch7;
			#else
				float4 staticSwitch6 = _InactiveColor_Instance;
			#endif
			o.Emission = ( tex2D( _Mask, uv_Mask ) * staticSwitch6 ).rgb;
			float2 uv_OSM = i.uv_texcoord * _OSM_ST.xy + _OSM_ST.zw;
			float4 tex2DNode3 = tex2D( _OSM, uv_OSM );
			o.Metallic = tex2DNode3.b;
			o.Smoothness = tex2DNode3.g;
			o.Occlusion = tex2DNode3.r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1921;1;1278;970;1692.708;119.7878;1.419251;True;False
Node;AmplifyShaderEditor.ColorNode;9;-1286.216,989.5171;Float;False;InstancedProperty;_CurrentCheckpointColor;CurrentCheckpointColor;8;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;8;-1297.119,722.1439;Float;False;InstancedProperty;_ActiveEmissive;ActiveEmissive;7;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;7;-938.0614,728.6408;Float;False;Property;_CurrentCheckpoint;CurrentCheckpoint;5;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;10;-760.9412,893.0428;Float;False;InstancedProperty;_InactiveColor;InactiveColor;6;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;4;-871.8519,433.9563;Float;True;Property;_Mask;Mask;3;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;6;-417.9226,663.8975;Float;False;Property;_Active;Active;4;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1;-785.8894,-183.2547;Float;True;Property;_Diff;Diff;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-765.2584,28.21313;Float;True;Property;_Norm;Norm;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-870.1325,213.8922;Float;True;Property;_OSM;OSM;2;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-357.7959,411.6061;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_Checkpoint;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;7;1;8;0
WireConnection;7;0;9;0
WireConnection;6;1;10;0
WireConnection;6;0;7;0
WireConnection;5;0;4;0
WireConnection;5;1;6;0
WireConnection;0;0;1;0
WireConnection;0;1;2;0
WireConnection;0;2;5;0
WireConnection;0;3;3;3
WireConnection;0;4;3;2
WireConnection;0;5;3;1
ASEEND*/
//CHKSM=DB8A98FA0759A5F8D429E4435A36F5628931CEED