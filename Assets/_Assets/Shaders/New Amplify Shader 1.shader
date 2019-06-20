// Upgrade NOTE: upgraded instancing buffer 'S_Rock' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_Rock"
{
	Properties
	{
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Normal("Normal", 2D) = "bump" {}
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_DetailNormalScale("Detail Normal Scale", Float) = 0
		_DetailNormal("DetailNormal", 2D) = "white" {}
		_DetailNormalIntensity("DetailNormalIntensity", Range( 0 , 1)) = 1
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
		};

		uniform sampler2D _DetailNormal;
		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;

		UNITY_INSTANCING_BUFFER_START(S_Rock)
			UNITY_DEFINE_INSTANCED_PROP(float, _DetailNormalScale)
#define _DetailNormalScale_arr S_Rock
			UNITY_DEFINE_INSTANCED_PROP(float, _DetailNormalIntensity)
#define _DetailNormalIntensity_arr S_Rock
			UNITY_DEFINE_INSTANCED_PROP(float, _Metallic)
#define _Metallic_arr S_Rock
			UNITY_DEFINE_INSTANCED_PROP(float, _Smoothness)
#define _Smoothness_arr S_Rock
		UNITY_INSTANCING_BUFFER_END(S_Rock)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float _DetailNormalScale_Instance = UNITY_ACCESS_INSTANCED_PROP(_DetailNormalScale_arr, _DetailNormalScale);
			float2 temp_cast_0 = (_DetailNormalScale_Instance).xx;
			float2 uv_TexCoord11 = i.uv_texcoord * temp_cast_0;
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			float _DetailNormalIntensity_Instance = UNITY_ACCESS_INSTANCED_PROP(_DetailNormalIntensity_arr, _DetailNormalIntensity);
			float4 lerpResult6 = lerp( tex2D( _DetailNormal, uv_TexCoord11 ) , float4( UnpackNormal( tex2D( _Normal, uv_Normal ) ) , 0.0 ) , _DetailNormalIntensity_Instance);
			o.Normal = lerpResult6.rgb;
			float4 color19 = IsGammaSpace() ? float4(0.509434,0.509434,0.509434,0) : float4(0.2228772,0.2228772,0.2228772,0);
			o.Albedo = color19.rgb;
			float _Metallic_Instance = UNITY_ACCESS_INSTANCED_PROP(_Metallic_arr, _Metallic);
			o.Metallic = _Metallic_Instance;
			float _Smoothness_Instance = UNITY_ACCESS_INSTANCED_PROP(_Smoothness_arr, _Smoothness);
			o.Smoothness = _Smoothness_Instance;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1543;1;1906;1020;2479.599;1012.578;1.6;True;True
Node;AmplifyShaderEditor.RangedFloatNode;13;-2140.893,-18.4281;Float;False;InstancedProperty;_DetailNormalScale;Detail Normal Scale;3;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;1;-1554.076,-526.9692;Float;False;1368.97;749.2253;Normal;4;8;7;6;3;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;11;-1877.893,12.5719;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-1529.076,-328.3896;Float;True;Property;_Normal;Normal;1;0;Create;True;0;0;False;0;None;60b9d862af15a804580f95576ebae59d;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;7;-1527.216,-50.36046;Float;True;Property;_DetailNormal;DetailNormal;4;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;8;-1505.416,140.8245;Float;False;InstancedProperty;_DetailNormalIntensity;DetailNormalIntensity;5;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;6;-660.1759,-362.2941;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-149.7672,-21.48596;Float;False;InstancedProperty;_Smoothness;Smoothness;2;0;Create;True;0;0;False;0;0;0.13;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-170.423,-130.3985;Float;False;InstancedProperty;_Metallic;Metallic;0;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;19;-87.75574,-457.8583;Float;False;Constant;_Color0;Color 0;6;0;Create;True;0;0;False;0;0.509434,0.509434,0.509434,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;334.7581,-251.9313;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_Rock;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;11;0;13;0
WireConnection;7;1;11;0
WireConnection;6;0;7;0
WireConnection;6;1;3;0
WireConnection;6;2;8;0
WireConnection;0;0;19;0
WireConnection;0;1;6;0
WireConnection;0;3;17;0
WireConnection;0;4;18;0
ASEEND*/
//CHKSM=910E936A61F5A6F4ECE5873CEECA655A1B1FACC6