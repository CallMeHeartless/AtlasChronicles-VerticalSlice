// Upgrade NOTE: upgraded instancing buffer 'S_TeleportBox' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_TeleportBox"
{
	Properties
	{
		_Diffuse("Diffuse", 2D) = "white" {}
		_Emissive("Emissive", 2D) = "white" {}
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_EmissiveIntensity("EmissiveIntensity", Float) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_EmissiveColor("EmissiveColor", Color) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Diffuse;
		uniform float4 _Diffuse_ST;
		uniform sampler2D _Emissive;
		uniform float4 _Emissive_ST;

		UNITY_INSTANCING_BUFFER_START(S_TeleportBox)
			UNITY_DEFINE_INSTANCED_PROP(float4, _EmissiveColor)
#define _EmissiveColor_arr S_TeleportBox
			UNITY_DEFINE_INSTANCED_PROP(float, _EmissiveIntensity)
#define _EmissiveIntensity_arr S_TeleportBox
			UNITY_DEFINE_INSTANCED_PROP(float, _Metallic)
#define _Metallic_arr S_TeleportBox
			UNITY_DEFINE_INSTANCED_PROP(float, _Smoothness)
#define _Smoothness_arr S_TeleportBox
		UNITY_INSTANCING_BUFFER_END(S_TeleportBox)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Diffuse = i.uv_texcoord * _Diffuse_ST.xy + _Diffuse_ST.zw;
			o.Albedo = tex2D( _Diffuse, uv_Diffuse ).rgb;
			float4 _EmissiveColor_Instance = UNITY_ACCESS_INSTANCED_PROP(_EmissiveColor_arr, _EmissiveColor);
			float2 uv_Emissive = i.uv_texcoord * _Emissive_ST.xy + _Emissive_ST.zw;
			float _EmissiveIntensity_Instance = UNITY_ACCESS_INSTANCED_PROP(_EmissiveIntensity_arr, _EmissiveIntensity);
			o.Emission = ( ( _EmissiveColor_Instance * tex2D( _Emissive, uv_Emissive ) ) * _EmissiveIntensity_Instance ).rgb;
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
1927;7;1266;958;1793.343;687.3682;1.73403;True;False
Node;AmplifyShaderEditor.SamplerNode;2;-962.7422,144.9661;Float;True;Property;_Emissive;Emissive;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;7;-919.3917,-28.43694;Float;False;InstancedProperty;_EmissiveColor;EmissiveColor;5;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-633.2767,131.094;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-787.6054,467.4957;Float;False;InstancedProperty;_EmissiveIntensity;EmissiveIntensity;3;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-373.1721,217.7954;Float;False;InstancedProperty;_Metallic;Metallic;2;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-378.3742,301.0288;Float;False;InstancedProperty;_Smoothness;Smoothness;4;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-468.544,172.7106;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1;-609.0003,-109.9363;Float;True;Property;_Diffuse;Diffuse;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_TeleportBox;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;6;0;7;0
WireConnection;6;1;2;0
WireConnection;8;0;6;0
WireConnection;8;1;3;0
WireConnection;0;0;1;0
WireConnection;0;2;8;0
WireConnection;0;3;5;0
WireConnection;0;4;4;0
ASEEND*/
//CHKSM=5F2CA107033C99E4D19010C2EDE8CD07D0B2BA9A