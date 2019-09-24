// Upgrade NOTE: upgraded instancing buffer 'S_StandardProp' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_StandardProp"
{
	Properties
	{
		_Diffuse("Diffuse", 2D) = "white" {}
		_Normal("Normal", 2D) = "bump" {}
		_Metallic("Metallic", 2D) = "white" {}
		_FlatMetallic("FlatMetallic", Range( 0 , 1)) = 0
		[Toggle(_USEFLATMETALLIC_ON)] _UseFlatMetallic("UseFlatMetallic", Float) = 0
		_Emissive("Emissive", 2D) = "white" {}
		_FlatEmissive("FlatEmissive", Color) = (0,0,0,0)
		[Toggle(_USEFLATEMISSIVE_ON)] _UseFlatEmissive("UseFlatEmissive", Float) = 0
		_Smoothness("Smoothness", 2D) = "white" {}
		_FlatSmoothness("FlatSmoothness", Range( 0 , 1)) = 0
		[Toggle(_USEFLATSMOOTHNESS_ON)] _UseFlatSmoothness("UseFlatSmoothness", Float) = 0
		_Occlusion("Occlusion", 2D) = "white" {}
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
		#pragma shader_feature _USEFLATEMISSIVE_ON
		#pragma shader_feature _USEFLATMETALLIC_ON
		#pragma shader_feature _USEFLATSMOOTHNESS_ON
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform sampler2D _Diffuse;
		uniform float4 _Diffuse_ST;
		uniform sampler2D _Emissive;
		uniform float4 _Emissive_ST;
		uniform sampler2D _Metallic;
		uniform float4 _Metallic_ST;
		uniform sampler2D _Smoothness;
		uniform float4 _Smoothness_ST;
		uniform sampler2D _Occlusion;
		uniform float4 _Occlusion_ST;

		UNITY_INSTANCING_BUFFER_START(S_StandardProp)
			UNITY_DEFINE_INSTANCED_PROP(float4, _FlatEmissive)
#define _FlatEmissive_arr S_StandardProp
			UNITY_DEFINE_INSTANCED_PROP(float, _FlatMetallic)
#define _FlatMetallic_arr S_StandardProp
			UNITY_DEFINE_INSTANCED_PROP(float, _FlatSmoothness)
#define _FlatSmoothness_arr S_StandardProp
		UNITY_INSTANCING_BUFFER_END(S_StandardProp)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			o.Normal = UnpackNormal( tex2D( _Normal, uv_Normal ) );
			float2 uv_Diffuse = i.uv_texcoord * _Diffuse_ST.xy + _Diffuse_ST.zw;
			o.Albedo = tex2D( _Diffuse, uv_Diffuse ).rgb;
			float2 uv_Emissive = i.uv_texcoord * _Emissive_ST.xy + _Emissive_ST.zw;
			float4 _FlatEmissive_Instance = UNITY_ACCESS_INSTANCED_PROP(_FlatEmissive_arr, _FlatEmissive);
			#ifdef _USEFLATEMISSIVE_ON
				float4 staticSwitch8 = _FlatEmissive_Instance;
			#else
				float4 staticSwitch8 = tex2D( _Emissive, uv_Emissive );
			#endif
			o.Emission = staticSwitch8.rgb;
			float2 uv_Metallic = i.uv_texcoord * _Metallic_ST.xy + _Metallic_ST.zw;
			float _FlatMetallic_Instance = UNITY_ACCESS_INSTANCED_PROP(_FlatMetallic_arr, _FlatMetallic);
			float4 temp_cast_2 = (_FlatMetallic_Instance).xxxx;
			#ifdef _USEFLATMETALLIC_ON
				float4 staticSwitch7 = temp_cast_2;
			#else
				float4 staticSwitch7 = tex2D( _Metallic, uv_Metallic );
			#endif
			o.Metallic = staticSwitch7.r;
			float2 uv_Smoothness = i.uv_texcoord * _Smoothness_ST.xy + _Smoothness_ST.zw;
			float _FlatSmoothness_Instance = UNITY_ACCESS_INSTANCED_PROP(_FlatSmoothness_arr, _FlatSmoothness);
			float4 temp_cast_4 = (_FlatSmoothness_Instance).xxxx;
			#ifdef _USEFLATSMOOTHNESS_ON
				float4 staticSwitch13 = temp_cast_4;
			#else
				float4 staticSwitch13 = tex2D( _Smoothness, uv_Smoothness );
			#endif
			o.Smoothness = staticSwitch13.r;
			float2 uv_Occlusion = i.uv_texcoord * _Occlusion_ST.xy + _Occlusion_ST.zw;
			o.Occlusion = tex2D( _Occlusion, uv_Occlusion ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1927;1;1266;964;1851.972;823.2615;1.774728;True;False
Node;AmplifyShaderEditor.SamplerNode;4;-604.8582,318.9237;Float;True;Property;_Smoothness;Smoothness;8;0;Create;True;0;0;False;0;None;2238ca504f41d0b49a53c32b9c8a4db9;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;11;-820.4048,64.10258;Float;False;InstancedProperty;_FlatEmissive;FlatEmissive;6;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;10;-1121.482,-44.74232;Float;True;Property;_Emissive;Emissive;5;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-1179.604,212.1531;Float;True;Property;_Metallic;Metallic;2;0;Create;True;0;0;False;0;None;9606229fe12782247a1c6a76d00639a7;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;6;-885.0344,334.5945;Float;False;InstancedProperty;_FlatMetallic;FlatMetallic;3;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-1173.435,-176.0871;Float;False;InstancedProperty;_FlatSmoothness;FlatSmoothness;9;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;5;-603.0594,507.3216;Float;True;Property;_Occlusion;Occlusion;11;0;Create;True;0;0;False;0;None;c86324b80fe70c547b786eadaabbc692;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-614.4267,-440.3005;Float;True;Property;_Diffuse;Diffuse;0;0;Create;True;0;0;False;0;None;1f25a648747866944bd7f47de5aec951;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-613.2278,-251.9023;Float;True;Property;_Normal;Normal;1;0;Create;True;0;0;False;0;None;36446398236765046a5d2bbf0f9dfc3e;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;8;-585.5836,-42.96772;Float;False;Property;_UseFlatEmissive;UseFlatEmissive;7;0;Create;True;0;0;False;0;0;0;1;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;7;-602.8868,213.9277;Float;False;Property;_UseFlatMetallic;UseFlatMetallic;4;0;Create;True;0;0;False;0;0;0;1;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;13;-323.3741,100.7851;Float;False;Property;_UseFlatSmoothness;UseFlatSmoothness;10;0;Create;True;0;0;False;0;0;0;1;True;;Toggle;2;Key0;Key1;Create;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_StandardProp;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;8;1;10;0
WireConnection;8;0;11;0
WireConnection;7;1;3;0
WireConnection;7;0;6;0
WireConnection;13;1;4;0
WireConnection;13;0;14;0
WireConnection;0;0;1;0
WireConnection;0;1;2;0
WireConnection;0;2;8;0
WireConnection;0;3;7;0
WireConnection;0;4;13;0
WireConnection;0;5;5;0
ASEEND*/
//CHKSM=FCE5BDFA22E648DC77585438597ABB72E4F8DA7E