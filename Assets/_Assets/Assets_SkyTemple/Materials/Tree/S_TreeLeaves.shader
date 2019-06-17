// Upgrade NOTE: upgraded instancing buffer 'S_TreeLeaves' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_TreeLeaves"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_GradientTop("Gradient Top", Color) = (0,0,0,0)
		_GradientBottom("Gradient Bottom", Color) = (0,0,0,0)
		_StartPoint("Start Point", Range( -10 , 10)) = 0.5882353
		_Distribution("Distribution", Range( 0.1 , 10)) = 10
		_Opacitymask("Opacity mask", 2D) = "white" {}
		[Toggle(_USEOPACITYMASK_ON)] _UseOpacityMask("Use Opacity Mask", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Geometry+0" }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma shader_feature _USEOPACITYMASK_ON
		#include "TerrainEngine.cginc"
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform sampler2D _Opacitymask;
		uniform float4 _Opacitymask_ST;
		uniform float _Cutoff = 0.5;

		UNITY_INSTANCING_BUFFER_START(S_TreeLeaves)
			UNITY_DEFINE_INSTANCED_PROP(float4, _GradientBottom)
#define _GradientBottom_arr S_TreeLeaves
			UNITY_DEFINE_INSTANCED_PROP(float4, _GradientTop)
#define _GradientTop_arr S_TreeLeaves
			UNITY_DEFINE_INSTANCED_PROP(float, _StartPoint)
#define _StartPoint_arr S_TreeLeaves
			UNITY_DEFINE_INSTANCED_PROP(float, _Distribution)
#define _Distribution_arr S_TreeLeaves
		UNITY_INSTANCING_BUFFER_END(S_TreeLeaves)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 _GradientBottom_Instance = UNITY_ACCESS_INSTANCED_PROP(_GradientBottom_arr, _GradientBottom);
			float4 _GradientTop_Instance = UNITY_ACCESS_INSTANCED_PROP(_GradientTop_arr, _GradientTop);
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float _StartPoint_Instance = UNITY_ACCESS_INSTANCED_PROP(_StartPoint_arr, _StartPoint);
			float _Distribution_Instance = UNITY_ACCESS_INSTANCED_PROP(_Distribution_arr, _Distribution);
			float4 lerpResult4 = lerp( _GradientBottom_Instance , _GradientTop_Instance , saturate( ( ( ase_vertex3Pos.y + _StartPoint_Instance ) / _Distribution_Instance ) ));
			o.Albedo = lerpResult4.rgb;
			o.Alpha = 1;
			float4 temp_cast_1 = (1.0).xxxx;
			float2 uv_Opacitymask = i.uv_texcoord * _Opacitymask_ST.xy + _Opacitymask_ST.zw;
			#ifdef _USEOPACITYMASK_ON
				float4 staticSwitch15 = tex2D( _Opacitymask, uv_Opacitymask );
			#else
				float4 staticSwitch15 = temp_cast_1;
			#endif
			clip( staticSwitch15.r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1927;1;1266;964;1593.21;62.35637;2.009187;True;False
Node;AmplifyShaderEditor.PosVertexDataNode;5;-1490.102,380.4199;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;7;-1568.501,541.8399;Float;False;InstancedProperty;_StartPoint;Start Point;3;0;Create;True;0;0;False;0;0.5882353;-0.19;-10;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-1586.103,710.0197;Float;False;InstancedProperty;_Distribution;Distribution;4;0;Create;True;0;0;False;0;10;0.5;0.1;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;6;-1090.102,422.0199;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;9;-979.703,569.2197;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;10;-779.7025,561.2198;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;1;-1142.615,-122.3756;Float;False;InstancedProperty;_GradientBottom;Gradient Bottom;2;0;Create;True;0;0;False;0;0,0,0,0;0.005937696,0.1529411,0.0007119988,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;14;-649.8035,462.0701;Float;True;Property;_Opacitymask;Opacity mask;5;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;2;-1132.763,95.35057;Float;False;InstancedProperty;_GradientTop;Gradient Top;1;0;Create;True;0;0;False;0;0,0,0,0;0.2431372,0.2830188,0.007843138,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;13;-634.3812,309.5591;Float;False;Constant;_Float0;Float 0;4;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-1400.761,1339.059;Float;False;Property;_SecondaryFactor;SecondaryFactor;9;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NormalizeNode;17;-1979.409,1095.963;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SwizzleNode;19;-1828.319,1151.348;Float;False;FLOAT;2;1;2;3;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SwizzleNode;18;-1822.409,1049.963;Float;False;FLOAT;0;1;2;3;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-1485.662,1035.092;Float;False;Property;_EdgeFlutter;EdgeFlutter;11;0;Create;True;0;0;False;0;1;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;21;-1652.687,1081.829;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-1408.861,1235.159;Float;False;Property;_PrimaryFactor;PrimaryFactor;10;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-1327.43,1101.645;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-1419.461,925.8596;Float;False;Property;_BranchPhase;BranchPhase;12;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;12;-782.9021,-169.9802;Float;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleAddOpNode;31;-456.2753,932.7617;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.WorldPosInputsNode;16;-2159.5,1064.956;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.FunctionNode;28;-959.0606,997.1595;Float;False;Terrain Wind Animate Vertex;-1;;2;3bc81bd4568a7094daabf2ccd6a7e125;0;3;2;FLOAT4;0,0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.Vector4Node;27;-895.3523,1163.994;Float;False;Property;_TreeInstanceScale;_TreeInstanceScale;8;0;Fetch;True;0;0;False;0;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;15;-347.4132,319.0075;Float;False;Property;_UseOpacityMask;Use Opacity Mask;6;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;4;-701.3026,89.22005;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-474.1024,-41.98017;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-633.1891,957.3064;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.Vector3Node;30;-591.9202,1226.045;Float;False;Property;_TreeOffset;Tree Offset;7;0;Create;True;0;0;False;0;0,5,0;0,0.48,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DynamicAppendNode;26;-1155.107,1111.286;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-2.6,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_TreeLeaves;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;TransparentCutout;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Absolute;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;6;0;5;2
WireConnection;6;1;7;0
WireConnection;9;0;6;0
WireConnection;9;1;8;0
WireConnection;10;0;9;0
WireConnection;17;0;16;0
WireConnection;19;0;17;0
WireConnection;18;0;17;0
WireConnection;21;0;18;0
WireConnection;21;1;19;0
WireConnection;25;0;20;0
WireConnection;25;1;21;0
WireConnection;31;0;29;0
WireConnection;31;1;30;0
WireConnection;28;4;26;0
WireConnection;15;1;13;0
WireConnection;15;0;14;0
WireConnection;4;0;1;0
WireConnection;4;1;2;0
WireConnection;4;2;10;0
WireConnection;11;0;12;1
WireConnection;11;1;4;0
WireConnection;29;0;28;0
WireConnection;29;1;27;0
WireConnection;26;0;24;0
WireConnection;26;1;25;0
WireConnection;26;2;23;0
WireConnection;26;3;22;0
WireConnection;0;0;4;0
WireConnection;0;10;15;0
ASEEND*/
//CHKSM=5CDF631FB570F89028D228731146048567ABF26D