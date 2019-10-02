// Upgrade NOTE: upgraded instancing buffer 'S_InkWellLevel' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_InkWellLevel"
{
	Properties
	{
		_RaiseLevel("RaiseLevel", Range( 0 , 2)) = 0
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
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
		};

		UNITY_INSTANCING_BUFFER_START(S_InkWellLevel)
			UNITY_DEFINE_INSTANCED_PROP(float, _RaiseLevel)
#define _RaiseLevel_arr S_InkWellLevel
		UNITY_INSTANCING_BUFFER_END(S_InkWellLevel)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float _RaiseLevel_Instance = UNITY_ACCESS_INSTANCED_PROP(_RaiseLevel_arr, _RaiseLevel);
			float clampResult7 = clamp( ( ( ( 1.0 - (i.uv_texcoord).y ) - 1.0 ) + _RaiseLevel_Instance ) , 0.0 , 1.0 );
			float3 temp_cast_0 = (clampResult7).xxx;
			o.Albedo = temp_cast_0;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1927;7;1266;958;1085.346;462.7893;1;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;2;-1147.668,-269.7387;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;1;-912.5283,-275.1265;Float;True;False;True;False;False;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;3;-663.1865,-268.9401;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;4;-488.4576,-269.9863;Float;True;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-559.1167,-46.48465;Float;False;InstancedProperty;_RaiseLevel;RaiseLevel;0;0;Create;True;0;0;False;0;0;0;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;6;-257.9105,-265.7126;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;7;-41.49338,-270.0739;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;144,-280;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_InkWellLevel;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;1;0;2;0
WireConnection;3;0;1;0
WireConnection;4;0;3;0
WireConnection;6;0;4;0
WireConnection;6;1;5;0
WireConnection;7;0;6;0
WireConnection;0;0;7;0
ASEEND*/
//CHKSM=BEEC46EC221F5216C300B70D8DC32AB64A9CA85F