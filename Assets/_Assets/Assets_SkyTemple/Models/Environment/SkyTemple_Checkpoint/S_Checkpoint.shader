// Upgrade NOTE: upgraded instancing buffer 'S_Checkpoint' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_Checkpoint"
{
	Properties
	{
		_Diff("Diff", 2D) = "white" {}
		_Norm("Norm", 2D) = "bump" {}
		_OSM("OSM", 2D) = "white" {}
		_Mask("Mask", 2D) = "white" {}
		_Select("Select", Int) = 0
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
			UNITY_DEFINE_INSTANCED_PROP(float4, _CurrentCheckpointColor)
#define _CurrentCheckpointColor_arr S_Checkpoint
			UNITY_DEFINE_INSTANCED_PROP(float4, _ActiveEmissive)
#define _ActiveEmissive_arr S_Checkpoint
			UNITY_DEFINE_INSTANCED_PROP(float4, _InactiveColor)
#define _InactiveColor_arr S_Checkpoint
			UNITY_DEFINE_INSTANCED_PROP(int, _Select)
#define _Select_arr S_Checkpoint
		UNITY_INSTANCING_BUFFER_END(S_Checkpoint)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Norm = i.uv_texcoord * _Norm_ST.xy + _Norm_ST.zw;
			o.Normal = UnpackNormal( tex2D( _Norm, uv_Norm ) );
			float2 uv_Diff = i.uv_texcoord * _Diff_ST.xy + _Diff_ST.zw;
			o.Albedo = tex2D( _Diff, uv_Diff ).rgb;
			float2 uv_Mask = i.uv_texcoord * _Mask_ST.xy + _Mask_ST.zw;
			int _Select_Instance = UNITY_ACCESS_INSTANCED_PROP(_Select_arr, _Select);
			float4 _CurrentCheckpointColor_Instance = UNITY_ACCESS_INSTANCED_PROP(_CurrentCheckpointColor_arr, _CurrentCheckpointColor);
			float4 _ActiveEmissive_Instance = UNITY_ACCESS_INSTANCED_PROP(_ActiveEmissive_arr, _ActiveEmissive);
			float4 _InactiveColor_Instance = UNITY_ACCESS_INSTANCED_PROP(_InactiveColor_arr, _InactiveColor);
			float4 ifLocalVar19 = 0;
			if( _Select_Instance > 1.0 )
				ifLocalVar19 = _CurrentCheckpointColor_Instance;
			else if( _Select_Instance == 1.0 )
				ifLocalVar19 = _ActiveEmissive_Instance;
			else if( _Select_Instance < 1.0 )
				ifLocalVar19 = _InactiveColor_Instance;
			o.Emission = ( tex2D( _Mask, uv_Mask ) * ifLocalVar19 ).rgb;
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
1927;7;1266;958;2244.719;474.1511;1.914235;True;False
Node;AmplifyShaderEditor.ColorNode;8;-1688.832,666.6988;Float;False;InstancedProperty;_ActiveEmissive;ActiveEmissive;6;0;Create;True;0;0;False;0;0,0,0,0;0.5488315,0,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;9;-1698.956,866.7625;Float;False;InstancedProperty;_CurrentCheckpointColor;CurrentCheckpointColor;7;0;Create;True;0;0;False;0;0,0,0,0;1,0.6431373,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;10;-1697.728,465.7781;Float;False;InstancedProperty;_InactiveColor;InactiveColor;5;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.IntNode;14;-1435.11,552.609;Float;False;InstancedProperty;_Select;Select;4;0;Create;True;0;0;False;0;0;0;0;1;INT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-544.8776,768.1874;Float;False;Constant;_BaseValue;BaseValue;8;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;4;-871.8519,433.9563;Float;True;Property;_Mask;Mask;3;0;Create;True;0;0;False;0;None;2685457848f051e4dbd34e51294d50ef;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ConditionalIfNode;19;-363.0255,750.9595;Float;False;False;5;0;INT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1;-785.8894,-183.2547;Float;True;Property;_Diff;Diff;0;0;Create;True;0;0;False;0;None;b1bf7946fbd284e4db22502bb9540d91;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-870.1325,213.8922;Float;True;Property;_OSM;OSM;2;0;Create;True;0;0;False;0;None;9805b5a9a413b074aa1b13a1500021c2;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-765.2584,28.21313;Float;True;Property;_Norm;Norm;1;0;Create;True;0;0;False;0;None;57b07b1e945d2fb4a80a536ba42b79f2;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-357.7959,411.6061;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_Checkpoint;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;19;0;14;0
WireConnection;19;1;20;0
WireConnection;19;2;9;0
WireConnection;19;3;8;0
WireConnection;19;4;10;0
WireConnection;5;0;4;0
WireConnection;5;1;19;0
WireConnection;0;0;1;0
WireConnection;0;1;2;0
WireConnection;0;2;5;0
WireConnection;0;3;3;3
WireConnection;0;4;3;2
WireConnection;0;5;3;1
ASEEND*/
//CHKSM=80CEDBE30CB47B95D3CA199440A767045C35067D