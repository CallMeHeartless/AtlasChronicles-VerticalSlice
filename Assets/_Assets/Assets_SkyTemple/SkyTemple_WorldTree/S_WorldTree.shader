// Upgrade NOTE: upgraded instancing buffer 'S_WorldTree' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_WorldTree"
{
	Properties
	{
		_Diff01("Diff01", 2D) = "white" {}
		_FlatMoss("FlatMoss", 2D) = "white" {}
		_Diff02("Diff02", 2D) = "white" {}
		_Norm01("Norm01", 2D) = "white" {}
		_Norm02("Norm02", 2D) = "white" {}
		_OSM01("OSM01", 2D) = "white" {}
		_OSM02("OSM02", 2D) = "white" {}
		_Tile("Tile", Float) = 1
		_Diff01_ColorVariation("Diff01_ColorVariation", Vector) = (0,1,1,0)
		_Vector0("Vector 0", Vector) = (0,1,1,0)
		_FlatMossColorVar("FlatMossColorVar", Vector) = (0,1,1,0)
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
			half2 uv_texcoord;
			float4 vertexColor : COLOR;
		};

		uniform sampler2D _Norm01;
		uniform sampler2D _Norm02;
		uniform half3 _FlatMossColorVar;
		uniform sampler2D _FlatMoss;
		uniform half3 _Diff01_ColorVariation;
		uniform sampler2D _Diff01;
		uniform half3 _Vector0;
		uniform sampler2D _Diff02;
		uniform sampler2D _OSM01;
		uniform sampler2D _OSM02;

		UNITY_INSTANCING_BUFFER_START(S_WorldTree)
			UNITY_DEFINE_INSTANCED_PROP(half, _Tile)
#define _Tile_arr S_WorldTree
		UNITY_INSTANCING_BUFFER_END(S_WorldTree)


		half3 HSVToRGB( half3 c )
		{
			half4 K = half4( 1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0 );
			half3 p = abs( frac( c.xxx + K.xyz ) * 6.0 - K.www );
			return c.z * lerp( K.xxx, saturate( p - K.xxx ), c.y );
		}


		float3 RGBToHSV(float3 c)
		{
			float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
			float4 p = lerp( float4( c.bg, K.wz ), float4( c.gb, K.xy ), step( c.b, c.g ) );
			float4 q = lerp( float4( p.xyw, c.r ), float4( c.r, p.yzx ), step( p.x, c.r ) );
			float d = q.x - min( q.w, q.y );
			float e = 1.0e-10;
			return float3( abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float _Tile_Instance = UNITY_ACCESS_INSTANCED_PROP(_Tile_arr, _Tile);
			half2 temp_cast_0 = (_Tile_Instance).xx;
			float2 uv_TexCoord7 = i.uv_texcoord * temp_cast_0;
			float4 lerpResult44 = lerp( tex2D( _Norm01, uv_TexCoord7 ) , tex2D( _Norm02, uv_TexCoord7 ) , i.vertexColor.r);
			o.Normal = lerpResult44.rgb;
			float3 hsvTorgb13_g6 = RGBToHSV( tex2D( _FlatMoss, uv_TexCoord7 ).rgb );
			float3 hsvTorgb17_g6 = HSVToRGB( half3(( _FlatMossColorVar.x + hsvTorgb13_g6.x ),( _FlatMossColorVar.y * hsvTorgb13_g6.y ),( hsvTorgb13_g6.z * _FlatMossColorVar.z )) );
			float3 hsvTorgb13_g5 = RGBToHSV( tex2D( _Diff01, uv_TexCoord7 ).rgb );
			float3 hsvTorgb17_g5 = HSVToRGB( half3(( _Diff01_ColorVariation.x + hsvTorgb13_g5.x ),( _Diff01_ColorVariation.y * hsvTorgb13_g5.y ),( hsvTorgb13_g5.z * _Diff01_ColorVariation.z )) );
			float3 hsvTorgb13_g4 = RGBToHSV( tex2D( _Diff02, uv_TexCoord7 ).rgb );
			float3 hsvTorgb17_g4 = HSVToRGB( half3(( _Vector0.x + hsvTorgb13_g4.x ),( _Vector0.y * hsvTorgb13_g4.y ),( hsvTorgb13_g4.z * _Vector0.z )) );
			float3 lerpResult41 = lerp( hsvTorgb17_g5 , hsvTorgb17_g4 , i.vertexColor.r);
			float3 lerpResult40 = lerp( hsvTorgb17_g6 , lerpResult41 , i.vertexColor.g);
			o.Albedo = lerpResult40;
			o.Metallic = 0.0;
			half4 tex2DNode5 = tex2D( _OSM01, uv_TexCoord7 );
			half4 tex2DNode11 = tex2D( _OSM02, uv_TexCoord7 );
			float lerpResult46 = lerp( tex2DNode5.g , tex2DNode11.g , i.vertexColor.r);
			o.Smoothness = lerpResult46;
			float lerpResult45 = lerp( tex2DNode5.r , tex2DNode11.r , i.vertexColor.r);
			o.Occlusion = lerpResult45;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1927;7;1266;958;4548.876;1675.442;2.477479;True;False
Node;AmplifyShaderEditor.RangedFloatNode;8;-3788.058,-165.7514;Float;False;InstancedProperty;_Tile;Tile;7;0;Create;True;0;0;False;0;1;0.6;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;7;-3651.013,-201.1177;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;42;-2789.254,56.76492;Float;False;Property;_Vector0;Vector 0;9;0;Create;True;0;0;False;0;0,1,1;0,1,1;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector3Node;16;-2749.068,-531.0298;Float;False;Property;_Diff01_ColorVariation;Diff01_ColorVariation;8;0;Create;True;0;0;False;0;0,1,1;0.999,1.2,1;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SamplerNode;9;-3083.566,-23.65179;Float;True;Property;_Diff02;Diff02;2;0;Create;True;0;0;False;0;None;a7269110e657f3643b46d8436a6a552f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-3072.473,-640.751;Float;True;Property;_Diff01;Diff01;0;0;Create;True;0;0;False;0;None;a7269110e657f3643b46d8436a6a552f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;30;-2366.353,-1391.984;Float;True;Property;_FlatMoss;FlatMoss;1;0;Create;True;0;0;False;0;None;a7269110e657f3643b46d8436a6a552f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;43;-2540.718,66.42014;Float;False;SF_ColorShift;-1;;4;f3651b3e85772604cb45f632c7f608e7;0;4;26;FLOAT;0;False;27;FLOAT;0;False;28;FLOAT;0;False;23;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector3Node;32;-2371.644,-1117.898;Float;False;Property;_FlatMossColorVar;FlatMossColorVar;10;0;Create;True;0;0;False;0;0,1,1;0,1,1;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.FunctionNode;14;-2500.532,-521.3746;Float;False;SF_ColorShift;-1;;5;f3651b3e85772604cb45f632c7f608e7;0;4;26;FLOAT;0;False;27;FLOAT;0;False;28;FLOAT;0;False;23;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.VertexColorNode;12;-2939.679,-836.6011;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;11;-3085.308,396.1183;Float;True;Property;_OSM02;OSM02;6;0;Create;True;0;0;False;0;None;f5f73324a6357f344b29772a93100a72;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;10;-3084.526,190.3252;Float;True;Property;_Norm02;Norm02;4;0;Create;True;0;0;False;0;None;c6b322a8f9f814c4194c03cf2c525c75;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;5;-3074.215,-220.9809;Float;True;Property;_OSM01;OSM01;5;0;Create;True;0;0;False;0;None;f5f73324a6357f344b29772a93100a72;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;41;-1782.266,-314.9407;Float;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;31;-2084.535,-1100.323;Float;False;SF_ColorShift;-1;;6;f3651b3e85772604cb45f632c7f608e7;0;4;26;FLOAT;0;False;27;FLOAT;0;False;28;FLOAT;0;False;23;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;3;-3079.038,-426.774;Float;True;Property;_Norm01;Norm01;3;0;Create;True;0;0;False;0;None;c6b322a8f9f814c4194c03cf2c525c75;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;40;-733.0734,-506.5778;Float;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;45;-1778.834,-35.23767;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-270.9536,62.17197;Float;False;Constant;_Met;Met;3;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;44;-1785.478,-170.3391;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;46;-1783.264,99.86382;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Half;False;True;2;Half;ASEMaterialInspector;0;0;Standard;S_WorldTree;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;7;0;8;0
WireConnection;9;1;7;0
WireConnection;1;1;7;0
WireConnection;30;1;7;0
WireConnection;43;26;42;1
WireConnection;43;27;42;2
WireConnection;43;28;42;3
WireConnection;43;23;9;0
WireConnection;14;26;16;1
WireConnection;14;27;16;2
WireConnection;14;28;16;3
WireConnection;14;23;1;0
WireConnection;11;1;7;0
WireConnection;10;1;7;0
WireConnection;5;1;7;0
WireConnection;41;0;14;0
WireConnection;41;1;43;0
WireConnection;41;2;12;1
WireConnection;31;26;32;1
WireConnection;31;27;32;2
WireConnection;31;28;32;3
WireConnection;31;23;30;0
WireConnection;3;1;7;0
WireConnection;40;0;31;0
WireConnection;40;1;41;0
WireConnection;40;2;12;2
WireConnection;45;0;5;1
WireConnection;45;1;11;1
WireConnection;45;2;12;1
WireConnection;44;0;3;0
WireConnection;44;1;10;0
WireConnection;44;2;12;1
WireConnection;46;0;5;2
WireConnection;46;1;11;2
WireConnection;46;2;12;1
WireConnection;0;0;40;0
WireConnection;0;1;44;0
WireConnection;0;3;4;0
WireConnection;0;4;46;0
WireConnection;0;5;45;0
ASEEND*/
//CHKSM=AB826D88D5E8C7A7E477E9B4AA2D62FF85AC6337