// Upgrade NOTE: upgraded instancing buffer 'S_Grass' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_Grass"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Diffuse("Diffuse", 2D) = "white" {}
		_WindDirection("Wind Direction", Float) = 0
		_Hue("Hue", Range( -1 , 1)) = 0
		_BendAmount("BendAmount", Float) = 0
		_Saturation("Saturation", Range( 0 , 2)) = 1
		_WorldFrequency("WorldFrequency", Float) = 0
		_Lightness("Lightness", Range( 0 , 5)) = 1
		_Speed("Speed", Float) = 0
		_Smo("Smo", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "GrassBillboard"  "Queue" = "Geometry+0" }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#pragma multi_compile_instancing
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform sampler2D _Diffuse;
		uniform float4 _Diffuse_ST;
		uniform float _Cutoff = 0.5;

		UNITY_INSTANCING_BUFFER_START(S_Grass)
			UNITY_DEFINE_INSTANCED_PROP(float, _WindDirection)
#define _WindDirection_arr S_Grass
			UNITY_DEFINE_INSTANCED_PROP(float, _WorldFrequency)
#define _WorldFrequency_arr S_Grass
			UNITY_DEFINE_INSTANCED_PROP(float, _Speed)
#define _Speed_arr S_Grass
			UNITY_DEFINE_INSTANCED_PROP(float, _BendAmount)
#define _BendAmount_arr S_Grass
			UNITY_DEFINE_INSTANCED_PROP(float, _Hue)
#define _Hue_arr S_Grass
			UNITY_DEFINE_INSTANCED_PROP(float, _Saturation)
#define _Saturation_arr S_Grass
			UNITY_DEFINE_INSTANCED_PROP(float, _Lightness)
#define _Lightness_arr S_Grass
			UNITY_DEFINE_INSTANCED_PROP(float, _Smo)
#define _Smo_arr S_Grass
		UNITY_INSTANCING_BUFFER_END(S_Grass)


		float3 RotateAroundAxis( float3 center, float3 original, float3 u, float angle )
		{
			original -= center;
			float C = cos( angle );
			float S = sin( angle );
			float t = 1 - C;
			float m00 = t * u.x * u.x + C;
			float m01 = t * u.x * u.y - S * u.z;
			float m02 = t * u.x * u.z + S * u.y;
			float m10 = t * u.x * u.y + S * u.z;
			float m11 = t * u.y * u.y + C;
			float m12 = t * u.y * u.z - S * u.x;
			float m20 = t * u.x * u.z - S * u.y;
			float m21 = t * u.y * u.z + S * u.x;
			float m22 = t * u.z * u.z + C;
			float3x3 finalMatrix = float3x3( m00, m01, m02, m10, m11, m12, m20, m21, m22 );
			return mul( finalMatrix, original ) + center;
		}


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

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float _WindDirection_Instance = UNITY_ACCESS_INSTANCED_PROP(_WindDirection_arr, _WindDirection);
			float3 ase_vertex3Pos = v.vertex.xyz;
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float _WorldFrequency_Instance = UNITY_ACCESS_INSTANCED_PROP(_WorldFrequency_arr, _WorldFrequency);
			float _Speed_Instance = UNITY_ACCESS_INSTANCED_PROP(_Speed_arr, _Speed);
			float _BendAmount_Instance = UNITY_ACCESS_INSTANCED_PROP(_BendAmount_arr, _BendAmount);
			float temp_output_25_0 = ( ( ase_vertex3Pos.y * cos( ( ( ( ase_worldPos.x + ase_worldPos.z ) * _WorldFrequency_Instance ) + ( _Time.y * _Speed_Instance ) ) ) ) * _BendAmount_Instance );
			float4 appendResult27 = (float4(temp_output_25_0 , 0.0 , temp_output_25_0 , 0.0));
			float4 break30 = mul( appendResult27, unity_ObjectToWorld );
			float4 appendResult34 = (float4(break30.x , 0 , break30.z , 0.0));
			float3 rotatedValue32 = RotateAroundAxis( float3( 0,0,0 ), appendResult34.xyz, float3( 0,0,0 ), _WindDirection_Instance );
			v.vertex.xyz += rotatedValue32;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float _Hue_Instance = UNITY_ACCESS_INSTANCED_PROP(_Hue_arr, _Hue);
			float2 uv_Diffuse = i.uv_texcoord * _Diffuse_ST.xy + _Diffuse_ST.zw;
			float4 tex2DNode37 = tex2D( _Diffuse, uv_Diffuse );
			float4 appendResult50 = (float4(tex2DNode37.r , tex2DNode37.g , tex2DNode37.b , 0.0));
			float3 hsvTorgb13_g2 = RGBToHSV( appendResult50.rgb );
			float _Saturation_Instance = UNITY_ACCESS_INSTANCED_PROP(_Saturation_arr, _Saturation);
			float _Lightness_Instance = UNITY_ACCESS_INSTANCED_PROP(_Lightness_arr, _Lightness);
			float3 hsvTorgb17_g2 = HSVToRGB( float3(( _Hue_Instance + hsvTorgb13_g2.x ),( _Saturation_Instance * hsvTorgb13_g2.y ),( hsvTorgb13_g2.z * _Lightness_Instance )) );
			o.Albedo = hsvTorgb17_g2;
			float _Smo_Instance = UNITY_ACCESS_INSTANCED_PROP(_Smo_arr, _Smo);
			o.Smoothness = _Smo_Instance;
			o.Alpha = tex2DNode37.a;
			clip( tex2DNode37.a - _Cutoff );
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows vertex:vertexDataFunc 

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
				vertexDataFunc( v, customInputData );
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
				surfIN.worldPos = worldPos;
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
1927;1;1266;964;1349.828;652.3794;2.227353;True;False
Node;AmplifyShaderEditor.WorldPosInputsNode;16;-2760.657,-227.9431;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;36;-2912.47,475.4436;Float;False;InstancedProperty;_Speed;Speed;9;0;Create;True;0;0;False;0;0;1.05;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;17;-2493.457,-189.543;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-2797.457,-15.1431;Float;False;InstancedProperty;_WorldFrequency;WorldFrequency;7;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;20;-2899.015,197.9231;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-2379.857,-147.9432;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;-2591.73,285.7583;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;19;-2251.857,-43.94314;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;23;-2167.056,-242.3433;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CosOpNode;21;-2079.057,188.0568;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-1911.057,178.4565;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-1951.057,407.2566;Float;False;InstancedProperty;_BendAmount;BendAmount;5;0;Create;True;0;0;False;0;0;0.24;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-1659.858,103.2568;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;27;-1471.057,80.8567;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ObjectToWorldMatrixNode;29;-1671.057,314.4568;Float;False;0;1;FLOAT4x4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-1328.657,96.85682;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4x4;0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1;False;1;FLOAT4;0
Node;AmplifyShaderEditor.Vector3Node;33;-1567.68,-87.65828;Float;False;Constant;_Vector0;Vector 0;0;0;Create;True;0;0;False;0;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.BreakToComponentsNode;30;-1114.676,137.5495;Float;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SamplerNode;37;-1681.729,-817.761;Float;True;Property;_Diffuse;Diffuse;1;0;Create;True;0;0;False;0;None;72888060a31f6754cb0e4d2b1bd712e1;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;50;-1286.991,-778.0212;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;53;-873.3838,-821.5461;Float;False;InstancedProperty;_Saturation;Saturation;6;0;Create;True;0;0;False;0;1;0.756;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;34;-813.1055,118.1349;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-1209.159,410.1776;Float;False;InstancedProperty;_WindDirection;Wind Direction;3;0;Create;True;0;0;False;0;0;0.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;54;-879.5536,-735.1675;Float;False;InstancedProperty;_Lightness;Lightness;8;0;Create;True;0;0;False;0;1;0.75;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;52;-874.9257,-900.2122;Float;False;InstancedProperty;_Hue;Hue;4;0;Create;True;0;0;False;0;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;40;-1475.658,-390.5356;Float;True;Property;_Opacity;Opacity;2;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;48;-1258.802,-128.1623;Float;False;InstancedProperty;_Float0;Float 0;11;0;Create;True;0;0;False;0;0;12.51;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;46;-693.9789,-324.1898;Float;False;InstancedProperty;_Smo;Smo;10;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RotateAboutAxisNode;32;-798.8677,300.631;Float;False;False;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;51;-530.9542,-628.7367;Float;False;SF_ColorShift;-1;;2;f3651b3e85772604cb45f632c7f608e7;0;4;26;FLOAT;0;False;27;FLOAT;0;False;28;FLOAT;0;False;23;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleContrastOpNode;47;-1091.806,-204.7021;Float;False;2;1;COLOR;0,0,0,0;False;0;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;56;241.056,-64.9724;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_Grass;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;GrassBillboard;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;17;0;16;1
WireConnection;17;1;16;3
WireConnection;24;0;17;0
WireConnection;24;1;18;0
WireConnection;35;0;20;2
WireConnection;35;1;36;0
WireConnection;19;0;24;0
WireConnection;19;1;35;0
WireConnection;21;0;19;0
WireConnection;22;0;23;2
WireConnection;22;1;21;0
WireConnection;25;0;22;0
WireConnection;25;1;26;0
WireConnection;27;0;25;0
WireConnection;27;2;25;0
WireConnection;28;0;27;0
WireConnection;28;1;29;0
WireConnection;30;0;28;0
WireConnection;50;0;37;1
WireConnection;50;1;37;2
WireConnection;50;2;37;3
WireConnection;34;0;30;0
WireConnection;34;1;33;2
WireConnection;34;2;30;2
WireConnection;32;1;31;0
WireConnection;32;3;34;0
WireConnection;51;26;52;0
WireConnection;51;27;53;0
WireConnection;51;28;54;0
WireConnection;51;23;50;0
WireConnection;47;1;40;1
WireConnection;47;0;48;0
WireConnection;56;0;51;0
WireConnection;56;4;46;0
WireConnection;56;9;37;4
WireConnection;56;10;37;4
WireConnection;56;11;32;0
ASEEND*/
//CHKSM=743E6908E7E7C71BC3C6F478FF3D6417782EF998