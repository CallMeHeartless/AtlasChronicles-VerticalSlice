// Upgrade NOTE: upgraded instancing buffer 'S_GatewayColorSelect' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_GatewayColorSelect"
{
	Properties
	{
		_Red("Red", Color) = (1,0.235221,0,0)
		_Green("Green ", Color) = (0,1,0.7063386,0)
		_Blue("Blue", Color) = (0,0.1054468,1,0)
		_Norm("Norm", 2D) = "white" {}
		_OSM("OSM", 2D) = "white" {}
		_Select("Select", Int) = 0
		_EmissiveIntensity("Emissive Intensity", Range( 0 , 5)) = 0
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

		uniform sampler2D _Norm;
		uniform float4 _Norm_ST;
		uniform sampler2D _OSM;
		uniform float4 _OSM_ST;

		UNITY_INSTANCING_BUFFER_START(S_GatewayColorSelect)
			UNITY_DEFINE_INSTANCED_PROP(float4, _Red)
#define _Red_arr S_GatewayColorSelect
			UNITY_DEFINE_INSTANCED_PROP(float4, _Green)
#define _Green_arr S_GatewayColorSelect
			UNITY_DEFINE_INSTANCED_PROP(float4, _Blue)
#define _Blue_arr S_GatewayColorSelect
			UNITY_DEFINE_INSTANCED_PROP(int, _Select)
#define _Select_arr S_GatewayColorSelect
			UNITY_DEFINE_INSTANCED_PROP(float, _EmissiveIntensity)
#define _EmissiveIntensity_arr S_GatewayColorSelect
		UNITY_INSTANCING_BUFFER_END(S_GatewayColorSelect)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Norm = i.uv_texcoord * _Norm_ST.xy + _Norm_ST.zw;
			o.Normal = UnpackNormal( tex2D( _Norm, uv_Norm ) );
			float4 _Red_Instance = UNITY_ACCESS_INSTANCED_PROP(_Red_arr, _Red);
			float4 _Green_Instance = UNITY_ACCESS_INSTANCED_PROP(_Green_arr, _Green);
			int _Select_Instance = UNITY_ACCESS_INSTANCED_PROP(_Select_arr, _Select);
			float4 lerpResult10 = lerp( _Red_Instance , _Green_Instance , (float)_Select_Instance);
			float4 _Blue_Instance = UNITY_ACCESS_INSTANCED_PROP(_Blue_arr, _Blue);
			float4 lerpResult11 = lerp( _Green_Instance , _Blue_Instance , (float)_Select_Instance);
			float4 lerpResult16 = lerp( lerpResult10 , lerpResult11 , ( _Select_Instance + -1.0 ));
			o.Albedo = lerpResult16.rgb;
			float _EmissiveIntensity_Instance = UNITY_ACCESS_INSTANCED_PROP(_EmissiveIntensity_arr, _EmissiveIntensity);
			o.Emission = ( lerpResult16 * _EmissiveIntensity_Instance ).rgb;
			float2 uv_OSM = i.uv_texcoord * _OSM_ST.xy + _OSM_ST.zw;
			float4 tex2DNode8 = tex2D( _OSM, uv_OSM );
			o.Metallic = tex2DNode8.b;
			o.Smoothness = tex2DNode8.g;
			o.Occlusion = tex2DNode8.r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1927;1;1266;964;1860.375;782.9413;1.913621;True;False
Node;AmplifyShaderEditor.RangedFloatNode;15;-1127.459,-610.7159;Float;False;Constant;_MinusOne;Minus One;5;0;Create;True;0;0;False;0;-1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;3;-1123.631,53.31084;Float;False;InstancedProperty;_Blue;Blue;2;0;Create;True;0;0;False;0;0,0.1054468,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.IntNode;13;-1125.545,-461.4531;Float;False;InstancedProperty;_Select;Select;5;0;Create;True;0;0;False;0;0;0;0;1;INT;0
Node;AmplifyShaderEditor.ColorNode;1;-1131.286,-277.7456;Float;False;InstancedProperty;_Red;Red;0;0;Create;True;0;0;False;0;1,0.235221,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;2;-1127.458,-113.1742;Float;False;InstancedProperty;_Green;Green ;1;0;Create;True;0;0;False;0;0,1,0.7063386,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;11;-767.6989,-2.18416;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;14;-844.2432,-499.7261;Float;False;2;2;0;INT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;10;-767.6985,-203.1144;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-987.7641,284.859;Float;False;InstancedProperty;_EmissiveIntensity;Emissive Intensity;6;0;Create;True;0;0;False;0;0;0;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;16;-530.4094,-92.12434;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;7;-622.2629,403.5034;Float;True;Property;_Norm;Norm;3;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;8;-616.5225,606.3472;Float;True;Property;_OSM;OSM;4;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-247.1926,74.36097;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_GatewayColorSelect;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;11;0;2;0
WireConnection;11;1;3;0
WireConnection;11;2;13;0
WireConnection;14;0;13;0
WireConnection;14;1;15;0
WireConnection;10;0;1;0
WireConnection;10;1;2;0
WireConnection;10;2;13;0
WireConnection;16;0;10;0
WireConnection;16;1;11;0
WireConnection;16;2;14;0
WireConnection;17;0;16;0
WireConnection;17;1;18;0
WireConnection;0;0;16;0
WireConnection;0;1;7;0
WireConnection;0;2;17;0
WireConnection;0;3;8;3
WireConnection;0;4;8;2
WireConnection;0;5;8;1
ASEEND*/
//CHKSM=148FE0FEF82C34A81097358DC9D6E09D015A49FD