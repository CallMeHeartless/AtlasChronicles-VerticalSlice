// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_Water"
{
	Properties
	{
		_WaterColor("Water Color", Color) = (0.49,0.87,1,1)
		_FoamColor("Foam Color", Color) = (0,0,0,0)
		_Opacity("Opacity", Range( 0 , 1)) = 1
		[Header(Refraction)]
		_ChromaticAberration("Chromatic Aberration", Range( 0 , 0.3)) = 0.1
		_WaveHeight("Wave Height", Range( 0 , 5)) = 0
		_WaveGuide("Wave Guide", 2D) = "white" {}
		_WaveSpeed("Wave Speed", Range( 0 , 5)) = 0
		_Refraction("Refraction", Range( 0.5 , 1.5)) = 1
		_Foam("Foam", 2D) = "white" {}
		_FoamDistance("Foam Distance", Range( 0 , 1)) = 0
		_FoamDistortion("Foam Distortion", 2D) = "white" {}
		_EdgeFoamTile("Edge Foam Tile", Vector) = (0,0,0,0)
		_FoamTile("Foam Tile", Vector) = (0,0,0,0)
		_FoamSpeedDivide("Foam Speed Divide", Float) = 0
		_MIN("MIN", Float) = 0
		_MAX("MAX", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Back
		GrabPass{ }
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#pragma multi_compile _ALPHAPREMULTIPLY_ON
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
			float3 worldPos;
		};

		uniform sampler2D _WaveGuide;
		uniform float _WaveSpeed;
		uniform float _WaveHeight;
		uniform float4 _WaterColor;
		uniform sampler2D _FoamDistortion;
		uniform float _FoamSpeedDivide;
		uniform float2 _FoamTile;
		uniform float4 _FoamColor;
		uniform sampler2D _Foam;
		uniform float2 _EdgeFoamTile;
		uniform float _MIN;
		uniform float _MAX;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _FoamDistance;
		uniform float _Opacity;
		uniform sampler2D _GrabTexture;
		uniform float _ChromaticAberration;
		uniform float _Refraction;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float Speed7 = ( _Time.x * _WaveSpeed );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float2 uv_TexCoord13 = v.texcoord.xy + ( Speed7 + (ase_vertex3Pos).xyz ).xy;
			float3 ase_vertexNormal = v.normal.xyz;
			float3 VertexAnimation20 = ( ( tex2Dlod( _WaveGuide, float4( uv_TexCoord13, 0, 0.0) ).r - 0.0 ) * ( ase_vertexNormal * _WaveHeight ) );
			v.vertex.xyz += VertexAnimation20;
		}

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
			color.rgb = color.rgb + Refraction( i, o, _Refraction, _ChromaticAberration ) * ( 1 - color.a );
			color.a = 1;
			#endif
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Normal = float3(0,0,1);
			float4 Albedo8 = _WaterColor;
			o.Albedo = Albedo8.rgb;
			float Speed7 = ( _Time.x * _WaveSpeed );
			float temp_output_65_0 = ( Speed7 / _FoamSpeedDivide );
			float2 uv_TexCoord42 = i.uv_texcoord * _FoamTile;
			float2 panner43 = ( temp_output_65_0 * float2( 0,0 ) + uv_TexCoord42);
			float cos45 = cos( temp_output_65_0 );
			float sin45 = sin( temp_output_65_0 );
			float2 rotator45 = mul( panner43 - float2( 0,0 ) , float2x2( cos45 , -sin45 , sin45 , cos45 )) + float2( 0,0 );
			float4 tex2DNode48 = tex2D( _FoamDistortion, rotator45 );
			float2 uv_TexCoord62 = i.uv_texcoord * _EdgeFoamTile;
			float2 panner63 = ( temp_output_65_0 * float2( 0,0 ) + uv_TexCoord62);
			float cos64 = cos( temp_output_65_0 );
			float sin64 = sin( temp_output_65_0 );
			float2 rotator64 = mul( panner63 - float2( 0,0 ) , float2x2( cos64 , -sin64 , sin64 , cos64 )) + float2( 0,0 );
			float4 color61 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
			float clampResult49 = clamp( tex2DNode48.r , _MIN , _MAX );
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float screenDepth47 = LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture,UNITY_PROJ_COORD( ase_screenPos ))));
			float distanceDepth47 = abs( ( screenDepth47 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _FoamDistance ) );
			float clampResult56 = clamp( ( clampResult49 * distanceDepth47 ) , 0.0 , 1.0 );
			float clampResult73 = clamp( tex2DNode48.r , 0.0 , 1.0 );
			float clampResult71 = clamp( ( clampResult73 * distanceDepth47 ) , 0.0 , 1.0 );
			float lerpResult74 = lerp( clampResult56 , clampResult71 , 0.0);
			float4 lerpResult55 = lerp( ( tex2DNode48.r * ( _FoamColor * tex2D( _Foam, rotator64 ).r ) ) , color61 , lerpResult74);
			float4 Emissive41 = lerpResult55;
			o.Emission = Emissive41.rgb;
			o.Alpha = _Opacity;
			o.Normal = o.Normal + 0.00001 * i.screenPos * i.worldPos;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha finalcolor:RefractionF fullforwardshadows exclude_path:deferred vertex:vertexDataFunc 

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
				vertexDataFunc( v, customInputData );
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
504;57;1266;964;3850.208;-346.1592;1.736795;True;False
Node;AmplifyShaderEditor.CommentaryNode;26;-2062.95,-226.7624;Float;False;709.3965;278.0725;Wave Time;4;7;5;6;4;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-2038.238,-29.97235;Float;False;Property;_WaveSpeed;Wave Speed;7;0;Create;True;0;0;False;0;0;3;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;4;-1975.139,-176.7624;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-1733.859,-48.60718;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;57;-3505.177,1232.786;Float;False;2179.31;787.3802;Emissive Foam;25;42;43;44;45;48;54;52;53;49;50;47;46;56;55;41;61;62;63;64;65;66;68;69;74;75;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;7;-1567.051,-32.65533;Float;False;Speed;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;66;-3479.913,1831.88;Float;False;Property;_FoamSpeedDivide;Foam Speed Divide;16;0;Create;True;0;0;False;0;0;21.78;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;44;-3405.693,1646.099;Float;False;7;Speed;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;67;-3714.458,1550.386;Float;False;Property;_FoamTile;Foam Tile;15;0;Create;True;0;0;False;0;0,0;0.2,0.2;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;42;-3455.177,1514.266;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;65;-3244.913,1755.88;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;43;-3192.501,1514.324;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;27;-3271.152,152.6224;Float;False;1928.762;526.3345;Wave Maker;12;20;19;17;18;16;15;14;13;12;9;10;11;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;59;-3715.569,1403.366;Float;False;Property;_EdgeFoamTile;Edge Foam Tile;14;0;Create;True;0;0;False;0;0,0;0.2,0.2;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PosVertexDataNode;10;-3242.502,226.0417;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;62;-3431.679,1354.184;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RotatorNode;45;-2985.197,1605.07;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;63;-3169.003,1354.242;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;11;-2995.558,304.0295;Float;False;True;True;True;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;69;-2800.37,1922.926;Float;False;Property;_MAX;MAX;18;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;9;-2965.089,202.6224;Float;False;7;Speed;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;46;-2860.763,2001.566;Float;False;Property;_FoamDistance;Foam Distance;12;0;Create;True;0;0;False;0;0;0.65;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;48;-2754.153,1673.272;Float;True;Property;_FoamDistortion;Foam Distortion;13;0;Create;True;0;0;False;0;None;cd460ee4ac5c1e746b7a734cc7cc64dd;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;68;-2630.771,1876.426;Float;False;Property;_MIN;MIN;17;0;Create;True;0;0;False;0;0;0.16;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;64;-2961.699,1444.988;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ClampOpNode;73;-2240.257,2136.943;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;49;-2419.858,1676.512;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;12;-2757.311,286.6803;Float;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DepthFade;47;-2543.511,1981.923;Float;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;52;-2669.139,1282.786;Float;False;Property;_FoamColor;Foam Color;1;0;Create;True;0;0;False;0;0,0,0,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;13;-2595.357,241.5081;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;50;-2221.75,1675.497;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;72;-2042.149,2135.927;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;54;-2752.413,1467.879;Float;True;Property;_Foam;Foam;11;0;Create;True;0;0;False;0;None;9fbef4b79ca3b784ba023cb1331520d5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;53;-2393.028,1450.519;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;14;-2314.496,215.2183;Float;True;Property;_WaveGuide;Wave Guide;6;0;Create;True;0;0;False;0;None;31890676c5b178840848afa665cb5a2f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;71;-1853.466,2137.262;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;56;-2033.067,1676.831;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-2300.498,593.3123;Float;False;Property;_WaveHeight;Wave Height;5;0;Create;True;0;0;False;0;0;0.7;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;16;-2212.54,432.4958;Float;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;75;-2078.673,1377.816;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;74;-1644.337,1820.082;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-1980.93,432.4783;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;25;-1871.483,-541.6321;Float;False;515.4661;225.3899;Color;2;8;3;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ColorNode;61;-2185.359,1498.369;Float;False;Constant;_Color0;Color 0;14;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;15;-1990.999,243.1848;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;3;-1846.771,-491.6319;Float;False;Property;_WaterColor;Water Color;0;0;Create;True;0;0;False;0;0.49,0.87,1,1;0.04313724,0.4470589,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;-1775.729,428.1295;Float;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;55;-1782.217,1455.416;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;8;-1565.3,-491.1602;Float;False;Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;39;-2826.616,706.2662;Float;False;1485.876;490.1007;Normal;10;38;28;29;30;31;32;33;35;37;36;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;41;-1565.868,1454.933;Float;False;Emissive;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;20;-1585.713,424.2086;Float;False;VertexAnimation;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CrossProductOpNode;31;-2365.492,828.5187;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;21;-363.3249,305.9796;Float;False;20;VertexAnimation;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;22;-359.787,-101.6022;Float;False;8;Albedo;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ToggleSwitchNode;35;-1819.917,820.0245;Float;False;Property;_LowPoly;LowPoly?;9;0;Create;True;0;0;False;0;1;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;38;-1580.74,819.9486;Float;False;Normal;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.UnpackScaleNormalNode;37;-2154.903,969.6582;Float;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DdxOpNode;29;-2527.493,756.2662;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;58;-357.9133,53.32407;Float;False;41;Emissive;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.WorldPosInputsNode;28;-2776.616,760.8354;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DdyOpNode;30;-2527.074,850.6295;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-368.2073,233.0753;Float;False;Property;_Opacity;Opacity;2;0;Create;True;0;0;False;0;1;0.7;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-362.9077,153.9853;Float;False;Property;_Refraction;Refraction;8;0;Create;True;0;0;False;0;1;1.3;0.5;1.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.NormalizeNode;33;-2013.655,828.5184;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;36;-2487.224,966.3677;Float;True;Property;_Normal;Normal;10;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;40;-353.8898,-27.40375;Float;False;38;Normal;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;32;-2169.012,828.5186;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_Water;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Translucent;0.5;True;True;0;False;Opaque;;Transparent;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;3;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.3;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;5;0;4;1
WireConnection;5;1;6;0
WireConnection;7;0;5;0
WireConnection;42;0;67;0
WireConnection;65;0;44;0
WireConnection;65;1;66;0
WireConnection;43;0;42;0
WireConnection;43;1;65;0
WireConnection;62;0;59;0
WireConnection;45;0;43;0
WireConnection;45;2;65;0
WireConnection;63;0;62;0
WireConnection;63;1;65;0
WireConnection;11;0;10;0
WireConnection;48;1;45;0
WireConnection;64;0;63;0
WireConnection;64;2;65;0
WireConnection;73;0;48;1
WireConnection;49;0;48;1
WireConnection;49;1;68;0
WireConnection;49;2;69;0
WireConnection;12;0;9;0
WireConnection;12;1;11;0
WireConnection;47;0;46;0
WireConnection;13;1;12;0
WireConnection;50;0;49;0
WireConnection;50;1;47;0
WireConnection;72;0;73;0
WireConnection;72;1;47;0
WireConnection;54;1;64;0
WireConnection;53;0;52;0
WireConnection;53;1;54;1
WireConnection;14;1;13;0
WireConnection;71;0;72;0
WireConnection;56;0;50;0
WireConnection;75;0;48;1
WireConnection;75;1;53;0
WireConnection;74;0;56;0
WireConnection;74;1;71;0
WireConnection;18;0;16;0
WireConnection;18;1;17;0
WireConnection;15;0;14;1
WireConnection;19;0;15;0
WireConnection;19;1;18;0
WireConnection;55;0;75;0
WireConnection;55;1;61;0
WireConnection;55;2;74;0
WireConnection;8;0;3;0
WireConnection;41;0;55;0
WireConnection;20;0;19;0
WireConnection;31;0;29;0
WireConnection;31;1;30;0
WireConnection;35;0;33;0
WireConnection;35;1;37;0
WireConnection;38;0;35;0
WireConnection;37;0;36;0
WireConnection;29;0;28;0
WireConnection;30;0;28;0
WireConnection;33;0;32;0
WireConnection;32;0;31;0
WireConnection;0;0;22;0
WireConnection;0;2;58;0
WireConnection;0;8;24;0
WireConnection;0;9;23;0
WireConnection;0;11;21;0
ASEEND*/
//CHKSM=C44E22E8AA3ACF7F70EDB15563A09D4CDF924160