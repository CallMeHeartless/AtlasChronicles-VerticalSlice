// Upgrade NOTE: upgraded instancing buffer 'S_DefaultSurface' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_DefaultSurface"
{
	Properties
	{
		_Diffuse("Diffuse", 2D) = "white" {}
		_DiffuseHue("DiffuseHue", Range( -1 , 1)) = 0
		_DiffuseSaturation("DiffuseSaturation", Range( 0 , 2)) = 1
		_DiffuseLightness("DiffuseLightness", Range( 0 , 5)) = 1
		_Normal("Normal", 2D) = "bump" {}
		_NormalIntensity("NormalIntensity", Range( 0 , 1)) = 1
		_Emissive("Emissive", 2D) = "white" {}
		[Toggle(_USEEMISSIVE_ON)] _UseEmissive("UseEmissive", Float) = 0
		_EmissiveIntensity("EmissiveIntensity", Range( 0 , 1)) = 1
		_FlatEmissive("FlatEmissive", Color) = (0,0,0,0)
		[Toggle(_USEFLATEMISSIVE_ON)] _UseFlatEmissive("UseFlatEmissive", Float) = 0
		_RMOH("RMOH", 2D) = "white" {}
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
		#pragma shader_feature _USEEMISSIVE_ON
		#pragma shader_feature _USEFLATEMISSIVE_ON
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform sampler2D _Diffuse;
		uniform float4 _Diffuse_ST;
		uniform sampler2D _Emissive;
		uniform float4 _Emissive_ST;
		uniform sampler2D _RMOH;
		uniform float4 _RMOH_ST;

		UNITY_INSTANCING_BUFFER_START(S_DefaultSurface)
			UNITY_DEFINE_INSTANCED_PROP(float4, _FlatEmissive)
#define _FlatEmissive_arr S_DefaultSurface
			UNITY_DEFINE_INSTANCED_PROP(float, _NormalIntensity)
#define _NormalIntensity_arr S_DefaultSurface
			UNITY_DEFINE_INSTANCED_PROP(float, _DiffuseHue)
#define _DiffuseHue_arr S_DefaultSurface
			UNITY_DEFINE_INSTANCED_PROP(float, _DiffuseSaturation)
#define _DiffuseSaturation_arr S_DefaultSurface
			UNITY_DEFINE_INSTANCED_PROP(float, _DiffuseLightness)
#define _DiffuseLightness_arr S_DefaultSurface
			UNITY_DEFINE_INSTANCED_PROP(float, _EmissiveIntensity)
#define _EmissiveIntensity_arr S_DefaultSurface
		UNITY_INSTANCING_BUFFER_END(S_DefaultSurface)


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
			o.Normal = lerpResult6;
			float _DiffuseHue_Instance = UNITY_ACCESS_INSTANCED_PROP(_DiffuseHue_arr, _DiffuseHue);
			float2 uv_Diffuse = i.uv_texcoord * _Diffuse_ST.xy + _Diffuse_ST.zw;
			float4 tex2DNode2 = tex2D( _Diffuse, uv_Diffuse );
			float4 appendResult72 = (float4(tex2DNode2.r , tex2DNode2.g , tex2DNode2.b , 0.0));
			float3 hsvTorgb13_g9 = RGBToHSV( appendResult72.rgb );
			float _DiffuseSaturation_Instance = UNITY_ACCESS_INSTANCED_PROP(_DiffuseSaturation_arr, _DiffuseSaturation);
			float _DiffuseLightness_Instance = UNITY_ACCESS_INSTANCED_PROP(_DiffuseLightness_arr, _DiffuseLightness);
			float3 hsvTorgb17_g9 = HSVToRGB( float3(( _DiffuseHue_Instance + hsvTorgb13_g9.x ),( _DiffuseSaturation_Instance * hsvTorgb13_g9.y ),( hsvTorgb13_g9.z * _DiffuseLightness_Instance )) );
			o.Albedo = hsvTorgb17_g9;
			float4 temp_cast_2 = (0.0).xxxx;
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
				float4 staticSwitch10 = temp_cast_2;
			#endif
			float _EmissiveIntensity_Instance = UNITY_ACCESS_INSTANCED_PROP(_EmissiveIntensity_arr, _EmissiveIntensity);
			float4 temp_output_14_0 = ( staticSwitch10 * _EmissiveIntensity_Instance );
			o.Emission = temp_output_14_0.rgb;
			float2 uv_RMOH = i.uv_texcoord * _RMOH_ST.xy + _RMOH_ST.zw;
			float4 tex2DNode64 = tex2D( _RMOH, uv_RMOH );
			o.Metallic = tex2DNode64.g;
			o.Smoothness = tex2DNode64.r;
			o.Occlusion = tex2DNode64.b;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1927;7;1266;958;3241.46;358.4569;2.41448;True;False
Node;AmplifyShaderEditor.CommentaryNode;48;-2613.768,350.6755;Float;False;1486.952;486.911;Emissive;11;50;14;10;15;12;11;13;9;52;53;54;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;58;-2029.452,-929.8132;Float;False;891.1621;486.1738;Diffuse;6;72;51;55;2;57;56;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;9;-2590.228,474.8298;Float;True;Property;_Emissive;Emissive;10;0;Create;True;0;0;False;0;None;770ce4d8975e6db42a898bd72a566a0a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;13;-2502.59,667.4271;Float;False;InstancedProperty;_FlatEmissive;FlatEmissive;13;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-2008.713,-878.8094;Float;True;Property;_Diffuse;Diffuse;0;0;Create;True;0;0;False;0;None;770ce4d8975e6db42a898bd72a566a0a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;63;-2503.46,-420.9148;Float;False;1368.97;749.2253;Normal;9;60;61;62;59;7;6;8;4;3;;1,1,1,1;0;0
Node;AmplifyShaderEditor.StaticSwitch;12;-2255.513,472.3145;Float;False;Property;_UseFlatEmissive;UseFlatEmissive;14;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-2457.655,399.9852;Float;False;Constant;_NoEmissive;NoEmissive;7;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;10;-1937.924,398.27;Float;False;Property;_UseEmissive;UseEmissive;11;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;43;-2041.765,1529.328;Float;False;936.0644;303.4944;Occlusion;7;42;40;41;37;39;38;36;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-2460.664,-27.11219;Float;False;InstancedProperty;_NormalIntensity;NormalIntensity;6;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;3;-2478.46,-222.3351;Float;True;Property;_Normal;Normal;4;0;Create;True;0;0;False;0;None;d4db25a4f8f6e0a49a0556b88bad519c;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;4;-2357.078,-371.9148;Float;False;Constant;_FlatNormal;FlatNormal;3;0;Create;True;0;0;False;0;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DynamicAppendNode;72;-1695.227,-850.5234;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;55;-1989.765,-680.0137;Float;False;InstancedProperty;_DiffuseHue;DiffuseHue;1;0;Create;True;0;0;False;0;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;35;-2398.572,1123.994;Float;False;1277.162;387.7154;Smoothness;9;21;30;29;33;28;31;32;34;22;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;56;-1986.618,-606.2164;Float;False;InstancedProperty;_DiffuseSaturation;DiffuseSaturation;2;0;Create;True;0;0;False;0;1;1.5;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;57;-1985.353,-531.5615;Float;False;InstancedProperty;_DiffuseLightness;DiffuseLightness;3;0;Create;True;0;0;False;0;1;1;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;44;-2605.473,860.8576;Float;False;900.0681;236.4732;Metallic;4;17;16;18;20;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-2247.939,576.8984;Float;False;InstancedProperty;_EmissiveIntensity;EmissiveIntensity;12;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;36;-2014.109,1596.159;Float;False;InstancedProperty;_OcclusionContrast;OcclusionContrast;27;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;6;-2119.617,-239.7694;Float;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;34;-2009.862,1281.839;Float;False;InstancedProperty;_SmoothnessContrast;SmoothnessContrast;23;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;41;-1482.854,1580.361;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;62;-2454.8,246.879;Float;False;InstancedProperty;_DetailNormalIntensity;DetailNormalIntensity;9;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-2461.144,905.8576;Float;False;Constant;_NoMetallic;NoMetallic;11;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;7;-1907.239,-368.806;Float;False;Property;_UseNormal;UseNormal;5;0;Create;True;0;0;False;0;0;1;0;True;;Toggle;2;Key0;Key1;Create;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;52;-1796.982,494.9432;Float;False;InstancedProperty;_EmissiveHue;EmissiveHue;15;0;Create;True;0;0;False;0;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-1652.654,402.3846;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;38;-1840.253,1673.874;Float;False;InstancedProperty;_OcclusionHigh;OcclusionHigh;28;0;Create;True;0;0;False;0;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;39;-1838.584,1752.329;Float;False;InstancedProperty;_OcclusionLow;OcclusionLow;29;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-2580.472,1007.123;Float;False;InstancedProperty;_FlatMetallic;FlatMetallic;19;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;59;-2476.6,55.694;Float;True;Property;_DetailNormal;DetailNormal;7;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;37;-1715.017,1578.734;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;22;-2084.463,1170.937;Float;False;Property;_UseFlatSmoothness;UseFlatSmoothness;22;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;40;-1534.35,1697.921;Float;False;InstancedProperty;_OcclusionIntensity;OcclusionIntensity;30;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-1261.241,1265.35;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;61;-1609.56,-256.2397;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;29;-1478.606,1266.041;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;60;-1421.729,-363.5715;Float;False;Property;_UseDetailNormal;UseDetailNormal;8;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;28;-1710.769,1264.415;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;51;-1453.621,-874.8996;Float;False;SF_ColorShift;-1;;9;f3651b3e85772604cb45f632c7f608e7;0;4;26;FLOAT;0;False;27;FLOAT;0;False;28;FLOAT;0;False;23;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StaticSwitch;16;-1949.004,907.6151;Float;False;Property;_UseMetallic;UseMetallic;18;0;Create;True;0;0;False;0;0;1;0;True;;Toggle;2;Key0;Key1;Create;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-2382.342,1198.433;Float;False;InstancedProperty;_FlatSmoothness;FlatSmoothness;21;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;50;-1444.374,399.3882;Float;False;SF_ColorShift;-1;;10;f3651b3e85772604cb45f632c7f608e7;0;4;26;FLOAT;0;False;27;FLOAT;0;False;28;FLOAT;0;False;23;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;33;-1547.117,1383.604;Float;False;InstancedProperty;_SmoothnessIntensity;SmoothnessIntensity;26;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;32;-1834.335,1438.011;Float;False;InstancedProperty;_SmoothnessLow;SmoothnessLow;25;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;54;-1790.571,643.3954;Float;False;InstancedProperty;_EmissiveLightness;EmissiveLightness;17;0;Create;True;0;0;False;0;1;1;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;53;-1793.836,568.7406;Float;False;InstancedProperty;_EmissiveSaturation;EmissiveSaturation;16;0;Create;True;0;0;False;0;1;1;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-1254.057,1583.957;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;20;-2260.083,984.9456;Float;False;Property;_UseFlatMetallic;UseFlatMetallic;20;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-1836.005,1359.556;Float;False;InstancedProperty;_SmoothnessHigh;SmoothnessHigh;24;0;Create;True;0;0;False;0;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;64;-1085.697,463.0647;Float;True;Property;_RMOH;RMOH;31;0;Create;True;0;0;False;0;None;6d500ea017c2b29458bae5b0f0219f94;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-450.4983,330.6276;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_DefaultSurface;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;12;1;9;0
WireConnection;12;0;13;0
WireConnection;10;1;11;0
WireConnection;10;0;12;0
WireConnection;72;0;2;1
WireConnection;72;1;2;2
WireConnection;72;2;2;3
WireConnection;6;0;4;0
WireConnection;6;1;3;0
WireConnection;6;2;8;0
WireConnection;41;0;38;0
WireConnection;41;1;39;0
WireConnection;41;2;37;0
WireConnection;7;1;4;0
WireConnection;7;0;6;0
WireConnection;14;0;10;0
WireConnection;14;1;15;0
WireConnection;37;0;64;3
WireConnection;37;1;36;0
WireConnection;22;1;64;1
WireConnection;22;0;21;0
WireConnection;30;0;29;0
WireConnection;30;1;33;0
WireConnection;61;0;59;0
WireConnection;61;1;7;0
WireConnection;61;2;62;0
WireConnection;29;0;31;0
WireConnection;29;1;32;0
WireConnection;29;2;28;0
WireConnection;60;1;7;0
WireConnection;60;0;61;0
WireConnection;28;0;22;0
WireConnection;28;1;34;0
WireConnection;51;26;55;0
WireConnection;51;27;56;0
WireConnection;51;28;57;0
WireConnection;51;23;72;0
WireConnection;16;1;18;0
WireConnection;16;0;20;0
WireConnection;50;26;52;0
WireConnection;50;27;53;0
WireConnection;50;28;54;0
WireConnection;50;23;14;0
WireConnection;42;0;41;0
WireConnection;42;1;40;0
WireConnection;20;1;64;2
WireConnection;20;0;17;0
WireConnection;0;0;51;0
WireConnection;0;1;6;0
WireConnection;0;2;14;0
WireConnection;0;3;64;2
WireConnection;0;4;64;1
WireConnection;0;5;64;3
ASEEND*/
//CHKSM=850F8F37C090C9C47A5B09EF6D5F7634C56A00EC