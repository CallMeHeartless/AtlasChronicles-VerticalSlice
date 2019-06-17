// Upgrade NOTE: upgraded instancing buffer 'S_DefaultSurface_SplitInput' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_DefaultSurface_SplitInput"
{
	Properties
	{
		_Diffuse("Diffuse", 2D) = "white" {}
		_DiffuseHue("DiffuseHue", Range( -1 , 1)) = 0
		_DiffuseSaturation("DiffuseSaturation", Range( 0 , 2)) = 1
		_DiffuseLightness("DiffuseLightness", Range( 0 , 5)) = 1
		_Normal("Normal", 2D) = "bump" {}
		[Toggle(_USENORMAL_ON)] _UseNormal("UseNormal", Float) = 1
		_NormalIntensity("NormalIntensity", Range( 0 , 1)) = 1
		_Smoothness("Smoothness", 2D) = "white" {}
		_Metallic("Metallic", 2D) = "white" {}
		_Occlusion("Occlusion", 2D) = "white" {}
		_Emissive("Emissive", 2D) = "white" {}
		[Toggle(_USEEMISSIVE_ON)] _UseEmissive("UseEmissive", Float) = 0
		_EmissiveIntensity("EmissiveIntensity", Range( 0 , 1)) = 1
		_FlatEmissive("FlatEmissive", Color) = (0,0,0,0)
		[Toggle(_USEFLATEMISSIVE_ON)] _UseFlatEmissive("UseFlatEmissive", Float) = 0
		_EmissiveHue("EmissiveHue", Range( -1 , 1)) = 0
		_EmissiveSaturation("EmissiveSaturation", Range( 0 , 2)) = 1
		_EmissiveLightness("EmissiveLightness", Range( 0 , 5)) = 1
		[Toggle(_USEMETALLIC_ON)] _UseMetallic("UseMetallic", Float) = 1
		_FlatMetallic("FlatMetallic", Range( 0 , 1)) = 0
		[Toggle(_USEFLATMETALLIC_ON)] _UseFlatMetallic("UseFlatMetallic", Float) = 0
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
		_Tiling("Tiling", Float) = 1
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
		uniform sampler2D _Diffuse;
		uniform sampler2D _Emissive;
		uniform sampler2D _Metallic;
		uniform sampler2D _Smoothness;
		uniform sampler2D _Occlusion;

		UNITY_INSTANCING_BUFFER_START(S_DefaultSurface_SplitInput)
			UNITY_DEFINE_INSTANCED_PROP(float4, _FlatEmissive)
#define _FlatEmissive_arr S_DefaultSurface_SplitInput
			UNITY_DEFINE_INSTANCED_PROP(float, _Tiling)
#define _Tiling_arr S_DefaultSurface_SplitInput
			UNITY_DEFINE_INSTANCED_PROP(float, _OcclusionLow)
#define _OcclusionLow_arr S_DefaultSurface_SplitInput
			UNITY_DEFINE_INSTANCED_PROP(float, _OcclusionHigh)
#define _OcclusionHigh_arr S_DefaultSurface_SplitInput
			UNITY_DEFINE_INSTANCED_PROP(float, _SmoothnessIntensity)
#define _SmoothnessIntensity_arr S_DefaultSurface_SplitInput
			UNITY_DEFINE_INSTANCED_PROP(float, _SmoothnessContrast)
#define _SmoothnessContrast_arr S_DefaultSurface_SplitInput
			UNITY_DEFINE_INSTANCED_PROP(float, _FlatSmoothness)
#define _FlatSmoothness_arr S_DefaultSurface_SplitInput
			UNITY_DEFINE_INSTANCED_PROP(float, _SmoothnessLow)
#define _SmoothnessLow_arr S_DefaultSurface_SplitInput
			UNITY_DEFINE_INSTANCED_PROP(float, _SmoothnessHigh)
#define _SmoothnessHigh_arr S_DefaultSurface_SplitInput
			UNITY_DEFINE_INSTANCED_PROP(float, _FlatMetallic)
#define _FlatMetallic_arr S_DefaultSurface_SplitInput
			UNITY_DEFINE_INSTANCED_PROP(float, _EmissiveLightness)
#define _EmissiveLightness_arr S_DefaultSurface_SplitInput
			UNITY_DEFINE_INSTANCED_PROP(float, _EmissiveSaturation)
#define _EmissiveSaturation_arr S_DefaultSurface_SplitInput
			UNITY_DEFINE_INSTANCED_PROP(float, _EmissiveIntensity)
#define _EmissiveIntensity_arr S_DefaultSurface_SplitInput
			UNITY_DEFINE_INSTANCED_PROP(float, _EmissiveHue)
#define _EmissiveHue_arr S_DefaultSurface_SplitInput
			UNITY_DEFINE_INSTANCED_PROP(float, _DiffuseLightness)
#define _DiffuseLightness_arr S_DefaultSurface_SplitInput
			UNITY_DEFINE_INSTANCED_PROP(float, _DiffuseSaturation)
#define _DiffuseSaturation_arr S_DefaultSurface_SplitInput
			UNITY_DEFINE_INSTANCED_PROP(float, _DiffuseHue)
#define _DiffuseHue_arr S_DefaultSurface_SplitInput
			UNITY_DEFINE_INSTANCED_PROP(float, _NormalIntensity)
#define _NormalIntensity_arr S_DefaultSurface_SplitInput
			UNITY_DEFINE_INSTANCED_PROP(float, _OcclusionContrast)
#define _OcclusionContrast_arr S_DefaultSurface_SplitInput
			UNITY_DEFINE_INSTANCED_PROP(float, _OcclusionIntensity)
#define _OcclusionIntensity_arr S_DefaultSurface_SplitInput
		UNITY_INSTANCING_BUFFER_END(S_DefaultSurface_SplitInput)


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
			float _Tiling_Instance = UNITY_ACCESS_INSTANCED_PROP(_Tiling_arr, _Tiling);
			float2 temp_cast_0 = (_Tiling_Instance).xx;
			float2 uv_TexCoord75 = i.uv_texcoord * temp_cast_0;
			float _NormalIntensity_Instance = UNITY_ACCESS_INSTANCED_PROP(_NormalIntensity_arr, _NormalIntensity);
			float3 lerpResult6 = lerp( _FlatNormal , UnpackNormal( tex2D( _Normal, uv_TexCoord75 ) ) , _NormalIntensity_Instance);
			#ifdef _USENORMAL_ON
				float3 staticSwitch7 = lerpResult6;
			#else
				float3 staticSwitch7 = _FlatNormal;
			#endif
			o.Normal = staticSwitch7;
			float _DiffuseHue_Instance = UNITY_ACCESS_INSTANCED_PROP(_DiffuseHue_arr, _DiffuseHue);
			float4 tex2DNode2 = tex2D( _Diffuse, uv_TexCoord75 );
			float4 appendResult72 = (float4(tex2DNode2.r , tex2DNode2.g , tex2DNode2.b , 0.0));
			float3 hsvTorgb13_g11 = RGBToHSV( appendResult72.rgb );
			float _DiffuseSaturation_Instance = UNITY_ACCESS_INSTANCED_PROP(_DiffuseSaturation_arr, _DiffuseSaturation);
			float _DiffuseLightness_Instance = UNITY_ACCESS_INSTANCED_PROP(_DiffuseLightness_arr, _DiffuseLightness);
			float3 hsvTorgb17_g11 = HSVToRGB( float3(( _DiffuseHue_Instance + hsvTorgb13_g11.x ),( _DiffuseSaturation_Instance * hsvTorgb13_g11.y ),( hsvTorgb13_g11.z * _DiffuseLightness_Instance )) );
			o.Albedo = hsvTorgb17_g11;
			float _EmissiveHue_Instance = UNITY_ACCESS_INSTANCED_PROP(_EmissiveHue_arr, _EmissiveHue);
			float4 temp_cast_3 = (0.0).xxxx;
			float4 _FlatEmissive_Instance = UNITY_ACCESS_INSTANCED_PROP(_FlatEmissive_arr, _FlatEmissive);
			#ifdef _USEFLATEMISSIVE_ON
				float4 staticSwitch12 = _FlatEmissive_Instance;
			#else
				float4 staticSwitch12 = tex2D( _Emissive, uv_TexCoord75 );
			#endif
			#ifdef _USEEMISSIVE_ON
				float4 staticSwitch10 = staticSwitch12;
			#else
				float4 staticSwitch10 = temp_cast_3;
			#endif
			float _EmissiveIntensity_Instance = UNITY_ACCESS_INSTANCED_PROP(_EmissiveIntensity_arr, _EmissiveIntensity);
			float3 hsvTorgb13_g12 = RGBToHSV( ( staticSwitch10 * _EmissiveIntensity_Instance ).rgb );
			float _EmissiveSaturation_Instance = UNITY_ACCESS_INSTANCED_PROP(_EmissiveSaturation_arr, _EmissiveSaturation);
			float _EmissiveLightness_Instance = UNITY_ACCESS_INSTANCED_PROP(_EmissiveLightness_arr, _EmissiveLightness);
			float3 hsvTorgb17_g12 = HSVToRGB( float3(( _EmissiveHue_Instance + hsvTorgb13_g12.x ),( _EmissiveSaturation_Instance * hsvTorgb13_g12.y ),( hsvTorgb13_g12.z * _EmissiveLightness_Instance )) );
			o.Emission = hsvTorgb17_g12;
			float4 temp_cast_5 = (0.0).xxxx;
			float _FlatMetallic_Instance = UNITY_ACCESS_INSTANCED_PROP(_FlatMetallic_arr, _FlatMetallic);
			float4 temp_cast_6 = (_FlatMetallic_Instance).xxxx;
			#ifdef _USEFLATMETALLIC_ON
				float4 staticSwitch20 = temp_cast_6;
			#else
				float4 staticSwitch20 = tex2D( _Metallic, uv_TexCoord75 );
			#endif
			#ifdef _USEMETALLIC_ON
				float4 staticSwitch16 = staticSwitch20;
			#else
				float4 staticSwitch16 = temp_cast_5;
			#endif
			o.Metallic = staticSwitch16.r;
			float _SmoothnessHigh_Instance = UNITY_ACCESS_INSTANCED_PROP(_SmoothnessHigh_arr, _SmoothnessHigh);
			float _SmoothnessLow_Instance = UNITY_ACCESS_INSTANCED_PROP(_SmoothnessLow_arr, _SmoothnessLow);
			float _FlatSmoothness_Instance = UNITY_ACCESS_INSTANCED_PROP(_FlatSmoothness_arr, _FlatSmoothness);
			float4 temp_cast_8 = (_FlatSmoothness_Instance).xxxx;
			#ifdef _USEFLATSMOOTHNESS_ON
				float4 staticSwitch22 = temp_cast_8;
			#else
				float4 staticSwitch22 = tex2D( _Smoothness, uv_TexCoord75 );
			#endif
			float _SmoothnessContrast_Instance = UNITY_ACCESS_INSTANCED_PROP(_SmoothnessContrast_arr, _SmoothnessContrast);
			float4 temp_cast_9 = (_SmoothnessContrast_Instance).xxxx;
			float lerpResult29 = lerp( _SmoothnessHigh_Instance , _SmoothnessLow_Instance , pow( staticSwitch22 , temp_cast_9 ).r);
			float _SmoothnessIntensity_Instance = UNITY_ACCESS_INSTANCED_PROP(_SmoothnessIntensity_arr, _SmoothnessIntensity);
			o.Smoothness = ( lerpResult29 * _SmoothnessIntensity_Instance );
			float _OcclusionHigh_Instance = UNITY_ACCESS_INSTANCED_PROP(_OcclusionHigh_arr, _OcclusionHigh);
			float _OcclusionLow_Instance = UNITY_ACCESS_INSTANCED_PROP(_OcclusionLow_arr, _OcclusionLow);
			float _OcclusionContrast_Instance = UNITY_ACCESS_INSTANCED_PROP(_OcclusionContrast_arr, _OcclusionContrast);
			float4 temp_cast_11 = (_OcclusionContrast_Instance).xxxx;
			float lerpResult41 = lerp( _OcclusionHigh_Instance , _OcclusionLow_Instance , pow( tex2D( _Occlusion, uv_TexCoord75 ) , temp_cast_11 ).r);
			float _OcclusionIntensity_Instance = UNITY_ACCESS_INSTANCED_PROP(_OcclusionIntensity_arr, _OcclusionIntensity);
			o.Occlusion = ( lerpResult41 * _OcclusionIntensity_Instance );
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
7;1;1906;1020;3367.019;686.5286;1.698482;True;False
Node;AmplifyShaderEditor.RangedFloatNode;76;-3595.203,222.5615;Float;False;InstancedProperty;_Tiling;Tiling;34;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;48;-2613.768,350.6755;Float;False;1486.952;486.911;Emissive;11;50;14;10;15;12;11;13;9;52;53;54;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;75;-3384.979,165.0507;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;35;-2398.572,1125.994;Float;False;1277.162;387.7154;Smoothness;9;21;30;29;33;28;31;32;34;22;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ColorNode;13;-2502.59,667.4271;Float;False;InstancedProperty;_FlatEmissive;FlatEmissive;16;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;9;-2590.228,474.8298;Float;True;Property;_Emissive;Emissive;13;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;21;-2382.342,1200.433;Float;False;InstancedProperty;_FlatSmoothness;FlatSmoothness;24;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;78;-2856.928,1091.613;Float;True;Property;_Smoothness;Smoothness;10;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;43;-2041.765,1531.328;Float;False;936.0644;303.4944;Occlusion;7;42;40;41;37;39;38;36;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;63;-2503.46,-420.9148;Float;False;1368.97;749.2253;Normal;9;60;61;62;59;7;6;8;4;3;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;44;-2605.473,862.8576;Float;False;900.0681;236.4732;Metallic;4;17;16;18;20;;1,1,1,1;0;0
Node;AmplifyShaderEditor.StaticSwitch;22;-2084.463,1172.937;Float;False;Property;_UseFlatSmoothness;UseFlatSmoothness;25;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;34;-2009.862,1283.839;Float;False;InstancedProperty;_SmoothnessContrast;SmoothnessContrast;26;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;58;-2029.452,-929.8132;Float;False;891.1621;486.1738;Diffuse;6;72;51;55;2;57;56;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;79;-2508.029,1566.09;Float;True;Property;_Occlusion;Occlusion;12;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;11;-2457.655,399.9852;Float;False;Constant;_NoEmissive;NoEmissive;7;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;12;-2255.513,472.3145;Float;False;Property;_UseFlatEmissive;UseFlatEmissive;17;0;Create;True;0;0;False;0;0;0;1;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;36;-2014.109,1598.159;Float;False;InstancedProperty;_OcclusionContrast;OcclusionContrast;30;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-2008.713,-878.8094;Float;True;Property;_Diffuse;Diffuse;0;0;Create;True;0;0;False;0;None;e92eb42195712e1429922c2e0833efb9;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;8;-2460.664,-27.11219;Float;False;InstancedProperty;_NormalIntensity;NormalIntensity;6;0;Create;True;0;0;False;0;1;0.45;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;3;-2478.46,-222.3351;Float;True;Property;_Normal;Normal;4;0;Create;True;0;0;False;0;None;60b9d862af15a804580f95576ebae59d;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;4;-2357.078,-371.9148;Float;False;Constant;_FlatNormal;FlatNormal;3;0;Create;True;0;0;False;0;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SamplerNode;77;-3103.211,783.0783;Float;True;Property;_Metallic;Metallic;11;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;15;-2247.939,576.8984;Float;False;InstancedProperty;_EmissiveIntensity;EmissiveIntensity;15;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;38;-1840.253,1675.874;Float;False;InstancedProperty;_OcclusionHigh;OcclusionHigh;31;0;Create;True;0;0;False;0;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;10;-1937.924,398.27;Float;False;Property;_UseEmissive;UseEmissive;14;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;32;-1834.335,1440.011;Float;False;InstancedProperty;_SmoothnessLow;SmoothnessLow;28;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;37;-1715.017,1580.734;Float;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;28;-1710.769,1266.415;Float;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-1836.005,1361.556;Float;False;InstancedProperty;_SmoothnessHigh;SmoothnessHigh;27;0;Create;True;0;0;False;0;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;39;-1838.584,1754.329;Float;False;InstancedProperty;_OcclusionLow;OcclusionLow;32;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-2580.472,1009.123;Float;False;InstancedProperty;_FlatMetallic;FlatMetallic;22;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;56;-1986.618,-606.2164;Float;False;InstancedProperty;_DiffuseSaturation;DiffuseSaturation;2;0;Create;True;0;0;False;0;1;0.804;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;33;-1547.117,1385.604;Float;False;InstancedProperty;_SmoothnessIntensity;SmoothnessIntensity;29;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;29;-1478.606,1268.041;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;20;-2260.083,986.9456;Float;False;Property;_UseFlatMetallic;UseFlatMetallic;23;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;40;-1534.35,1699.921;Float;False;InstancedProperty;_OcclusionIntensity;OcclusionIntensity;33;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;72;-1695.227,-850.5234;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-2461.144,907.8576;Float;False;Constant;_NoMetallic;NoMetallic;11;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;57;-1985.353,-531.5615;Float;False;InstancedProperty;_DiffuseLightness;DiffuseLightness;3;0;Create;True;0;0;False;0;1;0.21;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;53;-1793.836,568.7406;Float;False;InstancedProperty;_EmissiveSaturation;EmissiveSaturation;19;0;Create;True;0;0;False;0;1;1;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;54;-1790.571,643.3954;Float;False;InstancedProperty;_EmissiveLightness;EmissiveLightness;20;0;Create;True;0;0;False;0;1;1;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;55;-1989.765,-680.0137;Float;False;InstancedProperty;_DiffuseHue;DiffuseHue;1;0;Create;True;0;0;False;0;0;1;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;52;-1796.982,494.9432;Float;False;InstancedProperty;_EmissiveHue;EmissiveHue;18;0;Create;True;0;0;False;0;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-1652.654,402.3846;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;6;-2119.617,-239.7694;Float;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;41;-1482.854,1582.361;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;60;-1421.729,-363.5715;Float;False;Property;_UseDetailNormal;UseDetailNormal;8;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;50;-1461.316,389.9756;Float;False;SF_ColorShift;-1;;12;f3651b3e85772604cb45f632c7f608e7;0;4;26;FLOAT;0;False;27;FLOAT;0;False;28;FLOAT;0;False;23;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-1261.241,1267.35;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;61;-1609.56,-256.2397;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;7;-1907.239,-368.806;Float;False;Property;_UseNormal;UseNormal;5;0;Create;True;0;0;False;0;0;1;0;True;;Toggle;2;Key0;Key1;Create;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;59;-2476.6,55.694;Float;True;Property;_DetailNormal;DetailNormal;7;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;62;-2454.8,246.879;Float;False;InstancedProperty;_DetailNormalIntensity;DetailNormalIntensity;9;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;51;-1453.621,-874.8996;Float;False;SF_ColorShift;-1;;11;f3651b3e85772604cb45f632c7f608e7;0;4;26;FLOAT;0;False;27;FLOAT;0;False;28;FLOAT;0;False;23;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-1254.057,1585.957;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;16;-1949.004,909.6151;Float;False;Property;_UseMetallic;UseMetallic;21;0;Create;True;0;0;False;0;0;1;0;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-450.4983,330.6276;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_DefaultSurface_SplitInput;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;75;0;76;0
WireConnection;9;1;75;0
WireConnection;78;1;75;0
WireConnection;22;1;78;0
WireConnection;22;0;21;0
WireConnection;79;1;75;0
WireConnection;12;1;9;0
WireConnection;12;0;13;0
WireConnection;2;1;75;0
WireConnection;3;1;75;0
WireConnection;77;1;75;0
WireConnection;10;1;11;0
WireConnection;10;0;12;0
WireConnection;37;0;79;0
WireConnection;37;1;36;0
WireConnection;28;0;22;0
WireConnection;28;1;34;0
WireConnection;29;0;31;0
WireConnection;29;1;32;0
WireConnection;29;2;28;0
WireConnection;20;1;77;0
WireConnection;20;0;17;0
WireConnection;72;0;2;1
WireConnection;72;1;2;2
WireConnection;72;2;2;3
WireConnection;14;0;10;0
WireConnection;14;1;15;0
WireConnection;6;0;4;0
WireConnection;6;1;3;0
WireConnection;6;2;8;0
WireConnection;41;0;38;0
WireConnection;41;1;39;0
WireConnection;41;2;37;0
WireConnection;60;1;7;0
WireConnection;60;0;61;0
WireConnection;50;26;52;0
WireConnection;50;27;53;0
WireConnection;50;28;54;0
WireConnection;50;23;14;0
WireConnection;30;0;29;0
WireConnection;30;1;33;0
WireConnection;61;0;59;0
WireConnection;61;1;7;0
WireConnection;61;2;62;0
WireConnection;7;1;4;0
WireConnection;7;0;6;0
WireConnection;59;1;75;0
WireConnection;51;26;55;0
WireConnection;51;27;56;0
WireConnection;51;28;57;0
WireConnection;51;23;72;0
WireConnection;42;0;41;0
WireConnection;42;1;40;0
WireConnection;16;1;18;0
WireConnection;16;0;20;0
WireConnection;0;0;51;0
WireConnection;0;1;7;0
WireConnection;0;2;50;0
WireConnection;0;3;16;0
WireConnection;0;4;30;0
WireConnection;0;5;42;0
ASEEND*/
//CHKSM=B360623EBE3726C4AE04C78A119DE5BD9ECE6206