// Upgrade NOTE: upgraded instancing buffer 'CustomS_Mushroom' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/S_Mushroom"
{
	Properties
	{
		_Diffuse("Diffuse", Color) = (0.5471698,0.3019758,0.3019758,0)
		_Emissive("Emissive", Color) = (0.5471698,0.3019758,0.3019758,0)
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
			half filler;
		};

		UNITY_INSTANCING_BUFFER_START(CustomS_Mushroom)
			UNITY_DEFINE_INSTANCED_PROP(float4, _Diffuse)
#define _Diffuse_arr CustomS_Mushroom
			UNITY_DEFINE_INSTANCED_PROP(float4, _Emissive)
#define _Emissive_arr CustomS_Mushroom
		UNITY_INSTANCING_BUFFER_END(CustomS_Mushroom)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 _Diffuse_Instance = UNITY_ACCESS_INSTANCED_PROP(_Diffuse_arr, _Diffuse);
			o.Albedo = _Diffuse_Instance.rgb;
			float4 _Emissive_Instance = UNITY_ACCESS_INSTANCED_PROP(_Emissive_arr, _Emissive);
			o.Emission = _Emissive_Instance.rgb;
			float temp_output_2_0 = 0.0;
			o.Metallic = temp_output_2_0;
			o.Smoothness = temp_output_2_0;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1927;1;1266;964;1516.656;684.7166;1.706843;True;False
Node;AmplifyShaderEditor.ColorNode;1;-609.3167,-157.6532;Float;False;InstancedProperty;_Diffuse;Diffuse;0;0;Create;True;0;0;False;0;0.5471698,0.3019758,0.3019758,0;0.3867925,0.1842737,0.1842737,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;3;-623.9775,127.7406;Float;False;InstancedProperty;_Emissive;Emissive;1;0;Create;True;0;0;False;0;0.5471698,0.3019758,0.3019758,0;0.322,0.04965238,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;2;-166,138.5;Float;False;Constant;_SmoMet;Smo/Met;0;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Custom/S_Mushroom;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;1;0
WireConnection;0;2;3;0
WireConnection;0;3;2;0
WireConnection;0;4;2;0
ASEEND*/
//CHKSM=985EEE3F7CEB67AA929F0C3E55753ED26F54F6D0