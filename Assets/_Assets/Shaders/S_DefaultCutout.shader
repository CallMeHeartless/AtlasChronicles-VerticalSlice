// Upgrade NOTE: upgraded instancing buffer 'S_DefaultCutout' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_DefaultCutout"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Diffuse("Diffuse", 2D) = "white" {}
		_DiffuseHue("DiffuseHue", Range( -1 , 1)) = 0
		_DiffuseSaturation("DiffuseSaturation", Range( 0 , 2)) = 1
		_DiffuseLightness("DiffuseLightness", Range( 0 , 5)) = 1
		_Normal("Normal", 2D) = "bump" {}
		[Toggle(_USENORMAL_ON)] _UseNormal("UseNormal", Float) = 1
		_NormalIntensity("NormalIntensity", Range( 0 , 1)) = 1
		_DetailNormal("DetailNormal", 2D) = "white" {}
		[Toggle(_USEDETAILNORMAL_ON)] _UseDetailNormal("UseDetailNormal", Float) = 0
		_DetailNormalIntensity("DetailNormalIntensity", Range( 0 , 1)) = 1
		_Emissive("Emissive", 2D) = "white" {}
		[Toggle(_USEEMISSIVE_ON)] _UseEmissive("UseEmissive", Float) = 0
		_EmissiveIntensity("EmissiveIntensity", Range( 0 , 1)) = 1
		_FlatEmissive("FlatEmissive", Color) = (0,0,0,0)
		[Toggle(_USEFLATEMISSIVE_ON)] _UseFlatEmissive("UseFlatEmissive", Float) = 0
		[Toggle(_USEMETALLIC_ON)] _UseMetallic("UseMetallic", Float) = 1
		_FlatMetallic("FlatMetallic", Range( 0 , 1)) = 0
		[Toggle(_USEFLATMETALLIC_ON)] _UseFlatMetallic("UseFlatMetallic", Float) = 0
		_RMOH("RMOH", 2D) = "white" {}
		_FlatSmoothness("FlatSmoothness", Range( 0 , 1)) = 0
		[Toggle(_USEFLATSMOOTHNESS_ON)] _UseFlatSmoothness("UseFlatSmoothness", Float) = 0
		_SmoothnessContrast("SmoothnessContrast", Float) = 1
		_SmoothnessHigh("SmoothnessHigh", Range( 0 , 1)) = 1
		_SmoothnessLow("SmoothnessLow", Range( 0 , 1)) = 0
		_SmoothnessIntensity("SmoothnessIntensity", Float) = 1
		_OcclusionContrast("OcclusionContrast", Float) = 1
		_OcclusionHigh("OcclusionHigh", Range( 0 , 1)) = 1
		_OcclusionLow("OcclusionLow", Range( 0 , 1)) = 0
		_OcclusionIntensity("OcclusionIntensity", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma shader_feature _USEDETAILNORMAL_ON
		#pragma shader_feature _USENORMAL_ON
		#pragma shader_feature _USEEMISSIVE_ON
		#pragma shader_feature _USEFLATEMISSIVE_ON
		#pragma shader_feature _USEMETALLIC_ON
		#pragma shader_feature _USEFLATMETALLIC_ON
		#pragma shader_feature _USEFLATSMOOTHNESS_ON
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform sampler2D _DetailNormal;
		uniform float4 _DetailNormal_ST;
		uniform sampler2D _Diffuse;
		uniform float4 _Diffuse_ST;
		uniform sampler2D _Emissive;
		uniform float4 _Emissive_ST;
		uniform sampler2D _RMOH;
		uniform float4 _RMOH_ST;
		uniform float _Cutoff = 0.5;

		UNITY_INSTANCING_BUFFER_START(S_DefaultCutout)
			UNITY_DEFINE_INSTANCED_PROP(float4, _FlatEmissive)
#define _FlatEmissive_arr S_DefaultCutout
			UNITY_DEFINE_INSTANCED_PROP(float, _NormalIntensity)
#define _NormalIntensity_arr S_DefaultCutout
			UNITY_DEFINE_INSTANCED_PROP(float, _OcclusionLow)
#define _OcclusionLow_arr S_DefaultCutout
			UNITY_DEFINE_INSTANCED_PROP(float, _OcclusionHigh)
#define _OcclusionHigh_arr S_DefaultCutout
			UNITY_DEFINE_INSTANCED_PROP(float, _SmoothnessIntensity)
#define _SmoothnessIntensity_arr S_DefaultCutout
			UNITY_DEFINE_INSTANCED_PROP(float, _SmoothnessContrast)
#define _SmoothnessContrast_arr S_DefaultCutout
			UNITY_DEFINE_INSTANCED_PROP(float, _FlatSmoothness)
#define _FlatSmoothness_arr S_DefaultCutout
			UNITY_DEFINE_INSTANCED_PROP(float, _SmoothnessLow)
#define _SmoothnessLow_arr S_DefaultCutout
			UNITY_DEFINE_INSTANCED_PROP(float, _SmoothnessHigh)
#define _SmoothnessHigh_arr S_DefaultCutout
			UNITY_DEFINE_INSTANCED_PROP(float, _FlatMetallic)
#define _FlatMetallic_arr S_DefaultCutout
			UNITY_DEFINE_INSTANCED_PROP(float, _EmissiveIntensity)
#define _EmissiveIntensity_arr S_DefaultCutout
			UNITY_DEFINE_INSTANCED_PROP(float, _DiffuseLightness)
#define _DiffuseLightness_arr S_DefaultCutout
			UNITY_DEFINE_INSTANCED_PROP(float, _DiffuseSaturation)
#define _DiffuseSaturation_arr S_DefaultCutout
			UNITY_DEFINE_INSTANCED_PROP(float, _DiffuseHue)
#define _DiffuseHue_arr S_DefaultCutout
			UNITY_DEFINE_INSTANCED_PROP(float, _DetailNormalIntensity)
#define _DetailNormalIntensity_arr S_DefaultCutout
			UNITY_DEFINE_INSTANCED_PROP(float, _OcclusionContrast)
#define _OcclusionContrast_arr S_DefaultCutout
			UNITY_DEFINE_INSTANCED_PROP(float, _OcclusionIntensity)
#define _OcclusionIntensity_arr S_DefaultCutout
		UNITY_INSTANCING_BUFFER_END(S_DefaultCutout)


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
			float3 _FlatNormal = float3(0,0,1);
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			float _NormalIntensity_Instance = UNITY_ACCESS_INSTANCED_PROP(_NormalIntensity_arr, _NormalIntensity);
			float3 lerpResult6 = lerp( _FlatNormal , UnpackNormal( tex2D( _Normal, uv_Normal ) ) , _NormalIntensity_Instance);
			#ifdef _USENORMAL_ON
				float3 staticSwitch7 = lerpResult6;
			#else
				float3 staticSwitch7 = _FlatNormal;
			#endif
			float2 uv_DetailNormal = i.uv_texcoord * _DetailNormal_ST.xy + _DetailNormal_ST.zw;
			float _DetailNormalIntensity_Instance = UNITY_ACCESS_INSTANCED_PROP(_DetailNormalIntensity_arr, _DetailNormalIntensity);
			float4 lerpResult61 = lerp( tex2D( _DetailNormal, uv_DetailNormal ) , float4( staticSwitch7 , 0.0 ) , _DetailNormalIntensity_Instance);
			#ifdef _USEDETAILNORMAL_ON
				float4 staticSwitch60 = lerpResult61;
			#else
				float4 staticSwitch60 = float4( staticSwitch7 , 0.0 );
			#endif
			o.Normal = staticSwitch60.rgb;
			float _DiffuseHue_Instance = UNITY_ACCESS_INSTANCED_PROP(_DiffuseHue_arr, _DiffuseHue);
			float2 uv_Diffuse = i.uv_texcoord * _Diffuse_ST.xy + _Diffuse_ST.zw;
			float4 tex2DNode2 = tex2D( _Diffuse, uv_Diffuse );
			float4 appendResult65 = (float4(tex2DNode2.r , tex2DNode2.g , tex2DNode2.b , 0.0));
			float3 hsvTorgb13_g9 = RGBToHSV( appendResult65.rgb );
			float _DiffuseSaturation_Instance = UNITY_ACCESS_INSTANCED_PROP(_DiffuseSaturation_arr, _DiffuseSaturation);
			float _DiffuseLightness_Instance = UNITY_ACCESS_INSTANCED_PROP(_DiffuseLightness_arr, _DiffuseLightness);
			float3 hsvTorgb17_g9 = HSVToRGB( float3(( _DiffuseHue_Instance + hsvTorgb13_g9.x ),( _DiffuseSaturation_Instance * hsvTorgb13_g9.y ),( hsvTorgb13_g9.z * _DiffuseLightness_Instance )) );
			o.Albedo = hsvTorgb17_g9;
			float4 temp_cast_5 = (0.0).xxxx;
			float2 uv_Emissive = i.uv_texcoord * _Emissive_ST.xy + _Emissive_ST.zw;
			float4 _FlatEmissive_Instance = UNITY_ACCESS_INSTANCED_PROP(_FlatEmissive_arr, _FlatEmissive);
			#ifdef _USEFLATEMISSIVE_ON
				float4 staticSwitch12 = _FlatEmissive_Instance;
			#else
				float4 staticSwitch12 = tex2D( _Emissive, uv_Emissive );
			#endif
			#ifdef _USEEMISSIVE_ON
				float4 staticSwitch10 = staticSwitch12;
			#else
				float4 staticSwitch10 = temp_cast_5;
			#endif
			float _EmissiveIntensity_Instance = UNITY_ACCESS_INSTANCED_PROP(_EmissiveIntensity_arr, _EmissiveIntensity);
			float4 temp_output_14_0 = ( staticSwitch10 * _EmissiveIntensity_Instance );
			o.Emission = temp_output_14_0.rgb;
			float2 uv_RMOH = i.uv_texcoord * _RMOH_ST.xy + _RMOH_ST.zw;
			float4 tex2DNode23 = tex2D( _RMOH, uv_RMOH );
			float _FlatMetallic_Instance = UNITY_ACCESS_INSTANCED_PROP(_FlatMetallic_arr, _FlatMetallic);
			#ifdef _USEFLATMETALLIC_ON
				float staticSwitch20 = _FlatMetallic_Instance;
			#else
				float staticSwitch20 = tex2DNode23.g;
			#endif
			#ifdef _USEMETALLIC_ON
				float staticSwitch16 = staticSwitch20;
			#else
				float staticSwitch16 = 0.0;
			#endif
			o.Metallic = staticSwitch16;
			float _SmoothnessHigh_Instance = UNITY_ACCESS_INSTANCED_PROP(_SmoothnessHigh_arr, _SmoothnessHigh);
			float _SmoothnessLow_Instance = UNITY_ACCESS_INSTANCED_PROP(_SmoothnessLow_arr, _SmoothnessLow);
			float _FlatSmoothness_Instance = UNITY_ACCESS_INSTANCED_PROP(_FlatSmoothness_arr, _FlatSmoothness);
			#ifdef _USEFLATSMOOTHNESS_ON
				float staticSwitch22 = _FlatSmoothness_Instance;
			#else
				float staticSwitch22 = tex2DNode23.r;
			#endif
			float _SmoothnessContrast_Instance = UNITY_ACCESS_INSTANCED_PROP(_SmoothnessContrast_arr, _SmoothnessContrast);
			float lerpResult29 = lerp( _SmoothnessHigh_Instance , _SmoothnessLow_Instance , pow( staticSwitch22 , _SmoothnessContrast_Instance ));
			float _SmoothnessIntensity_Instance = UNITY_ACCESS_INSTANCED_PROP(_SmoothnessIntensity_arr, _SmoothnessIntensity);
			o.Smoothness = ( lerpResult29 * _SmoothnessIntensity_Instance );
			float _OcclusionHigh_Instance = UNITY_ACCESS_INSTANCED_PROP(_OcclusionHigh_arr, _OcclusionHigh);
			float _OcclusionLow_Instance = UNITY_ACCESS_INSTANCED_PROP(_OcclusionLow_arr, _OcclusionLow);
			float _OcclusionContrast_Instance = UNITY_ACCESS_INSTANCED_PROP(_OcclusionContrast_arr, _OcclusionContrast);
			float lerpResult41 = lerp( _OcclusionHigh_Instance , _OcclusionLow_Instance , pow( tex2DNode23.b , _OcclusionContrast_Instance ));
			float _OcclusionIntensity_Instance = UNITY_ACCESS_INSTANCED_PROP(_OcclusionIntensity_arr, _OcclusionIntensity);
			o.Occlusion = ( lerpResult41 * _OcclusionIntensity_Instance );
			o.Alpha = 1;
			clip( tex2DNode2.a - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1927;7;1266;958;3208.005;425.1132;2.1994;True;False
Node;AmplifyShaderEditor.CommentaryNode;63;-2503.46,-420.9148;Float;False;1368.97;749.2253;Normal;9;60;61;62;59;7;6;8;4;3;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;35;-2403.138,1115.858;Float;False;1282.516;395.7463;Smoothness;9;21;30;29;33;32;31;28;22;34;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;23;-3030.509,1159.543;Float;True;Property;_RMOH;RMOH;22;0;Create;True;0;0;False;0;None;794951406b0c5c54ab2502d090cc0510;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;48;-2613.768,350.6755;Float;False;1486.952;486.911;Emissive;11;50;14;10;15;12;11;13;9;52;53;54;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;3;-2478.46,-222.3351;Float;True;Property;_Normal;Normal;5;0;Create;True;0;0;False;0;None;0a6b5af8299cdd0479582e5b80fe99c2;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;43;-2041.345,1526.808;Float;False;925.2512;300.8698;Occlusion;7;42;40;41;37;39;38;36;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-2387.013,1184.575;Float;False;InstancedProperty;_FlatSmoothness;FlatSmoothness;23;0;Create;True;0;0;False;0;0;0.422;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;4;-2357.078,-371.9148;Float;False;Constant;_FlatNormal;FlatNormal;3;0;Create;True;0;0;False;0;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WireNode;70;-2574.323,1542.94;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-2460.664,-27.11219;Float;False;InstancedProperty;_NormalIntensity;NormalIntensity;7;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;68;-2569.494,1197.66;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;6;-2119.617,-239.7694;Float;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WireNode;71;-2564.665,1050.373;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;44;-1993.716,863.3095;Float;False;866.9992;235.5807;Metallic;4;17;16;20;18;;1,1,1,1;0;0
Node;AmplifyShaderEditor.StaticSwitch;22;-2083.676,1162.801;Float;False;Property;_UseFlatSmoothness;UseFlatSmoothness;24;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;36;-2015.736,1590.227;Float;False;InstancedProperty;_OcclusionContrast;OcclusionContrast;29;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;34;-2009.075,1273.703;Float;False;InstancedProperty;_SmoothnessContrast;SmoothnessContrast;25;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;58;-2500.415,-929.8132;Float;False;1362.125;489.206;Diffuse;7;51;55;2;57;56;65;66;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ColorNode;13;-2502.59,667.4271;Float;False;InstancedProperty;_FlatEmissive;FlatEmissive;14;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;9;-2590.228,474.8298;Float;True;Property;_Emissive;Emissive;11;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WireNode;69;-2149.362,1586.402;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-1972.809,1008.63;Float;False;InstancedProperty;_FlatMetallic;FlatMetallic;20;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;62;-2454.8,246.879;Float;False;InstancedProperty;_DetailNormalIntensity;DetailNormalIntensity;10;0;Create;True;0;0;False;0;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;67;-2086.585,1016.569;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;28;-1709.982,1256.279;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;59;-2476.6,55.694;Float;True;Property;_DetailNormal;DetailNormal;8;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-2464.118,-877.0599;Float;True;Property;_Diffuse;Diffuse;1;0;Create;True;0;0;False;0;None;45fd9abd83194bc4f93baf15dcde50f8;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;31;-1835.218,1351.42;Float;False;InstancedProperty;_SmoothnessHigh;SmoothnessHigh;26;0;Create;True;0;0;False;0;1;0.697;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;7;-1907.239,-368.806;Float;False;Property;_UseNormal;UseNormal;6;0;Create;True;0;0;False;0;0;1;1;True;;Toggle;2;Key0;Key1;Create;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;39;-1840.211,1746.397;Float;False;InstancedProperty;_OcclusionLow;OcclusionLow;31;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;37;-1716.644,1572.802;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;32;-1833.548,1429.875;Float;False;InstancedProperty;_SmoothnessLow;SmoothnessLow;27;0;Create;True;0;0;False;0;0;0.205;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-2457.655,399.9852;Float;False;Constant;_NoEmissive;NoEmissive;7;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;12;-2255.513,472.3145;Float;False;Property;_UseFlatEmissive;UseFlatEmissive;15;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;38;-1841.88,1667.942;Float;False;InstancedProperty;_OcclusionHigh;OcclusionHigh;30;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;33;-1546.33,1375.468;Float;False;InstancedProperty;_SmoothnessIntensity;SmoothnessIntensity;28;0;Create;True;0;0;False;0;1;0.74;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;57;-1838.312,-527.9752;Float;False;InstancedProperty;_DiffuseLightness;DiffuseLightness;4;0;Create;True;0;0;False;0;1;2;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;56;-1839.577,-602.6301;Float;False;InstancedProperty;_DiffuseSaturation;DiffuseSaturation;3;0;Create;True;0;0;False;0;1;0.9;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;41;-1484.481,1574.429;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;55;-1842.724,-676.4275;Float;False;InstancedProperty;_DiffuseHue;DiffuseHue;2;0;Create;True;0;0;False;0;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;40;-1535.977,1691.989;Float;False;InstancedProperty;_OcclusionIntensity;OcclusionIntensity;32;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;65;-2137.966,-872.2031;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-1882.456,908.3095;Float;False;Constant;_NoMetallic;NoMetallic;11;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;29;-1477.819,1257.905;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;61;-1609.56,-256.2397;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;10;-1937.924,398.27;Float;False;Property;_UseEmissive;UseEmissive;12;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-2247.939,576.8984;Float;False;InstancedProperty;_EmissiveIntensity;EmissiveIntensity;13;0;Create;True;0;0;False;0;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;20;-1681.395,987.3975;Float;False;Property;_UseFlatMetallic;UseFlatMetallic;21;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;50;-1444.374,399.3882;Float;False;SF_ColorShift;-1;;8;f3651b3e85772604cb45f632c7f608e7;0;4;26;FLOAT;0;False;27;FLOAT;0;False;28;FLOAT;0;False;23;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StaticSwitch;16;-1370.317,910.067;Float;False;Property;_UseMetallic;UseMetallic;19;0;Create;True;0;0;False;0;0;1;0;True;;Toggle;2;Key0;Key1;Create;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;51;-1453.621,-874.8996;Float;False;SF_ColorShift;-1;;9;f3651b3e85772604cb45f632c7f608e7;0;4;26;FLOAT;0;False;27;FLOAT;0;False;28;FLOAT;0;False;23;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-1255.684,1578.025;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;60;-1421.729,-363.5715;Float;False;Property;_UseDetailNormal;UseDetailNormal;9;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-1260.453,1257.214;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;53;-1793.836,568.7406;Float;False;InstancedProperty;_EmissiveSaturation;EmissiveSaturation;17;0;Create;True;0;0;False;0;1;1;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;52;-1796.982,494.9432;Float;False;InstancedProperty;_EmissiveHue;EmissiveHue;16;0;Create;True;0;0;False;0;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;54;-1790.571,643.3954;Float;False;InstancedProperty;_EmissiveLightness;EmissiveLightness;18;0;Create;True;0;0;False;0;1;1;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-1652.654,402.3846;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;66;-1212.086,-574.071;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-640.1187,455.1956;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_DefaultCutout;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Masked;0.5;True;True;0;False;TransparentCutout;;AlphaTest;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;70;0;23;3
WireConnection;68;0;23;1
WireConnection;6;0;4;0
WireConnection;6;1;3;0
WireConnection;6;2;8;0
WireConnection;71;0;23;2
WireConnection;22;1;68;0
WireConnection;22;0;21;0
WireConnection;69;0;70;0
WireConnection;67;0;71;0
WireConnection;28;0;22;0
WireConnection;28;1;34;0
WireConnection;7;1;4;0
WireConnection;7;0;6;0
WireConnection;37;0;69;0
WireConnection;37;1;36;0
WireConnection;12;1;9;0
WireConnection;12;0;13;0
WireConnection;41;0;38;0
WireConnection;41;1;39;0
WireConnection;41;2;37;0
WireConnection;65;0;2;1
WireConnection;65;1;2;2
WireConnection;65;2;2;3
WireConnection;29;0;31;0
WireConnection;29;1;32;0
WireConnection;29;2;28;0
WireConnection;61;0;59;0
WireConnection;61;1;7;0
WireConnection;61;2;62;0
WireConnection;10;1;11;0
WireConnection;10;0;12;0
WireConnection;20;1;67;0
WireConnection;20;0;17;0
WireConnection;50;26;52;0
WireConnection;50;27;53;0
WireConnection;50;28;54;0
WireConnection;50;23;14;0
WireConnection;16;1;18;0
WireConnection;16;0;20;0
WireConnection;51;26;55;0
WireConnection;51;27;56;0
WireConnection;51;28;57;0
WireConnection;51;23;65;0
WireConnection;42;0;41;0
WireConnection;42;1;40;0
WireConnection;60;1;7;0
WireConnection;60;0;61;0
WireConnection;30;0;29;0
WireConnection;30;1;33;0
WireConnection;14;0;10;0
WireConnection;14;1;15;0
WireConnection;66;0;2;4
WireConnection;0;0;51;0
WireConnection;0;1;60;0
WireConnection;0;2;14;0
WireConnection;0;3;16;0
WireConnection;0;4;30;0
WireConnection;0;5;42;0
WireConnection;0;10;66;0
ASEEND*/
//CHKSM=CAE3DD18D9594AB5EBBBB41B53749443D00416C7