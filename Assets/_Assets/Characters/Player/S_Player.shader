// Upgrade NOTE: upgraded instancing buffer 'S_Player' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_Player"
{
	Properties
	{
		_Diff("Diff", 2D) = "white" {}
		_Norm("Norm", 2D) = "bump" {}
		_Hue("Hue", Range( -1 , 1)) = 0
		_Smoothness("Smoothness", Float) = 0
		_Metallic("Metallic", Float) = 0
		_Saturation("Saturation", Range( 0 , 2)) = 1
		_Lightness("Lightness", Range( 0 , 5)) = 1
		[Toggle(_USENORM_ON)] _UseNorm("UseNorm", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Off
		CGPROGRAM
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma shader_feature _USENORM_ON
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Norm;
		uniform float4 _Norm_ST;
		uniform float _Hue;
		uniform sampler2D _Diff;
		uniform float4 _Diff_ST;

		UNITY_INSTANCING_BUFFER_START(S_Player)
			UNITY_DEFINE_INSTANCED_PROP(float, _Saturation)
#define _Saturation_arr S_Player
			UNITY_DEFINE_INSTANCED_PROP(float, _Lightness)
#define _Lightness_arr S_Player
			UNITY_DEFINE_INSTANCED_PROP(float, _Smoothness)
#define _Smoothness_arr S_Player
			UNITY_DEFINE_INSTANCED_PROP(float, _Metallic)
#define _Metallic_arr S_Player
		UNITY_INSTANCING_BUFFER_END(S_Player)


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
			float4 color13 = IsGammaSpace() ? float4(0,0.07058824,0.9960785,1) : float4(0,0.006048833,0.9911022,1);
			float2 uv_Norm = i.uv_texcoord * _Norm_ST.xy + _Norm_ST.zw;
			#ifdef _USENORM_ON
				float4 staticSwitch12 = float4( UnpackNormal( tex2D( _Norm, uv_Norm ) ) , 0.0 );
			#else
				float4 staticSwitch12 = color13;
			#endif
			o.Normal = staticSwitch12.rgb;
			float2 uv_Diff = i.uv_texcoord * _Diff_ST.xy + _Diff_ST.zw;
			float3 hsvTorgb13_g1 = RGBToHSV( tex2D( _Diff, uv_Diff ).rgb );
			float _Saturation_Instance = UNITY_ACCESS_INSTANCED_PROP(_Saturation_arr, _Saturation);
			float _Lightness_Instance = UNITY_ACCESS_INSTANCED_PROP(_Lightness_arr, _Lightness);
			float3 hsvTorgb17_g1 = HSVToRGB( float3(( _Hue + hsvTorgb13_g1.x ),( _Saturation_Instance * hsvTorgb13_g1.y ),( hsvTorgb13_g1.z * _Lightness_Instance )) );
			o.Albedo = hsvTorgb17_g1;
			float _Smoothness_Instance = UNITY_ACCESS_INSTANCED_PROP(_Smoothness_arr, _Smoothness);
			o.Metallic = _Smoothness_Instance;
			float _Metallic_Instance = UNITY_ACCESS_INSTANCED_PROP(_Metallic_arr, _Metallic);
			o.Smoothness = _Metallic_Instance;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1921;1;1278;970;1175.954;249.0878;1.215587;True;True
Node;AmplifyShaderEditor.RangedFloatNode;7;-647.2678,-496.2184;Float;False;Property;_Hue;Hue;2;0;Create;True;0;0;False;0;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-662.8402,-397.9973;Float;False;InstancedProperty;_Saturation;Saturation;5;0;Create;True;0;0;False;0;1;1;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-680.8076,-284.2039;Float;False;InstancedProperty;_Lightness;Lightness;6;0;Create;True;0;0;False;0;1;1;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-604.5616,-86.94638;Float;True;Property;_Diff;Diff;0;0;Create;True;0;0;False;0;f25b33b2754fdee40bb432a92012dd1a;e223a15e39406f148a02ec366c6e87fb;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;10;-828.0934,175.3916;Float;True;Property;_Norm;Norm;1;0;Create;True;0;0;False;0;f25b33b2754fdee40bb432a92012dd1a;b2f3cc3d775f2dc498eb5e588a89af2d;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;13;-581.532,459.5993;Float;False;Constant;_NormColor;NormColor;8;0;Create;True;0;0;False;0;0,0.07058824,0.9960785,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;6;-302.2944,-225.5105;Float;False;SF_ColorShift;-1;;1;f3651b3e85772604cb45f632c7f608e7;0;4;26;FLOAT;0;False;27;FLOAT;0;False;28;FLOAT;0;False;23;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-377.7573,118.2655;Float;False;InstancedProperty;_Smoothness;Smoothness;3;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-354.7897,201.7177;Float;False;InstancedProperty;_Metallic;Metallic;4;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;12;-323.8276,352.6277;Float;False;Property;_UseNorm;UseNorm;7;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_Player;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;6;26;7;0
WireConnection;6;27;8;0
WireConnection;6;28;9;0
WireConnection;6;23;2;0
WireConnection;12;1;13;0
WireConnection;12;0;10;0
WireConnection;0;0;6;0
WireConnection;0;1;12;0
WireConnection;0;3;4;0
WireConnection;0;4;5;0
ASEEND*/
//CHKSM=8F116B1043B14CE056BC874A6AA1D72D61E12FB6