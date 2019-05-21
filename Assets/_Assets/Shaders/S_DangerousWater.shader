// Upgrade NOTE: upgraded instancing buffer 'S_DangerousWater' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_DangerousWater"
{
	Properties
	{
		_Normal("Normal", 2D) = "white" {}
		_NormalScale("Normal Scale", Float) = 0
		_DeepColor("Deep Color", Color) = (0,0,0,0)
		_ShalowColor("Shalow Color", Color) = (1,1,1,0)
		_WaterDepth("Water Depth", Float) = 0
		_WaterFalloff("Water Falloff", Float) = 0
		_WaterSmoothness("Water Smoothness", Float) = 0
		_Distortion("Distortion", Float) = 0.5
		_Normal01_PanSpeed("Normal01_PanSpeed", Vector) = (0,0,0,0)
		_Normal02_PanSpeed("Normal02_PanSpeed", Vector) = (0,0,0,0)
		_NormalTile0("NormalTile 0", Vector) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Back
		GrabPass{ }
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
		};

		uniform sampler2D _Normal;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform sampler2D _GrabTexture;

		UNITY_INSTANCING_BUFFER_START(S_DangerousWater)
			UNITY_DEFINE_INSTANCED_PROP(float4, _DeepColor)
#define _DeepColor_arr S_DangerousWater
			UNITY_DEFINE_INSTANCED_PROP(float4, _ShalowColor)
#define _ShalowColor_arr S_DangerousWater
			UNITY_DEFINE_INSTANCED_PROP(float2, _Normal01_PanSpeed)
#define _Normal01_PanSpeed_arr S_DangerousWater
			UNITY_DEFINE_INSTANCED_PROP(float2, _NormalTile0)
#define _NormalTile0_arr S_DangerousWater
			UNITY_DEFINE_INSTANCED_PROP(float2, _Normal02_PanSpeed)
#define _Normal02_PanSpeed_arr S_DangerousWater
			UNITY_DEFINE_INSTANCED_PROP(float, _NormalScale)
#define _NormalScale_arr S_DangerousWater
			UNITY_DEFINE_INSTANCED_PROP(float, _WaterDepth)
#define _WaterDepth_arr S_DangerousWater
			UNITY_DEFINE_INSTANCED_PROP(float, _WaterFalloff)
#define _WaterFalloff_arr S_DangerousWater
			UNITY_DEFINE_INSTANCED_PROP(float, _Distortion)
#define _Distortion_arr S_DangerousWater
			UNITY_DEFINE_INSTANCED_PROP(float, _WaterSmoothness)
#define _WaterSmoothness_arr S_DangerousWater
		UNITY_INSTANCING_BUFFER_END(S_DangerousWater)


		inline float4 ASE_ComputeGrabScreenPos( float4 pos )
		{
			#if UNITY_UV_STARTS_AT_TOP
			float scale = -1.0;
			#else
			float scale = 1.0;
			#endif
			float4 o = pos;
			o.y = pos.w * 0.5f;
			o.y = ( pos.y - o.y ) * _ProjectionParams.x * scale + o.y;
			return o;
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float _NormalScale_Instance = UNITY_ACCESS_INSTANCED_PROP(_NormalScale_arr, _NormalScale);
			float2 _Normal01_PanSpeed_Instance = UNITY_ACCESS_INSTANCED_PROP(_Normal01_PanSpeed_arr, _Normal01_PanSpeed);
			float2 _NormalTile0_Instance = UNITY_ACCESS_INSTANCED_PROP(_NormalTile0_arr, _NormalTile0);
			float2 uv_TexCoord34 = i.uv_texcoord * _NormalTile0_Instance;
			float2 panner35 = ( _Time.y * _Normal01_PanSpeed_Instance + uv_TexCoord34);
			float2 _Normal02_PanSpeed_Instance = UNITY_ACCESS_INSTANCED_PROP(_Normal02_PanSpeed_arr, _Normal02_PanSpeed);
			float2 panner36 = ( _Time.y * _Normal02_PanSpeed_Instance + uv_TexCoord34);
			float3 temp_output_40_0 = BlendNormals( UnpackScaleNormal( tex2D( _Normal, panner35 ), _NormalScale_Instance ) , UnpackScaleNormal( tex2D( _Normal, panner36 ), _NormalScale_Instance ) );
			o.Normal = temp_output_40_0;
			float4 _DeepColor_Instance = UNITY_ACCESS_INSTANCED_PROP(_DeepColor_arr, _DeepColor);
			float4 _ShalowColor_Instance = UNITY_ACCESS_INSTANCED_PROP(_ShalowColor_arr, _ShalowColor);
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float eyeDepth9 = LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture,UNITY_PROJ_COORD( ase_screenPosNorm ))));
			float temp_output_11_0 = abs( ( eyeDepth9 - ase_screenPosNorm.w ) );
			float _WaterDepth_Instance = UNITY_ACCESS_INSTANCED_PROP(_WaterDepth_arr, _WaterDepth);
			float _WaterFalloff_Instance = UNITY_ACCESS_INSTANCED_PROP(_WaterFalloff_arr, _WaterFalloff);
			float temp_output_20_0 = saturate( pow( ( temp_output_11_0 + _WaterDepth_Instance ) , _WaterFalloff_Instance ) );
			float4 lerpResult14 = lerp( _DeepColor_Instance , _ShalowColor_Instance , temp_output_20_0);
			float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( ase_screenPos );
			float4 ase_grabScreenPosNorm = ase_grabScreenPos / ase_grabScreenPos.w;
			float _Distortion_Instance = UNITY_ACCESS_INSTANCED_PROP(_Distortion_arr, _Distortion);
			float4 screenColor43 = tex2D( _GrabTexture, ( float3( (ase_grabScreenPosNorm).xy ,  0.0 ) + ( temp_output_40_0 * _Distortion_Instance ) ).xy );
			float4 lerpResult48 = lerp( lerpResult14 , screenColor43 , temp_output_20_0);
			o.Albedo = lerpResult48.rgb;
			float _WaterSmoothness_Instance = UNITY_ACCESS_INSTANCED_PROP(_WaterSmoothness_arr, _WaterSmoothness);
			o.Smoothness = _WaterSmoothness_Instance;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1927;1;1266;964;3671.845;1074.002;1.3;True;False
Node;AmplifyShaderEditor.CommentaryNode;12;-3610.626,327.3815;Float;False;915.2477;285.8345;Screen Depth;4;8;9;10;11;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;41;-2892.256,-836.9761;Float;False;1357.936;592.1338;Normals;9;34;35;36;38;37;39;40;54;55;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;57;-3110.246,-563.1016;Float;False;InstancedProperty;_NormalTile0;NormalTile 0;14;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.ScreenPosInputsNode;8;-3560.626,406.216;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TimeNode;54;-2767.888,-431.9736;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;55;-2752.279,-740.2373;Float;False;InstancedProperty;_Normal01_PanSpeed;Normal01_PanSpeed;12;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;56;-3015.021,-405.9595;Float;False;InstancedProperty;_Normal02_PanSpeed;Normal02_PanSpeed;13;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;34;-2842.256,-583.4793;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenDepthNode;9;-3302.718,377.3815;Float;False;0;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;10;-3011.171,465.4868;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;36;-2460.86,-484.8335;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;39;-2429.564,-359.8423;Float;False;InstancedProperty;_NormalScale;Normal Scale;2;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;21;-2628.664,-204.473;Float;False;1063.09;864.0524;Depth Controls;8;13;15;17;16;18;19;20;14;;1,1,1,1;0;0
Node;AmplifyShaderEditor.PannerNode;35;-2429.388,-643.569;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;49;-1406.523,-1138.115;Float;False;1050.035;562.2715;Refraction;6;44;45;42;47;46;43;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;38;-2115.832,-543.3128;Float;True;Property;_Normal;Normal;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;37;-2108.035,-786.9761;Float;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;True;Instance;38;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.AbsOpNode;11;-2849.378,463.8848;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-2563.316,470.2803;Float;False;InstancedProperty;_WaterDepth;Water Depth;5;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-2283.399,544.5794;Float;False;InstancedProperty;_WaterFalloff;Water Falloff;6;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;45;-1190.53,-690.8438;Float;False;InstancedProperty;_Distortion;Distortion;9;0;Create;True;0;0;False;0;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.BlendNormalsNode;40;-1766.321,-625.0132;Float;False;0;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GrabScreenPosition;42;-1356.523,-1088.115;Float;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;16;-2312.773,361.4231;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;18;-2082.964,437.4503;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;-945.8683,-801.7577;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ComponentMaskNode;47;-981.0811,-1054.092;Float;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;15;-2305.659,56.95245;Float;False;InstancedProperty;_ShalowColor;Shalow Color;4;0;Create;True;0;0;False;0;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;46;-740.7546,-891.0143;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SaturateNode;20;-1877.345,446.0896;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;13;-2556.925,-154.473;Float;False;InstancedProperty;_DeepColor;Deep Color;3;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;31;-2540.29,929.9532;Float;False;1107.178;606.7319;Foam;9;22;23;24;25;26;27;28;30;29;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ScreenColorNode;43;-558.4885,-893.0073;Float;False;Global;_GrabScreen0;Grab Screen 0;6;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;14;-1749.574,157.7763;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;28;-1915.742,1306.685;Float;True;Property;_Foam;Foam;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;53;-1044.73,309.6011;Float;False;InstancedProperty;_FoamSmoothness;Foam Smoothness;8;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;33;-1623.909,746.0374;Float;False;Constant;_Color0;Color 0;12;0;Create;True;0;0;False;0;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;52;-1019.447,224.7215;Float;False;InstancedProperty;_WaterSmoothness;Water Smoothness;7;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;32;-1256.958,651.6696;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;50;-654.0938,326.7059;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-2285.035,1164.748;Float;False;InstancedProperty;_FoamFalloff;Foam Falloff;11;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;26;-1784.219,1085.795;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;25;-1999.786,1017.112;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;23;-2196.512,979.9532;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;30;-2440.144,1335.351;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;22;-2490.29,1008.383;Float;False;InstancedProperty;_FoamDepth;Foam Depth;10;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-1602.111,1156.615;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;48;-371.3043,-342.6074;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.PannerNode;29;-2192.275,1337.036;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_DangerousWater;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Translucent;0.5;True;True;0;False;Opaque;;Transparent;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;34;0;57;0
WireConnection;9;0;8;0
WireConnection;10;0;9;0
WireConnection;10;1;8;4
WireConnection;36;0;34;0
WireConnection;36;2;56;0
WireConnection;36;1;54;2
WireConnection;35;0;34;0
WireConnection;35;2;55;0
WireConnection;35;1;54;2
WireConnection;38;1;36;0
WireConnection;38;5;39;0
WireConnection;37;1;35;0
WireConnection;37;5;39;0
WireConnection;11;0;10;0
WireConnection;40;0;37;0
WireConnection;40;1;38;0
WireConnection;16;0;11;0
WireConnection;16;1;17;0
WireConnection;18;0;16;0
WireConnection;18;1;19;0
WireConnection;44;0;40;0
WireConnection;44;1;45;0
WireConnection;47;0;42;0
WireConnection;46;0;47;0
WireConnection;46;1;44;0
WireConnection;20;0;18;0
WireConnection;43;0;46;0
WireConnection;14;0;13;0
WireConnection;14;1;15;0
WireConnection;14;2;20;0
WireConnection;28;1;29;0
WireConnection;32;0;14;0
WireConnection;32;1;33;0
WireConnection;32;2;27;0
WireConnection;50;0;52;0
WireConnection;50;1;53;0
WireConnection;50;2;27;0
WireConnection;26;0;25;0
WireConnection;25;0;23;0
WireConnection;25;1;24;0
WireConnection;23;0;11;0
WireConnection;23;1;22;0
WireConnection;27;0;26;0
WireConnection;27;1;28;1
WireConnection;48;0;14;0
WireConnection;48;1;43;0
WireConnection;48;2;20;0
WireConnection;29;0;30;0
WireConnection;0;0;48;0
WireConnection;0;1;40;0
WireConnection;0;4;52;0
ASEEND*/
//CHKSM=D9941C76EA30B598E708427F2E012569ECA28AB4