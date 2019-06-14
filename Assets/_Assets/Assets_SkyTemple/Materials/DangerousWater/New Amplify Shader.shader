// Upgrade NOTE: upgraded instancing buffer 'S_EdgeDetect' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_EdgeDetect"
{
	Properties
	{
		_Float0("Float 0", Range( 0 , 2)) = 2
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityCG.cginc"
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float4 screenPos;
		};

		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;

		UNITY_INSTANCING_BUFFER_START(S_EdgeDetect)
			UNITY_DEFINE_INSTANCED_PROP(float, _Float0)
#define _Float0_arr S_EdgeDetect
		UNITY_INSTANCING_BUFFER_END(S_EdgeDetect)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float _Float0_Instance = UNITY_ACCESS_INSTANCED_PROP(_Float0_arr, _Float0);
			float screenDepth15 = LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture,UNITY_PROJ_COORD( ase_screenPos ))));
			float distanceDepth15 = abs( ( screenDepth15 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _Float0_Instance ) );
			float3 temp_cast_0 = (distanceDepth15).xxx;
			o.Emission = temp_cast_0;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
-4;364;1918;1026;1817.843;331.1813;1.301111;True;False
Node;AmplifyShaderEditor.RangedFloatNode;17;-657.2521,-186.7581;Float;False;InstancedProperty;_Float0;Float 0;3;0;Create;True;0;0;False;0;2;2;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;5;-1351.084,305.9656;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CameraWorldClipPlanes;3;-1130.031,159.8032;Float;False;Far;0;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-746.4578,505.2209;Float;False;InstancedProperty;_Offset;Offset;2;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;-771.8304,-23.08487;Float;False;2;2;0;FLOAT;0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;11;-589.5487,331.7359;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;7;-621.4078,55.91407;Float;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.OneMinusNode;8;-391.7538,-21.86415;Float;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-466.8937,230.0759;Float;False;InstancedProperty;_Multy;Multy;0;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenDepthNode;1;-1087.617,-85.78507;Float;False;0;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-254.2788,105.9258;Float;True;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.CameraDepthFade;13;-976.2456,264.7924;Float;False;3;2;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DistanceOpNode;16;-832.902,-216.6836;Float;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;15;-480.2162,-178.595;Float;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;15.22111,45.73498;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_EdgeDetect;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;False;Transparent;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;0;15;0
WireConnection;2;1;3;0
WireConnection;11;0;5;4
WireConnection;11;1;12;0
WireConnection;7;0;2;0
WireConnection;7;1;11;0
WireConnection;8;0;7;0
WireConnection;1;0;5;0
WireConnection;9;0;8;0
WireConnection;9;1;10;0
WireConnection;15;0;17;0
WireConnection;0;2;15;0
ASEEND*/
//CHKSM=5D1B47BD4C8524CCB4FCB3F2FB582BE1C3080867