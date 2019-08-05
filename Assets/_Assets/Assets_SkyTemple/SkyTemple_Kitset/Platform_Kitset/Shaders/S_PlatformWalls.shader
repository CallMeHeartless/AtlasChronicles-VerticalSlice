// Upgrade NOTE: upgraded instancing buffer 'S_PlatformWalls' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_PlatformWalls"
{
	Properties
	{
		_Occ("Occ", 2D) = "white" {}
		_Color("Color", Float) = 0
		_OccMult("Occ Mult", Float) = 0
		_Norm("Norm", 2D) = "bump" {}
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
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Norm;
		uniform float4 _Norm_ST;
		uniform sampler2D _Occ;
		uniform float4 _Occ_ST;

		UNITY_INSTANCING_BUFFER_START(S_PlatformWalls)
			UNITY_DEFINE_INSTANCED_PROP(float, _Color)
#define _Color_arr S_PlatformWalls
			UNITY_DEFINE_INSTANCED_PROP(float, _OccMult)
#define _OccMult_arr S_PlatformWalls
		UNITY_INSTANCING_BUFFER_END(S_PlatformWalls)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Norm = i.uv_texcoord * _Norm_ST.xy + _Norm_ST.zw;
			o.Normal = UnpackNormal( tex2D( _Norm, uv_Norm ) );
			float _Color_Instance = UNITY_ACCESS_INSTANCED_PROP(_Color_arr, _Color);
			float3 temp_cast_0 = (_Color_Instance).xxx;
			o.Albedo = temp_cast_0;
			o.Metallic = 0.0;
			o.Smoothness = 0.0;
			float _OccMult_Instance = UNITY_ACCESS_INSTANCED_PROP(_OccMult_arr, _OccMult);
			float2 uv_Occ = i.uv_texcoord * _Occ_ST.xy + _Occ_ST.zw;
			o.Occlusion = ( _OccMult_Instance * tex2D( _Occ, uv_Occ ) ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1927;1;1266;964;1654.478;619.3391;1.742339;True;False
Node;AmplifyShaderEditor.RangedFloatNode;11;-813.886,425.4031;Float;False;InstancedProperty;_OccMult;Occ Mult;3;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;15;-1217.151,304.1007;Float;True;Property;_Occ;Occ;0;0;Create;True;0;0;False;0;None;63be43012ad88de4f9a4ecf7435595e0;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;14;-1160.554,-11.22418;Float;True;Property;_Norm;Norm;4;0;Create;True;0;0;False;0;None;59ee2d7613f96774bb1bb0e022ab85cb;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;4;-716.7026,-8.771878;Float;False;InstancedProperty;_Smoothness;Smoothness;1;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-604.1564,-285.357;Float;False;InstancedProperty;_Color;Color;2;0;Create;True;0;0;False;0;0;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-592.1861,328.533;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-320.2746,184.4824;Float;False;Constant;_Float0;Float 0;8;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-288.1556,307.887;Float;False;Constant;_Float1;Float 1;8;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_PlatformWalls;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;10;0;11;0
WireConnection;10;1;15;0
WireConnection;0;0;7;0
WireConnection;0;1;14;0
WireConnection;0;3;13;0
WireConnection;0;4;12;0
WireConnection;0;5;10;0
ASEEND*/
//CHKSM=838B34BC9E48A46CB0A820562BC06EC59F80D62C