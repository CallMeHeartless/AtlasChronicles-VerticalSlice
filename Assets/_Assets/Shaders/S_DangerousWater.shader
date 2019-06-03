// Upgrade NOTE: upgraded instancing buffer 'S_DangerousWater' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_DangerousWater"
{
	Properties
	{
		[Header(Refraction)]
		_ChromaticAberration("Chromatic Aberration", Range( 0 , 0.3)) = 0.1
		_Normal("Normal", 2D) = "bump" {}
		_NormalScale("Normal Scale", Float) = 0
		_DeepColor("Deep Color", Color) = (0,0,0,0)
		_ShalowColor("Shalow Color", Color) = (1,1,1,0)
		_WaterDepth("Water Depth", Float) = 0
		_WaterFalloff("Water Falloff", Float) = 0
		_WaterSmoothness("Water Smoothness", Float) = 0
		_Normal01_PanSpeed("Normal01_PanSpeed", Vector) = (0,0,0,0)
		_Normal02_PanSpeed("Normal02_PanSpeed", Vector) = (0,0,0,0)
		_NormalTile0("NormalTile 0", Vector) = (1,1,0,0)
		_Opacity("Opacity", Float) = 0
		_Float2("Float 2", Float) = 0
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
		#include "UnityCG.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma multi_compile _ALPHAPREMULTIPLY_ON
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
			float3 worldPos;
		};

		uniform sampler2D _Normal;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform sampler2D _GrabTexture;
		uniform float _ChromaticAberration;

		UNITY_INSTANCING_BUFFER_START(S_DangerousWater)
			UNITY_DEFINE_INSTANCED_PROP(float4, _DeepColor)
#define _DeepColor_arr S_DangerousWater
			UNITY_DEFINE_INSTANCED_PROP(float4, _ShalowColor)
#define _ShalowColor_arr S_DangerousWater
			UNITY_DEFINE_INSTANCED_PROP(float2, _Normal01_PanSpeed)
#define _Normal01_PanSpeed_arr S_DangerousWater
			UNITY_DEFINE_INSTANCED_PROP(float2, _NormalTile0)
#define _NormalTile0_arr S_DangerousWater
			UNITY_DEFINE_INSTANCED_PROP(float2, _Normal02_PanSpeed)
#define _Normal02_PanSpeed_arr S_DangerousWater
			UNITY_DEFINE_INSTANCED_PROP(float, _NormalScale)
#define _NormalScale_arr S_DangerousWater
			UNITY_DEFINE_INSTANCED_PROP(float, _WaterDepth)
#define _WaterDepth_arr S_DangerousWater
			UNITY_DEFINE_INSTANCED_PROP(float, _WaterFalloff)
#define _WaterFalloff_arr S_DangerousWater
			UNITY_DEFINE_INSTANCED_PROP(float, _WaterSmoothness)
#define _WaterSmoothness_arr S_DangerousWater
			UNITY_DEFINE_INSTANCED_PROP(float, _Opacity)
#define _Opacity_arr S_DangerousWater
			UNITY_DEFINE_INSTANCED_PROP(float, _Float2)
#define _Float2_arr S_DangerousWater
		UNITY_INSTANCING_BUFFER_END(S_DangerousWater)

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
			float _Float2_Instance = UNITY_ACCESS_INSTANCED_PROP(_Float2_arr, _Float2);
			color.rgb = color.rgb + Refraction( i, o, _Float2_Instance, _ChromaticAberration ) * ( 1 - color.a );
			color.a = 1;
			#endif
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Normal = float3(0,0,1);
			float _NormalScale_Instance = UNITY_ACCESS_INSTANCED_PROP(_NormalScale_arr, _NormalScale);
			float2 _Normal01_PanSpeed_Instance = UNITY_ACCESS_INSTANCED_PROP(_Normal01_PanSpeed_arr, _Normal01_PanSpeed);
			float2 _NormalTile0_Instance = UNITY_ACCESS_INSTANCED_PROP(_NormalTile0_arr, _NormalTile0);
			float2 uv_TexCoord34 = i.uv_texcoord * _NormalTile0_Instance;
			float2 panner35 = ( _Time.y * _Normal01_PanSpeed_Instance + uv_TexCoord34);
			float2 _Normal02_PanSpeed_Instance = UNITY_ACCESS_INSTANCED_PROP(_Normal02_PanSpeed_arr, _Normal02_PanSpeed);
			float2 panner36 = ( _Time.y * _Normal02_PanSpeed_Instance + uv_TexCoord34);
			float3 temp_output_40_0 = BlendNormals( UnpackScaleNormal( tex2D( _Normal, panner35 ), _NormalScale_Instance ) , UnpackScaleNormal( tex2D( _Normal, panner36 ), _NormalScale_Instance ) );
			o.Normal = temp_output_40_0;
			float4 _DeepColor_Instance = UNITY_ACCESS_INSTANCED_PROP(_DeepColor_arr, _DeepColor);
			float4 _ShalowColor_Instance = UNITY_ACCESS_INSTANCED_PROP(_ShalowColor_arr, _ShalowColor);
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float eyeDepth9 = LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture,UNITY_PROJ_COORD( ase_screenPosNorm ))));
			float temp_output_11_0 = abs( ( eyeDepth9 - ase_screenPosNorm.w ) );
			float _WaterDepth_Instance = UNITY_ACCESS_INSTANCED_PROP(_WaterDepth_arr, _WaterDepth);
			float _WaterFalloff_Instance = UNITY_ACCESS_INSTANCED_PROP(_WaterFalloff_arr, _WaterFalloff);
			float temp_output_20_0 = saturate( pow( ( temp_output_11_0 + _WaterDepth_Instance ) , _WaterFalloff_Instance ) );
			float4 lerpResult14 = lerp( _DeepColor_Instance , _ShalowColor_Instance , temp_output_20_0);
			o.Albedo = lerpResult14.rgb;
			float _WaterSmoothness_Instance = UNITY_ACCESS_INSTANCED_PROP(_WaterSmoothness_arr, _WaterSmoothness);
			o.Smoothness = _WaterSmoothness_Instance;
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
				float3 worldPos : TEXCOORD2;
				float4 screenPos : TEXCOORD3;
				float4 tSpace0 : TEXCOORD4;
				float4 tSpace1 : TEXCOORD5;
				float4 tSpace2 : TEXCOORD6;
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
				o.worldPos = worldPos;
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
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
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
1927;7;1266;958;526.2211;286.9367;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;12;-3610.626,327.3815;Float;False;915.2477;285.8345;Screen Depth;4;8;9;10;11;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;8;-3560.626,406.216;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenDepthNode;9;-3302.718,377.3815;Float;False;0;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;10;-3011.171,465.4868;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;21;-2628.664,-204.473;Float;False;1063.09;864.0524;Depth Controls;8;13;15;17;16;18;19;20;14;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;57;-3110.246,-563.1016;Float;False;InstancedProperty;_NormalTile0;NormalTile 0;16;0;Create;True;0;0;False;0;1,1;0.5,0.5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;17;-2563.316,470.2803;Float;False;InstancedProperty;_WaterDepth;Water Depth;7;0;Create;True;0;0;False;0;0;38.82;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;11;-2849.378,463.8848;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;41;-2892.256,-836.9761;Float;False;1357.936;592.1338;Normals;9;34;35;36;38;37;39;40;54;55;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TimeNode;54;-2767.888,-431.9736;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;34;-2842.256,-583.4793;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;16;-2312.773,361.4231;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;56;-3015.021,-405.9595;Float;False;InstancedProperty;_Normal02_PanSpeed;Normal02_PanSpeed;15;0;Create;True;0;0;False;0;0,0;-0.005,0.02;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;55;-2752.279,-740.2373;Float;False;InstancedProperty;_Normal01_PanSpeed;Normal01_PanSpeed;14;0;Create;True;0;0;False;0;0,0;0.05,0.02;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;19;-2283.399,544.5794;Float;False;InstancedProperty;_WaterFalloff;Water Falloff;8;0;Create;True;0;0;False;0;0;-0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;18;-2082.964,437.4503;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;35;-2429.388,-643.569;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;39;-2429.564,-359.8423;Float;False;InstancedProperty;_NormalScale;Normal Scale;4;0;Create;True;0;0;False;0;0;0.21;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;36;-2460.86,-484.8335;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;15;-2305.659,56.95245;Float;False;InstancedProperty;_ShalowColor;Shalow Color;6;0;Create;True;0;0;False;0;1,1,1,0;0.1650941,1,0.9268927,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;37;-2108.035,-786.9761;Float;True;Property;_TextureSample0;Texture Sample 0;3;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;True;Instance;38;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;49;-1406.523,-1138.115;Float;False;1050.035;562.2715;Refraction;6;44;45;42;47;46;43;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;38;-2115.832,-543.3128;Float;True;Property;_Normal;Normal;3;0;Create;True;0;0;False;0;None;759cb64ffbd2e404db48fc61910d01f0;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;13;-2556.925,-154.473;Float;False;InstancedProperty;_DeepColor;Deep Color;5;0;Create;True;0;0;False;0;0,0,0,0;0.01481216,0,0.3962262,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;31;-2540.29,929.9532;Float;False;1107.178;606.7319;Foam;9;22;23;24;25;26;27;28;30;29;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SaturateNode;20;-1877.345,446.0896;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;76;-47.77473,859.3743;Float;False;InstancedProperty;_CellsIntensity;Cells Intensity;22;0;Create;True;0;0;False;0;0;0.08;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;60;-410.9106,772.619;Float;True;Property;_TextureSample1;Texture Sample 1;18;0;Create;True;0;0;False;0;7b5189b03ec71094f9f9077d7b76d522;7b5189b03ec71094f9f9077d7b76d522;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;62;-658.1516,806.7605;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;63;-632.0445,1058;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;77;-845.7493,871.4475;Float;False;Property;_FoamPan;Foam Pan;23;0;Create;True;0;0;False;0;0,0;0.41,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.LerpOp;78;-71.77785,618.0621;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;30;-2440.144,1335.351;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;70;-760.1629,603.4587;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;79;-255.7778,581.2623;Float;False;Constant;_Float0;Float 0;23;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;14;-1749.574,157.7763;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;71;-977.5059,589.8257;Float;False;InstancedProperty;_FoamTiling;Foam Tiling;19;0;Create;True;0;0;False;0;1;0.61;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;75;88.85178,656.7797;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;72;-938.7915,677.5782;Float;False;Constant;_Float1;Float 1;18;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;29;-2192.275,1337.036;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;23;-2196.512,979.9532;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;74;32.48322,264.438;Float;False;InstancedProperty;_Float2;Float 2;20;0;Create;True;0;0;False;0;0;1.03;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;25;-1999.786,1017.112;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenColorNode;43;-558.4885,-893.0073;Float;False;Global;_GrabScreen0;Grab Screen 0;6;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;26;-1784.219,1085.795;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;33;-1623.909,746.0374;Float;False;Constant;_Color0;Color 0;12;0;Create;True;0;0;False;0;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GrabScreenPosition;42;-1356.523,-1088.115;Float;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;50;-654.0938,326.7059;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;61;-428.3889,1065.458;Float;True;Property;_TextureSample2;Texture Sample 2;21;0;Create;True;0;0;False;0;bf264c122c0b06e44989fc25d2985f3a;bf264c122c0b06e44989fc25d2985f3a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;52;-1019.447,224.7215;Float;False;InstancedProperty;_WaterSmoothness;Water Smoothness;9;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;53;-1044.73,309.6011;Float;False;InstancedProperty;_FoamSmoothness;Foam Smoothness;10;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;59;-428.2372,337.6335;Float;False;InstancedProperty;_Opacity;Opacity;17;0;Create;True;0;0;False;0;0;0.6;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-2285.035,1164.748;Float;False;InstancedProperty;_FoamFalloff;Foam Falloff;13;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;68;-536.0532,41.81067;Float;False;Constant;_Color2;Color 2;18;0;Create;True;0;0;False;0;0,0.1992587,0.6509434,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BlendNormalsNode;40;-1766.321,-625.0132;Float;False;0;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;67;-532.8721,-112.4656;Float;False;Constant;_Color1;Color 1;18;0;Create;True;0;0;False;0;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;22;-2490.29,1008.383;Float;False;InstancedProperty;_FoamDepth;Foam Depth;12;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;47;-981.0811,-1054.092;Float;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;45;-1190.53,-690.8438;Float;False;InstancedProperty;_Distortion;Distortion;11;0;Create;True;0;0;False;0;0.5;0.05;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;46;-740.7546,-891.0143;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;32;-1256.958,651.6696;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;66;-169.9484,-60.40196;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;-945.8683,-801.7577;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;28;-1915.742,1306.685;Float;True;Property;_Foam;Foam;2;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;48;-182.7595,-342.6074;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;73;3.381966,427.0931;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-1602.111,1156.615;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;237.8862,-6.897979;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_DangerousWater;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;0;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;9;0;8;0
WireConnection;10;0;9;0
WireConnection;10;1;8;4
WireConnection;11;0;10;0
WireConnection;34;0;57;0
WireConnection;16;0;11;0
WireConnection;16;1;17;0
WireConnection;18;0;16;0
WireConnection;18;1;19;0
WireConnection;35;0;34;0
WireConnection;35;2;55;0
WireConnection;35;1;54;2
WireConnection;36;0;34;0
WireConnection;36;2;56;0
WireConnection;36;1;54;2
WireConnection;37;1;35;0
WireConnection;37;5;39;0
WireConnection;38;1;36;0
WireConnection;38;5;39;0
WireConnection;20;0;18;0
WireConnection;60;1;62;0
WireConnection;62;0;70;0
WireConnection;62;2;77;0
WireConnection;62;1;54;1
WireConnection;63;0;70;0
WireConnection;63;2;55;0
WireConnection;63;1;54;2
WireConnection;78;0;60;1
WireConnection;78;1;61;1
WireConnection;78;2;79;0
WireConnection;70;0;71;0
WireConnection;70;1;72;0
WireConnection;14;0;13;0
WireConnection;14;1;15;0
WireConnection;14;2;20;0
WireConnection;75;0;78;0
WireConnection;75;1;76;0
WireConnection;29;0;30;0
WireConnection;23;0;11;0
WireConnection;23;1;22;0
WireConnection;25;0;23;0
WireConnection;25;1;24;0
WireConnection;43;0;46;0
WireConnection;26;0;25;0
WireConnection;50;0;52;0
WireConnection;50;1;53;0
WireConnection;50;2;27;0
WireConnection;61;1;63;0
WireConnection;40;0;37;0
WireConnection;40;1;38;0
WireConnection;47;0;42;0
WireConnection;46;0;47;0
WireConnection;46;1;44;0
WireConnection;32;0;14;0
WireConnection;32;1;33;0
WireConnection;32;2;27;0
WireConnection;66;0;14;0
WireConnection;66;1;67;0
WireConnection;66;2;75;0
WireConnection;44;0;40;0
WireConnection;44;1;45;0
WireConnection;28;1;29;0
WireConnection;48;0;14;0
WireConnection;48;1;43;0
WireConnection;48;2;20;0
WireConnection;73;0;66;0
WireConnection;73;1;67;0
WireConnection;73;2;61;1
WireConnection;27;0;26;0
WireConnection;27;1;28;1
WireConnection;0;0;14;0
WireConnection;0;1;40;0
WireConnection;0;4;52;0
WireConnection;0;8;74;0
WireConnection;0;9;59;0
ASEEND*/
//CHKSM=164DCBA8630673672F3EF0CBBE774D0A1EFE7424