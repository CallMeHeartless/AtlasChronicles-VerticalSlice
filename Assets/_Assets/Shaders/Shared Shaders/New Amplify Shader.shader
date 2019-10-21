// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_LandscapePaint"
{
	Properties
	{
		_GrassHSL("Grass HSL", Vector) = (0,0,1,0)
		_GrassTile("Grass Tile", Float) = 1
		_GrassDiff("GrassDiff", 2D) = "white" {}
		_GrassOSM("Grass OSM", 2D) = "white" {}
		_DirtHSL("DirtHSL", Vector) = (0,0,0,0)
		_DirtTile("Dirt Tile ", Float) = 1
		_DirtDiff("DirtDiff", 2D) = "white" {}
		_DirtNorm("DirtNorm", 2D) = "bump" {}
		_DirtOSM("Dirt OSM", 2D) = "white" {}
		_TileHSL("TileHSL", Vector) = (0,0,0,0)
		_TileTile("Tile Tile", Float) = 1
		_TileDiff("TileDiff", 2D) = "white" {}
		_TileNorm("TileNorm", 2D) = "bump" {}
		_TileOSM("Tile OSM", 2D) = "white" {}
		[Toggle(_USEFLATMETALLIC_ON)] _UseFlatMetallic("UseFlatMetallic", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma shader_feature _USEFLATMETALLIC_ON
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float4 vertexColor : COLOR;
			half2 uv_texcoord;
		};

		uniform sampler2D _DirtNorm;
		uniform half _DirtTile;
		uniform sampler2D _TileNorm;
		uniform half _TileTile;
		uniform half3 _GrassHSL;
		uniform sampler2D _GrassDiff;
		uniform half _GrassTile;
		uniform half3 _DirtHSL;
		uniform sampler2D _DirtDiff;
		uniform half3 _TileHSL;
		uniform sampler2D _TileDiff;
		uniform sampler2D _GrassOSM;
		uniform sampler2D _DirtOSM;
		uniform sampler2D _TileOSM;


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
			half2 temp_cast_0 = (_DirtTile).xx;
			float2 uv_TexCoord22 = i.uv_texcoord * temp_cast_0;
			half2 temp_cast_1 = (_TileTile).xx;
			float2 uv_TexCoord23 = i.uv_texcoord * temp_cast_1;
			float4 weightedBlendVar13 = i.vertexColor;
			float3 weightedAvg13 = ( ( weightedBlendVar13.x*half3(0,0,1) + weightedBlendVar13.y*UnpackNormal( tex2D( _DirtNorm, uv_TexCoord22 ) ) + weightedBlendVar13.z*UnpackNormal( tex2D( _TileNorm, uv_TexCoord23 ) ) + weightedBlendVar13.w*float3( 0,0,0 ) )/( weightedBlendVar13.x + weightedBlendVar13.y + weightedBlendVar13.z + weightedBlendVar13.w ) );
			o.Normal = weightedAvg13;
			half2 temp_cast_2 = (_GrassTile).xx;
			float2 uv_TexCoord18 = i.uv_texcoord * temp_cast_2;
			float3 hsvTorgb13_g2 = RGBToHSV( tex2D( _GrassDiff, uv_TexCoord18 ).rgb );
			float3 hsvTorgb17_g2 = HSVToRGB( half3(( _GrassHSL.x + hsvTorgb13_g2.x ),( _GrassHSL.y * hsvTorgb13_g2.y ),( hsvTorgb13_g2.z * _GrassHSL.z )) );
			float3 hsvTorgb13_g3 = RGBToHSV( tex2D( _DirtDiff, uv_TexCoord22 ).rgb );
			float3 hsvTorgb17_g3 = HSVToRGB( half3(( _DirtHSL.x + hsvTorgb13_g3.x ),( _DirtHSL.y * hsvTorgb13_g3.y ),( hsvTorgb13_g3.z * _DirtHSL.z )) );
			float3 hsvTorgb13_g4 = RGBToHSV( tex2D( _TileDiff, uv_TexCoord23 ).rgb );
			float3 hsvTorgb17_g4 = HSVToRGB( half3(( _TileHSL.x + hsvTorgb13_g4.x ),( _TileHSL.y * hsvTorgb13_g4.y ),( hsvTorgb13_g4.z * _TileHSL.z )) );
			float4 weightedBlendVar1 = i.vertexColor;
			float3 weightedAvg1 = ( ( weightedBlendVar1.x*hsvTorgb17_g2 + weightedBlendVar1.y*hsvTorgb17_g3 + weightedBlendVar1.z*hsvTorgb17_g4 + weightedBlendVar1.w*float3( 0,0,0 ) )/( weightedBlendVar1.x + weightedBlendVar1.y + weightedBlendVar1.z + weightedBlendVar1.w ) );
			o.Albedo = weightedAvg1;
			float4 weightedBlendVar14 = i.vertexColor;
			float4 weightedAvg14 = ( ( weightedBlendVar14.x*tex2D( _GrassOSM, uv_TexCoord18 ) + weightedBlendVar14.y*tex2D( _DirtOSM, uv_TexCoord22 ) + weightedBlendVar14.z*tex2D( _TileOSM, uv_TexCoord23 ) + weightedBlendVar14.w*float4( 0,0,0,0 ) )/( weightedBlendVar14.x + weightedBlendVar14.y + weightedBlendVar14.z + weightedBlendVar14.w ) );
			float4 break15 = weightedAvg14;
			#ifdef _USEFLATMETALLIC_ON
				float staticSwitch16 = 0.0;
			#else
				float staticSwitch16 = break15.b;
			#endif
			o.Metallic = staticSwitch16;
			o.Smoothness = break15.g;
			o.Occlusion = break15;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1927;1;1266;964;1718.505;161.7555;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;19;-2076.211,-322.6039;Float;False;Property;_GrassTile;Grass Tile;1;0;Create;True;0;0;False;0;1;0.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-2100.109,465.0518;Float;False;Property;_TileTile;Tile Tile;10;0;Create;True;0;0;False;0;1;0.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-2101.084,47.17973;Float;False;Property;_DirtTile;Dirt Tile ;5;0;Create;True;0;0;False;0;1;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;22;-1837.427,28.9393;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;23;-1877.225,441.8367;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;18;-1844.721,-341.7556;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;12;-1216.795,-566.4963;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;10;-1363.729,1168.837;Float;True;Property;_TileOSM;Tile OSM;13;0;Create;True;0;0;False;0;None;548415fece0bb9a4ea3c846cbf984a62;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;9;-1361.829,976.937;Float;True;Property;_DirtOSM;Dirt OSM;8;0;Create;True;0;0;False;0;None;b3d3948bddcd1634a9e96ad1a7611c19;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;8;-1358.029,783.1371;Float;True;Property;_GrassOSM;Grass OSM;3;0;Create;True;0;0;False;0;None;cefc4be6b4624e94c911d5e7d20e7bc7;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;30;-1344.219,-721.2226;Float;False;Property;_GrassHSL;Grass HSL;0;0;Create;True;0;0;False;0;0,0,1;0,1,1;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WeightedBlendNode;14;-737.9952,600.1036;Float;False;5;0;COLOR;3,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;2;-1332.695,-397.3964;Float;True;Property;_GrassDiff;GrassDiff;2;0;Create;True;0;0;False;0;None;1e7f9f10e50cb114e8d9da3813b19e4e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;34;-1334.253,-1027.004;Float;False;Property;_TileHSL;TileHSL;9;0;Create;True;0;0;False;0;0,0,0;0,1,1;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SamplerNode;4;-1338.395,-11.69637;Float;True;Property;_TileDiff;TileDiff;11;0;Create;True;0;0;False;0;None;28a15d43fcc92b346a882fea16d7011e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;32;-1335.98,-876.6573;Float;False;Property;_DirtHSL;DirtHSL;4;0;Create;True;0;0;False;0;0,0,0;0,1,1;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SamplerNode;3;-1336.495,-203.5964;Float;True;Property;_DirtDiff;DirtDiff;6;0;Create;True;0;0;False;0;None;d38dc5fc57e343a42838128945cb5e83;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;7;-1340.929,579.8371;Float;True;Property;_TileNorm;TileNorm;12;0;Create;True;0;0;False;0;None;3c9b200529380db429fa84c9014a7878;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;28;-1026.571,-452.0001;Float;False;SF_ColorShift;-1;;2;f3651b3e85772604cb45f632c7f608e7;0;4;26;FLOAT;0;False;27;FLOAT;0;False;28;FLOAT;0;False;23;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;31;-1018.333,-607.4349;Float;False;SF_ColorShift;-1;;3;f3651b3e85772604cb45f632c7f608e7;0;4;26;FLOAT;0;False;27;FLOAT;0;False;28;FLOAT;0;False;23;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;33;-1016.605,-757.7821;Float;False;SF_ColorShift;-1;;4;f3651b3e85772604cb45f632c7f608e7;0;4;26;FLOAT;0;False;27;FLOAT;0;False;28;FLOAT;0;False;23;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-452.7579,745.4117;Float;False;Constant;_FlatMetallic;Flat Metallic;10;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;6;-1342.829,382.237;Float;True;Property;_DirtNorm;DirtNorm;7;0;Create;True;0;0;False;0;None;e5c78e9bfc25ca04792efb78cd550cb8;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BreakToComponentsNode;15;-527.0952,599.904;Float;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.Vector3Node;27;-1295.136,130.4061;Float;True;Constant;_FlatNormal;FlatNormal;12;0;Create;True;0;0;False;0;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.StaticSwitch;16;-251.8946,655.9036;Float;False;Property;_UseFlatMetallic;UseFlatMetallic;14;0;Create;True;0;0;False;0;0;0;1;True;;Toggle;2;Key0;Key1;Create;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WeightedBlendNode;13;-730.3953,212.5037;Float;False;5;0;COLOR;3,0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WeightedBlendNode;1;-726.5948,-154.1964;Float;False;5;0;COLOR;3,0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;78.4,24;Half;False;True;2;Half;ASEMaterialInspector;0;0;Standard;S_LandscapePaint;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;22;0;20;0
WireConnection;23;0;21;0
WireConnection;18;0;19;0
WireConnection;10;1;23;0
WireConnection;9;1;22;0
WireConnection;8;1;18;0
WireConnection;14;0;12;0
WireConnection;14;1;8;0
WireConnection;14;2;9;0
WireConnection;14;3;10;0
WireConnection;2;1;18;0
WireConnection;4;1;23;0
WireConnection;3;1;22;0
WireConnection;7;1;23;0
WireConnection;28;26;30;1
WireConnection;28;27;30;2
WireConnection;28;28;30;3
WireConnection;28;23;2;0
WireConnection;31;26;32;1
WireConnection;31;27;32;2
WireConnection;31;28;32;3
WireConnection;31;23;3;0
WireConnection;33;26;34;1
WireConnection;33;27;34;2
WireConnection;33;28;34;3
WireConnection;33;23;4;0
WireConnection;6;1;22;0
WireConnection;15;0;14;0
WireConnection;16;1;15;2
WireConnection;16;0;17;0
WireConnection;13;0;12;0
WireConnection;13;1;27;0
WireConnection;13;2;6;0
WireConnection;13;3;7;0
WireConnection;1;0;12;0
WireConnection;1;1;28;0
WireConnection;1;2;31;0
WireConnection;1;3;33;0
WireConnection;0;0;1;0
WireConnection;0;1;13;0
WireConnection;0;3;16;0
WireConnection;0;4;15;1
WireConnection;0;5;15;0
ASEEND*/
//CHKSM=6588C32003C176DF751C54E63A0E6E12A9FD79E0