// Upgrade NOTE: upgraded instancing buffer 'S_Boulder' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_Boulder"
{
	Properties
	{
		_Diff("Diff", 2D) = "white" {}
		_Norm("Norm", 2D) = "white" {}
		_Grass_Diff("Grass_Diff", 2D) = "white" {}
		_OSM("OSM", 2D) = "bump" {}
		_Grass_Smo("Grass_Smo", Range( 0 , 1)) = 0
		_GrassTile("Grass Tile", Range( 0 , 500)) = 0
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
		uniform sampler2D _OSM;
		uniform float4 _OSM_ST;

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
			float2 temp_cast_0 = (( _GrassTile_Instance * -0.1 )).xx;
			float3 ase_worldPos = i.worldPos;
			float4 appendResult16 = (float4(ase_worldPos.x , ase_worldPos.z , 0.0 , 0.0));
			float2 uv_TexCoord14 = i.uv_texcoord * temp_cast_0 + appendResult16.xy;
			float3 hsvTorgb13_g2 = RGBToHSV( tex2D( _Grass_Diff, uv_TexCoord14 ).rgb );
			float _Grass_Saturation_Instance = UNITY_ACCESS_INSTANCED_PROP(_Grass_Saturation_arr, _Grass_Saturation);
			float _Grass_Lightness_Instance = UNITY_ACCESS_INSTANCED_PROP(_Grass_Lightness_arr, _Grass_Lightness);
			float3 hsvTorgb17_g2 = HSVToRGB( float3(( _Grass_Hue_Instance + hsvTorgb13_g2.x ),( _Grass_Saturation_Instance * hsvTorgb13_g2.y ),( hsvTorgb13_g2.z * _Grass_Lightness_Instance )) );
			float2 uv_OSM = i.uv_texcoord * _OSM_ST.xy + _OSM_ST.zw;
			float4 tex2DNode2 = tex2D( _OSM, uv_OSM );
			float4 lerpResult6 = lerp( tex2D( _Diff, uv_Diff ) , float4( hsvTorgb17_g2 , 0.0 ) , tex2DNode2.b);
			o.Albedo = lerpResult6.rgb;
			o.Metallic = 0.0;
			float _Grass_Smo_Instance = UNITY_ACCESS_INSTANCED_PROP(_Grass_Smo_arr, _Grass_Smo);
			float lerpResult8 = lerp( tex2DNode2.g , _Grass_Smo_Instance , tex2DNode2.b);
			o.Smoothness = lerpResult8;
			o.Occlusion = tex2DNode2.r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1927;7;1266;958;1978.081;1325.949;2.12957;True;False
Node;AmplifyShaderEditor.WorldPosInputsNode;15;-1838.766,-793.1657;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;22;-1977.384,-904.8;Float;False;Constant;_Float0;Float 0;11;0;Create;True;0;0;False;0;-0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-2073.829,-1035.456;Float;False;InstancedProperty;_GrassTile;Grass Tile;5;0;Create;True;0;0;False;0;0;185;0;500;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;16;-1574.035,-769.3923;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-1772.223,-1020.834;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;14;-1374.595,-816.5056;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;9;-1085.971,-844.4841;Float;True;Property;_Grass_Diff;Grass_Diff;2;0;Create;True;0;0;False;0;None;2e2992173cd3b3e40a9b4389684c4ad5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;18;-726.3854,-1239.552;Float;False;InstancedProperty;_Grass_Hue;Grass_Hue;6;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-812.7342,-1126.831;Float;False;InstancedProperty;_Grass_Saturation;Grass_Saturation;7;0;Create;True;0;0;False;0;1;1.02;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-832.4003,-981.1403;Float;False;InstancedProperty;_Grass_Lightness;Grass_Lightness;8;0;Create;True;0;0;False;0;1;0.75;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-1041.277,-516.9289;Float;False;InstancedProperty;_Grass_Smo;Grass_Smo;4;0;Create;True;0;0;False;0;0;0.15;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-1044.392,-423.0715;Float;True;Property;_Diff;Diff;0;0;Create;True;0;0;False;0;None;c97ed815a3ec3334ca861f955d17199d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-1057.808,-26.02166;Float;True;Property;_OSM;OSM;3;0;Create;True;0;0;False;0;None;2f37e114633501746baa3632a8cbb174;True;0;True;bump;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;13;-597.0535,-1058.288;Float;False;SF_ColorShift;-1;;2;f3651b3e85772604cb45f632c7f608e7;0;4;26;FLOAT;0;False;27;FLOAT;0;False;28;FLOAT;0;False;23;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;8;-486.5323,-40.88153;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;6;-397.5272,-422.9235;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-176.3539,-266.8387;Float;False;Constant;_Met;Met;3;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;23;-1056.945,-231.4392;Float;True;Property;_Norm;Norm;1;0;Create;True;0;0;False;0;None;c97ed815a3ec3334ca861f955d17199d;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;110,-230;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_Boulder;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;16;0;15;1
WireConnection;16;1;15;3
WireConnection;21;0;20;0
WireConnection;21;1;22;0
WireConnection;14;0;21;0
WireConnection;14;1;16;0
WireConnection;9;1;14;0
WireConnection;13;26;18;0
WireConnection;13;27;17;0
WireConnection;13;28;19;0
WireConnection;13;23;9;0
WireConnection;8;0;2;2
WireConnection;8;1;11;0
WireConnection;8;2;2;3
WireConnection;6;0;1;0
WireConnection;6;1;13;0
WireConnection;6;2;2;3
WireConnection;0;0;6;0
WireConnection;0;1;23;0
WireConnection;0;3;4;0
WireConnection;0;4;8;0
WireConnection;0;5;2;1
ASEEND*/
//CHKSM=AF76DE2CA951C5B2F581D36A205F86329F8A442A