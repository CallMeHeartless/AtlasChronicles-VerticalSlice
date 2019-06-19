// Upgrade NOTE: upgraded instancing buffer 'S_BreakableChest' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_BreakableChest"
{
	Properties
	{
		_Diff("Diff", 2D) = "white" {}
		_Smo("Smo", Float) = 0
		_Met("Met", Float) = 0
		_Normal("Normal", 2D) = "bump" {}
		_NormalIntensity("NormalIntensity", Range( 0 , 1)) = 1
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

		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform sampler2D _Diff;
		uniform float4 _Diff_ST;

		UNITY_INSTANCING_BUFFER_START(S_BreakableChest)
			UNITY_DEFINE_INSTANCED_PROP(float, _NormalIntensity)
#define _NormalIntensity_arr S_BreakableChest
			UNITY_DEFINE_INSTANCED_PROP(float, _Met)
#define _Met_arr S_BreakableChest
			UNITY_DEFINE_INSTANCED_PROP(float, _Smo)
#define _Smo_arr S_BreakableChest
		UNITY_INSTANCING_BUFFER_END(S_BreakableChest)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			float _NormalIntensity_Instance = UNITY_ACCESS_INSTANCED_PROP(_NormalIntensity_arr, _NormalIntensity);
			float3 lerpResult6 = lerp( float3(0,0,1) , UnpackNormal( tex2D( _Normal, uv_Normal ) ) , _NormalIntensity_Instance);
			o.Normal = lerpResult6;
			float2 uv_Diff = i.uv_texcoord * _Diff_ST.xy + _Diff_ST.zw;
			o.Albedo = tex2D( _Diff, uv_Diff ).rgb;
			float _Met_Instance = UNITY_ACCESS_INSTANCED_PROP(_Met_arr, _Met);
			o.Metallic = _Met_Instance;
			float _Smo_Instance = UNITY_ACCESS_INSTANCED_PROP(_Smo_arr, _Smo);
			o.Smoothness = _Smo_Instance;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1543;7;1906;1014;957.1444;435.3855;1;True;True
Node;AmplifyShaderEditor.SamplerNode;4;-749.5049,192.4478;Float;True;Property;_Normal;Normal;3;0;Create;True;0;0;False;0;None;60b9d862af15a804580f95576ebae59d;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;3;-731.709,387.6707;Float;False;InstancedProperty;_NormalIntensity;NormalIntensity;4;0;Create;True;0;0;False;0;1;0.45;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;5;-628.1228,42.8681;Float;False;Constant;_FlatNormal;FlatNormal;3;0;Create;True;0;0;False;0;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SamplerNode;1;-585,-146.5;Float;True;Property;_Diff;Diff;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;6;-390.6619,175.0135;Float;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-211.1444,130.6145;Float;False;InstancedProperty;_Smo;Smo;1;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-194.1444,223.6145;Float;False;InstancedProperty;_Met;Met;2;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1,-108;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_BreakableChest;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;6;0;5;0
WireConnection;6;1;4;0
WireConnection;6;2;3;0
WireConnection;0;0;1;0
WireConnection;0;1;6;0
WireConnection;0;3;8;0
WireConnection;0;4;7;0
ASEEND*/
//CHKSM=BFBAB04409896465792A4AD5D65462558C961F23