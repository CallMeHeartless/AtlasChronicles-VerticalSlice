// Upgrade NOTE: upgraded instancing buffer 'S_TeleportBox' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_TeleportBox"
{
	Properties
	{
		_Diff("Diff", 2D) = "white" {}
		_Emi("Emi", 2D) = "white" {}
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_EmissiveColor("EmissiveColor", Color) = (0,0,0,0)
		_EmissiveMult("EmissiveMult", Float) = 0
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

		uniform sampler2D _Diff;
		uniform float4 _Diff_ST;
		uniform sampler2D _Emi;
		uniform float4 _Emi_ST;

		UNITY_INSTANCING_BUFFER_START(S_TeleportBox)
			UNITY_DEFINE_INSTANCED_PROP(float4, _EmissiveColor)
#define _EmissiveColor_arr S_TeleportBox
			UNITY_DEFINE_INSTANCED_PROP(float, _EmissiveMult)
#define _EmissiveMult_arr S_TeleportBox
			UNITY_DEFINE_INSTANCED_PROP(float, _Metallic)
#define _Metallic_arr S_TeleportBox
			UNITY_DEFINE_INSTANCED_PROP(float, _Smoothness)
#define _Smoothness_arr S_TeleportBox
		UNITY_INSTANCING_BUFFER_END(S_TeleportBox)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Diff = i.uv_texcoord * _Diff_ST.xy + _Diff_ST.zw;
			o.Albedo = tex2D( _Diff, uv_Diff ).rgb;
			float4 _EmissiveColor_Instance = UNITY_ACCESS_INSTANCED_PROP(_EmissiveColor_arr, _EmissiveColor);
			float2 uv_Emi = i.uv_texcoord * _Emi_ST.xy + _Emi_ST.zw;
			float _EmissiveMult_Instance = UNITY_ACCESS_INSTANCED_PROP(_EmissiveMult_arr, _EmissiveMult);
			o.Emission = ( ( _EmissiveColor_Instance * tex2D( _Emi, uv_Emi ) ) * _EmissiveMult_Instance ).rgb;
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
1927;1;1266;964;2061.729;674.4091;1.819095;True;False
Node;AmplifyShaderEditor.SamplerNode;4;-1437.266,181.3356;Float;True;Property;_Emi;Emi;1;0;Create;True;0;0;False;0;9fa899f32991ee84faf9bdaf587476d6;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;6;-1314.509,-46.38854;Float;False;InstancedProperty;_EmissiveColor;EmissiveColor;4;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-855.5021,277.4068;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-937.3405,480.2236;Float;False;InstancedProperty;_EmissiveMult;EmissiveMult;5;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-355.5764,250.7202;Float;False;InstancedProperty;_Smoothness;Smoothness;2;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;2;-375.1465,158.2072;Float;False;InstancedProperty;_Metallic;Metallic;3;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-853.7231,-169.1463;Float;True;Property;_Diff;Diff;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-617.1035,261.395;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_TeleportBox;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;5;0;6;0
WireConnection;5;1;4;0
WireConnection;7;0;5;0
WireConnection;7;1;8;0
WireConnection;0;0;1;0
WireConnection;0;2;7;0
WireConnection;0;3;2;0
WireConnection;0;4;3;0
ASEEND*/
//CHKSM=0B3B9F903B6710316B88FD9F5CB8BAE184DDE218