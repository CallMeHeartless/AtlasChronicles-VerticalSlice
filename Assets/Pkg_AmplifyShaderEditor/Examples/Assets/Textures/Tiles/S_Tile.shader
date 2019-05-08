// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "New Amplify Shader"
{
	Properties
	{
		_Tile_A("Tile_A", 2D) = "white" {}
		_Tile_N("Tile_N", 2D) = "bump" {}
		_Tile_M("Tile_M", 2D) = "white" {}
		_Tile_O("Tile_O", 2D) = "white" {}
		_Tile_R("Tile_R", 2D) = "white" {}
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

		uniform sampler2D _Tile_N;
		uniform float4 _Tile_N_ST;
		uniform sampler2D _Tile_A;
		uniform float4 _Tile_A_ST;
		uniform sampler2D _Tile_M;
		uniform float4 _Tile_M_ST;
		uniform sampler2D _Tile_R;
		uniform float4 _Tile_R_ST;
		uniform sampler2D _Tile_O;
		uniform float4 _Tile_O_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Tile_N = i.uv_texcoord * _Tile_N_ST.xy + _Tile_N_ST.zw;
			o.Normal = UnpackNormal( tex2D( _Tile_N, uv_Tile_N ) );
			float2 uv_Tile_A = i.uv_texcoord * _Tile_A_ST.xy + _Tile_A_ST.zw;
			o.Albedo = tex2D( _Tile_A, uv_Tile_A ).rgb;
			float2 uv_Tile_M = i.uv_texcoord * _Tile_M_ST.xy + _Tile_M_ST.zw;
			o.Metallic = tex2D( _Tile_M, uv_Tile_M ).r;
			float2 uv_Tile_R = i.uv_texcoord * _Tile_R_ST.xy + _Tile_R_ST.zw;
			o.Smoothness = ( 1.0 - tex2D( _Tile_R, uv_Tile_R ) ).r;
			float2 uv_Tile_O = i.uv_texcoord * _Tile_O_ST.xy + _Tile_O_ST.zw;
			o.Occlusion = tex2D( _Tile_O, uv_Tile_O ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
41;51;1266;970;1288.143;827.6689;1.814928;True;True
Node;AmplifyShaderEditor.SamplerNode;6;-477.4089,-241.1476;Float;True;Property;_Tile_R;Tile_R;4;0;Create;True;0;0;False;0;b97db8acddac10d4c867939fcd38e487;b97db8acddac10d4c867939fcd38e487;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-490.0291,359.9741;Float;True;Property;_Tile_N;Tile_N;1;0;Create;True;0;0;False;0;00e0021f9f2d77a4fb3d5957267488e7;00e0021f9f2d77a4fb3d5957267488e7;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-507.629,81.31418;Float;True;Property;_Tile_A;Tile_A;0;0;Create;True;0;0;False;0;efd0eee9e4394a94db1426ce0bb7a902;efd0eee9e4394a94db1426ce0bb7a902;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;4;-581.1328,591.0944;Float;True;Property;_Tile_M;Tile_M;2;0;Create;True;0;0;False;0;d4940c862b9d0f349b3eec6e0da05578;d4940c862b9d0f349b3eec6e0da05578;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;5;-94.71269,537.4944;Float;True;Property;_Tile_O;Tile_O;3;0;Create;True;0;0;False;0;120ede8c4897abf488ebbcd0d0b46ede;120ede8c4897abf488ebbcd0d0b46ede;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;7;-151.998,141.5026;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;New Amplify Shader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;7;0;6;0
WireConnection;0;0;1;0
WireConnection;0;1;3;0
WireConnection;0;3;4;0
WireConnection;0;4;7;0
WireConnection;0;5;5;0
ASEEND*/
//CHKSM=D400FF7E43E7C58F5CA2571578B2E548F285E4C4