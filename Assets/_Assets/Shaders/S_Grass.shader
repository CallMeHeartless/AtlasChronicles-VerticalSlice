// Upgrade NOTE: upgraded instancing buffer 'S_Grass' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_Grass"
{
	Properties
	{
		_WindDirection("Wind Direction", Float) = 0
		_BendAmount("BendAmount", Float) = 0
		_WorldFrequency("WorldFrequency", Float) = 0
		_Speed("Speed", Float) = 0
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
		};

		UNITY_INSTANCING_BUFFER_START(S_Grass)
			UNITY_DEFINE_INSTANCED_PROP(float, _WindDirection)
#define _WindDirection_arr S_Grass
			UNITY_DEFINE_INSTANCED_PROP(float, _WorldFrequency)
#define _WorldFrequency_arr S_Grass
			UNITY_DEFINE_INSTANCED_PROP(float, _Speed)
#define _Speed_arr S_Grass
			UNITY_DEFINE_INSTANCED_PROP(float, _BendAmount)
#define _BendAmount_arr S_Grass
		UNITY_INSTANCING_BUFFER_END(S_Grass)


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
			float temp_output_25_0 = ( ( ase_vertex3Pos.y * cos( ( ( ( ase_worldPos.x + ase_worldPos.z ) * _WorldFrequency_Instance ) + ( _Time.y * _Speed_Instance ) ) ) ) * _BendAmount_Instance );
			float4 appendResult27 = (float4(temp_output_25_0 , 0.0 , temp_output_25_0 , 0.0));
			float4 break30 = mul( appendResult27, unity_ObjectToWorld );
			float4 appendResult34 = (float4(break30.x , 0 , break30.z , 0.0));
			float3 rotatedValue32 = RotateAroundAxis( float3( 0,0,0 ), appendResult34.xyz, float3( 0,0,0 ), _WindDirection_Instance );
			v.vertex.xyz += rotatedValue32;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
2052;84;1266;964;3224.572;510.8448;1.724412;True;False
Node;AmplifyShaderEditor.WorldPosInputsNode;16;-2298.498,-150.2694;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleAddOpNode;17;-2031.298,-111.8693;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-2335.298,62.53059;Float;False;InstancedProperty;_WorldFrequency;WorldFrequency;2;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;20;-2436.856,275.5968;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;36;-2450.311,553.1173;Float;False;InstancedProperty;_Speed;Speed;3;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-1917.698,-70.26951;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;-2129.571,363.432;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;19;-1789.698,33.73055;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CosOpNode;21;-1616.899,265.7305;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;23;-1704.898,-164.6696;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;26;-1488.899,484.9303;Float;False;InstancedProperty;_BendAmount;BendAmount;1;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-1448.899,256.1302;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-1197.7,180.9305;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;27;-1008.899,158.5304;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ObjectToWorldMatrixNode;29;-1208.899,392.1305;Float;False;0;1;FLOAT4x4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-866.4991,174.5305;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4x4;0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1;False;1;FLOAT4;0
Node;AmplifyShaderEditor.Vector3Node;33;-1105.522,-9.984589;Float;False;Constant;_Vector0;Vector 0;0;0;Create;True;0;0;False;0;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.BreakToComponentsNode;30;-652.5179,215.2232;Float;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.DynamicAppendNode;34;-350.947,195.8086;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-747.0013,487.8513;Float;False;InstancedProperty;_WindDirection;Wind Direction;0;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RotateAboutAxisNode;32;-336.7092,378.3047;Float;False;False;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-4.8,-92.80001;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_Grass;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;17;0;16;1
WireConnection;17;1;16;3
WireConnection;24;0;17;0
WireConnection;24;1;18;0
WireConnection;35;0;20;2
WireConnection;35;1;36;0
WireConnection;19;0;24;0
WireConnection;19;1;35;0
WireConnection;21;0;19;0
WireConnection;22;0;23;2
WireConnection;22;1;21;0
WireConnection;25;0;22;0
WireConnection;25;1;26;0
WireConnection;27;0;25;0
WireConnection;27;2;25;0
WireConnection;28;0;27;0
WireConnection;28;1;29;0
WireConnection;30;0;28;0
WireConnection;34;0;30;0
WireConnection;34;1;33;2
WireConnection;34;2;30;2
WireConnection;32;1;31;0
WireConnection;32;3;34;0
WireConnection;0;11;32;0
ASEEND*/
//CHKSM=BC58EB8864B3D5537D248EFBAE6FCD7D658F4AB1