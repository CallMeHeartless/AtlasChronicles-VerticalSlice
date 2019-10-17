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
		_WindDirection("Wind Direction", Float) = 0
		_Distribution("Distribution", Range( 0.1 , 10)) = 10
		_BendAmount("BendAmount", Float) = 0
		_Opacitymask("Opacity mask", 2D) = "white" {}
		[Toggle(_USEOPACITYMASK_ON)] _UseOpacityMask("Use Opacity Mask", Float) = 0
		_WorldFrequency("WorldFrequency", Float) = 0
		_Speed("Speed", Float) = 0
		_ColorVarScale("ColorVarScale", Float) = 100
		_VarColor02("VarColor02", Color) = (0.1943086,1,0,0)
		_VarColor01("VarColor01", Color) = (1,0,0,0)
		_VarColor03("VarColor03", Color) = (0,0.1164293,1,0)
		_T_ColorVar_Mask("T_ColorVar_Mask", 2D) = "white" {}
		_ColorVar_Intensity("ColorVar_Intensity", Range( 0 , 1)) = 0
		_VariationOffset("Variation Offset", Vector) = (0,0,0,0)
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
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform sampler2D _T_ColorVar_Mask;
		uniform sampler2D _Opacitymask;
		uniform float4 _Opacitymask_ST;
		uniform float _Cutoff = 0.5;

		UNITY_INSTANCING_BUFFER_START(S_TreeLeaves)
			UNITY_DEFINE_INSTANCED_PROP(float4, _GradientBottom)
#define _GradientBottom_arr S_TreeLeaves
			UNITY_DEFINE_INSTANCED_PROP(float4, _GradientTop)
#define _GradientTop_arr S_TreeLeaves
			UNITY_DEFINE_INSTANCED_PROP(float4, _VarColor01)
#define _VarColor01_arr S_TreeLeaves
			UNITY_DEFINE_INSTANCED_PROP(float4, _VarColor02)
#define _VarColor02_arr S_TreeLeaves
			UNITY_DEFINE_INSTANCED_PROP(float4, _VarColor03)
#define _VarColor03_arr S_TreeLeaves
			UNITY_DEFINE_INSTANCED_PROP(float2, _VariationOffset)
#define _VariationOffset_arr S_TreeLeaves
			UNITY_DEFINE_INSTANCED_PROP(float, _WindDirection)
#define _WindDirection_arr S_TreeLeaves
			UNITY_DEFINE_INSTANCED_PROP(float, _WorldFrequency)
#define _WorldFrequency_arr S_TreeLeaves
			UNITY_DEFINE_INSTANCED_PROP(float, _Speed)
#define _Speed_arr S_TreeLeaves
			UNITY_DEFINE_INSTANCED_PROP(float, _BendAmount)
#define _BendAmount_arr S_TreeLeaves
			UNITY_DEFINE_INSTANCED_PROP(float, _StartPoint)
#define _StartPoint_arr S_TreeLeaves
			UNITY_DEFINE_INSTANCED_PROP(float, _Distribution)
#define _Distribution_arr S_TreeLeaves
			UNITY_DEFINE_INSTANCED_PROP(float, _ColorVarScale)
#define _ColorVarScale_arr S_TreeLeaves
			UNITY_DEFINE_INSTANCED_PROP(float, _ColorVar_Intensity)
#define _ColorVar_Intensity_arr S_TreeLeaves
		UNITY_INSTANCING_BUFFER_END(S_TreeLeaves)


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
			float temp_output_41_0 = ( ( ase_vertex3Pos.y * cos( ( ( ( ase_worldPos.x + ase_worldPos.z ) * _WorldFrequency_Instance ) + ( _Time.y * _Speed_Instance ) ) ) ) * _BendAmount_Instance );
			float4 appendResult33 = (float4(temp_output_41_0 , 0.0 , temp_output_41_0 , 0.0));
			float4 break35 = mul( appendResult33, unity_ObjectToWorld );
			float4 appendResult36 = (float4(break35.x , 0 , break35.z , 0.0));
			float3 rotatedValue38 = RotateAroundAxis( float3( 0,0,0 ), appendResult36.xyz, float3( 0,0,0 ), _WindDirection_Instance );
			v.vertex.xyz += rotatedValue38;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 _GradientBottom_Instance = UNITY_ACCESS_INSTANCED_PROP(_GradientBottom_arr, _GradientBottom);
			float4 _GradientTop_Instance = UNITY_ACCESS_INSTANCED_PROP(_GradientTop_arr, _GradientTop);
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float _StartPoint_Instance = UNITY_ACCESS_INSTANCED_PROP(_StartPoint_arr, _StartPoint);
			float _Distribution_Instance = UNITY_ACCESS_INSTANCED_PROP(_Distribution_arr, _Distribution);
			float4 lerpResult4 = lerp( _GradientBottom_Instance , _GradientTop_Instance , saturate( ( ( ase_vertex3Pos.y + _StartPoint_Instance ) / _Distribution_Instance ) ));
			float4 _VarColor01_Instance = UNITY_ACCESS_INSTANCED_PROP(_VarColor01_arr, _VarColor01);
			float4 _VarColor02_Instance = UNITY_ACCESS_INSTANCED_PROP(_VarColor02_arr, _VarColor02);
			float3 ase_worldPos = i.worldPos;
			float _ColorVarScale_Instance = UNITY_ACCESS_INSTANCED_PROP(_ColorVarScale_arr, _ColorVarScale);
			float2 _VariationOffset_Instance = UNITY_ACCESS_INSTANCED_PROP(_VariationOffset_arr, _VariationOffset);
			float2 uv_TexCoord83 = i.uv_texcoord * ( (ase_worldPos).xy / _ColorVarScale_Instance ) + _VariationOffset_Instance;
			float4 tex2DNode76 = tex2D( _T_ColorVar_Mask, uv_TexCoord83 );
			float4 lerpResult77 = lerp( _VarColor01_Instance , _VarColor02_Instance , tex2DNode76.r);
			float4 _VarColor03_Instance = UNITY_ACCESS_INSTANCED_PROP(_VarColor03_arr, _VarColor03);
			float4 lerpResult78 = lerp( lerpResult77 , _VarColor03_Instance , tex2DNode76.g);
			float4 blendOpSrc71 = lerpResult4;
			float4 blendOpDest71 = lerpResult78;
			float _ColorVar_Intensity_Instance = UNITY_ACCESS_INSTANCED_PROP(_ColorVar_Intensity_arr, _ColorVar_Intensity);
			float4 lerpResult81 = lerp( ( saturate( (( blendOpDest71 > 0.5 ) ? ( 1.0 - ( 1.0 - 2.0 * ( blendOpDest71 - 0.5 ) ) * ( 1.0 - blendOpSrc71 ) ) : ( 2.0 * blendOpDest71 * blendOpSrc71 ) ) )) , lerpResult4 , _ColorVar_Intensity_Instance);
			o.Albedo = lerpResult81.rgb;
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
2016;147;1266;942;3716.278;1776.035;2.359955;True;False
Node;AmplifyShaderEditor.WorldPosInputsNode;44;-2592.849,1561.87;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TimeNode;49;-2767.144,2087.574;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;48;-2671.144,1879.574;Float;False;InstancedProperty;_WorldFrequency;WorldFrequency;9;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;39;-2783.144,2359.574;Float;False;InstancedProperty;_Speed;Speed;13;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;45;-2325.649,1600.27;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;50;-2463.144,2183.574;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;46;-2212.049,1641.87;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;51;-2127.144,1847.574;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;54;-2991.315,-953.6793;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.PosVertexDataNode;47;-1999.248,1547.47;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;59;-2650.941,-822.603;Float;False;InstancedProperty;_ColorVarScale;ColorVarScale;17;0;Create;True;0;0;False;0;100;100;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CosOpNode;52;-1951.143,2071.574;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;55;-2693.334,-960.9105;Float;False;True;True;False;True;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;84;-2607.36,-681.7394;Float;False;InstancedProperty;_VariationOffset;Variation Offset;23;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleDivideOpNode;58;-2396.933,-957.5444;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-1568.501,541.8399;Float;False;InstancedProperty;_StartPoint;Start Point;3;0;Create;True;0;0;False;0;0.5882353;-0.2;-10;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;40;-1791.144,2071.574;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;53;-1823.145,2295.574;Float;False;InstancedProperty;_BendAmount;BendAmount;6;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;5;-1490.102,380.4199;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;6;-1090.102,422.0199;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-1586.103,710.0197;Float;False;InstancedProperty;_Distribution;Distribution;5;0;Create;True;0;0;False;0;10;0.4;0.1;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;-1535.145,1991.574;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;83;-2246.989,-948.1006;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;63;-1841.274,-786.313;Float;False;InstancedProperty;_VarColor01;VarColor01;19;0;Create;True;0;0;False;0;1,0,0,0;1,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;76;-1945.228,-981.9212;Float;True;Property;_T_ColorVar_Mask;T_ColorVar_Mask;21;0;Create;True;0;0;False;0;901c4f7fd89e3fb49887fe132f8c13dc;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;66;-1840.564,-601.954;Float;False;InstancedProperty;_VarColor02;VarColor02;18;0;Create;True;0;0;False;0;0.1943086,1,0,0;0.1943085,1,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;9;-979.703,569.2197;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ObjectToWorldMatrixNode;42;-1551.145,2199.574;Float;False;0;1;FLOAT4x4;0
Node;AmplifyShaderEditor.DynamicAppendNode;33;-1070.668,1630.182;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.LerpOp;77;-1491.605,-988.9163;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;2;-1132.763,95.35057;Float;False;InstancedProperty;_GradientTop;Gradient Top;1;0;Create;True;0;0;False;0;0,0,0,0;0.3667674,0.5283019,0.1171234,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;-926.6684,1646.182;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4x4;0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SaturateNode;10;-779.7025,561.2198;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;68;-1839.902,-419.3852;Float;False;InstancedProperty;_VarColor03;VarColor03;20;0;Create;True;0;0;False;0;0,0.1164293,1,0;0,0.1164292,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;1;-1138.646,-68.13436;Float;False;InstancedProperty;_GradientBottom;Gradient Bottom;2;0;Create;True;0;0;False;0;0,0,0,0;0.1786568,0.2924527,0.05380018,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;78;-1164.448,-957.8151;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector3Node;43;-1439.145,1799.574;Float;False;Constant;_Vector2;Vector 2;0;0;Create;True;0;0;False;0;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.BreakToComponentsNode;35;-718.6684,1678.182;Float;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.LerpOp;4;-701.3026,89.22005;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;14;-649.8035,462.0701;Float;True;Property;_Opacitymask;Opacity mask;7;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;37;-814.6684,1950.182;Float;False;InstancedProperty;_WindDirection;Wind Direction;4;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.BlendOpsNode;71;-397.2753,-648.065;Float;False;Overlay;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;36;-414.6684,1662.182;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;82;-569.8248,-208.5459;Float;False;InstancedProperty;_ColorVar_Intensity;ColorVar_Intensity;22;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-634.3812,309.5591;Float;False;Constant;_Float0;Float 0;4;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-489.5555,-85.24875;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WorldPosInputsNode;16;-2435.154,1045.24;Float;True;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;22;-1400.761,1339.059;Float;False;Property;_SecondaryFactor;SecondaryFactor;12;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-1419.461,925.8596;Float;False;Property;_BranchPhase;BranchPhase;16;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NormalizeNode;17;-2132.085,1040.874;Float;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;21;-1652.687,1081.829;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-1408.861,1235.159;Float;False;Property;_PrimaryFactor;PrimaryFactor;14;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-1327.43,1101.645;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-633.1891,957.3064;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SwizzleNode;19;-1817.301,1294.581;Float;True;FLOAT;2;1;2;3;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-1485.662,1036.666;Float;False;Property;_EdgeFlutter;EdgeFlutter;15;0;Create;True;0;0;False;0;1;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RotateAboutAxisNode;38;-398.6684,1838.182;Float;False;False;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;81;-237.8054,-171.1662;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector3Node;30;-591.9202,1226.045;Float;False;Property;_TreeOffset;Tree Offset;10;0;Create;True;0;0;False;0;0,5,0;0,0.48,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DynamicAppendNode;26;-1155.107,1111.286;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SwizzleNode;18;-1822.409,1049.963;Float;True;FLOAT;0;1;2;3;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;12;-730.0281,-100.8421;Float;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.FunctionNode;28;-959.0606,997.1595;Float;False;Terrain Wind Animate Vertex;-1;;2;3bc81bd4568a7094daabf2ccd6a7e125;0;3;2;FLOAT4;0,0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.StaticSwitch;15;-347.4132,319.0075;Float;False;Property;_UseOpacityMask;Use Opacity Mask;8;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector4Node;27;-895.3523,1163.994;Float;False;Property;_TreeInstanceScale;_TreeInstanceScale;11;0;Fetch;True;0;0;False;0;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;31;-456.2753,932.7617;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-2.6,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_TreeLeaves;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;TransparentCutout;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;45;0;44;1
WireConnection;45;1;44;3
WireConnection;50;0;49;2
WireConnection;50;1;39;0
WireConnection;46;0;45;0
WireConnection;46;1;48;0
WireConnection;51;0;46;0
WireConnection;51;1;50;0
WireConnection;52;0;51;0
WireConnection;55;0;54;0
WireConnection;58;0;55;0
WireConnection;58;1;59;0
WireConnection;40;0;47;2
WireConnection;40;1;52;0
WireConnection;6;0;5;2
WireConnection;6;1;7;0
WireConnection;41;0;40;0
WireConnection;41;1;53;0
WireConnection;83;0;58;0
WireConnection;83;1;84;0
WireConnection;76;1;83;0
WireConnection;9;0;6;0
WireConnection;9;1;8;0
WireConnection;33;0;41;0
WireConnection;33;2;41;0
WireConnection;77;0;63;0
WireConnection;77;1;66;0
WireConnection;77;2;76;1
WireConnection;34;0;33;0
WireConnection;34;1;42;0
WireConnection;10;0;9;0
WireConnection;78;0;77;0
WireConnection;78;1;68;0
WireConnection;78;2;76;2
WireConnection;35;0;34;0
WireConnection;4;0;1;0
WireConnection;4;1;2;0
WireConnection;4;2;10;0
WireConnection;71;0;4;0
WireConnection;71;1;78;0
WireConnection;36;0;35;0
WireConnection;36;1;43;2
WireConnection;36;2;35;2
WireConnection;11;0;12;1
WireConnection;11;1;4;0
WireConnection;17;0;16;0
WireConnection;21;0;18;0
WireConnection;21;1;19;0
WireConnection;25;0;20;0
WireConnection;25;1;21;0
WireConnection;29;0;28;0
WireConnection;29;1;27;0
WireConnection;19;0;17;0
WireConnection;38;1;37;0
WireConnection;38;3;36;0
WireConnection;81;0;71;0
WireConnection;81;1;4;0
WireConnection;81;2;82;0
WireConnection;26;0;24;0
WireConnection;26;1;25;0
WireConnection;26;2;23;0
WireConnection;26;3;22;0
WireConnection;18;0;17;0
WireConnection;28;4;26;0
WireConnection;15;1;13;0
WireConnection;15;0;14;0
WireConnection;31;0;29;0
WireConnection;31;1;30;0
WireConnection;0;0;81;0
WireConnection;0;10;15;0
WireConnection;0;11;38;0
ASEEND*/
//CHKSM=25F14A695C1ACCC0D3E8AB410BE330B78436B72E