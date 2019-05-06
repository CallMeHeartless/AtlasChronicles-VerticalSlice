// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Substance2018"
{
	Properties
	{
		_Assembly_Tiles_baseColor("Assembly_Tiles_baseColor", 2D) = "white"{}
		_Assembly_Tiles_mask("Assembly_Tiles_mask", 2D) = "white"{}
		_Assembly_Tiles_normal("Assembly_Tiles_normal", 2D) = "white"{}
		_Moss_normal("Moss_normal", 2D) = "white"{}
		_Moss_roughness("Moss_roughness", 2D) = "white"{}
		_Assembly_Tiles_ambientOcclusion("Assembly_Tiles_ambientOcclusion", 2D) = "white"{}
		_Moss_baseColor("Moss_baseColor", 2D) = "white"{}
		_Assembly_Tiles_roughness("Assembly_Tiles_roughness", 2D) = "white"{}
		_Assembly_Tiles_height("Assembly_Tiles_height", 2D) = "white"{}
		_TessValue( "Max Tessellation", Range( 1, 32 ) ) = 32
		_TessPhongStrength( "Phong Tess Strength", Range( 0, 1 ) ) = 0.84
		_Assembly_Tiles_noisemask("Assembly_Tiles_noisemask", 2D) = "white" {}
		_MossCoverageHardness("Moss Coverage Hardness", Range( 0 , 5)) = 2.69
		_MossCoverage("Moss Coverage", Range( 0 , 1)) = 0
		_MossTint("Moss Tint", Color) = (0,0,0,0)
		_MossExtraTweak("Moss Extra Tweak", Float) = 0
		_MossRoughnessTweak("Moss Roughness Tweak", Range( 0.69 , 1.17)) = 0
		_DarkenMoss("Darken Moss", Range( 0 , 1)) = 0
		_TileTint("Tile Tint", Color) = (0,0,0,0)
		_TileHeight("Tile Height", Range( 0 , 1)) = 0
		_TileNoise("Tile Noise", Range( 0 , 1)) = 0
		_BaseSubstanceRoughnessMultiplier("Base Substance Roughness Multiplier", Float) = 0
		_SubstanceAlbedoTweak("Substance Albedo Tweak", Float) = 0
		_OcclusionIntensity("Occlusion Intensity", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 4.6
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc tessellate:tessFunction tessphong:_TessPhongStrength 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Assembly_Tiles_height;
		uniform float4 _Assembly_Tiles_roughness_ST;
		uniform float _TileHeight;
		uniform sampler2D _Assembly_Tiles_noisemask;
		uniform float _TileNoise;
		uniform float _MossExtraTweak;
		uniform float _MossCoverage;
		uniform float _MossCoverageHardness;
		uniform sampler2D _Assembly_Tiles_normal;
		uniform sampler2D _Moss_normal;
		uniform float4 _Moss_roughness_ST;
		uniform float4 _TileTint;
		uniform sampler2D _Assembly_Tiles_baseColor;
		uniform sampler2D _Assembly_Tiles_mask;
		uniform sampler2D _Moss_baseColor;
		uniform float4 _MossTint;
		uniform float _DarkenMoss;
		uniform float _SubstanceAlbedoTweak;
		uniform sampler2D _Assembly_Tiles_roughness;
		uniform float _BaseSubstanceRoughnessMultiplier;
		uniform sampler2D _Moss_roughness;
		uniform float _MossRoughnessTweak;
		uniform sampler2D _Assembly_Tiles_ambientOcclusion;
		uniform float _OcclusionIntensity;
		uniform float _TessValue;
		uniform float _TessPhongStrength;

		float4 tessFunction( )
		{
			return _TessValue;
		}

		void vertexDataFunc( inout appdata_full v )
		{
			float4 uv_Assembly_Tiles_roughness = float4(v.texcoord * _Assembly_Tiles_roughness_ST.xy + _Assembly_Tiles_roughness_ST.zw, 0 ,0);
			float4 _Assembly_Tiles_height231 = tex2Dlod(_Assembly_Tiles_height, uv_Assembly_Tiles_roughness);
			float BaseHeight186 = (_Assembly_Tiles_height231).r;
			float3 ase_vertexNormal = v.normal.xyz;
			v.vertex.xyz += ( ( (BaseHeight186*1.0 + -0.5) * _TileHeight ) * ase_vertexNormal );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Assembly_Tiles_roughness = i.uv_texcoord * _Assembly_Tiles_roughness_ST.xy + _Assembly_Tiles_roughness_ST.zw;
			float4 _Assembly_Tiles_height231 = tex2D(_Assembly_Tiles_height, uv_Assembly_Tiles_roughness);
			float BaseHeight186 = (_Assembly_Tiles_height231).r;
			float4 transform259 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float temp_output_263_0 = ( sin( ( transform259.x + transform259.y + transform259.z ) ) * 998.21 );
			float2 appendResult264 = (float2(temp_output_263_0 , temp_output_263_0));
			float2 lerpResult271 = lerp( appendResult264 , i.uv_texcoord , _TileNoise);
			float ExtraNoise273 = tex2D( _Assembly_Tiles_noisemask, lerpResult271 ).r;
			float temp_output_197_0 = saturate( (0.0 + (( 1.0 - ( BaseHeight186 + pow( ExtraNoise273 , _MossExtraTweak ) ) ) - ( 1.0 - _MossCoverage )) * (_MossCoverageHardness - 0.0) / (1.0 - ( 1.0 - _MossCoverage ))) );
			float MainMossMask305 = temp_output_197_0;
			float3 _Assembly_Tiles_normal231 = UnpackNormal( tex2D(_Assembly_Tiles_normal, uv_Assembly_Tiles_roughness) );
			float3 BaseNormal237 = _Assembly_Tiles_normal231;
			float2 uv_Moss_roughness = i.uv_texcoord * _Moss_roughness_ST.xy + _Moss_roughness_ST.zw;
			float3 _Moss_normal233 = UnpackNormal( tex2D(_Moss_normal, uv_Moss_roughness) );
			float3 MossNormal236 = _Moss_normal233;
			float layeredBlendVar302 = MainMossMask305;
			float3 layeredBlend302 = ( lerp( BaseNormal237,MossNormal236 , layeredBlendVar302 ) );
			float3 normalizeResult308 = normalize( layeredBlend302 );
			o.Normal = normalizeResult308;
			float4 _Assembly_Tiles_baseColor231 = tex2D(_Assembly_Tiles_baseColor, uv_Assembly_Tiles_roughness);
			float4 BaseColor189 = _Assembly_Tiles_baseColor231;
			float4 _Assembly_Tiles_mask231 = tex2D(_Assembly_Tiles_mask, uv_Assembly_Tiles_roughness);
			float4 BaseColorMask205 = _Assembly_Tiles_mask231;
			float4 lerpResult201 = lerp( ( _TileTint * BaseColor189 ) , BaseColor189 , ( 1.0 - BaseColorMask205 ));
			float4 _Moss_baseColor233 = tex2D(_Moss_baseColor, uv_Moss_roughness);
			float4 MossCover191 = ( _Moss_baseColor233 * _MossTint );
			float4 lerpResult239 = lerp( ( lerpResult201 * ( 1.0 - temp_output_197_0 ) ) , ( MossCover191 * ExtraNoise273 * _DarkenMoss ) , temp_output_197_0);
			o.Albedo = ( lerpResult239 * ( ExtraNoise273 * _SubstanceAlbedoTweak ) ).rgb;
			float4 _Assembly_Tiles_roughness231 = tex2D(_Assembly_Tiles_roughness, uv_Assembly_Tiles_roughness);
			float4 BaseRoughness232 = ( _Assembly_Tiles_roughness231 * _BaseSubstanceRoughnessMultiplier );
			float4 _Moss_roughness233 = tex2D(_Moss_roughness, uv_Moss_roughness);
			float4 MossRoughness234 = _Moss_roughness233;
			float4 lerpResult297 = lerp( BaseRoughness232 , ( MossRoughness234 * _MossRoughnessTweak ) , MainMossMask305);
			o.Smoothness = saturate( saturate( ( ( 1.0 - lerpResult297 ) * ExtraNoise273 ) ) ).r;
			float4 _Assembly_Tiles_ambientOcclusion231 = tex2D(_Assembly_Tiles_ambientOcclusion, uv_Assembly_Tiles_roughness);
			float4 BaseOcclusion187 = _Assembly_Tiles_ambientOcclusion231;
			o.Occlusion = ( BaseOcclusion187 * _OcclusionIntensity ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15800
553;147;1106;886;-1158.491;818.9753;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;359;1802.97,423.9503;Float;False;2077.416;427.3209;Random Moss;10;259;260;262;263;270;264;272;271;255;273;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ObjectToWorldTransfNode;259;1852.97,473.9503;Float;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;260;2108.97,473.9503;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;262;2316.971,473.9503;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;263;2508.971,473.9503;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;998.21;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;270;2736.464,587.8483;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;272;2780.88,736.2714;Float;False;Property;_TileNoise;Tile Noise;16;0;Create;True;0;0;False;0;0;0.356;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;264;2716.971,473.9503;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;361;1077.652,-659.3774;Float;False;1379.444;792.7505;Base Substance;10;231;185;186;187;237;205;189;332;333;232;;1,1,1,1;0;0
Node;AmplifyShaderEditor.LerpOp;271;3059.178,540.1093;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;255;3261.277,601.6943;Float;True;Property;_Assembly_Tiles_noisemask;Assembly_Tiles_noisemask;6;0;Create;True;0;0;False;0;None;4d4b2b6a902ce7448ab8dacce9d7906b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SubstanceSamplerNode;231;1113.652,-566.2943;Float;True;Property;_SubstanceSample3;Substance Sample 3;7;0;Create;True;0;0;False;0;4d4b2b6a902ce7448ab8dacce9d7906b;0;True;1;0;FLOAT2;0,0;False;7;COLOR;0;COLOR;1;FLOAT3;2;COLOR;3;COLOR;4;COLOR;5;COLOR;6
Node;AmplifyShaderEditor.CommentaryNode;362;2036.931,-1900.001;Float;False;2471.759;887.2072;Base and Moss Albedo;28;288;290;289;241;291;242;195;243;199;198;194;193;211;212;215;197;305;201;245;238;293;276;286;244;292;239;285;284;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ComponentMaskNode;185;1647.919,-222.6269;Float;False;True;False;False;False;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;273;3637.385,603.5314;Float;False;ExtraNoise;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;288;2086.931,-1344.42;Float;False;273;ExtraNoise;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;290;2139.24,-1259.565;Float;False;Property;_MossExtraTweak;Moss Extra Tweak;11;0;Create;True;0;0;False;0;0;3.47;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;186;1959.369,-256.7618;Float;False;BaseHeight;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;241;2126.73,-1466.277;Float;False;186;BaseHeight;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;289;2324.24,-1343.565;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;242;2484.653,-1234.397;Float;False;Property;_MossCoverage;Moss Coverage;9;0;Create;True;0;0;False;0;0;0.271;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;291;2476.241,-1458.565;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;360;2626.462,-929.7169;Float;False;845.796;617.5857;Moss;8;233;234;235;200;247;236;248;191;;1,1,1,1;0;0
Node;AmplifyShaderEditor.OneMinusNode;199;2730.946,-1317.291;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;195;2662.575,-1120.794;Float;False;Property;_MossCoverageHardness;Moss Coverage Hardness;8;0;Create;True;0;0;False;0;2.69;3.73;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;243;2759.654,-1231.397;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;332;1565.935,-494.0959;Float;False;Property;_BaseSubstanceRoughnessMultiplier;Base Substance Roughness Multiplier;17;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;198;2944.759,-1321.118;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;2.69;False;1;FLOAT;0
Node;AmplifyShaderEditor.SubstanceSamplerNode;233;2630.462,-676.2327;Float;True;Property;_SubstanceSample0;Substance Sample 0;5;0;Create;True;0;0;False;0;636a520b068ebe542aa5383904ee54cf;0;True;1;0;FLOAT2;0,0;False;6;COLOR;0;COLOR;1;FLOAT3;2;COLOR;3;COLOR;4;COLOR;5
Node;AmplifyShaderEditor.RegisterLocalVarNode;234;3215.26,-603.4625;Float;False;MossRoughness;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;333;1873.932,-604.0962;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;197;3201.052,-1351.622;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;364;3037.316,-260.0893;Float;False;1614.012;519.7097;Roughness;11;344;296;343;295;314;297;298;313;301;300;354;;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;296;3113.531,-45.83586;Float;False;234;MossRoughness;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;344;3087.316,42.49772;Float;False;Property;_MossRoughnessTweak;Moss Roughness Tweak;12;0;Create;True;0;0;False;0;0;1.019;0.69;1.17;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;232;2193.093,-605.1066;Float;False;BaseRoughness;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;305;3345.874,-1229.072;Float;False;MainMossMask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;248;2655.944,-866.7169;Float;False;Property;_MossTint;Moss Tint;10;0;Create;True;0;0;False;0;0,0,0,0;0.30709,0.3602937,0.06358098,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;295;3419.821,-115.2936;Float;False;232;BaseRoughness;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;314;3419.988,61.32058;Float;False;305;MainMossMask;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;205;1616.919,18.37313;Float;False;BaseColorMask;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;189;1680.031,-395.3774;Float;False;BaseColor;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;343;3437.851,-40.59814;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;211;2577.483,-1829.375;Float;False;Property;_TileTint;Tile Tint;14;0;Create;True;0;0;False;0;0,0,0,0;0.5019608,0.1882349,0.04705852,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;297;3686.712,-48.49763;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;194;2501.583,-1568.325;Float;False;205;BaseColorMask;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;247;3037.946,-789.7168;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0.3014706,0.3014706,0.3014706,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;193;2597.084,-1640.644;Float;False;189;BaseColor;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;236;3220.604,-690.1313;Float;False;MossNormal;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.OneMinusNode;298;3841.887,-118.0452;Float;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;237;1664.92,-312.627;Float;False;BaseNormal;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;191;3216.822,-773.9449;Float;False;MossCover;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;313;3828.503,144.6204;Float;False;273;ExtraNoise;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;212;2836.483,-1691.375;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;215;2803.483,-1563.374;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;363;3788.148,-779.5911;Float;False;773.7559;373.9999;Normals;5;306;307;309;302;308;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;187;1653.919,-110.6269;Float;False;BaseOcclusion;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;307;3869.401,-595.1113;Float;False;237;BaseNormal;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;238;3503.864,-1850.001;Float;False;191;MossCover;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;309;3874.904,-520.5912;Float;False;236;MossNormal;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;306;3838.148,-689.5941;Float;False;305;MainMossMask;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;301;4044.847,-202.9744;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;367;5225.456,-590.7736;Float;False;186;BaseHeight;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;201;3008.294,-1640.207;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;365;4050.55,305.886;Float;False;758.8357;586.8755;Occlusion;3;339;335;338;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;293;3424.139,-1768.834;Float;False;Property;_DarkenMoss;Darken Moss;13;0;Create;True;0;0;False;0;0;0.795;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;276;3627.551,-1517.075;Float;False;273;ExtraNoise;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;245;3356.953,-1523.097;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;335;4100.549,585.7615;Float;True;187;BaseOcclusion;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LayeredBlendNode;302;4159.048,-708.9922;Float;False;6;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;339;4100.549,777.7612;Float;False;Property;_OcclusionIntensity;Occlusion Intensity;19;0;Create;True;0;0;False;0;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;244;3595.652,-1645.397;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;300;4310.909,-206.6492;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;375;5478.618,-827.1682;Float;False;3;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;-0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;292;3901.058,-1736.685;Float;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;286;3866.338,-1321.771;Float;False;Property;_SubstanceAlbedoTweak;Substance Albedo Tweak;18;0;Create;True;0;0;False;0;0;1.7;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;374;5227.653,-475.8734;Float;False;Property;_TileHeight;Tile Height;15;0;Create;True;0;0;False;0;0;0.199;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;373;5847.575,-905.8879;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;285;4089.062,-1384.942;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;354;4476.33,-210.0894;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;338;4574.385,355.8859;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.NormalizeNode;308;4376.902,-729.5911;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;239;4074.845,-1537.59;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.NormalVertexDataNode;369;5759.323,-646.6132;Float;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;235;3220.604,-427.1307;Float;False;MossOcclusion;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;379;5077.51,-769.1299;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;370;6031.321,-915.6763;Float;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WireNode;380;5180.097,-657.0295;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;378;4843.231,-1208.089;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;284;4339.692,-1488.712;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;200;3214.785,-520.4772;Float;False;MossHeight;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;6554.756,-1452.109;Float;False;True;6;Float;ASEMaterialInspector;0;0;Standard;Substance2018;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;True;1;32;75.6;100;True;0.84;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;0;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;260;0;259;1
WireConnection;260;1;259;2
WireConnection;260;2;259;3
WireConnection;262;0;260;0
WireConnection;263;0;262;0
WireConnection;264;0;263;0
WireConnection;264;1;263;0
WireConnection;271;0;264;0
WireConnection;271;1;270;0
WireConnection;271;2;272;0
WireConnection;255;1;271;0
WireConnection;185;0;231;4
WireConnection;273;0;255;1
WireConnection;186;0;185;0
WireConnection;289;0;288;0
WireConnection;289;1;290;0
WireConnection;291;0;241;0
WireConnection;291;1;289;0
WireConnection;199;0;291;0
WireConnection;243;0;242;0
WireConnection;198;0;199;0
WireConnection;198;1;243;0
WireConnection;198;4;195;0
WireConnection;234;0;233;0
WireConnection;333;0;231;0
WireConnection;333;1;332;0
WireConnection;197;0;198;0
WireConnection;232;0;333;0
WireConnection;305;0;197;0
WireConnection;205;0;231;6
WireConnection;189;0;231;1
WireConnection;343;0;296;0
WireConnection;343;1;344;0
WireConnection;297;0;295;0
WireConnection;297;1;343;0
WireConnection;297;2;314;0
WireConnection;247;0;233;1
WireConnection;247;1;248;0
WireConnection;236;0;233;2
WireConnection;298;0;297;0
WireConnection;237;0;231;2
WireConnection;191;0;247;0
WireConnection;212;0;211;0
WireConnection;212;1;193;0
WireConnection;215;0;194;0
WireConnection;187;0;231;5
WireConnection;301;0;298;0
WireConnection;301;1;313;0
WireConnection;201;0;212;0
WireConnection;201;1;193;0
WireConnection;201;2;215;0
WireConnection;245;0;197;0
WireConnection;302;0;306;0
WireConnection;302;1;307;0
WireConnection;302;2;309;0
WireConnection;244;0;201;0
WireConnection;244;1;245;0
WireConnection;300;0;301;0
WireConnection;375;0;367;0
WireConnection;292;0;238;0
WireConnection;292;1;276;0
WireConnection;292;2;293;0
WireConnection;373;0;375;0
WireConnection;373;1;374;0
WireConnection;285;0;276;0
WireConnection;285;1;286;0
WireConnection;354;0;300;0
WireConnection;338;0;335;0
WireConnection;338;1;339;0
WireConnection;308;0;302;0
WireConnection;239;0;244;0
WireConnection;239;1;292;0
WireConnection;239;2;197;0
WireConnection;235;0;233;5
WireConnection;379;0;354;0
WireConnection;370;0;373;0
WireConnection;370;1;369;0
WireConnection;380;0;338;0
WireConnection;378;0;308;0
WireConnection;284;0;239;0
WireConnection;284;1;285;0
WireConnection;200;0;233;4
WireConnection;0;0;284;0
WireConnection;0;1;378;0
WireConnection;0;4;379;0
WireConnection;0;5;380;0
WireConnection;0;11;370;0
ASEEND*/
//CHKSM=57730BC144A1A8F0D879091B6699114AE00FACAD