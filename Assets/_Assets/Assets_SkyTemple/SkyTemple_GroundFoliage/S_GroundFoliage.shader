// Upgrade NOTE: upgraded instancing buffer 'S_GroundFoliage' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_GroundFoliage"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Gradient("Gradient", 2D) = "white" {}
		_ID("ID", 2D) = "white" {}
		_Mask("Mask", 2D) = "white" {}
		_GrassColor01("GrassColor01", Color) = (0.1538998,0.4339623,0,0)
		_GrassColor02("GrassColor02", Color) = (0.07743207,0.6792453,0,0)
		_FlowerColor01("FlowerColor01", Color) = (1,0.8991845,0,0)
		_FlowerColor02("FlowerColor02", Color) = (0.1213083,0,1,0)
		_FlowerColor03("FlowerColor03", Color) = (1,0,0,0)
		_GroundAO_Intensity("GroundAO_Intensity", Range( 0 , 1)) = 0
		_WindDirection("Wind Direction", Float) = 0
		_BendAmount("BendAmount", Float) = 0
		_WorldFrequency("WorldFrequency", Float) = 0
		_Speed("Speed", Float) = 0
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
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform sampler2D _ID;
		uniform float4 _ID_ST;
		uniform sampler2D _Gradient;
		uniform float4 _Gradient_ST;
		uniform float _GroundAO_Intensity;
		uniform sampler2D _Mask;
		uniform float4 _Mask_ST;
		uniform float _Cutoff = 0.5;

		UNITY_INSTANCING_BUFFER_START(S_GroundFoliage)
			UNITY_DEFINE_INSTANCED_PROP(float4, _FlowerColor01)
#define _FlowerColor01_arr S_GroundFoliage
			UNITY_DEFINE_INSTANCED_PROP(float4, _FlowerColor02)
#define _FlowerColor02_arr S_GroundFoliage
			UNITY_DEFINE_INSTANCED_PROP(float4, _FlowerColor03)
#define _FlowerColor03_arr S_GroundFoliage
			UNITY_DEFINE_INSTANCED_PROP(float4, _GrassColor01)
#define _GrassColor01_arr S_GroundFoliage
			UNITY_DEFINE_INSTANCED_PROP(float4, _GrassColor02)
#define _GrassColor02_arr S_GroundFoliage
			UNITY_DEFINE_INSTANCED_PROP(float, _WindDirection)
#define _WindDirection_arr S_GroundFoliage
			UNITY_DEFINE_INSTANCED_PROP(float, _WorldFrequency)
#define _WorldFrequency_arr S_GroundFoliage
			UNITY_DEFINE_INSTANCED_PROP(float, _Speed)
#define _Speed_arr S_GroundFoliage
			UNITY_DEFINE_INSTANCED_PROP(float, _BendAmount)
#define _BendAmount_arr S_GroundFoliage
		UNITY_INSTANCING_BUFFER_END(S_GroundFoliage)


		float3 RotateAroundAxis( float3 center, float3 original, float3 u, float angle )
		{
			original -= center;
			float C = cos( angle );
			float S = sin( angle );
			float t = 1 - C;
			float m00 = t * u.x * u.x + C;
			float m01 = t * u.x * u.y - S * u.z;
			float m02 = t * u.x * u.z + S * u.y;
			float m10 = t * u.x * u.y + S * u.z;
			float m11 = t * u.y * u.y + C;
			float m12 = t * u.y * u.z - S * u.x;
			float m20 = t * u.x * u.z - S * u.y;
			float m21 = t * u.y * u.z + S * u.x;
			float m22 = t * u.z * u.z + C;
			float3x3 finalMatrix = float3x3( m00, m01, m02, m10, m11, m12, m20, m21, m22 );
			return mul( finalMatrix, original ) + center;
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float _WindDirection_Instance = UNITY_ACCESS_INSTANCED_PROP(_WindDirection_arr, _WindDirection);
			float3 ase_vertex3Pos = v.vertex.xyz;
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float _WorldFrequency_Instance = UNITY_ACCESS_INSTANCED_PROP(_WorldFrequency_arr, _WorldFrequency);
			float _Speed_Instance = UNITY_ACCESS_INSTANCED_PROP(_Speed_arr, _Speed);
			float _BendAmount_Instance = UNITY_ACCESS_INSTANCED_PROP(_BendAmount_arr, _BendAmount);
			float temp_output_31_0 = ( ( ase_vertex3Pos.y * cos( ( ( ( ase_worldPos.x + ase_worldPos.z ) * _WorldFrequency_Instance ) + ( _Time.y * _Speed_Instance ) ) ) ) * _BendAmount_Instance );
			float4 appendResult32 = (float4(temp_output_31_0 , 0.0 , temp_output_31_0 , 0.0));
			float4 break36 = mul( appendResult32, unity_ObjectToWorld );
			float4 appendResult37 = (float4(break36.x , 0 , break36.z , 0.0));
			float3 rotatedValue39 = RotateAroundAxis( float3( 0,0,0 ), appendResult37.xyz, float3( 0,0,0 ), _WindDirection_Instance );
			v.vertex.xyz += rotatedValue39;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 _FlowerColor01_Instance = UNITY_ACCESS_INSTANCED_PROP(_FlowerColor01_arr, _FlowerColor01);
			float4 _FlowerColor02_Instance = UNITY_ACCESS_INSTANCED_PROP(_FlowerColor02_arr, _FlowerColor02);
			float2 uv_ID = i.uv_texcoord * _ID_ST.xy + _ID_ST.zw;
			float4 tex2DNode2 = tex2D( _ID, uv_ID );
			float4 lerpResult8 = lerp( _FlowerColor01_Instance , _FlowerColor02_Instance , tex2DNode2.g);
			float4 _FlowerColor03_Instance = UNITY_ACCESS_INSTANCED_PROP(_FlowerColor03_arr, _FlowerColor03);
			float4 lerpResult7 = lerp( lerpResult8 , _FlowerColor03_Instance , tex2DNode2.b);
			float4 _GrassColor01_Instance = UNITY_ACCESS_INSTANCED_PROP(_GrassColor01_arr, _GrassColor01);
			float4 _GrassColor02_Instance = UNITY_ACCESS_INSTANCED_PROP(_GrassColor02_arr, _GrassColor02);
			float2 uv_Gradient = i.uv_texcoord * _Gradient_ST.xy + _Gradient_ST.zw;
			float4 tex2DNode1 = tex2D( _Gradient, uv_Gradient );
			float4 lerpResult16 = lerp( _GrassColor01_Instance , _GrassColor02_Instance , tex2DNode1);
			float4 lerpResult6 = lerp( lerpResult7 , lerpResult16 , tex2DNode2.a);
			float4 lerpResult17 = lerp( ( lerpResult6 * tex2DNode1 ) , lerpResult6 , _GroundAO_Intensity);
			o.Albedo = lerpResult17.rgb;
			float temp_output_22_0 = 0.0;
			o.Metallic = temp_output_22_0;
			o.Smoothness = temp_output_22_0;
			o.Alpha = 1;
			float2 uv_Mask = i.uv_texcoord * _Mask_ST.xy + _Mask_ST.zw;
			clip( tex2D( _Mask, uv_Mask ).r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1927;7;1266;958;1748.335;479.0116;2.340179;True;False
Node;AmplifyShaderEditor.WorldPosInputsNode;40;-2448.332,533.4113;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;23;-2600.145,1236.798;Float;False;InstancedProperty;_Speed;Speed;13;0;Create;True;0;0;False;0;0;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;41;-2181.132,571.8114;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;25;-2586.69,959.2775;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;24;-2485.132,746.2113;Float;False;InstancedProperty;_WorldFrequency;WorldFrequency;12;0;Create;True;0;0;False;0;0;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-2279.405,1047.113;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-2067.532,613.4113;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;27;-1939.532,717.4113;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;43;-1854.731,519.0112;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CosOpNode;28;-1766.732,949.4113;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;30;-1638.732,1168.611;Float;False;InstancedProperty;_BendAmount;BendAmount;11;0;Create;True;0;0;False;0;0;0.08;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-1598.732,939.8109;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-1688.083,64.39349;Float;True;Property;_ID;ID;2;0;Create;True;0;0;False;0;a1a14c63961047b419598dd83e0661af;a1a14c63961047b419598dd83e0661af;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;11;-1527.944,-486.6603;Float;False;InstancedProperty;_FlowerColor01;FlowerColor01;6;0;Create;True;0;0;False;0;1,0.8991845,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;12;-1531.519,-321.0299;Float;False;InstancedProperty;_FlowerColor02;FlowerColor02;7;0;Create;True;0;0;False;0;0.1213083,0,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;-1347.533,864.6112;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;13;-1280.264,-485.4685;Float;False;InstancedProperty;_FlowerColor03;FlowerColor03;8;0;Create;True;0;0;False;0;1,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;8;-1187.284,-196.7438;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;32;-1158.732,842.2111;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ColorNode;15;-981.0081,-465.2117;Float;False;InstancedProperty;_GrassColor02;GrassColor02;5;0;Create;True;0;0;False;0;0.07743207,0.6792453,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;14;-972.6671,-633.2252;Float;False;InstancedProperty;_GrassColor01;GrassColor01;4;0;Create;True;0;0;False;0;0.1538998,0.4339623,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ObjectToWorldMatrixNode;33;-1358.732,1075.812;Float;False;0;1;FLOAT4x4;0
Node;AmplifyShaderEditor.SamplerNode;1;-1688.083,-126.2971;Float;True;Property;_Gradient;Gradient;1;0;Create;True;0;0;False;0;a1c4c620c804a1b4abf41d34288c78b7;a1c4c620c804a1b4abf41d34288c78b7;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;7;-940.7223,-202.1465;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;16;-731.9675,-480.7025;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;-1016.332,858.2112;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4x4;0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1;False;1;FLOAT4;0
Node;AmplifyShaderEditor.Vector3Node;35;-1255.355,673.6962;Float;False;Constant;_Vector0;Vector 0;0;0;Create;True;0;0;False;0;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.BreakToComponentsNode;36;-802.3509,898.9039;Float;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.LerpOp;6;-638.2216,-203.6378;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;38;-896.8339,1171.532;Float;False;InstancedProperty;_WindDirection;Wind Direction;10;0;Create;True;0;0;False;0;0;0.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;37;-500.7803,879.4893;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-426.0983,-14.41308;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-618.0127,245.9531;Float;False;Property;_GroundAO_Intensity;GroundAO_Intensity;9;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RotateAboutAxisNode;39;-486.5425,1061.986;Float;False;False;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;-1188.151,250.5286;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;3;-1691.715,251.4518;Float;True;Property;_Mask;Mask;3;0;Create;True;0;0;False;0;9a8cc6d65f7ccaa449cc9d2c3a803552;9a8cc6d65f7ccaa449cc9d2c3a803552;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-868.3505,33.42859;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;17;-195.428,37.41993;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-194.2765,176.5493;Float;False;Constant;_Float0;Float 0;10;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-921.6508,162.1286;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_GroundFoliage;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;TransparentCutout;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;41;0;40;1
WireConnection;41;1;40;3
WireConnection;26;0;25;2
WireConnection;26;1;23;0
WireConnection;42;0;41;0
WireConnection;42;1;24;0
WireConnection;27;0;42;0
WireConnection;27;1;26;0
WireConnection;28;0;27;0
WireConnection;29;0;43;2
WireConnection;29;1;28;0
WireConnection;31;0;29;0
WireConnection;31;1;30;0
WireConnection;8;0;11;0
WireConnection;8;1;12;0
WireConnection;8;2;2;2
WireConnection;32;0;31;0
WireConnection;32;2;31;0
WireConnection;7;0;8;0
WireConnection;7;1;13;0
WireConnection;7;2;2;3
WireConnection;16;0;14;0
WireConnection;16;1;15;0
WireConnection;16;2;1;0
WireConnection;34;0;32;0
WireConnection;34;1;33;0
WireConnection;36;0;34;0
WireConnection;6;0;7;0
WireConnection;6;1;16;0
WireConnection;6;2;2;4
WireConnection;37;0;36;0
WireConnection;37;1;35;2
WireConnection;37;2;36;2
WireConnection;4;0;6;0
WireConnection;4;1;1;0
WireConnection;39;1;38;0
WireConnection;39;3;37;0
WireConnection;19;0;2;4
WireConnection;19;1;2;4
WireConnection;21;0;2;1
WireConnection;21;1;2;1
WireConnection;17;0;4;0
WireConnection;17;1;6;0
WireConnection;17;2;18;0
WireConnection;20;0;2;3
WireConnection;20;1;2;3
WireConnection;0;0;17;0
WireConnection;0;3;22;0
WireConnection;0;4;22;0
WireConnection;0;10;3;0
WireConnection;0;11;39;0
ASEEND*/
//CHKSM=457C73CC7FDE766BFD50312FB955863ECF665C74