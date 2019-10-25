// Upgrade NOTE: upgraded instancing buffer 'S_DefaultTerrain' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_DefaultTerrain"
{
	Properties
	{
		_Tex01_Tile("Tex01_Tile", Float) = 0
		_Tex01_Diffuse("Tex01_Diffuse", 2D) = "white" {}
		_Tex01_DiffuseHue("Tex01_DiffuseHue", Range( -1 , 1)) = 0
		_Tex01_DiffuseSaturation("Tex01_DiffuseSaturation", Range( 0 , 2)) = 1
		_Tex01_DiffuseLightness("Tex01_DiffuseLightness", Range( 0 , 5)) = 1
		_Tex01_Norm("Tex01_Norm", 2D) = "white" {}
		[Toggle(_TEX01_USENORMAL_ON)] _Tex01_UseNormal("Tex01_UseNormal", Float) = 1
		_Tex01_RMOH("Tex01_RMOH", 2D) = "white" {}
		[Toggle(_TEX01_USEMETALLIC_ON)] _Tex01_UseMetallic("Tex01_UseMetallic", Float) = 1
		_Tex02_Tile("Tex02_Tile", Float) = 0
		_Tex02_Diffuse("Tex02_Diffuse", 2D) = "white" {}
		_Tex02_DiffuseHue("Tex02_DiffuseHue", Range( -1 , 1)) = 0
		_Tex02_DiffuseSaturation("Tex02_DiffuseSaturation", Range( 0 , 2)) = 1
		_Tex02_DiffuseLightness("Tex02_DiffuseLightness", Range( 0 , 5)) = 1
		_Tex02_Norm("Tex02_Norm", 2D) = "bump" {}
		[Toggle(_TEX02_USENORMAL_ON)] _Tex02_UseNormal("Tex02_UseNormal", Float) = 1
		_Tex03_RMOH("Tex03_RMOH", 2D) = "white" {}
		[Toggle(_TEX02_USEMETALLIC_ON)] _Tex02_UseMetallic("Tex02_UseMetallic", Float) = 1
		_Tex03_Tile("Tex03_Tile", Float) = 0
		_Tex03_Diffuse("Tex03_Diffuse", 2D) = "white" {}
		_Tex03_DiffuseHue("Tex03_DiffuseHue", Range( -1 , 1)) = 0
		_Tex03_DiffuseSaturation("Tex03_DiffuseSaturation", Range( 0 , 2)) = 1
		_Tex03_DiffuseLightness("Tex03_DiffuseLightness", Range( 0 , 5)) = 1
		_Tex03_Norm("Tex03_Norm", 2D) = "bump" {}
		[Toggle(_TEX03_USENORMAL_ON)] _Tex03_UseNormal("Tex03_UseNormal", Float) = 1
		_Tex02_RMOH("Tex02_RMOH", 2D) = "white" {}
		[Toggle(_TEX03_USEMETALLIC_ON)] _Tex03_UseMetallic("Tex03_UseMetallic", Float) = 1
		_Power("Power", Float) = 0
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
		#pragma shader_feature _TEX01_USENORMAL_ON
		#pragma shader_feature _TEX02_USENORMAL_ON
		#pragma shader_feature _TEX03_USENORMAL_ON
		#pragma shader_feature _TEX01_USEMETALLIC_ON
		#pragma shader_feature _TEX02_USEMETALLIC_ON
		#pragma shader_feature _TEX03_USEMETALLIC_ON
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float4 vertexColor : COLOR;
			float2 uv_texcoord;
		};

		uniform sampler2D _Tex01_Norm;
		uniform sampler2D _Tex02_Norm;
		uniform sampler2D _Tex03_Norm;
		uniform sampler2D _Tex01_Diffuse;
		uniform sampler2D _Tex02_Diffuse;
		uniform sampler2D _Tex03_Diffuse;
		uniform sampler2D _Tex01_RMOH;
		uniform sampler2D _Tex02_RMOH;
		uniform sampler2D _Tex03_RMOH;

		UNITY_INSTANCING_BUFFER_START(S_DefaultTerrain)
			UNITY_DEFINE_INSTANCED_PROP(float, _Power)
#define _Power_arr S_DefaultTerrain
			UNITY_DEFINE_INSTANCED_PROP(float, _Tex01_Tile)
#define _Tex01_Tile_arr S_DefaultTerrain
			UNITY_DEFINE_INSTANCED_PROP(float, _Tex02_Tile)
#define _Tex02_Tile_arr S_DefaultTerrain
			UNITY_DEFINE_INSTANCED_PROP(float, _Tex03_Tile)
#define _Tex03_Tile_arr S_DefaultTerrain
			UNITY_DEFINE_INSTANCED_PROP(float, _Tex01_DiffuseHue)
#define _Tex01_DiffuseHue_arr S_DefaultTerrain
			UNITY_DEFINE_INSTANCED_PROP(float, _Tex01_DiffuseSaturation)
#define _Tex01_DiffuseSaturation_arr S_DefaultTerrain
			UNITY_DEFINE_INSTANCED_PROP(float, _Tex01_DiffuseLightness)
#define _Tex01_DiffuseLightness_arr S_DefaultTerrain
			UNITY_DEFINE_INSTANCED_PROP(float, _Tex02_DiffuseHue)
#define _Tex02_DiffuseHue_arr S_DefaultTerrain
			UNITY_DEFINE_INSTANCED_PROP(float, _Tex02_DiffuseSaturation)
#define _Tex02_DiffuseSaturation_arr S_DefaultTerrain
			UNITY_DEFINE_INSTANCED_PROP(float, _Tex02_DiffuseLightness)
#define _Tex02_DiffuseLightness_arr S_DefaultTerrain
			UNITY_DEFINE_INSTANCED_PROP(float, _Tex03_DiffuseHue)
#define _Tex03_DiffuseHue_arr S_DefaultTerrain
			UNITY_DEFINE_INSTANCED_PROP(float, _Tex03_DiffuseSaturation)
#define _Tex03_DiffuseSaturation_arr S_DefaultTerrain
			UNITY_DEFINE_INSTANCED_PROP(float, _Tex03_DiffuseLightness)
#define _Tex03_DiffuseLightness_arr S_DefaultTerrain
		UNITY_INSTANCING_BUFFER_END(S_DefaultTerrain)


		float3 HSVToRGB( float3 c )
		{
			float4 K = float4( 1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0 );
			float3 p = abs( frac( c.xxx + K.xyz ) * 6.0 - K.www );
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
			float _Power_Instance = UNITY_ACCESS_INSTANCED_PROP(_Power_arr, _Power);
			float4 temp_cast_0 = (_Power_Instance).xxxx;
			float4 temp_output_77_0 = pow( i.vertexColor , temp_cast_0 );
			float3 _FlatNorm = float3(0,0,1);
			float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );
			float4 appendResult70 = (float4(ase_objectScale.x , ase_objectScale.y , 0.0 , 0.0));
			float4 temp_output_72_0 = ( float4( i.uv_texcoord, 0.0 , 0.0 ) * appendResult70 );
			float _Tex01_Tile_Instance = UNITY_ACCESS_INSTANCED_PROP(_Tex01_Tile_arr, _Tex01_Tile);
			float4 temp_output_75_0 = ( temp_output_72_0 * _Tex01_Tile_Instance );
			#ifdef _TEX01_USENORMAL_ON
				float3 staticSwitch67 = UnpackNormal( tex2D( _Tex01_Norm, temp_output_75_0.xy ) );
			#else
				float3 staticSwitch67 = _FlatNorm;
			#endif
			float _Tex02_Tile_Instance = UNITY_ACCESS_INSTANCED_PROP(_Tex02_Tile_arr, _Tex02_Tile);
			float4 temp_output_74_0 = ( temp_output_72_0 * _Tex02_Tile_Instance );
			#ifdef _TEX02_USENORMAL_ON
				float3 staticSwitch66 = UnpackNormal( tex2D( _Tex02_Norm, temp_output_74_0.xy ) );
			#else
				float3 staticSwitch66 = _FlatNorm;
			#endif
			float _Tex03_Tile_Instance = UNITY_ACCESS_INSTANCED_PROP(_Tex03_Tile_arr, _Tex03_Tile);
			float4 temp_output_76_0 = ( temp_output_72_0 * _Tex03_Tile_Instance );
			#ifdef _TEX03_USENORMAL_ON
				float3 staticSwitch65 = UnpackNormal( tex2D( _Tex03_Norm, temp_output_76_0.xy ) );
			#else
				float3 staticSwitch65 = _FlatNorm;
			#endif
			float4 weightedBlendVar68 = temp_output_77_0;
			float3 weightedAvg68 = ( ( weightedBlendVar68.x*staticSwitch67 + weightedBlendVar68.y*staticSwitch66 + weightedBlendVar68.z*staticSwitch65 + weightedBlendVar68.w*float3( 0,0,0 ) )/( weightedBlendVar68.x + weightedBlendVar68.y + weightedBlendVar68.z + weightedBlendVar68.w ) );
			o.Normal = weightedAvg68;
			float _Tex01_DiffuseHue_Instance = UNITY_ACCESS_INSTANCED_PROP(_Tex01_DiffuseHue_arr, _Tex01_DiffuseHue);
			float4 tex2DNode8 = tex2D( _Tex01_Diffuse, temp_output_75_0.xy );
			float4 appendResult12 = (float4(tex2DNode8.r , tex2DNode8.g , tex2DNode8.b , 0.0));
			float3 hsvTorgb13_g34 = RGBToHSV( appendResult12.rgb );
			float _Tex01_DiffuseSaturation_Instance = UNITY_ACCESS_INSTANCED_PROP(_Tex01_DiffuseSaturation_arr, _Tex01_DiffuseSaturation);
			float _Tex01_DiffuseLightness_Instance = UNITY_ACCESS_INSTANCED_PROP(_Tex01_DiffuseLightness_arr, _Tex01_DiffuseLightness);
			float3 hsvTorgb17_g34 = HSVToRGB( float3(( _Tex01_DiffuseHue_Instance + hsvTorgb13_g34.x ),( _Tex01_DiffuseSaturation_Instance * hsvTorgb13_g34.y ),( hsvTorgb13_g34.z * _Tex01_DiffuseLightness_Instance )) );
			float _Tex02_DiffuseHue_Instance = UNITY_ACCESS_INSTANCED_PROP(_Tex02_DiffuseHue_arr, _Tex02_DiffuseHue);
			float4 tex2DNode10 = tex2D( _Tex02_Diffuse, temp_output_74_0.xy );
			float4 appendResult18 = (float4(tex2DNode10.r , tex2DNode10.g , tex2DNode10.b , 0.0));
			float3 hsvTorgb13_g31 = RGBToHSV( appendResult18.rgb );
			float _Tex02_DiffuseSaturation_Instance = UNITY_ACCESS_INSTANCED_PROP(_Tex02_DiffuseSaturation_arr, _Tex02_DiffuseSaturation);
			float _Tex02_DiffuseLightness_Instance = UNITY_ACCESS_INSTANCED_PROP(_Tex02_DiffuseLightness_arr, _Tex02_DiffuseLightness);
			float3 hsvTorgb17_g31 = HSVToRGB( float3(( _Tex02_DiffuseHue_Instance + hsvTorgb13_g31.x ),( _Tex02_DiffuseSaturation_Instance * hsvTorgb13_g31.y ),( hsvTorgb13_g31.z * _Tex02_DiffuseLightness_Instance )) );
			float _Tex03_DiffuseHue_Instance = UNITY_ACCESS_INSTANCED_PROP(_Tex03_DiffuseHue_arr, _Tex03_DiffuseHue);
			float4 tex2DNode9 = tex2D( _Tex03_Diffuse, temp_output_76_0.xy );
			float4 appendResult6 = (float4(tex2DNode9.r , tex2DNode9.g , tex2DNode9.b , 0.0));
			float3 hsvTorgb13_g33 = RGBToHSV( appendResult6.rgb );
			float _Tex03_DiffuseSaturation_Instance = UNITY_ACCESS_INSTANCED_PROP(_Tex03_DiffuseSaturation_arr, _Tex03_DiffuseSaturation);
			float _Tex03_DiffuseLightness_Instance = UNITY_ACCESS_INSTANCED_PROP(_Tex03_DiffuseLightness_arr, _Tex03_DiffuseLightness);
			float3 hsvTorgb17_g33 = HSVToRGB( float3(( _Tex03_DiffuseHue_Instance + hsvTorgb13_g33.x ),( _Tex03_DiffuseSaturation_Instance * hsvTorgb13_g33.y ),( hsvTorgb13_g33.z * _Tex03_DiffuseLightness_Instance )) );
			float4 weightedBlendVar23 = temp_output_77_0;
			float3 weightedAvg23 = ( ( weightedBlendVar23.x*hsvTorgb17_g34 + weightedBlendVar23.y*hsvTorgb17_g31 + weightedBlendVar23.z*hsvTorgb17_g33 + weightedBlendVar23.w*float3( 0,0,0 ) )/( weightedBlendVar23.x + weightedBlendVar23.y + weightedBlendVar23.z + weightedBlendVar23.w ) );
			o.Albedo = weightedAvg23;
			float4 tex2DNode47 = tex2D( _Tex01_RMOH, temp_output_75_0.xy );
			#ifdef _TEX01_USEMETALLIC_ON
				float staticSwitch52 = tex2DNode47.g;
			#else
				float staticSwitch52 = 0.0;
			#endif
			float4 tex2DNode43 = tex2D( _Tex02_RMOH, temp_output_74_0.xy );
			#ifdef _TEX02_USEMETALLIC_ON
				float staticSwitch50 = tex2DNode43.g;
			#else
				float staticSwitch50 = 0.0;
			#endif
			float4 tex2DNode48 = tex2D( _Tex03_RMOH, temp_output_76_0.xy );
			#ifdef _TEX03_USEMETALLIC_ON
				float staticSwitch51 = tex2DNode48.g;
			#else
				float staticSwitch51 = 0.0;
			#endif
			float4 weightedBlendVar55 = temp_output_77_0;
			float weightedAvg55 = ( ( weightedBlendVar55.x*staticSwitch52 + weightedBlendVar55.y*staticSwitch50 + weightedBlendVar55.z*staticSwitch51 + weightedBlendVar55.w*0.0 )/( weightedBlendVar55.x + weightedBlendVar55.y + weightedBlendVar55.z + weightedBlendVar55.w ) );
			o.Metallic = weightedAvg55;
			float4 weightedBlendVar56 = temp_output_77_0;
			float weightedAvg56 = ( ( weightedBlendVar56.x*tex2DNode47.r + weightedBlendVar56.y*tex2DNode43.r + weightedBlendVar56.z*tex2DNode48.r + weightedBlendVar56.w*0.0 )/( weightedBlendVar56.x + weightedBlendVar56.y + weightedBlendVar56.z + weightedBlendVar56.w ) );
			o.Smoothness = weightedAvg56;
			float4 weightedBlendVar57 = temp_output_77_0;
			float weightedAvg57 = ( ( weightedBlendVar57.x*tex2DNode47.b + weightedBlendVar57.y*tex2DNode43.b + weightedBlendVar57.z*tex2DNode48.b + weightedBlendVar57.w*0.0 )/( weightedBlendVar57.x + weightedBlendVar57.y + weightedBlendVar57.z + weightedBlendVar57.w ) );
			o.Occlusion = weightedAvg57;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1927;7;1266;958;1828.435;1096.156;1.757345;True;False
Node;AmplifyShaderEditor.ObjectScaleNode;69;-4068.419,356.5;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DynamicAppendNode;70;-3887.623,357.595;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;71;-3957.273,236.8552;Float;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;31;-3738.285,414.2709;Float;False;InstancedProperty;_Tex03_Tile;Tex03_Tile;18;0;Create;True;0;0;False;0;0;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;30;-3787.702,-88.62228;Float;False;InstancedProperty;_Tex01_Tile;Tex01_Tile;0;0;Create;True;0;0;False;0;0;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;27;-3754.26,100.7399;Float;False;InstancedProperty;_Tex02_Tile;Tex02_Tile;9;0;Create;True;0;0;False;0;0;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;72;-3709.273,236.8552;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;75;-3535.655,71.05185;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;76;-3553.408,438.788;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;74;-3549.054,237.9791;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.CommentaryNode;2;-2014.476,312.6599;Float;False;891.1621;486.1738;Tex02_Diffuse;6;20;9;7;6;5;4;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;1;-2014.476,-183.3404;Float;False;891.1621;486.1738;Tex02_Diffuse;6;21;18;14;13;11;10;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;3;-2014.476,-679.3417;Float;False;891.1621;486.1738;Tex01_Diffuse;6;22;19;17;16;12;8;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;9;-1998.476,360.66;Float;True;Property;_Tex03_Diffuse;Tex03_Diffuse;19;0;Create;True;0;0;False;0;None;992e974e79fb4cf4ca75cd5f54d38467;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;58;-1912.739,1668.498;Float;False;861.1868;345.227;Tex03_Normal;2;65;61;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;59;-1905.413,821.5745;Float;False;881.678;477.488;Tex01_Normal;3;67;64;63;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;10;-1998.476,-135.3403;Float;True;Property;_Tex02_Diffuse;Tex02_Diffuse;10;0;Create;True;0;0;False;0;None;c44c56dd0e8a31c45b84d54e40408099;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;60;-1903.913,1318.059;Float;False;857.4611;332.1872;Tex02_Normal;2;66;62;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;38;-1974.367,2050.26;Float;False;900.0681;236.4732;Tex01_Metallic;2;52;40;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;8;-1998.477,-634.5692;Float;True;Property;_Tex01_Diffuse;Tex01_Diffuse;1;0;Create;True;0;0;False;0;None;2e2992173cd3b3e40a9b4389684c4ad5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;61;-1885.876,1729.229;Float;True;Property;_Tex03_Norm;Tex03_Norm;23;0;Create;True;0;0;False;0;None;029eb7217a718674ba1f8f2f35f5525e;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;11;-1982.476,216.6599;Float;False;InstancedProperty;_Tex02_DiffuseLightness;Tex02_DiffuseLightness;13;0;Create;True;0;0;False;0;1;2;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-1982.476,712.6597;Float;False;InstancedProperty;_Tex03_DiffuseLightness;Tex03_DiffuseLightness;22;0;Create;True;0;0;False;0;1;2;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-1982.476,-359.3407;Float;False;InstancedProperty;_Tex01_DiffuseSaturation;Tex01_DiffuseSaturation;3;0;Create;True;0;0;False;0;1;0.95;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;78;-1204.685,-993.3214;Float;False;InstancedProperty;_Power;Power;27;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;15;-1278.476,-871.3414;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;48;-2513.512,3080.239;Float;True;Property;_Tex03_RMOH;Tex03_RMOH;16;0;Create;True;0;0;False;0;None;e9003c114484aa64b9dd2ca7796d0f99;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;40;-1830.04,2095.26;Float;False;Constant;_NoMetallic;NoMetallic;11;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;63;-1880.413,1020.154;Float;True;Property;_Tex01_Norm;Tex01_Norm;5;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;64;-1759.032,870.5748;Float;False;Constant;_FlatNorm;FlatNorm;3;0;Create;True;0;0;False;0;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;7;-1982.476,648.6596;Float;False;InstancedProperty;_Tex03_DiffuseSaturation;Tex03_DiffuseSaturation;21;0;Create;True;0;0;False;0;1;1;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-1982.476,-279.3406;Float;False;InstancedProperty;_Tex01_DiffuseLightness;Tex01_DiffuseLightness;4;0;Create;True;0;0;False;0;1;2;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-1982.476,136.6599;Float;False;InstancedProperty;_Tex02_DiffuseSaturation;Tex02_DiffuseSaturation;12;0;Create;True;0;0;False;0;1;1;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;36;-1970.742,2861.829;Float;False;884.8632;152.8463;Tex03_Metallic;1;51;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;37;-1970.228,2503.173;Float;False;895.2662;156.4424;Tex02_Metallic;1;50;;1,1,1,1;0;0
Node;AmplifyShaderEditor.DynamicAppendNode;18;-1678.476,-103.3403;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SamplerNode;62;-1884.501,1373.201;Float;True;Property;_Tex02_Norm;Tex02_Norm;14;0;Create;True;0;0;False;0;None;870d56afd8d447e4e83ca254de36060f;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;43;-2516.799,2645.656;Float;True;Property;_Tex02_RMOH;Tex02_RMOH;25;0;Create;True;0;0;False;0;None;dd1842009a6d7b44a80ee378d3ecf769;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;5;-1982.476,568.6598;Float;False;InstancedProperty;_Tex03_DiffuseHue;Tex03_DiffuseHue;20;0;Create;True;0;0;False;0;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;6;-1678.476,392.6599;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-1982.476,72.65989;Float;False;InstancedProperty;_Tex02_DiffuseHue;Tex02_DiffuseHue;11;0;Create;True;0;0;False;0;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;47;-2520.938,2192.743;Float;True;Property;_Tex01_RMOH;Tex01_RMOH;7;0;Create;True;0;0;False;0;None;d76de142c000f4c40a04e966c0827587;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;12;-1678.476,-599.3417;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-1998.476,-423.3409;Float;False;InstancedProperty;_Tex01_DiffuseHue;Tex01_DiffuseHue;2;0;Create;True;0;0;False;0;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;65;-1554.576,1712.883;Float;False;Property;_Tex03_UseNormal;Tex03_UseNormal;24;0;Create;True;0;0;False;0;0;1;1;True;;Toggle;2;Key0;Key1;Create;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StaticSwitch;66;-1527.592,1354.679;Float;False;Property;_Tex02_UseNormal;Tex02_UseNormal;15;0;Create;True;0;0;False;0;0;1;1;True;;Toggle;2;Key0;Key1;Create;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;22;-1438.476,-631.3417;Float;False;SF_ColorShift;-1;;34;f3651b3e85772604cb45f632c7f608e7;0;4;26;FLOAT;0;False;27;FLOAT;0;False;28;FLOAT;0;False;23;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StaticSwitch;52;-1368.13,2097.018;Float;False;Property;_Tex01_UseMetallic;Tex01_UseMetallic;8;0;Create;True;0;0;False;0;0;1;0;True;;Toggle;2;Key0;Key1;Create;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;20;-1438.476,376.66;Float;False;SF_ColorShift;-1;;33;f3651b3e85772604cb45f632c7f608e7;0;4;26;FLOAT;0;False;27;FLOAT;0;False;28;FLOAT;0;False;23;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StaticSwitch;51;-1385.311,2906.985;Float;False;Property;_Tex03_UseMetallic;Tex03_UseMetallic;26;0;Create;True;0;0;False;0;0;1;0;True;;Toggle;2;Key0;Key1;Create;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;50;-1337.931,2549.93;Float;False;Property;_Tex02_UseMetallic;Tex02_UseMetallic;17;0;Create;True;0;0;False;0;0;1;0;True;;Toggle;2;Key0;Key1;Create;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;21;-1438.476,-135.3403;Float;False;SF_ColorShift;-1;;31;f3651b3e85772604cb45f632c7f608e7;0;4;26;FLOAT;0;False;27;FLOAT;0;False;28;FLOAT;0;False;23;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StaticSwitch;67;-1494.058,875.2911;Float;False;Property;_Tex01_UseNormal;Tex01_UseNormal;6;0;Create;True;0;0;False;0;0;1;0;True;;Toggle;2;Key0;Key1;Create;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.PowerNode;77;-929.1129,-830.391;Float;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.WeightedBlendNode;56;-486.7496,938.6987;Float;False;5;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WeightedBlendNode;23;-459.7853,-70.00697;Float;False;5;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WeightedBlendNode;57;-459.2936,1175.492;Float;False;5;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WeightedBlendNode;68;-471.691,153.6572;Float;False;5;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WeightedBlendNode;55;-493.1367,701.2137;Float;False;5;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;115.2722,1124.259;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_DefaultTerrain;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;70;0;69;1
WireConnection;70;1;69;2
WireConnection;72;0;71;0
WireConnection;72;1;70;0
WireConnection;75;0;72;0
WireConnection;75;1;30;0
WireConnection;76;0;72;0
WireConnection;76;1;31;0
WireConnection;74;0;72;0
WireConnection;74;1;27;0
WireConnection;9;1;76;0
WireConnection;10;1;74;0
WireConnection;8;1;75;0
WireConnection;61;1;76;0
WireConnection;48;1;76;0
WireConnection;63;1;75;0
WireConnection;18;0;10;1
WireConnection;18;1;10;2
WireConnection;18;2;10;3
WireConnection;62;1;74;0
WireConnection;43;1;74;0
WireConnection;6;0;9;1
WireConnection;6;1;9;2
WireConnection;6;2;9;3
WireConnection;47;1;75;0
WireConnection;12;0;8;1
WireConnection;12;1;8;2
WireConnection;12;2;8;3
WireConnection;65;1;64;0
WireConnection;65;0;61;0
WireConnection;66;1;64;0
WireConnection;66;0;62;0
WireConnection;22;26;19;0
WireConnection;22;27;17;0
WireConnection;22;28;16;0
WireConnection;22;23;12;0
WireConnection;52;1;40;0
WireConnection;52;0;47;2
WireConnection;20;26;5;0
WireConnection;20;27;7;0
WireConnection;20;28;4;0
WireConnection;20;23;6;0
WireConnection;51;1;40;0
WireConnection;51;0;48;2
WireConnection;50;1;40;0
WireConnection;50;0;43;2
WireConnection;21;26;13;0
WireConnection;21;27;14;0
WireConnection;21;28;11;0
WireConnection;21;23;18;0
WireConnection;67;1;64;0
WireConnection;67;0;63;0
WireConnection;77;0;15;0
WireConnection;77;1;78;0
WireConnection;56;0;77;0
WireConnection;56;1;47;1
WireConnection;56;2;43;1
WireConnection;56;3;48;1
WireConnection;23;0;77;0
WireConnection;23;1;22;0
WireConnection;23;2;21;0
WireConnection;23;3;20;0
WireConnection;57;0;77;0
WireConnection;57;1;47;3
WireConnection;57;2;43;3
WireConnection;57;3;48;3
WireConnection;68;0;77;0
WireConnection;68;1;67;0
WireConnection;68;2;66;0
WireConnection;68;3;65;0
WireConnection;55;0;77;0
WireConnection;55;1;52;0
WireConnection;55;2;50;0
WireConnection;55;3;51;0
WireConnection;0;0;23;0
WireConnection;0;1;68;0
WireConnection;0;3;55;0
WireConnection;0;4;56;0
WireConnection;0;5;57;0
ASEEND*/
//CHKSM=3C1FE140F95F0EFAD41EBD9D497594A907B8C201