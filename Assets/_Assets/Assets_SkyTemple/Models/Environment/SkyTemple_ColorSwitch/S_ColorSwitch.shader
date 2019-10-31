// Upgrade NOTE: upgraded instancing buffer 'S_ColorSwitch' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_ColorSwitch"
{
	Properties
	{
		_Diff("Diff", 2D) = "white" {}
		_Norm("Norm", 2D) = "bump" {}
		_OSM("OSM", 2D) = "white" {}
		_Color03("Color03", Color) = (0,0,0,0)
		_Color02("Color02", Color) = (0,0,0,0)
		_Color01("Color01", Color) = (0,0,0,0)
		_Mask("Mask", 2D) = "white" {}
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
		uniform sampler2D _Diff;
		uniform float4 _Diff_ST;
		uniform sampler2D _Mask;
		uniform float4 _Mask_ST;
		uniform sampler2D _OSM;
		uniform float4 _OSM_ST;

		UNITY_INSTANCING_BUFFER_START(S_ColorSwitch)
			UNITY_DEFINE_INSTANCED_PROP(float4, _Color01)
#define _Color01_arr S_ColorSwitch
			UNITY_DEFINE_INSTANCED_PROP(float4, _Color02)
#define _Color02_arr S_ColorSwitch
			UNITY_DEFINE_INSTANCED_PROP(float4, _Color03)
#define _Color03_arr S_ColorSwitch
		UNITY_INSTANCING_BUFFER_END(S_ColorSwitch)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Norm = i.uv_texcoord * _Norm_ST.xy + _Norm_ST.zw;
			o.Normal = UnpackNormal( tex2D( _Norm, uv_Norm ) );
			float2 uv_Diff = i.uv_texcoord * _Diff_ST.xy + _Diff_ST.zw;
			float4 _Color01_Instance = UNITY_ACCESS_INSTANCED_PROP(_Color01_arr, _Color01);
			float2 uv_Mask = i.uv_texcoord * _Mask_ST.xy + _Mask_ST.zw;
			float4 tex2DNode10 = tex2D( _Mask, uv_Mask );
			float4 lerpResult4 = lerp( tex2D( _Diff, uv_Diff ) , _Color01_Instance , tex2DNode10.r);
			float4 _Color02_Instance = UNITY_ACCESS_INSTANCED_PROP(_Color02_arr, _Color02);
			float4 lerpResult5 = lerp( lerpResult4 , _Color02_Instance , tex2DNode10.g);
			float4 _Color03_Instance = UNITY_ACCESS_INSTANCED_PROP(_Color03_arr, _Color03);
			float4 lerpResult6 = lerp( lerpResult5 , _Color03_Instance , tex2DNode10.b);
			o.Albedo = lerpResult6.rgb;
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
1927;7;1266;958;1460.06;573.4551;1.236159;True;False
Node;AmplifyShaderEditor.SamplerNode;1;-993.6942,-244.1453;Float;True;Property;_Diff;Diff;0;0;Create;True;0;0;False;0;None;a04e637cffabbb742b7e49a397b36798;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;7;-885.361,-519.5691;Float;False;InstancedProperty;_Color01;Color01;5;0;Create;True;0;0;False;0;0,0,0,0;0.4213733,0,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;10;-1160.727,-1.852568;Float;True;Property;_Mask;Mask;6;0;Create;True;0;0;False;0;None;b69b01094cb35884194fc76ec0bdd8c6;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;4;-556.6887,-146.8288;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;8;-646.6605,-510.3883;Float;False;InstancedProperty;_Color02;Color02;4;0;Create;True;0;0;False;0;0,0,0,0;0,0.5566037,0.115959,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;5;-316.1518,-148.665;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;9;-294.1179,-493.8628;Float;False;InstancedProperty;_Color03;Color03;3;0;Create;True;0;0;False;0;0,0,0,0;0.7169812,0.4144416,0.01014594,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;6;-108.6658,-150.5012;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;2;-634.2118,195.5645;Float;True;Property;_Norm;Norm;1;0;Create;True;0;0;False;0;None;f4d5c5948b1fea7499ddfe4aa900c872;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-690.3666,419.6332;Float;True;Property;_OSM;OSM;2;0;Create;True;0;0;False;0;None;041290300656edd4b9ea10910d5e7a4e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_ColorSwitch;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;4;0;1;0
WireConnection;4;1;7;0
WireConnection;4;2;10;1
WireConnection;5;0;4;0
WireConnection;5;1;8;0
WireConnection;5;2;10;2
WireConnection;6;0;5;0
WireConnection;6;1;9;0
WireConnection;6;2;10;3
WireConnection;0;0;6;0
WireConnection;0;1;2;0
WireConnection;0;3;3;3
WireConnection;0;4;3;2
WireConnection;0;5;3;1
ASEEND*/
//CHKSM=134C398C39636CF3220FABA87EF03335DB623ED5