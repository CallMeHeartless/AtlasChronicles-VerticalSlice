// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/S_Dust"
{
	Properties
	{
		_T_Dust_Mask("T_Dust_Mask", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Background+0" "IgnoreProjector" = "True" }
		Cull Off
		AlphaToMask On
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _T_Dust_Mask;
		uniform float4 _T_Dust_Mask_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 color1 = IsGammaSpace() ? float4(0.6320754,0.6206868,0.5396493,0) : float4(0.3572768,0.3432284,0.2525924,0);
			o.Albedo = color1.rgb;
			float2 uv_T_Dust_Mask = i.uv_texcoord * _T_Dust_Mask_ST.xy + _T_Dust_Mask_ST.zw;
			o.Alpha = tex2D( _T_Dust_Mask, uv_T_Dust_Mask ).r;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1921;1;1278;970;1009.521;581.6701;1.488982;True;True
Node;AmplifyShaderEditor.RangedFloatNode;2;-108.8138,70.63107;Float;False;Constant;_Float0;Float 0;0;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;1;-337.7451,-59.54096;Float;False;Constant;_Color0;Color 0;0;0;Create;True;0;0;False;0;0.6320754,0.6206868,0.5396493,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;14;-370.7477,174.7327;Float;True;Property;_T_Dust_Mask;T_Dust_Mask;1;0;Create;True;0;0;False;0;324386b3646dfe243a119401c7e63a6b;324386b3646dfe243a119401c7e63a6b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;51.41437,-5.95593;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Custom/S_Dust;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;False;Opaque;;Background;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;True;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;1;0
WireConnection;0;9;14;1
ASEEND*/
//CHKSM=3CDBD5FE21DD641E0A81BAF7C7CBAFDC087E029F