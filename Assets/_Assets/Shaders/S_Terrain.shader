// Upgrade NOTE: upgraded instancing buffer 'S_Terrain' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_Terrain"
{
	Properties
	{
		_Tex01_Tile("Tex01_Tile", Float) = 1
		_Tex01_Diff("Tex01_Diff", 2D) = "white" {}
		[Toggle(_USE_TEX01_NORM_ON)] _Use_Tex01_Norm("Use_Tex01_Norm", Float) = 0
		_Tex01_Norm("Tex01_Norm", 2D) = "white" {}
		[Toggle(_USE_TEX01_MET_ON)] _Use_Tex01_Met("Use_Tex01_Met", Float) = 0
		_Tex01_Met("Tex01_Met", 2D) = "white" {}
		_Tex01_Smo("Tex01_Smo", 2D) = "white" {}
		_Tex02_Tile("Tex02_Tile", Float) = 1
		_Tex02_Diff("Tex02_Diff", 2D) = "white" {}
		[Toggle(_USE_TEX02_NORM_ON)] _Use_Tex02_Norm("Use_Tex02_Norm", Float) = 0
		_Tex02_Norm("Tex02_Norm", 2D) = "white" {}
		[Toggle(_USE_TEX02_MET_ON)] _use_Tex02_Met("use_Tex02_Met", Float) = 0
		_Tex02_Met("Tex02_Met", 2D) = "white" {}
		_Tex02_Smo("Tex02_Smo", 2D) = "white" {}
		_Tex03_Tile("Tex03_Tile", Float) = 1
		_Tex03_Diff("Tex03_Diff", 2D) = "white" {}
		[Toggle(_USE_TEX03_NORM_ON)] _Use_Tex03_Norm("Use_Tex03_Norm", Float) = 0
		_Tex03_Norm("Tex03_Norm", 2D) = "white" {}
		[Toggle(_USE_TEX03_MET_ON)] _Use_Tex03_met("Use_Tex03_met", Float) = 0
		_Tex03_Met("Tex03_Met", 2D) = "white" {}
		_Tex03_Smo("Tex03_Smo", 2D) = "white" {}
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
		#pragma shader_feature _USE_TEX01_NORM_ON
		#pragma shader_feature _USE_TEX02_NORM_ON
		#pragma shader_feature _USE_TEX03_NORM_ON
		#pragma shader_feature _USE_TEX01_MET_ON
		#pragma shader_feature _USE_TEX02_MET_ON
		#pragma shader_feature _USE_TEX03_MET_ON
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float4 vertexColor : COLOR;
			float2 uv_texcoord;
		};

		uniform sampler2D _Tex01_Norm;
		uniform sampler2D _Tex02_Norm;
		uniform sampler2D _Tex03_Norm;
		uniform sampler2D _Tex01_Diff;
		uniform sampler2D _Tex02_Diff;
		uniform sampler2D _Tex03_Diff;
		uniform sampler2D _Tex01_Met;
		uniform sampler2D _Tex02_Met;
		uniform sampler2D _Tex03_Met;
		uniform sampler2D _Tex01_Smo;
		uniform sampler2D _Tex02_Smo;
		uniform sampler2D _Tex03_Smo;

		UNITY_INSTANCING_BUFFER_START(S_Terrain)
			UNITY_DEFINE_INSTANCED_PROP(float, _Tex01_Tile)
#define _Tex01_Tile_arr S_Terrain
			UNITY_DEFINE_INSTANCED_PROP(float, _Tex02_Tile)
#define _Tex02_Tile_arr S_Terrain
			UNITY_DEFINE_INSTANCED_PROP(float, _Tex03_Tile)
#define _Tex03_Tile_arr S_Terrain
		UNITY_INSTANCING_BUFFER_END(S_Terrain)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 _Vector0 = float3(0,0,1);
			float _Tex01_Tile_Instance = UNITY_ACCESS_INSTANCED_PROP(_Tex01_Tile_arr, _Tex01_Tile);
			float2 temp_cast_0 = (_Tex01_Tile_Instance).xx;
			float2 uv_TexCoord5 = i.uv_texcoord * temp_cast_0;
			#ifdef _USE_TEX01_NORM_ON
				float3 staticSwitch28 = UnpackNormal( tex2D( _Tex01_Norm, uv_TexCoord5 ) );
			#else
				float3 staticSwitch28 = _Vector0;
			#endif
			float _Tex02_Tile_Instance = UNITY_ACCESS_INSTANCED_PROP(_Tex02_Tile_arr, _Tex02_Tile);
			float2 temp_cast_1 = (_Tex02_Tile_Instance).xx;
			float2 uv_TexCoord4 = i.uv_texcoord * temp_cast_1;
			#ifdef _USE_TEX02_NORM_ON
				float3 staticSwitch30 = UnpackNormal( tex2D( _Tex02_Norm, uv_TexCoord4 ) );
			#else
				float3 staticSwitch30 = _Vector0;
			#endif
			float _Tex03_Tile_Instance = UNITY_ACCESS_INSTANCED_PROP(_Tex03_Tile_arr, _Tex03_Tile);
			float2 temp_cast_2 = (_Tex03_Tile_Instance).xx;
			float2 uv_TexCoord6 = i.uv_texcoord * temp_cast_2;
			#ifdef _USE_TEX03_NORM_ON
				float3 staticSwitch31 = UnpackNormal( tex2D( _Tex03_Norm, uv_TexCoord6 ) );
			#else
				float3 staticSwitch31 = _Vector0;
			#endif
			float4 weightedBlendVar23 = i.vertexColor;
			float3 weightedAvg23 = ( ( weightedBlendVar23.x*staticSwitch28 + weightedBlendVar23.y*staticSwitch30 + weightedBlendVar23.z*staticSwitch31 + weightedBlendVar23.w*float3( 0,0,0 ) )/( weightedBlendVar23.x + weightedBlendVar23.y + weightedBlendVar23.z + weightedBlendVar23.w ) );
			o.Normal = weightedAvg23;
			float4 weightedBlendVar27 = i.vertexColor;
			float4 weightedAvg27 = ( ( weightedBlendVar27.x*tex2D( _Tex01_Diff, uv_TexCoord5 ) + weightedBlendVar27.y*tex2D( _Tex02_Diff, uv_TexCoord4 ) + weightedBlendVar27.z*tex2D( _Tex03_Diff, uv_TexCoord6 ) + weightedBlendVar27.w*float4( 0,0,0,0 ) )/( weightedBlendVar27.x + weightedBlendVar27.y + weightedBlendVar27.z + weightedBlendVar27.w ) );
			o.Albedo = weightedAvg27.rgb;
			float4 temp_cast_4 = (0.0).xxxx;
			#ifdef _USE_TEX01_MET_ON
				float4 staticSwitch32 = tex2D( _Tex01_Met, uv_TexCoord5 );
			#else
				float4 staticSwitch32 = temp_cast_4;
			#endif
			float4 temp_cast_5 = (0.0).xxxx;
			#ifdef _USE_TEX02_MET_ON
				float4 staticSwitch33 = tex2D( _Tex02_Met, uv_TexCoord4 );
			#else
				float4 staticSwitch33 = temp_cast_5;
			#endif
			float4 temp_cast_6 = (0.0).xxxx;
			#ifdef _USE_TEX03_MET_ON
				float4 staticSwitch34 = tex2D( _Tex03_Met, uv_TexCoord6 );
			#else
				float4 staticSwitch34 = temp_cast_6;
			#endif
			float4 weightedBlendVar26 = i.vertexColor;
			float4 weightedAvg26 = ( ( weightedBlendVar26.x*staticSwitch32 + weightedBlendVar26.y*staticSwitch33 + weightedBlendVar26.z*staticSwitch34 + weightedBlendVar26.w*float4( 0,0,0,0 ) )/( weightedBlendVar26.x + weightedBlendVar26.y + weightedBlendVar26.z + weightedBlendVar26.w ) );
			o.Metallic = weightedAvg26.r;
			float4 weightedBlendVar24 = i.vertexColor;
			float4 weightedAvg24 = ( ( weightedBlendVar24.x*tex2D( _Tex01_Smo, uv_TexCoord5 ) + weightedBlendVar24.y*tex2D( _Tex02_Smo, uv_TexCoord4 ) + weightedBlendVar24.z*tex2D( _Tex03_Smo, uv_TexCoord6 ) + weightedBlendVar24.w*float4( 0,0,0,0 ) )/( weightedBlendVar24.x + weightedBlendVar24.y + weightedBlendVar24.z + weightedBlendVar24.w ) );
			o.Smoothness = weightedAvg24.r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
2022;23;1266;964;1791.504;280.5269;3.052395;True;True
Node;AmplifyShaderEditor.RangedFloatNode;1;-2038.701,166.046;Float;False;InstancedProperty;_Tex03_Tile;Tex03_Tile;20;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;2;-2053.378,-234.4657;Float;False;InstancedProperty;_Tex01_Tile;Tex01_Tile;0;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-2053.377,-28.17609;Float;False;InstancedProperty;_Tex02_Tile;Tex02_Tile;10;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;4;-1819.201,-47.30309;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;5;-1824.661,-249.1308;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;6;-1805.815,145.6293;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;18;-1250.514,515.6091;Float;True;Property;_Tex03_Met;Tex03_Met;25;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;22;-1250.176,-68.33213;Float;True;Property;_Tex03_Norm;Tex03_Norm;23;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;29;-646.2083,-1072.071;Float;True;Constant;_Vector0;Vector 0;19;0;Create;True;0;0;False;0;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SamplerNode;19;-1253.129,139.9505;Float;True;Property;_Tex01_Met;Tex01_Met;5;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;17;-1243.645,-463.122;Float;True;Property;_Tex01_Norm;Tex01_Norm;3;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;35;-880.78,8.502666;Float;False;Constant;_FlatMetallic;FlatMetallic;24;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;20;-1250.515,327.9484;Float;True;Property;_Tex02_Met;Tex02_Met;15;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;7;-1262.155,-265.9749;Float;True;Property;_Tex02_Norm;Tex02_Norm;13;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;9;-1249.555,721.572;Float;True;Property;_Tex01_Smo;Tex01_Smo;6;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;31;-243.8518,-439.0225;Float;False;Property;_Use_Tex03_Norm;Use_Tex03_Norm;22;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.VertexColorNode;13;-1101.572,-1233.5;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;33;-599.8648,195.2286;Float;False;Property;_use_Tex02_Met;use_Tex02_Met;14;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;21;-1245.439,924.253;Float;True;Property;_Tex02_Smo;Tex02_Smo;16;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;30;-256.3067,-572.4673;Float;False;Property;_Use_Tex02_Norm;Use_Tex02_Norm;12;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StaticSwitch;28;-258.6431,-691.1886;Float;False;Property;_Use_Tex01_Norm;Use_Tex01_Norm;2;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;10;-1235.827,-1064.87;Float;True;Property;_Tex01_Diff;Tex01_Diff;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;8;-1231.018,-664.5434;Float;True;Property;_Tex03_Diff;Tex03_Diff;21;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;34;-591.6026,329.0764;Float;False;Property;_Use_Tex03_met;Use_Tex03_met;24;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;12;-1237.279,-856.5684;Float;True;Property;_Tex02_Diff;Tex02_Diff;11;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;32;-606.0049,46.64182;Float;False;Property;_Use_Tex01_Met;Use_Tex01_Met;4;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;14;-1245.438,1123.893;Float;True;Property;_Tex03_Smo;Tex03_Smo;26;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;41;-194.9709,1616.558;Float;False;Property;_Use_Tex01_Hei;Use_Tex01_Hei;30;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;43;-225.3612,1895.939;Float;False;Property;_Use_Tex03_Hei;Use_Tex03_Hei;32;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;47;-1276.676,2182.659;Float;True;Property;_Tex02_Hei;Tex02_Hei;29;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;46;-1281.828,1983.105;Float;True;Property;_Tex01_Hei;Tex01_Hei;9;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;45;-1294.643,2366.326;Float;True;Property;_Tex03_Hei;Tex03_Hei;18;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;11;-1243.99,1541.807;Float;True;Property;_Tex03_Occ;Tex03_Occ;28;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;15;-1249.142,1342.253;Float;True;Property;_Tex01_Occ;Tex01_Occ;8;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;16;-1261.957,1725.474;Float;True;Property;_Tex02_Occ;Tex02_Occ;19;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WeightedBlendNode;23;222.8954,-129.9869;Float;False;5;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WeightedBlendNode;27;223.2858,-295.5278;Float;False;5;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WeightedBlendNode;24;225.157,211.5094;Float;False;5;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WeightedBlendNode;25;186.625,690.7338;Float;False;5;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;38;-303.5186,1193.308;Float;False;Property;_Use_Tex02_Occ;Use_Tex02_Occ;17;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector3Node;40;-590.3633,989.8617;Float;False;Constant;_Vector1;Vector 1;27;0;Create;True;0;0;False;0;1,1,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WeightedBlendNode;26;211.5872,44.15361;Float;False;5;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;39;-289.4785,1312.229;Float;False;Property;_Use_Tex03_Occ;Use_Tex03_Occ;27;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;44;-506.2218,1570.219;Float;False;Constant;_FlatHeight;FlatHeight;30;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;36;-303.103,1080.954;Float;False;Property;_Use_Tex01_Occ;Use_Tex01_Occ;7;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;42;-182.4519,1745.758;Float;False;Property;_Use_Tex02_Hei;Use_Tex02_Hei;31;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;631.8697,-75.93298;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_Terrain;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;4;0;3;0
WireConnection;5;0;2;0
WireConnection;6;0;1;0
WireConnection;18;1;6;0
WireConnection;22;1;6;0
WireConnection;19;1;5;0
WireConnection;17;1;5;0
WireConnection;20;1;4;0
WireConnection;7;1;4;0
WireConnection;9;1;5;0
WireConnection;31;1;29;0
WireConnection;31;0;22;0
WireConnection;33;1;35;0
WireConnection;33;0;20;0
WireConnection;21;1;4;0
WireConnection;30;1;29;0
WireConnection;30;0;7;0
WireConnection;28;1;29;0
WireConnection;28;0;17;0
WireConnection;10;1;5;0
WireConnection;8;1;6;0
WireConnection;34;1;35;0
WireConnection;34;0;18;0
WireConnection;12;1;4;0
WireConnection;32;1;35;0
WireConnection;32;0;19;0
WireConnection;14;1;6;0
WireConnection;41;1;44;0
WireConnection;41;0;46;0
WireConnection;43;1;44;0
WireConnection;43;0;45;0
WireConnection;47;1;4;0
WireConnection;46;1;5;0
WireConnection;45;1;6;0
WireConnection;11;1;4;0
WireConnection;15;1;5;0
WireConnection;16;1;6;0
WireConnection;23;0;13;0
WireConnection;23;1;28;0
WireConnection;23;2;30;0
WireConnection;23;3;31;0
WireConnection;27;0;13;0
WireConnection;27;1;10;0
WireConnection;27;2;12;0
WireConnection;27;3;8;0
WireConnection;24;0;13;0
WireConnection;24;1;9;0
WireConnection;24;2;21;0
WireConnection;24;3;14;0
WireConnection;25;0;13;0
WireConnection;25;1;36;0
WireConnection;25;2;38;0
WireConnection;25;3;39;0
WireConnection;38;1;40;0
WireConnection;38;0;11;0
WireConnection;26;0;13;0
WireConnection;26;1;32;0
WireConnection;26;2;33;0
WireConnection;26;3;34;0
WireConnection;39;1;40;0
WireConnection;39;0;16;0
WireConnection;36;1;40;0
WireConnection;36;0;15;0
WireConnection;42;1;44;0
WireConnection;42;0;47;0
WireConnection;0;0;27;0
WireConnection;0;1;23;0
WireConnection;0;3;26;0
WireConnection;0;4;24;0
ASEEND*/
//CHKSM=43DB6DD76ACAABAA697AF866DCE49E843B0033A7