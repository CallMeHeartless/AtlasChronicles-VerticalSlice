// Upgrade NOTE: upgraded instancing buffer 'S_KillBoxClouds' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_KillBoxClouds"
{
	Properties
	{
		_TransparencyMask("TransparencyMask", 2D) = "white" {}
		_TransparencyMultiplier("TransparencyMultiplier", Float) = 0
		_CloudColor("CloudColor", Color) = (0,0,0,0)
		_CloudEmission("CloudEmission", Color) = (0,0,0,0)
		_Tiling("Tiling", Float) = 0
		_PanSpeedU("Pan Speed U", Float) = 0
		_PanSpeedV("Pan Speed V", Float) = 0
		_Float0("Float 0", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#pragma multi_compile_instancing
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _TransparencyMask;

		UNITY_INSTANCING_BUFFER_START(S_KillBoxClouds)
			UNITY_DEFINE_INSTANCED_PROP(float4, _CloudColor)
#define _CloudColor_arr S_KillBoxClouds
			UNITY_DEFINE_INSTANCED_PROP(float4, _CloudEmission)
#define _CloudEmission_arr S_KillBoxClouds
			UNITY_DEFINE_INSTANCED_PROP(float, _PanSpeedU)
#define _PanSpeedU_arr S_KillBoxClouds
			UNITY_DEFINE_INSTANCED_PROP(float, _PanSpeedV)
#define _PanSpeedV_arr S_KillBoxClouds
			UNITY_DEFINE_INSTANCED_PROP(float, _Tiling)
#define _Tiling_arr S_KillBoxClouds
			UNITY_DEFINE_INSTANCED_PROP(float, _Float0)
#define _Float0_arr S_KillBoxClouds
			UNITY_DEFINE_INSTANCED_PROP(float, _TransparencyMultiplier)
#define _TransparencyMultiplier_arr S_KillBoxClouds
		UNITY_INSTANCING_BUFFER_END(S_KillBoxClouds)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 _CloudColor_Instance = UNITY_ACCESS_INSTANCED_PROP(_CloudColor_arr, _CloudColor);
			o.Albedo = _CloudColor_Instance.rgb;
			float4 _CloudEmission_Instance = UNITY_ACCESS_INSTANCED_PROP(_CloudEmission_arr, _CloudEmission);
			o.Emission = _CloudEmission_Instance.rgb;
			float _PanSpeedU_Instance = UNITY_ACCESS_INSTANCED_PROP(_PanSpeedU_arr, _PanSpeedU);
			float _PanSpeedV_Instance = UNITY_ACCESS_INSTANCED_PROP(_PanSpeedV_arr, _PanSpeedV);
			float4 appendResult19 = (float4(_PanSpeedU_Instance , _PanSpeedV_Instance , 0.0 , 0.0));
			float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );
			float4 appendResult10 = (float4(ase_objectScale.x , ase_objectScale.y , 0.0 , 0.0));
			float _Tiling_Instance = UNITY_ACCESS_INSTANCED_PROP(_Tiling_arr, _Tiling);
			float2 panner16 = ( 1.0 * _Time.y * appendResult19.xy + ( ( float4( i.uv_texcoord, 0.0 , 0.0 ) * appendResult10 ) * _Tiling_Instance ).xy);
			float _Float0_Instance = UNITY_ACCESS_INSTANCED_PROP(_Float0_arr, _Float0);
			float cos22 = cos( radians( _Float0_Instance ) );
			float sin22 = sin( radians( _Float0_Instance ) );
			float2 rotator22 = mul( panner16 - float2( 0.5,0.5 ) , float2x2( cos22 , -sin22 , sin22 , cos22 )) + float2( 0.5,0.5 );
			float _TransparencyMultiplier_Instance = UNITY_ACCESS_INSTANCED_PROP(_TransparencyMultiplier_arr, _TransparencyMultiplier);
			float4 temp_output_2_0 = ( tex2D( _TransparencyMask, rotator22 ) * _TransparencyMultiplier_Instance );
			o.Alpha = temp_output_2_0.r;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1927;1;1266;964;3014.33;735.4468;1.597725;True;False
Node;AmplifyShaderEditor.ObjectScaleNode;9;-3125.226,367.8322;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DynamicAppendNode;10;-2944.43,368.9272;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;11;-3014.08,248.1875;Float;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-2766.08,248.1875;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-2764.852,365.0079;Float;False;InstancedProperty;_Tiling;Tiling;4;0;Create;True;0;0;False;0;0;64.34;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-2498.7,-11.80286;Float;False;InstancedProperty;_PanSpeedV;Pan Speed V;6;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-2493.505,-85.8353;Float;False;InstancedProperty;_PanSpeedU;Pan Speed U;5;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;19;-2149.319,58.33319;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-1908.704,127.3247;Float;False;InstancedProperty;_Float0;Float 0;7;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-2605.861,249.3114;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.Vector2Node;28;-1879.945,-78.7818;Float;False;Constant;_Vector0;Vector 0;8;0;Create;True;0;0;False;0;0.5,0.5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PannerNode;16;-1929.088,-277.762;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RadiansOpNode;27;-1747.334,144.8997;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;22;-1624.646,-140.2537;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-868.3782,259.2105;Float;False;InstancedProperty;_TransparencyMultiplier;TransparencyMultiplier;1;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-1261.92,69.82849;Float;True;Property;_TransparencyMask;TransparencyMask;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;6;-743.2214,-71.79626;Float;False;InstancedProperty;_CloudEmission;CloudEmission;3;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;-659.2346,153.8153;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;5;-789.3318,-274.3526;Float;False;InstancedProperty;_CloudColor;CloudColor;2;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_KillBoxClouds;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;10;0;9;1
WireConnection;10;1;9;2
WireConnection;12;0;11;0
WireConnection;12;1;10;0
WireConnection;19;0;21;0
WireConnection;19;1;20;0
WireConnection;14;0;12;0
WireConnection;14;1;13;0
WireConnection;16;0;14;0
WireConnection;16;2;19;0
WireConnection;27;0;24;0
WireConnection;22;0;16;0
WireConnection;22;1;28;0
WireConnection;22;2;27;0
WireConnection;1;1;22;0
WireConnection;2;0;1;0
WireConnection;2;1;3;0
WireConnection;0;0;5;0
WireConnection;0;2;6;0
WireConnection;0;9;2;0
ASEEND*/
//CHKSM=03756D2E3266FB3E9CB9E32142B15A0B1633DAA0