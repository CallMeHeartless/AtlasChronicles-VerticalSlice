// Upgrade NOTE: upgraded instancing buffer 'S_Master' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_Master"
{
	Properties
	{
		_Diffuse("Diffuse", 2D) = "white" {}
		_DiffuseTint("DiffuseTint", Vector) = (0,0,0,0)
		_Material("Material", 2D) = "white" {}
		_Normal("Normal", 2D) = "white" {}
		_NormalIntensity("NormalIntensity", Float) = 0
		_TextureTiling("Texture Tiling", Float) = 1
		_TextureOffset("Texture Offset", Vector) = (0,0,0,0)
		[Toggle(_USESMOOTHNESS_ON)] _UseSmoothness("UseSmoothness", Float) = 0
		[Toggle(_EMMISSIVEON_ON)] _EmmissiveOn("EmmissiveOn", Float) = 0
		_EmissiveTint("EmissiveTint", Vector) = (0,0,0,0)
		_EmissiveIntensity("EmissiveIntensity", Float) = 0
		[Toggle(_USEDETAILNORMAL_ON)] _UseDetailNormal("UseDetailNormal", Float) = 0
		_DetailNormalTiling("DetailNormalTiling", Float) = 1
		_DetailNormalOffset("DetailNormalOffset", Vector) = (0,0,0,0)
		_DetailNormalIntensity("DetailNormalIntensity", Float) = 0
		_SmoothnessIntensity("SmoothnessIntensity", Float) = 0
		_SmoothnessContrast("SmoothnessContrast", Float) = 0
		_SmoothnessLow("SmoothnessLow", Float) = 0
		_SmoothnessHigh("SmoothnessHigh", Float) = 0
		_MetallicIntensity("MetallicIntensity", Float) = 0
		_MetallicContrast("MetallicContrast", Float) = 0
		_MetallicLow("MetallicLow", Float) = 0
		_MetallicHigh("MetallicHigh", Float) = 0
		_Emissive("Emissive", 2D) = "white" {}
		_NormalDetail("NormalDetail", 2D) = "white" {}
		[Toggle(_OPENGLON_ON)] _OpenGLON("OpenGLON", Float) = 0
		[Toggle(_FLATEMISSIVE_ON)] _FlatEmissive("FlatEmissive", Float) = 0
		_FlatEmissiveColor("FlatEmissiveColor", Vector) = (0,0,0,0)
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
		#pragma shader_feature _OPENGLON_ON
		#pragma shader_feature _USEDETAILNORMAL_ON
		#pragma shader_feature _EMMISSIVEON_ON
		#pragma shader_feature _FLATEMISSIVE_ON
		#pragma shader_feature _USESMOOTHNESS_ON
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Normal;
		uniform sampler2D _NormalDetail;
		uniform sampler2D _Diffuse;
		uniform sampler2D _Emissive;
		uniform sampler2D _Material;

		UNITY_INSTANCING_BUFFER_START(S_Master)
			UNITY_DEFINE_INSTANCED_PROP(float3, _FlatEmissiveColor)
#define _FlatEmissiveColor_arr S_Master
			UNITY_DEFINE_INSTANCED_PROP(float3, _DiffuseTint)
#define _DiffuseTint_arr S_Master
			UNITY_DEFINE_INSTANCED_PROP(float3, _EmissiveTint)
#define _EmissiveTint_arr S_Master
			UNITY_DEFINE_INSTANCED_PROP(float2, _TextureOffset)
#define _TextureOffset_arr S_Master
			UNITY_DEFINE_INSTANCED_PROP(float2, _DetailNormalOffset)
#define _DetailNormalOffset_arr S_Master
			UNITY_DEFINE_INSTANCED_PROP(float, _SmoothnessHigh)
#define _SmoothnessHigh_arr S_Master
			UNITY_DEFINE_INSTANCED_PROP(float, _SmoothnessLow)
#define _SmoothnessLow_arr S_Master
			UNITY_DEFINE_INSTANCED_PROP(float, _MetallicIntensity)
#define _MetallicIntensity_arr S_Master
			UNITY_DEFINE_INSTANCED_PROP(float, _MetallicContrast)
#define _MetallicContrast_arr S_Master
			UNITY_DEFINE_INSTANCED_PROP(float, _MetallicHigh)
#define _MetallicHigh_arr S_Master
			UNITY_DEFINE_INSTANCED_PROP(float, _TextureTiling)
#define _TextureTiling_arr S_Master
			UNITY_DEFINE_INSTANCED_PROP(float, _EmissiveIntensity)
#define _EmissiveIntensity_arr S_Master
			UNITY_DEFINE_INSTANCED_PROP(float, _SmoothnessContrast)
#define _SmoothnessContrast_arr S_Master
			UNITY_DEFINE_INSTANCED_PROP(float, _DetailNormalIntensity)
#define _DetailNormalIntensity_arr S_Master
			UNITY_DEFINE_INSTANCED_PROP(float, _DetailNormalTiling)
#define _DetailNormalTiling_arr S_Master
			UNITY_DEFINE_INSTANCED_PROP(float, _NormalIntensity)
#define _NormalIntensity_arr S_Master
			UNITY_DEFINE_INSTANCED_PROP(float, _MetallicLow)
#define _MetallicLow_arr S_Master
			UNITY_DEFINE_INSTANCED_PROP(float, _SmoothnessIntensity)
#define _SmoothnessIntensity_arr S_Master
		UNITY_INSTANCING_BUFFER_END(S_Master)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 _NormalColor = float3(0,0,1);
			float _TextureTiling_Instance = UNITY_ACCESS_INSTANCED_PROP(_TextureTiling_arr, _TextureTiling);
			float2 temp_cast_1 = (_TextureTiling_Instance).xx;
			float2 _TextureOffset_Instance = UNITY_ACCESS_INSTANCED_PROP(_TextureOffset_arr, _TextureOffset);
			float2 uv_TexCoord6 = i.uv_texcoord * temp_cast_1 + _TextureOffset_Instance;
			float _NormalIntensity_Instance = UNITY_ACCESS_INSTANCED_PROP(_NormalIntensity_arr, _NormalIntensity);
			float4 lerpResult48 = lerp( float4( _NormalColor , 0.0 ) , tex2D( _Normal, uv_TexCoord6 ) , _NormalIntensity_Instance);
			float _DetailNormalTiling_Instance = UNITY_ACCESS_INSTANCED_PROP(_DetailNormalTiling_arr, _DetailNormalTiling);
			float2 temp_cast_3 = (_DetailNormalTiling_Instance).xx;
			float2 _DetailNormalOffset_Instance = UNITY_ACCESS_INSTANCED_PROP(_DetailNormalOffset_arr, _DetailNormalOffset);
			float2 uv_TexCoord54 = i.uv_texcoord * temp_cast_3 + _DetailNormalOffset_Instance;
			float _DetailNormalIntensity_Instance = UNITY_ACCESS_INSTANCED_PROP(_DetailNormalIntensity_arr, _DetailNormalIntensity);
			float4 lerpResult51 = lerp( float4( _NormalColor , 0.0 ) , tex2D( _NormalDetail, uv_TexCoord54 ) , _DetailNormalIntensity_Instance);
			#ifdef _USEDETAILNORMAL_ON
				float4 staticSwitch58 = ( lerpResult48 + lerpResult51 );
			#else
				float4 staticSwitch58 = lerpResult48;
			#endif
			float4 break64 = staticSwitch58;
			float4 appendResult67 = (float4(break64.r , ( 1.0 - break64.g ) , break64.b , 0.0));
			#ifdef _OPENGLON_ON
				float4 staticSwitch63 = staticSwitch58;
			#else
				float4 staticSwitch63 = appendResult67;
			#endif
			o.Normal = staticSwitch63.xyz;
			float3 _DiffuseTint_Instance = UNITY_ACCESS_INSTANCED_PROP(_DiffuseTint_arr, _DiffuseTint);
			o.Albedo = ( float4( _DiffuseTint_Instance , 0.0 ) * tex2D( _Diffuse, uv_TexCoord6 ) ).rgb;
			float3 _EmissiveTint_Instance = UNITY_ACCESS_INSTANCED_PROP(_EmissiveTint_arr, _EmissiveTint);
			float3 _FlatEmissiveColor_Instance = UNITY_ACCESS_INSTANCED_PROP(_FlatEmissiveColor_arr, _FlatEmissiveColor);
			#ifdef _FLATEMISSIVE_ON
				float4 staticSwitch69 = float4( _FlatEmissiveColor_Instance , 0.0 );
			#else
				float4 staticSwitch69 = tex2D( _Emissive, uv_TexCoord6 );
			#endif
			#ifdef _EMMISSIVEON_ON
				float4 staticSwitch39 = staticSwitch69;
			#else
				float4 staticSwitch39 = float4( float3(0,0,0) , 0.0 );
			#endif
			float _EmissiveIntensity_Instance = UNITY_ACCESS_INSTANCED_PROP(_EmissiveIntensity_arr, _EmissiveIntensity);
			o.Emission = ( ( float4( _EmissiveTint_Instance , 0.0 ) * staticSwitch39 ) * _EmissiveIntensity_Instance ).rgb;
			float _MetallicLow_Instance = UNITY_ACCESS_INSTANCED_PROP(_MetallicLow_arr, _MetallicLow);
			float _MetallicHigh_Instance = UNITY_ACCESS_INSTANCED_PROP(_MetallicHigh_arr, _MetallicHigh);
			float4 tex2DNode4 = tex2D( _Material, uv_TexCoord6 );
			float _MetallicContrast_Instance = UNITY_ACCESS_INSTANCED_PROP(_MetallicContrast_arr, _MetallicContrast);
			float lerpResult32 = lerp( _MetallicLow_Instance , _MetallicHigh_Instance , pow( tex2DNode4.g , _MetallicContrast_Instance ));
			float _MetallicIntensity_Instance = UNITY_ACCESS_INSTANCED_PROP(_MetallicIntensity_arr, _MetallicIntensity);
			o.Metallic = ( lerpResult32 * _MetallicIntensity_Instance );
			float _SmoothnessLow_Instance = UNITY_ACCESS_INSTANCED_PROP(_SmoothnessLow_arr, _SmoothnessLow);
			float _SmoothnessHigh_Instance = UNITY_ACCESS_INSTANCED_PROP(_SmoothnessHigh_arr, _SmoothnessHigh);
			float _SmoothnessContrast_Instance = UNITY_ACCESS_INSTANCED_PROP(_SmoothnessContrast_arr, _SmoothnessContrast);
			float lerpResult16 = lerp( _SmoothnessLow_Instance , _SmoothnessHigh_Instance , pow( tex2DNode4.r , _SmoothnessContrast_Instance ));
			float _SmoothnessIntensity_Instance = UNITY_ACCESS_INSTANCED_PROP(_SmoothnessIntensity_arr, _SmoothnessIntensity);
			float temp_output_21_0 = ( lerpResult16 * _SmoothnessIntensity_Instance );
			#ifdef _USESMOOTHNESS_ON
				float staticSwitch9 = temp_output_21_0;
			#else
				float staticSwitch9 = ( 1.0 - temp_output_21_0 );
			#endif
			o.Smoothness = staticSwitch9;
			o.Occlusion = tex2DNode4.g;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1921;1;1278;970;1203.614;92.92377;1.579446;True;True
Node;AmplifyShaderEditor.CommentaryNode;25;-2037.424,-121.1287;Float;False;430.4119;265.9822;Texture Tiling amd Offset Control;3;11;7;6;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;60;-1644.57,-569.9572;Float;False;550.1182;303.9451;Detail Normal Tiling and Offset;3;54;55;56;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;11;-2014.261,13.54128;Float;False;InstancedProperty;_TextureOffset;Texture Offset;6;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;7;-2019.274,-70.31213;Float;False;InstancedProperty;_TextureTiling;Texture Tiling;5;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;55;-1595.604,-512.1653;Float;False;InstancedProperty;_DetailNormalTiling;DetailNormalTiling;12;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;56;-1592.157,-427.0119;Float;False;InstancedProperty;_DetailNormalOffset;DetailNormalOffset;13;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;6;-1836.137,-71.12872;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;26;-1488.287,-149.1239;Float;False;367.2808;936.3002;Input Maps;4;2;4;5;38;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;59;-1037.058,-727.0199;Float;False;1088.682;643.0764;Detail Normal Control;8;46;51;53;50;58;57;48;49;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;54;-1354.452,-519.9571;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;57;-942.2435,-198.943;Float;False;InstancedProperty;_DetailNormalIntensity;DetailNormalIntensity;14;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;46;-919.5624,-677.0199;Float;False;Constant;_NormalColor;NormalColor;19;0;Create;True;0;0;False;0;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;49;-638.8983,-265.6702;Float;False;InstancedProperty;_NormalIntensity;NormalIntensity;4;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;5;-1449.014,108.5991;Float;True;Property;_Normal;Normal;3;0;Create;True;0;0;False;0;None;51112485e0223d2439c1ac53c6c07291;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;53;-987.058,-405.1754;Float;True;Property;_NormalDetail;NormalDetail;24;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;48;-382.0471,-397.6717;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;51;-669.155,-515.1049;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;28;-1115.189,697.4802;Float;False;839.1052;427.9286;Smoothness Tweaks;7;22;21;17;20;16;15;14;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleAddOpNode;50;-432.7911,-663.9949;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-1095.075,972.9868;Float;False;InstancedProperty;_SmoothnessContrast;SmoothnessContrast;16;0;Create;True;0;0;False;0;0;1.14;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;4;-1449.567,329.3212;Float;True;Property;_Material;Material;2;0;Create;True;0;0;False;0;None;083cd0aa0cfb1944ea8776f791515b68;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;45;-1040.613,281.1901;Float;False;998.8641;397.4269;Emissive Control;6;44;43;41;42;40;39;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-854.8688,823.0663;Float;False;InstancedProperty;_SmoothnessHigh;SmoothnessHigh;18;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;70;-1378.506,756.6566;Float;False;InstancedProperty;_FlatEmissiveColor;FlatEmissiveColor;27;0;Create;True;0;0;False;0;0,0,0;3.18,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.PowerNode;14;-830.5524,911.1902;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;58;-270.2568,-674.8175;Float;False;Property;_UseDetailNormal;UseDetailNormal;11;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;38;-1444.739,548.7115;Float;True;Property;_Emissive;Emissive;23;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;37;-1125.889,1139.661;Float;False;858.3248;426.2928;Metallic Tweaks;7;36;31;33;34;35;32;30;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-845.4297,747.4802;Float;False;InstancedProperty;_SmoothnessLow;SmoothnessLow;17;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-1097.995,1415.168;Float;False;InstancedProperty;_MetallicContrast;MetallicContrast;20;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;64;105.793,-699.5907;Float;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.Vector3Node;40;-1004.084,385.1587;Float;False;Constant;_NoEmissive;NoEmissive;17;0;Create;True;0;0;False;0;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.LerpOp;16;-604.1757,865.2474;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-721.8947,1025.352;Float;False;InstancedProperty;_SmoothnessIntensity;SmoothnessIntensity;15;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;69;-1060.628,577.9261;Float;False;Property;_FlatEmissive;FlatEmissive;26;0;Create;True;0;0;False;0;0;0;1;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;34;-848.3482,1189.661;Float;False;InstancedProperty;_MetallicLow;MetallicLow;21;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;30;-833.4709,1353.371;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;68;383.1409,-659.2642;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;27;-1021.612,-38.55283;Float;False;418.3249;270.9868;Diffuse Tint;2;12;13;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;33;-857.7873,1268.01;Float;False;InstancedProperty;_MetallicHigh;MetallicHigh;22;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;29;-252.1472,810.9182;Float;False;474.2031;231.5701;Smoothness Roughness Toggle;2;8;9;;1,1,1,1;0;0
Node;AmplifyShaderEditor.StaticSwitch;39;-784.5597,501.8294;Float;False;Property;_EmmissiveOn;EmmissiveOn;8;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-417.066,864.7729;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;42;-614.2899,331.1901;Float;False;InstancedProperty;_EmissiveTint;EmissiveTint;9;0;Create;True;0;0;False;0;0,0,0;1,1,1;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector3Node;13;-971.6121,11.44713;Float;False;InstancedProperty;_DiffuseTint;DiffuseTint;1;0;Create;True;0;0;False;0;0,0,0;1,1,1;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.OneMinusNode;8;-209.6186,860.9183;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;-405.0463,484.9206;Float;False;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;67;571.9918,-700.5263;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.LerpOp;32;-607.0942,1307.428;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;36;-724.8132,1467.533;Float;False;InstancedProperty;_MetallicIntensity;MetallicIntensity;19;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-1443.982,-99.12395;Float;True;Property;_Diffuse;Diffuse;0;0;Create;True;0;0;False;0;None;2b7d5eefb8614dd4ab5a42958ca78c3c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;44;-415.7222,595.9473;Float;False;InstancedProperty;_EmissiveIntensity;EmissiveIntensity;10;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;43;-210.7488,474.2447;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-772.2872,99.43391;Float;False;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;9;-26.52974,930.6384;Float;False;Property;_UseSmoothness;UseSmoothness;7;0;Create;True;0;0;False;0;0;0;1;True;;Toggle;2;Key0;Key1;Create;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;63;714.492,-292.7686;Float;False;Property;_OpenGLON;OpenGLON;25;0;Create;True;0;0;False;0;0;0;1;True;;Toggle;2;Key0;Key1;Create;9;1;FLOAT4;0,0,0,0;False;0;FLOAT4;0,0,0,0;False;2;FLOAT4;0,0,0,0;False;3;FLOAT4;0,0,0,0;False;4;FLOAT4;0,0,0,0;False;5;FLOAT4;0,0,0,0;False;6;FLOAT4;0,0,0,0;False;7;FLOAT4;0,0,0,0;False;8;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;-419.9846,1306.954;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;999.7756,49.14986;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_Master;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;6;0;7;0
WireConnection;6;1;11;0
WireConnection;54;0;55;0
WireConnection;54;1;56;0
WireConnection;5;1;6;0
WireConnection;53;1;54;0
WireConnection;48;0;46;0
WireConnection;48;1;5;0
WireConnection;48;2;49;0
WireConnection;51;0;46;0
WireConnection;51;1;53;0
WireConnection;51;2;57;0
WireConnection;50;0;48;0
WireConnection;50;1;51;0
WireConnection;4;1;6;0
WireConnection;14;0;4;1
WireConnection;14;1;15;0
WireConnection;58;1;48;0
WireConnection;58;0;50;0
WireConnection;38;1;6;0
WireConnection;64;0;58;0
WireConnection;16;0;17;0
WireConnection;16;1;20;0
WireConnection;16;2;14;0
WireConnection;69;1;38;0
WireConnection;69;0;70;0
WireConnection;30;0;4;2
WireConnection;30;1;31;0
WireConnection;68;0;64;1
WireConnection;39;1;40;0
WireConnection;39;0;69;0
WireConnection;21;0;16;0
WireConnection;21;1;22;0
WireConnection;8;0;21;0
WireConnection;41;0;42;0
WireConnection;41;1;39;0
WireConnection;67;0;64;0
WireConnection;67;1;68;0
WireConnection;67;2;64;2
WireConnection;32;0;34;0
WireConnection;32;1;33;0
WireConnection;32;2;30;0
WireConnection;2;1;6;0
WireConnection;43;0;41;0
WireConnection;43;1;44;0
WireConnection;12;0;13;0
WireConnection;12;1;2;0
WireConnection;9;1;8;0
WireConnection;9;0;21;0
WireConnection;63;1;67;0
WireConnection;63;0;58;0
WireConnection;35;0;32;0
WireConnection;35;1;36;0
WireConnection;0;0;12;0
WireConnection;0;1;63;0
WireConnection;0;2;43;0
WireConnection;0;3;35;0
WireConnection;0;4;9;0
WireConnection;0;5;4;2
ASEEND*/
//CHKSM=17D95FB0E76A352F70004D5B0A1310AEE60E3B15