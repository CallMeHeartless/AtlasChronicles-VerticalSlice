// Upgrade NOTE: upgraded instancing buffer 'S_Boulder' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_Boulder"
{
	Properties
	{
		_Diff("Diff", 2D) = "white" {}
		_Mask("Mask", 2D) = "white" {}
		_Grass_Diff("Grass_Diff", 2D) = "white" {}
		_Norm("Norm", 2D) = "bump" {}
		_Smo("Smo", 2D) = "white" {}
		_Occ("Occ", 2D) = "white" {}
		_Grass_Smo("Grass_Smo", Range( 0 , 1)) = 0
		_GrassTile("Grass Tile", Float) = 0
		_Grass_Hue("Grass_Hue", Float) = 1
		_Grass_Saturation("Grass_Saturation", Float) = 1
		_Grass_Lightness("Grass_Lightness", Float) = 1
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
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform sampler2D _Norm;
		uniform float4 _Norm_ST;
		uniform sampler2D _Diff;
		uniform float4 _Diff_ST;
		uniform sampler2D _Grass_Diff;
		uniform sampler2D _Mask;
		uniform float4 _Mask_ST;
		uniform sampler2D _Smo;
		uniform float4 _Smo_ST;
		uniform sampler2D _Occ;
		uniform float4 _Occ_ST;

		UNITY_INSTANCING_BUFFER_START(S_Boulder)
			UNITY_DEFINE_INSTANCED_PROP(float, _Grass_Hue)
#define _Grass_Hue_arr S_Boulder
			UNITY_DEFINE_INSTANCED_PROP(float, _GrassTile)
#define _GrassTile_arr S_Boulder
			UNITY_DEFINE_INSTANCED_PROP(float, _Grass_Saturation)
#define _Grass_Saturation_arr S_Boulder
			UNITY_DEFINE_INSTANCED_PROP(float, _Grass_Lightness)
#define _Grass_Lightness_arr S_Boulder
			UNITY_DEFINE_INSTANCED_PROP(float, _Grass_Smo)
#define _Grass_Smo_arr S_Boulder
		UNITY_INSTANCING_BUFFER_END(S_Boulder)


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
			float2 uv_Norm = i.uv_texcoord * _Norm_ST.xy + _Norm_ST.zw;
			o.Normal = UnpackNormal( tex2D( _Norm, uv_Norm ) );
			float2 uv_Diff = i.uv_texcoord * _Diff_ST.xy + _Diff_ST.zw;
			float _Grass_Hue_Instance = UNITY_ACCESS_INSTANCED_PROP(_Grass_Hue_arr, _Grass_Hue);
			float _GrassTile_Instance = UNITY_ACCESS_INSTANCED_PROP(_GrassTile_arr, _GrassTile);
			float2 temp_cast_0 = (_GrassTile_Instance).xx;
			float3 ase_worldPos = i.worldPos;
			float4 appendResult16 = (float4(ase_worldPos.x , ase_worldPos.z , 0.0 , 0.0));
			float2 uv_TexCoord14 = i.uv_texcoord * temp_cast_0 + appendResult16.xy;
			float3 hsvTorgb13_g2 = RGBToHSV( tex2D( _Grass_Diff, uv_TexCoord14 ).rgb );
			float _Grass_Saturation_Instance = UNITY_ACCESS_INSTANCED_PROP(_Grass_Saturation_arr, _Grass_Saturation);
			float _Grass_Lightness_Instance = UNITY_ACCESS_INSTANCED_PROP(_Grass_Lightness_arr, _Grass_Lightness);
			float3 hsvTorgb17_g2 = HSVToRGB( float3(( _Grass_Hue_Instance + hsvTorgb13_g2.x ),( _Grass_Saturation_Instance * hsvTorgb13_g2.y ),( hsvTorgb13_g2.z * _Grass_Lightness_Instance )) );
			float2 uv_Mask = i.uv_texcoord * _Mask_ST.xy + _Mask_ST.zw;
			float4 tex2DNode7 = tex2D( _Mask, uv_Mask );
			float4 lerpResult6 = lerp( tex2D( _Diff, uv_Diff ) , float4( hsvTorgb17_g2 , 0.0 ) , tex2DNode7);
			o.Albedo = lerpResult6.rgb;
			o.Metallic = 0.0;
			float2 uv_Smo = i.uv_texcoord * _Smo_ST.xy + _Smo_ST.zw;
			float _Grass_Smo_Instance = UNITY_ACCESS_INSTANCED_PROP(_Grass_Smo_arr, _Grass_Smo);
			float4 temp_cast_5 = (_Grass_Smo_Instance).xxxx;
			float4 lerpResult8 = lerp( tex2D( _Smo, uv_Smo ) , temp_cast_5 , tex2DNode7);
			o.Smoothness = lerpResult8.r;
			float2 uv_Occ = i.uv_texcoord * _Occ_ST.xy + _Occ_ST.zw;
			o.Occlusion = tex2D( _Occ, uv_Occ ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1927;1;1266;964;2656.753;1532.439;1.3;True;False
Node;AmplifyShaderEditor.WorldPosInputsNode;15;-2015.338,-808.3005;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;20;-2021.699,-924.4672;Float;False;InstancedProperty;_GrassTile;Grass Tile;7;0;Create;True;0;0;False;0;0;100;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;16;-1767.424,-730.7146;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;14;-1537.714,-777.8279;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;18;-491.3251,-869.8655;Float;False;InstancedProperty;_Grass_Hue;Grass_Hue;8;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-577.6738,-757.1451;Float;False;InstancedProperty;_Grass_Saturation;Grass_Saturation;9;0;Create;True;0;0;False;0;1;1.02;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-597.34,-611.4542;Float;False;InstancedProperty;_Grass_Lightness;Grass_Lightness;10;0;Create;True;0;0;False;0;1;0.75;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;9;-1022.802,-844.4841;Float;True;Property;_Grass_Diff;Grass_Diff;2;0;Create;True;0;0;False;0;None;2e2992173cd3b3e40a9b4389684c4ad5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-1044.392,-423.0715;Float;True;Property;_Diff;Diff;0;0;Create;True;0;0;False;0;None;ef5a5c7dd54bf5946a31589b0e1c99c1;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-1047.166,8.000259;Float;True;Property;_Smo;Smo;4;0;Create;True;0;0;False;0;None;b8401d3442aee7944af2b93add554333;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;7;-1042.444,412.5406;Float;True;Property;_Mask;Mask;1;0;Create;True;0;0;False;0;None;0d986e89265430042bce714dcdfee32c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;11;-936.5683,-523.3396;Float;False;InstancedProperty;_Grass_Smo;Grass_Smo;6;0;Create;True;0;0;False;0;0;0.1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;13;-361.9931,-688.6014;Float;False;SF_ColorShift;-1;;2;f3651b3e85772604cb45f632c7f608e7;0;4;26;FLOAT;0;False;27;FLOAT;0;False;28;FLOAT;0;False;23;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;6;-446.5072,-518.7541;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-187.0385,-138.6236;Float;False;Constant;_Met;Met;3;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-1044.986,-199.1118;Float;True;Property;_Norm;Norm;3;0;Create;True;0;0;False;0;None;367a759d4368d8c40a03b0f1acbde534;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;5;-1036.193,206.1198;Float;True;Property;_Occ;Occ;5;0;Create;True;0;0;False;0;None;325d33acae42fc74e9429ff14da4d8fe;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;8;-445.9307,-57.97687;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;110,-230;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_Boulder;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;16;0;15;1
WireConnection;16;1;15;3
WireConnection;14;0;20;0
WireConnection;14;1;16;0
WireConnection;9;1;14;0
WireConnection;13;26;18;0
WireConnection;13;27;17;0
WireConnection;13;28;19;0
WireConnection;13;23;9;0
WireConnection;6;0;1;0
WireConnection;6;1;13;0
WireConnection;6;2;7;0
WireConnection;8;0;3;0
WireConnection;8;1;11;0
WireConnection;8;2;7;0
WireConnection;0;0;6;0
WireConnection;0;1;2;0
WireConnection;0;3;4;0
WireConnection;0;4;8;0
WireConnection;0;5;5;0
ASEEND*/
//CHKSM=E5B519FF21996A623CA427522C31D05B79E534F0