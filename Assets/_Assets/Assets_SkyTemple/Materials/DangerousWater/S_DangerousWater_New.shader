// Upgrade NOTE: upgraded instancing buffer 'S_DangerousWater_New' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_DangerousWater_New"
{
	Properties
	{
		[Header(Refraction)]
		_ChromaticAberration("Chromatic Aberration", Range( 0 , 0.3)) = 0.1
		_Normal("Normal", 2D) = "bump" {}
		_NormalScale("Normal Scale", Float) = 0
		_ShalowColor("Shalow Color", Color) = (1,1,1,0)
		_Normal01_PanSpeed("Normal01_PanSpeed", Vector) = (0,0,0,0)
		_Normal02_PanSpeed("Normal02_PanSpeed", Vector) = (0,0,0,0)
		_UVScale("UVScale", Float) = 0
		_Opacity("Opacity", Float) = 0
		_Smoothness("Smoothness", Float) = 0
		_Refraction("Refraction", Float) = 0
		_Metallic("Metallic", Float) = 0
		_ReflectionColor("Reflection Color", Color) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Off
		GrabPass{ }
		CGINCLUDE
		#include "UnityStandardUtils.cginc"
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma multi_compile _ALPHAPREMULTIPLY_ON
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
			float3 worldNormal;
			INTERNAL_DATA
			float4 screenPos;
		};

		uniform sampler2D _Normal;
		uniform sampler2D _GrabTexture;
		uniform float _ChromaticAberration;

		UNITY_INSTANCING_BUFFER_START(S_DangerousWater_New)
			UNITY_DEFINE_INSTANCED_PROP(float4, _ShalowColor)
#define _ShalowColor_arr S_DangerousWater_New
			UNITY_DEFINE_INSTANCED_PROP(float4, _ReflectionColor)
#define _ReflectionColor_arr S_DangerousWater_New
			UNITY_DEFINE_INSTANCED_PROP(float2, _Normal01_PanSpeed)
#define _Normal01_PanSpeed_arr S_DangerousWater_New
			UNITY_DEFINE_INSTANCED_PROP(float2, _Normal02_PanSpeed)
#define _Normal02_PanSpeed_arr S_DangerousWater_New
			UNITY_DEFINE_INSTANCED_PROP(float, _NormalScale)
#define _NormalScale_arr S_DangerousWater_New
			UNITY_DEFINE_INSTANCED_PROP(float, _UVScale)
#define _UVScale_arr S_DangerousWater_New
			UNITY_DEFINE_INSTANCED_PROP(float, _Metallic)
#define _Metallic_arr S_DangerousWater_New
			UNITY_DEFINE_INSTANCED_PROP(float, _Smoothness)
#define _Smoothness_arr S_DangerousWater_New
			UNITY_DEFINE_INSTANCED_PROP(float, _Opacity)
#define _Opacity_arr S_DangerousWater_New
			UNITY_DEFINE_INSTANCED_PROP(float, _Refraction)
#define _Refraction_arr S_DangerousWater_New
		UNITY_INSTANCING_BUFFER_END(S_DangerousWater_New)

		inline float4 Refraction( Input i, SurfaceOutputStandard o, float indexOfRefraction, float chomaticAberration ) {
			float3 worldNormal = o.Normal;
			float4 screenPos = i.screenPos;
			#if UNITY_UV_STARTS_AT_TOP
				float scale = -1.0;
			#else
				float scale = 1.0;
			#endif
			float halfPosW = screenPos.w * 0.5;
			screenPos.y = ( screenPos.y - halfPosW ) * _ProjectionParams.x * scale + halfPosW;
			#if SHADER_API_D3D9 || SHADER_API_D3D11
				screenPos.w += 0.00000000001;
			#endif
			float2 projScreenPos = ( screenPos / screenPos.w ).xy;
			float3 worldViewDir = normalize( UnityWorldSpaceViewDir( i.worldPos ) );
			float3 refractionOffset = ( ( ( ( indexOfRefraction - 1.0 ) * mul( UNITY_MATRIX_V, float4( worldNormal, 0.0 ) ) ) * ( 1.0 / ( screenPos.z + 1.0 ) ) ) * ( 1.0 - dot( worldNormal, worldViewDir ) ) );
			float2 cameraRefraction = float2( refractionOffset.x, -( refractionOffset.y * _ProjectionParams.x ) );
			float4 redAlpha = tex2D( _GrabTexture, ( projScreenPos + cameraRefraction ) );
			float green = tex2D( _GrabTexture, ( projScreenPos + ( cameraRefraction * ( 1.0 - chomaticAberration ) ) ) ).g;
			float blue = tex2D( _GrabTexture, ( projScreenPos + ( cameraRefraction * ( 1.0 + chomaticAberration ) ) ) ).b;
			return float4( redAlpha.r, green, blue, redAlpha.a );
		}

		void RefractionF( Input i, SurfaceOutputStandard o, inout half4 color )
		{
			#ifdef UNITY_PASS_FORWARDBASE
			float _Refraction_Instance = UNITY_ACCESS_INSTANCED_PROP(_Refraction_arr, _Refraction);
			color.rgb = color.rgb + Refraction( i, o, _Refraction_Instance, _ChromaticAberration ) * ( 1 - color.a );
			color.a = 1;
			#endif
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Normal = float3(0,0,1);
			float _NormalScale_Instance = UNITY_ACCESS_INSTANCED_PROP(_NormalScale_arr, _NormalScale);
			float2 _Normal01_PanSpeed_Instance = UNITY_ACCESS_INSTANCED_PROP(_Normal01_PanSpeed_arr, _Normal01_PanSpeed);
			float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );
			float4 appendResult81 = (float4(ase_objectScale.x , ase_objectScale.y , 0.0 , 0.0));
			float _UVScale_Instance = UNITY_ACCESS_INSTANCED_PROP(_UVScale_arr, _UVScale);
			float4 temp_output_85_0 = ( ( float4( i.uv_texcoord, 0.0 , 0.0 ) * appendResult81 ) * _UVScale_Instance );
			float2 panner35 = ( _Time.y * _Normal01_PanSpeed_Instance + temp_output_85_0.xy);
			float3 tex2DNode37 = UnpackScaleNormal( tex2D( _Normal, panner35 ), _NormalScale_Instance );
			float2 _Normal02_PanSpeed_Instance = UNITY_ACCESS_INSTANCED_PROP(_Normal02_PanSpeed_arr, _Normal02_PanSpeed);
			float2 panner36 = ( _Time.y * _Normal02_PanSpeed_Instance + temp_output_85_0.xy);
			o.Normal = BlendNormals( tex2DNode37 , UnpackScaleNormal( tex2D( _Normal, panner36 ), _NormalScale_Instance ) );
			float4 _ShalowColor_Instance = UNITY_ACCESS_INSTANCED_PROP(_ShalowColor_arr, _ShalowColor);
			float4 _ReflectionColor_Instance = UNITY_ACCESS_INSTANCED_PROP(_ReflectionColor_arr, _ReflectionColor);
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float fresnelNdotV123 = dot( (WorldNormalVector( i , tex2DNode37 )), ase_worldViewDir );
			float fresnelNode123 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV123, 5.0 ) );
			float4 lerpResult128 = lerp( _ShalowColor_Instance , _ReflectionColor_Instance , fresnelNode123);
			o.Albedo = lerpResult128.rgb;
			float _Metallic_Instance = UNITY_ACCESS_INSTANCED_PROP(_Metallic_arr, _Metallic);
			o.Metallic = _Metallic_Instance;
			float _Smoothness_Instance = UNITY_ACCESS_INSTANCED_PROP(_Smoothness_arr, _Smoothness);
			o.Smoothness = _Smoothness_Instance;
			float _Opacity_Instance = UNITY_ACCESS_INSTANCED_PROP(_Opacity_arr, _Opacity);
			o.Alpha = _Opacity_Instance;
			o.Normal = o.Normal + 0.00001 * i.screenPos * i.worldPos;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha finalcolor:RefractionF fullforwardshadows exclude_path:deferred 

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
				float4 screenPos : TEXCOORD2;
				float4 tSpace0 : TEXCOORD3;
				float4 tSpace1 : TEXCOORD4;
				float4 tSpace2 : TEXCOORD5;
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
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.screenPos = ComputeScreenPos( o.pos );
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
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				surfIN.screenPos = IN.screenPos;
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
1934;96;1266;964;3638.498;1655.359;2.858003;True;False
Node;AmplifyShaderEditor.ObjectScaleNode;80;-3908.164,-73.89185;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DynamicAppendNode;81;-3727.368,-72.79682;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;83;-3797.018,-193.5366;Float;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;84;-3549.018,-193.5366;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;89;-3547.79,-76.71613;Float;False;InstancedProperty;_UVScale;UVScale;10;0;Create;True;0;0;False;0;0;64.34;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;85;-3388.799,-192.4127;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.Vector2Node;55;-3032.224,-718.955;Float;False;InstancedProperty;_Normal01_PanSpeed;Normal01_PanSpeed;8;0;Create;True;0;0;False;0;0,0;0.05,0.02;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TimeNode;54;-3004.183,-868.8718;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;35;-2712.026,-719.486;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;39;-2712.705,-807.1456;Float;False;InstancedProperty;_NormalScale;Normal Scale;3;0;Create;True;0;0;False;0;0;0.25;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;56;-3027.418,-462.4742;Float;False;InstancedProperty;_Normal02_PanSpeed;Normal02_PanSpeed;9;0;Create;True;0;0;False;0;0,0;-0.005,0.02;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SamplerNode;37;-2449.924,-824.9337;Float;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;True;Instance;38;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;36;-2714.24,-594.478;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WorldNormalVector;124;-1576.047,-1306.301;Float;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.CommentaryNode;21;-2613.298,78.6972;Float;False;1063.09;864.0524;Depth Controls;8;13;15;17;16;18;19;20;14;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;125;-1664.184,-1098.8;Float;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SamplerNode;38;-2453.931,-626.7536;Float;True;Property;_Normal;Normal;2;0;Create;True;0;0;False;0;None;759cb64ffbd2e404db48fc61910d01f0;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;127;-1286.09,-1432.199;Float;False;InstancedProperty;_ReflectionColor;Reflection Color;21;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FresnelNode;123;-1349.417,-1240.791;Float;True;Standard;WorldNormal;ViewDir;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;15;-2290.292,340.1227;Float;False;InstancedProperty;_ShalowColor;Shalow Color;5;0;Create;True;0;0;False;0;1,1,1,0;0,0.7968903,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;12;-3590.594,240.5741;Float;False;915.2477;285.8345;Screen Depth;4;8;9;10;11;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleAddOpNode;16;-2297.406,644.5934;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BlendNormalsNode;40;-2118.475,-831.878;Float;False;0;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;96;-2479.611,-376.8445;Float;False;InstancedProperty;_FoamColor;Foam Color;12;0;Create;True;0;0;False;0;0,0,0,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;101;-2312.748,-1382.497;Float;True;Property;_WAterFoamMask;WAterFoamMask;15;0;Create;True;0;0;False;0;None;7b5189b03ec71094f9f9077d7b76d522;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;105;-1701.553,-435.5722;Float;False;InstancedProperty;_Opacity;Opacity;16;0;Create;True;0;0;False;0;0;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;120;-1012.746,-801.6823;Float;False;InstancedProperty;_Smoothness;Smoothness;18;0;Create;True;0;0;False;0;0;0.8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;90;-2672.552,-194.9151;Float;True;Property;_FoamAlpha;Foam Alpha;11;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;107;-2032.795,-461.0236;Float;False;InstancedProperty;_FoamIntensity;Foam Intensity;17;0;Create;True;0;0;False;0;0;0.22;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;93;-3229.072,-25.42033;Float;False;InstancedProperty;_FoamTiling;Foam Tiling;13;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;119;-1510.499,-593.9586;Float;False;InstancedProperty;_Refraction;Refraction;19;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;98;-1888.308,-1018.17;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;106;-1876.041,-605.3083;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;100;-1803.42,-789.17;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;18;-2067.597,720.6207;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;121;-1176.546,-822.4823;Float;False;InstancedProperty;_Metallic;Metallic;20;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;92;-3036.984,-88.34602;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;128;-976.3271,-1277.313;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;8;-3540.594,319.4086;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;14;-1734.208,440.9465;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ScreenDepthNode;9;-3282.686,290.5741;Float;False;0;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;10;-2991.138,378.6794;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-2547.949,753.4507;Float;False;InstancedProperty;_WaterDepth;Water Depth;6;0;Create;True;0;0;False;0;0;8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;13;-2541.559,128.6972;Float;False;InstancedProperty;_Color;Color;4;0;Create;True;0;0;False;0;0,0,0,0;0,0.757643,0.9509999,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.AbsOpNode;11;-2829.345,377.0774;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;94;-2098.794,-293.2071;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-2268.032,827.7498;Float;False;InstancedProperty;_WaterFalloff;Water Falloff;7;0;Create;True;0;0;False;0;0;-0.45;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;20;-1861.979,729.26;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;97;-2501.377,-1091.319;Float;True;Property;_WaterColormask;WaterColormask;14;0;Create;True;0;0;False;0;None;4b7e59b23e2465d45b198be8ba2ff831;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-699.9362,-974.2162;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_DangerousWater_New;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;0;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;81;0;80;1
WireConnection;81;1;80;2
WireConnection;84;0;83;0
WireConnection;84;1;81;0
WireConnection;85;0;84;0
WireConnection;85;1;89;0
WireConnection;35;0;85;0
WireConnection;35;2;55;0
WireConnection;35;1;54;2
WireConnection;37;1;35;0
WireConnection;37;5;39;0
WireConnection;36;0;85;0
WireConnection;36;2;56;0
WireConnection;36;1;54;2
WireConnection;124;0;37;0
WireConnection;38;1;36;0
WireConnection;38;5;39;0
WireConnection;123;0;124;0
WireConnection;123;4;125;0
WireConnection;16;0;11;0
WireConnection;16;1;17;0
WireConnection;40;0;37;0
WireConnection;40;1;38;0
WireConnection;101;1;36;0
WireConnection;90;1;35;0
WireConnection;98;0;15;0
WireConnection;98;1;13;0
WireConnection;98;2;97;0
WireConnection;106;0;101;1
WireConnection;106;1;107;0
WireConnection;100;0;98;0
WireConnection;100;1;96;0
WireConnection;100;2;106;0
WireConnection;18;0;16;0
WireConnection;18;1;19;0
WireConnection;92;0;93;0
WireConnection;128;0;15;0
WireConnection;128;1;127;0
WireConnection;128;2;123;0
WireConnection;14;0;13;0
WireConnection;14;1;15;0
WireConnection;14;2;20;0
WireConnection;9;0;8;0
WireConnection;10;0;9;0
WireConnection;10;1;8;4
WireConnection;11;0;10;0
WireConnection;94;0;13;0
WireConnection;94;1;96;0
WireConnection;94;2;90;0
WireConnection;20;0;18;0
WireConnection;97;1;35;0
WireConnection;0;0;128;0
WireConnection;0;1;40;0
WireConnection;0;3;121;0
WireConnection;0;4;120;0
WireConnection;0;8;119;0
WireConnection;0;9;105;0
ASEEND*/
//CHKSM=414DAE4DF3CDAEA4BCC260A16349FAA810D2B8E4