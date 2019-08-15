// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_TempleKitset"
{
	Properties
	{
		_Norm("Norm", 2D) = "bump" {}
		_Occ("Occ", 2D) = "white" {}
		_OccIntensity("Occ Intensity", Float) = 0
		_Diff("Diff", 2D) = "white" {}
		_MetallicMask("MetallicMask", 2D) = "white" {}
		[Toggle]_UseMet("UseMet", Float) = 0
		_FlatMet("Flat Met", Range( 0 , 1)) = 0
		_Smo("Smo", 2D) = "white" {}
		_MetallicValue("Metallic Value", Range( 0 , 1)) = 0
		_Hue("Hue", Float) = 0
		_Saturation("Saturation", Float) = 0
		_Lightness("Lightness", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
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
		uniform float _Saturation;
		uniform float _Lightness;
		uniform float _UseMet;
		uniform float _FlatMet;
		uniform float _MetallicValue;
		uniform sampler2D _MetallicMask;
		uniform float4 _MetallicMask_ST;
		uniform sampler2D _Smo;
		uniform float4 _Smo_ST;
		uniform sampler2D _Occ;
		uniform float4 _Occ_ST;
		uniform float _OccIntensity;


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
			float3 hsvTorgb13_g1 = RGBToHSV( tex2D( _Diff, uv_Diff ).rgb );
			float3 hsvTorgb17_g1 = HSVToRGB( float3(( _Hue + hsvTorgb13_g1.x ),( _Saturation * hsvTorgb13_g1.y ),( hsvTorgb13_g1.z * _Lightness )) );
			o.Albedo = hsvTorgb17_g1;
			float4 temp_cast_1 = (_FlatMet).xxxx;
			float2 uv_MetallicMask = i.uv_texcoord * _MetallicMask_ST.xy + _MetallicMask_ST.zw;
			o.Metallic = lerp(temp_cast_1,( _MetallicValue * tex2D( _MetallicMask, uv_MetallicMask ) ),_UseMet).r;
			float2 uv_Smo = i.uv_texcoord * _Smo_ST.xy + _Smo_ST.zw;
			o.Smoothness = tex2D( _Smo, uv_Smo ).r;
			float2 uv_Occ = i.uv_texcoord * _Occ_ST.xy + _Occ_ST.zw;
			float lerpResult8 = lerp( -1.0 , 1.0 , _OccIntensity);
			float4 clampResult10 = clamp( ( tex2D( _Occ, uv_Occ ) + lerpResult8 ) , float4( 0,0,0,0 ) , float4( 1,0,0,0 ) );
			o.Occlusion = clampResult10.r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1921;1;1278;970;1761.748;705.4677;1.820135;True;False
Node;AmplifyShaderEditor.RangedFloatNode;13;-1072.83,1004.764;Float;False;Property;_OccIntensity;Occ Intensity;2;0;Create;True;0;0;False;0;0;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-1036.599,920.7106;Float;False;Constant;_Float4;Float 4;2;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-1035.15,841.0044;Float;False;Constant;_Float3;Float 3;2;0;Create;True;0;0;False;0;-1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;8;-827.9144,871.4382;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-1221.23,587.9299;Float;True;Property;_Occ;Occ;1;0;Create;True;0;0;False;0;None;50c16348f03e36e489e90cd741e1f2fa;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;15;-1139.762,125.4814;Float;True;Property;_MetallicMask;MetallicMask;4;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;20;-1091.939,381.1526;Float;False;Property;_MetallicValue;Metallic Value;8;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;14;-642.5507,-481.7877;Float;True;Property;_Diff;Diff;3;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;17;-626.0806,411.4425;Float;False;Property;_FlatMet;Flat Met;6;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-205.5326,-241.3334;Float;False;Property;_Lightness;Lightness;11;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-209.1729,-448.8287;Float;False;Property;_Hue;Hue;9;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;-793.4372,357.491;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;9;-571.4049,823.6139;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-227.3743,-339.6207;Float;False;Property;_Saturation;Saturation;10;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;18;-594.7007,-180.8476;Float;True;Property;_Smo;Smo;7;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;10;-400.3985,667.0997;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;21;92.96949,-292.297;Float;False;SF_ColorShift;-1;;1;f3651b3e85772604cb45f632c7f608e7;0;4;26;FLOAT;0;False;27;FLOAT;0;False;28;FLOAT;0;False;23;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;1;-896.0578,-159.4982;Float;True;Property;_Norm;Norm;0;0;Create;True;0;0;False;0;None;cec58600938ff8544a17418de401c9a7;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ToggleSwitchNode;16;-368.502,198.5493;Float;False;Property;_UseMet;UseMet;5;0;Create;True;0;0;False;0;0;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_TempleKitset;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;8;0;11;0
WireConnection;8;1;12;0
WireConnection;8;2;13;0
WireConnection;19;0;20;0
WireConnection;19;1;15;0
WireConnection;9;0;2;0
WireConnection;9;1;8;0
WireConnection;10;0;9;0
WireConnection;21;26;22;0
WireConnection;21;27;23;0
WireConnection;21;28;24;0
WireConnection;21;23;14;0
WireConnection;16;0;17;0
WireConnection;16;1;19;0
WireConnection;0;0;21;0
WireConnection;0;1;1;0
WireConnection;0;3;16;0
WireConnection;0;4;18;0
WireConnection;0;5;10;0
ASEEND*/
//CHKSM=9DEA33030ACA5DC183086BFE6D53F714771314F0