// Upgrade NOTE: upgraded instancing buffer 'S_DangerousWater_New' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_DangerousWater_New"
{
	Properties
	{
		_Normal("Normal", 2D) = "bump" {}
		_NormalScale("Normal Scale", Float) = 0
		_DeepColor("Deep Color", Color) = (0,0,0,0)
		_ShalowColor("Shalow Color", Color) = (1,1,1,0)
		_WaterDepth("Water Depth", Float) = 0
		_WaterFalloff("Water Falloff", Float) = 0
		_Normal01_PanSpeed("Normal01_PanSpeed", Vector) = (0,0,0,0)
		_Normal02_PanSpeed("Normal02_PanSpeed", Vector) = (0,0,0,0)
		_UVScale("UVScale", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Off
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma surface surf Standard alpha:fade keepalpha addshadow fullforwardshadows exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
		};

		uniform sampler2D _Normal;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;

		UNITY_INSTANCING_BUFFER_START(S_DangerousWater_New)
			UNITY_DEFINE_INSTANCED_PROP(float4, _DeepColor)
#define _DeepColor_arr S_DangerousWater_New
			UNITY_DEFINE_INSTANCED_PROP(float4, _ShalowColor)
#define _ShalowColor_arr S_DangerousWater_New
			UNITY_DEFINE_INSTANCED_PROP(float2, _Normal01_PanSpeed)
#define _Normal01_PanSpeed_arr S_DangerousWater_New
			UNITY_DEFINE_INSTANCED_PROP(float2, _Normal02_PanSpeed)
#define _Normal02_PanSpeed_arr S_DangerousWater_New
			UNITY_DEFINE_INSTANCED_PROP(float, _NormalScale)
#define _NormalScale_arr S_DangerousWater_New
			UNITY_DEFINE_INSTANCED_PROP(float, _UVScale)
#define _UVScale_arr S_DangerousWater_New
			UNITY_DEFINE_INSTANCED_PROP(float, _WaterDepth)
#define _WaterDepth_arr S_DangerousWater_New
			UNITY_DEFINE_INSTANCED_PROP(float, _WaterFalloff)
#define _WaterFalloff_arr S_DangerousWater_New
		UNITY_INSTANCING_BUFFER_END(S_DangerousWater_New)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float _NormalScale_Instance = UNITY_ACCESS_INSTANCED_PROP(_NormalScale_arr, _NormalScale);
			float2 _Normal01_PanSpeed_Instance = UNITY_ACCESS_INSTANCED_PROP(_Normal01_PanSpeed_arr, _Normal01_PanSpeed);
			float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );
			float4 appendResult81 = (float4(ase_objectScale.x , ase_objectScale.y , 0.0 , 0.0));
			float _UVScale_Instance = UNITY_ACCESS_INSTANCED_PROP(_UVScale_arr, _UVScale);
			float4 temp_output_85_0 = ( ( float4( i.uv_texcoord, 0.0 , 0.0 ) * appendResult81 ) * _UVScale_Instance );
			float2 panner35 = ( _Time.y * _Normal01_PanSpeed_Instance + temp_output_85_0.xy);
			float2 _Normal02_PanSpeed_Instance = UNITY_ACCESS_INSTANCED_PROP(_Normal02_PanSpeed_arr, _Normal02_PanSpeed);
			float2 panner36 = ( _Time.y * _Normal02_PanSpeed_Instance + temp_output_85_0.xy);
			o.Normal = BlendNormals( UnpackScaleNormal( tex2D( _Normal, panner35 ), _NormalScale_Instance ) , UnpackScaleNormal( tex2D( _Normal, panner36 ), _NormalScale_Instance ) );
			float4 _DeepColor_Instance = UNITY_ACCESS_INSTANCED_PROP(_DeepColor_arr, _DeepColor);
			float4 _ShalowColor_Instance = UNITY_ACCESS_INSTANCED_PROP(_ShalowColor_arr, _ShalowColor);
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float eyeDepth9 = LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture,UNITY_PROJ_COORD( ase_screenPosNorm ))));
			float _WaterDepth_Instance = UNITY_ACCESS_INSTANCED_PROP(_WaterDepth_arr, _WaterDepth);
			float _WaterFalloff_Instance = UNITY_ACCESS_INSTANCED_PROP(_WaterFalloff_arr, _WaterFalloff);
			float4 lerpResult14 = lerp( _DeepColor_Instance , _ShalowColor_Instance , saturate( pow( ( abs( ( eyeDepth9 - ase_screenPosNorm.w ) ) + _WaterDepth_Instance ) , _WaterFalloff_Instance ) ));
			o.Albedo = lerpResult14.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1910;82;1266;912;4766.374;1239.134;2.6639;True;False
Node;AmplifyShaderEditor.CommentaryNode;12;-3590.594,240.5741;Float;False;915.2477;285.8345;Screen Depth;4;8;9;10;11;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;8;-3540.594,319.4086;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenDepthNode;9;-3282.686,290.5741;Float;False;0;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ObjectScaleNode;80;-3908.164,-73.89185;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DynamicAppendNode;81;-3727.368,-72.79682;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;83;-3797.018,-193.5366;Float;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;10;-2991.138,378.6794;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;21;-2613.298,78.6972;Float;False;1063.09;864.0524;Depth Controls;8;13;15;17;16;18;19;20;14;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;84;-3549.018,-193.5366;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-2547.949,753.4507;Float;False;InstancedProperty;_WaterDepth;Water Depth;4;0;Create;True;0;0;False;0;0;38.82;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;11;-2829.345,377.0774;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;89;-3547.79,-76.71613;Float;False;InstancedProperty;_UVScale;UVScale;8;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-2268.032,827.7498;Float;False;InstancedProperty;_WaterFalloff;Water Falloff;5;0;Create;True;0;0;False;0;0;-0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;16;-2297.406,644.5934;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;85;-3388.799,-192.4127;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.Vector2Node;55;-3032.224,-718.955;Float;False;InstancedProperty;_Normal01_PanSpeed;Normal01_PanSpeed;6;0;Create;True;0;0;False;0;0,0;0.05,0.02;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;56;-3027.418,-462.4742;Float;False;InstancedProperty;_Normal02_PanSpeed;Normal02_PanSpeed;7;0;Create;True;0;0;False;0;0,0;-0.005,0.02;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TimeNode;54;-3004.183,-868.8718;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;39;-2712.705,-807.1456;Float;False;InstancedProperty;_NormalScale;Normal Scale;1;0;Create;True;0;0;False;0;0;0.21;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;35;-2712.026,-719.486;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PowerNode;18;-2067.597,720.6207;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;36;-2714.24,-594.478;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;15;-2290.292,340.1227;Float;False;InstancedProperty;_ShalowColor;Shalow Color;3;0;Create;True;0;0;False;0;1,1,1,0;0.1650941,1,0.9268927,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;13;-2541.559,128.6972;Float;False;InstancedProperty;_DeepColor;Deep Color;2;0;Create;True;0;0;False;0;0,0,0,0;0.01481216,0,0.3962262,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;37;-2449.924,-824.9337;Float;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;True;Instance;38;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;38;-2453.931,-626.7536;Float;True;Property;_Normal;Normal;0;0;Create;True;0;0;False;0;None;759cb64ffbd2e404db48fc61910d01f0;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;20;-1861.979,729.26;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BlendNormalsNode;40;-2118.475,-831.878;Float;False;0;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;14;-1734.208,440.9465;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-1677.578,-461.8016;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_DangerousWater_New;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;0;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;9;0;8;0
WireConnection;81;0;80;1
WireConnection;81;1;80;2
WireConnection;10;0;9;0
WireConnection;10;1;8;4
WireConnection;84;0;83;0
WireConnection;84;1;81;0
WireConnection;11;0;10;0
WireConnection;16;0;11;0
WireConnection;16;1;17;0
WireConnection;85;0;84;0
WireConnection;85;1;89;0
WireConnection;35;0;85;0
WireConnection;35;2;55;0
WireConnection;35;1;54;2
WireConnection;18;0;16;0
WireConnection;18;1;19;0
WireConnection;36;0;85;0
WireConnection;36;2;56;0
WireConnection;36;1;54;2
WireConnection;37;1;35;0
WireConnection;37;5;39;0
WireConnection;38;1;36;0
WireConnection;38;5;39;0
WireConnection;20;0;18;0
WireConnection;40;0;37;0
WireConnection;40;1;38;0
WireConnection;14;0;13;0
WireConnection;14;1;15;0
WireConnection;14;2;20;0
WireConnection;0;0;14;0
WireConnection;0;1;40;0
ASEEND*/
//CHKSM=6FA191B3DCBCDDA0C4AF4465B2624D01800F2FE7