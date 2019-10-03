// Upgrade NOTE: upgraded instancing buffer 'S_InkWellLevel' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_InkWellLevel"
{
	Properties
	{
		_Height("Height", Range( 0 , 1)) = 1
		_Diff("Diff", 2D) = "white" {}
		_EmissiveColor("EmissiveColor", Color) = (0,0,0,0)
		_InkColor("InkColor", Color) = (0,0,0,0)
		_EmissiveIntensity("EmissiveIntensity", Float) = 0
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
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Diff;
		uniform float4 _Diff_ST;

		UNITY_INSTANCING_BUFFER_START(S_InkWellLevel)
			UNITY_DEFINE_INSTANCED_PROP(float4, _InkColor)
#define _InkColor_arr S_InkWellLevel
			UNITY_DEFINE_INSTANCED_PROP(float4, _EmissiveColor)
#define _EmissiveColor_arr S_InkWellLevel
			UNITY_DEFINE_INSTANCED_PROP(float, _Height)
#define _Height_arr S_InkWellLevel
			UNITY_DEFINE_INSTANCED_PROP(float, _EmissiveIntensity)
#define _EmissiveIntensity_arr S_InkWellLevel
		UNITY_INSTANCING_BUFFER_END(S_InkWellLevel)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Diff = i.uv_texcoord * _Diff_ST.xy + _Diff_ST.zw;
			float4 _InkColor_Instance = UNITY_ACCESS_INSTANCED_PROP(_InkColor_arr, _InkColor);
			float _Height_Instance = UNITY_ACCESS_INSTANCED_PROP(_Height_arr, _Height);
			float clampResult22 = clamp( ( ( ( 1.0 - (i.uv_texcoord).y ) - 1.0 ) + ( _Height_Instance + 1.0 ) ) , 0.0 , 1.0 );
			float temp_output_11_0 = pow( clampResult22 , 100.0 );
			float4 lerpResult17 = lerp( tex2D( _Diff, uv_Diff ) , _InkColor_Instance , temp_output_11_0);
			o.Albedo = lerpResult17.rgb;
			float4 temp_cast_1 = (0.0).xxxx;
			float4 _EmissiveColor_Instance = UNITY_ACCESS_INSTANCED_PROP(_EmissiveColor_arr, _EmissiveColor);
			float4 lerpResult18 = lerp( temp_cast_1 , _EmissiveColor_Instance , temp_output_11_0);
			float _EmissiveIntensity_Instance = UNITY_ACCESS_INSTANCED_PROP(_EmissiveIntensity_arr, _EmissiveIntensity);
			o.Emission = ( lerpResult18 * _EmissiveIntensity_Instance ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1927;7;1266;958;2165.83;967.3127;1.602109;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;2;-1926.835,-379.3794;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;1;-1691.695,-384.7672;Float;True;False;True;False;False;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-1688.923,-113.4179;Float;False;Constant;_PlusOne;PlusOne;5;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-1427.614,-130.071;Float;False;InstancedProperty;_Height;Height;0;0;Create;True;0;0;False;0;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;3;-1442.353,-378.5808;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;25;-1108.277,-105.9738;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;4;-1267.625,-379.627;Float;True;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-765.2464,-246.0958;Float;False;Constant;_Float0;Float 0;5;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;6;-1037.078,-375.3533;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-775.8386,-172.7827;Float;False;Constant;_Float1;Float 1;5;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;22;-539.3932,-336.2202;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-1316.063,44.29032;Float;False;Constant;_POWER;POWER;1;0;Create;True;0;0;False;0;100;0;0;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-648.1023,-108.6448;Float;False;Constant;_NoEmissive;No Emissive;4;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;11;-385.4237,-270.8917;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;15;-717.958,50.11788;Float;False;InstancedProperty;_EmissiveColor;EmissiveColor;2;0;Create;True;0;0;False;0;0,0,0,0;0.6093364,0,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;18;-294.591,-106.528;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;13;-840.7344,-860.1205;Float;True;Property;_Diff;Diff;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;21;-288.2401,86.10352;Float;False;InstancedProperty;_EmissiveIntensity;EmissiveIntensity;4;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;16;-707.3733,-567.9978;Float;False;InstancedProperty;_InkColor;InkColor;3;0;Create;True;0;0;False;0;0,0,0,0;0,0.1544723,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-97.72494,-104.4114;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;17;-336.9276,-527.7779;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;144,-280;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_InkWellLevel;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;1;0;2;0
WireConnection;3;0;1;0
WireConnection;25;0;5;0
WireConnection;25;1;26;0
WireConnection;4;0;3;0
WireConnection;6;0;4;0
WireConnection;6;1;25;0
WireConnection;22;0;6;0
WireConnection;22;1;23;0
WireConnection;22;2;24;0
WireConnection;11;0;22;0
WireConnection;11;1;12;0
WireConnection;18;0;19;0
WireConnection;18;1;15;0
WireConnection;18;2;11;0
WireConnection;20;0;18;0
WireConnection;20;1;21;0
WireConnection;17;0;13;0
WireConnection;17;1;16;0
WireConnection;17;2;11;0
WireConnection;0;0;17;0
WireConnection;0;2;20;0
ASEEND*/
//CHKSM=84CCE44BA049B000F655473C74E88510DCF83388