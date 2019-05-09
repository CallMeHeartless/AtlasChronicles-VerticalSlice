// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "New Amplify Shader"
{
	Properties
	{
		_T_MapFragment_Diff("T_MapFragment_Diff", 2D) = "white" {}
		_T_MapFragment_Norm("T_MapFragment_Norm", 2D) = "white" {}
		_T_MapFragment_Occ("T_MapFragment_Occ", 2D) = "white" {}
		_T_MapFragment_Smo("T_MapFragment_Smo", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _T_MapFragment_Norm;
		uniform float4 _T_MapFragment_Norm_ST;
		uniform sampler2D _T_MapFragment_Diff;
		uniform float4 _T_MapFragment_Diff_ST;
		uniform sampler2D _T_MapFragment_Smo;
		uniform float4 _T_MapFragment_Smo_ST;
		uniform sampler2D _T_MapFragment_Occ;
		uniform float4 _T_MapFragment_Occ_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_T_MapFragment_Norm = i.uv_texcoord * _T_MapFragment_Norm_ST.xy + _T_MapFragment_Norm_ST.zw;
			o.Normal = tex2D( _T_MapFragment_Norm, uv_T_MapFragment_Norm ).rgb;
			float2 uv_T_MapFragment_Diff = i.uv_texcoord * _T_MapFragment_Diff_ST.xy + _T_MapFragment_Diff_ST.zw;
			o.Albedo = tex2D( _T_MapFragment_Diff, uv_T_MapFragment_Diff ).rgb;
			o.Metallic = 0.0;
			float2 uv_T_MapFragment_Smo = i.uv_texcoord * _T_MapFragment_Smo_ST.xy + _T_MapFragment_Smo_ST.zw;
			o.Smoothness = tex2D( _T_MapFragment_Smo, uv_T_MapFragment_Smo ).r;
			float2 uv_T_MapFragment_Occ = i.uv_texcoord * _T_MapFragment_Occ_ST.xy + _T_MapFragment_Occ_ST.zw;
			o.Occlusion = tex2D( _T_MapFragment_Occ, uv_T_MapFragment_Occ ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1924;28;1266;964;813.3037;535.2163;1.3;True;True
Node;AmplifyShaderEditor.SamplerNode;1;-415.504,-177.7162;Float;True;Property;_T_MapFragment_Diff;T_MapFragment_Diff;0;0;Create;True;0;0;False;0;770ce4d8975e6db42a898bd72a566a0a;770ce4d8975e6db42a898bd72a566a0a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-405.1039,9.48381;Float;True;Property;_T_MapFragment_Norm;T_MapFragment_Norm;1;0;Create;True;0;0;False;0;d4db25a4f8f6e0a49a0556b88bad519c;d4db25a4f8f6e0a49a0556b88bad519c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-405.104,334.4838;Float;True;Property;_T_MapFragment_Occ;T_MapFragment_Occ;2;0;Create;True;0;0;False;0;c0094ccea734b9d4cbfc14583ef68677;c0094ccea734b9d4cbfc14583ef68677;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;4;-450.604,205.7838;Float;True;Property;_T_MapFragment_Smo;T_MapFragment_Smo;3;0;Create;True;0;0;False;0;651c41d8995011247beb6d10ef2225a5;651c41d8995011247beb6d10ef2225a5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;5;-154.2038,122.5837;Float;False;Constant;_Float0;Float 0;4;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;New Amplify Shader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;1;0
WireConnection;0;1;2;0
WireConnection;0;3;5;0
WireConnection;0;4;4;0
WireConnection;0;5;3;0
ASEEND*/
//CHKSM=4613F713CEC45441A40EFA444C5882AA1437EF92