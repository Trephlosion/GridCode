// Made with Amplify Shader Editor v1.9.3.2
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Piloto Studio/Screenspace Alpha"
{
	Properties
	{
		_MainTex("Main Texture", 2D) = "white" {}
		_MainTextureChannel("Main Texture Channel", Vector) = (1,1,1,0)
		_MainTexturePanning("Main Texture Panning", Vector) = (0,0,0,0)
		_AlphaOverride("Alpha Override", 2D) = "white" {}
		_AlphaOverridePanning("Alpha Override Panning", Vector) = (0,0,0,0)
		_AlphaOverrideChannel("Alpha Override Channel", Vector) = (1,0,0,0)
		_DetailNoise("Detail Noise", 2D) = "white" {}
		_DetailNoisePanning("Detail Noise Panning", Vector) = (0,0,0,0)
		_DetailDistortionChannel("Detail Distortion Channel", Vector) = (1,0,0,0)
		_DistortionStrenght("Distortion Strenght", Float) = 0
		_DetailMultiplyChannel("Detail Multiply Channel", Vector) = (0,0,0,0)
		_DetailAdditiveChannel("Detail Additive Channel", Vector) = (0,0,0,0)
		_Desaturate("Desaturate?", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "AlphaTest+0" "IsEmissive" = "true"  }
		Cull Back
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 5.0
		#undef TRANSFORM_TEX
		#define TRANSFORM_TEX(tex,name) float4(tex.xy * name##_ST.xy + name##_ST.zw, tex.z, tex.w)
		struct Input
		{
			float4 vertexColor : COLOR;
			float4 uv_texcoord;
			float4 screenPos;
		};

		uniform sampler2D _MainTex;
		uniform float2 _MainTexturePanning;
		uniform sampler2D _DetailNoise;
		uniform float2 _DetailNoisePanning;
		uniform float4 _DetailDistortionChannel;
		uniform float _DistortionStrenght;
		uniform float4 _MainTextureChannel;
		uniform float _Desaturate;
		uniform float4 _DetailMultiplyChannel;
		uniform float4 _DetailAdditiveChannel;
		uniform sampler2D _AlphaOverride;
		uniform float2 _AlphaOverridePanning;
		uniform float4 _AlphaOverrideChannel;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 panner80 = ( 1.0 * _Time.y * _DetailNoisePanning + i.uv_texcoord.xy);
			float4 tex2DNode79 = tex2D( _DetailNoise, panner80 );
			float4 break17_g63 = tex2DNode79;
			float4 appendResult18_g63 = (float4(break17_g63.x , break17_g63.y , break17_g63.z , break17_g63.w));
			float4 clampResult19_g63 = clamp( ( appendResult18_g63 * _DetailDistortionChannel ) , float4( 0,0,0,0 ) , float4( 1,1,1,1 ) );
			float4 break2_g63 = clampResult19_g63;
			float clampResult20_g63 = clamp( ( break2_g63.x + break2_g63.y + break2_g63.z + break2_g63.w ) , 0.0 , 1.0 );
			float DistortionNoise90 = clampResult20_g63;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float2 panner22 = ( 1.0 * _Time.y * _MainTexturePanning + ( ( DistortionNoise90 * _DistortionStrenght ) + ( (float2( 0,0 ) + ((ase_screenPosNorm).xy - float2( 0,0 )) * (float2( 1,1 ) - float2( 0,0 )) / (float2( 1,1 ) - float2( 0,0 ))) * float2( 4,4 ) ) ));
			float4 tex2DNode150 = tex2D( _MainTex, panner22 );
			float4 break17_g60 = tex2DNode150;
			float4 appendResult18_g60 = (float4(break17_g60.x , break17_g60.y , break17_g60.z , break17_g60.w));
			float4 clampResult19_g60 = clamp( ( appendResult18_g60 * _MainTextureChannel ) , float4( 0,0,0,0 ) , float4( 1,1,1,1 ) );
			float4 break2_g60 = clampResult19_g60;
			float clampResult20_g60 = clamp( ( break2_g60.x + break2_g60.y + break2_g60.z + break2_g60.w ) , 0.0 , 1.0 );
			float3 temp_cast_2 = (clampResult20_g60).xxx;
			float3 desaturateInitialColor158 = temp_cast_2;
			float desaturateDot158 = dot( desaturateInitialColor158, float3( 0.299, 0.587, 0.114 ));
			float3 desaturateVar158 = lerp( desaturateInitialColor158, desaturateDot158.xxx, _Desaturate );
			float4 break17_g62 = tex2DNode79;
			float4 appendResult18_g62 = (float4(break17_g62.x , break17_g62.y , break17_g62.z , break17_g62.w));
			float4 clampResult19_g62 = clamp( ( appendResult18_g62 * _DetailMultiplyChannel ) , float4( 0,0,0,0 ) , float4( 1,1,1,1 ) );
			float4 break2_g62 = clampResult19_g62;
			float clampResult20_g62 = clamp( ( break2_g62.x + break2_g62.y + break2_g62.z + break2_g62.w ) , 0.0 , 1.0 );
			float ifLocalVar106 = 0;
			if( ( _DetailMultiplyChannel.x + _DetailMultiplyChannel.y + _DetailMultiplyChannel.z + _DetailMultiplyChannel.w ) <= 0.0 )
				ifLocalVar106 = 1.0;
			else
				ifLocalVar106 = clampResult20_g62;
			float MultiplyNoise92 = ifLocalVar106;
			float4 break17_g57 = tex2DNode79;
			float4 appendResult18_g57 = (float4(break17_g57.x , break17_g57.y , break17_g57.z , break17_g57.w));
			float4 clampResult19_g57 = clamp( ( appendResult18_g57 * _DetailAdditiveChannel ) , float4( 0,0,0,0 ) , float4( 1,1,1,1 ) );
			float4 break2_g57 = clampResult19_g57;
			float clampResult20_g57 = clamp( ( break2_g57.x + break2_g57.y + break2_g57.z + break2_g57.w ) , 0.0 , 1.0 );
			float AdditiveNoise91 = clampResult20_g57;
			o.Emission = ( ( i.vertexColor * float4( desaturateVar158 , 0.0 ) * ( i.uv_texcoord.z + 1.0 ) * MultiplyNoise92 ) + AdditiveNoise91 ).rgb;
			float2 panner44 = ( 1.0 * _Time.y * _AlphaOverridePanning + i.uv_texcoord.xy);
			float4 tex2DNode45 = tex2D( _AlphaOverride, panner44 );
			float4 break2_g50 = ( tex2DNode45 * _AlphaOverrideChannel );
			float AlphaOverride49 = saturate( ( break2_g50.x + break2_g50.y + break2_g50.z + break2_g50.w ) );
			float temp_output_3_0_g51 = ( i.uv_texcoord.w - ( 1.0 - AlphaOverride49 ) );
			o.Alpha = ( i.vertexColor.a * saturate( saturate( ( temp_output_3_0_g51 / fwidth( temp_output_3_0_g51 ) ) ) ) * AlphaOverride49 );
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Unlit keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 5.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float4 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float4 screenPos : TEXCOORD3;
				half4 color : COLOR0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xyzw = customInputData.uv_texcoord;
				o.customPack1.xyzw = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.screenPos = ComputeScreenPos( o.pos );
				o.color = v.color;
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xyzw;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.screenPos = IN.screenPos;
				surfIN.vertexColor = IN.color;
				SurfaceOutput o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutput, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19302
Node;AmplifyShaderEditor.CommentaryNode;103;-784.2378,-2597.328;Inherit;False;1462.886;1030;Extra Noise Setup;14;106;105;86;91;87;92;90;79;85;80;83;81;84;108;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;84;-734.2378,-2214.328;Inherit;False;Property;_DetailNoisePanning;Detail Noise Panning;8;0;Create;True;0;0;0;False;0;False;0,0;0.2,-0.2;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;81;-631.819,-2456.415;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;83;-482.86,-2241.615;Inherit;True;Property;_DetailNoise;Detail Noise;7;0;Create;True;0;0;0;False;0;False;None;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.PannerNode;80;-391.8206,-2408.415;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;34;-2955.764,-1202.928;Inherit;False;2252.64;1173.84;Main Texture Set Vars;17;149;23;147;27;110;145;146;138;135;22;136;10;111;109;150;151;156;;0,0.5461459,1,1;0;0
Node;AmplifyShaderEditor.Vector4Node;85;-180.2379,-2145.328;Inherit;False;Property;_DetailDistortionChannel;Detail Distortion Channel;9;0;Create;True;0;0;0;False;0;False;1,0,0,0;1,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;79;-200.3228,-2353.517;Inherit;True;Property;_TextureSample3;Texture Sample 3;4;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;50;-642.348,-1541.705;Inherit;False;1249.023;565.425;Alpha Override;8;43;44;45;47;48;49;51;157;;0,0.5461459,1,1;0;0
Node;AmplifyShaderEditor.FunctionNode;154;153.6483,-2241.953;Inherit;False;Channel Picker;-1;;63;dc5f4cb24a8bdf448b40a1ec5866280e;0;2;5;FLOAT4;1,0,0,0;False;7;FLOAT4;0,0,0,1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;145;-2846.101,-404.1232;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;43;-592.348,-1491.705;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;51;-616.1282,-1132.945;Inherit;False;Property;_AlphaOverridePanning;Alpha Override Panning;5;0;Create;True;0;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.ComponentMaskNode;146;-2604.829,-359.4064;Inherit;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;90;416.6483,-2215.953;Inherit;False;DistortionNoise;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;44;-352.3488,-1443.705;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;47;-399.7247,-1273.259;Inherit;True;Property;_AlphaOverride;Alpha Override;4;0;Create;True;0;0;0;False;0;False;None;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.GetLocalVarNode;135;-2339.662,-547.2616;Inherit;False;90;DistortionNoise;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;149;-2394.645,-634.368;Inherit;False;Property;_DistortionStrenght;Distortion Strenght;10;0;Create;True;0;0;0;False;0;False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;147;-2375.829,-403.4063;Inherit;True;5;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT2;1,1;False;3;FLOAT2;0,0;False;4;FLOAT2;1,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;45;-160.851,-1388.807;Inherit;True;Property;_TextureSample2;Texture Sample 2;4;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;48;-97.32484,-1188.281;Inherit;False;Property;_AlphaOverrideChannel;Alpha Override Channel;6;0;Create;True;0;0;0;False;0;False;1,0,0,0;1,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;136;-2124.063,-659.4615;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;151;-2072.593,-317.8097;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;4,4;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;157;239.2312,-1200.401;Inherit;False;Channel Picker Alpha;-1;;50;e49841402b321534583d1dc019041b68;0;2;5;FLOAT4;1,0,0,0;False;7;FLOAT4;0,0,0,1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;138;-1995.662,-662.2618;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;23;-2017.901,-501.3873;Inherit;False;Property;_MainTexturePanning;Main Texture Panning;3;0;Create;True;0;0;0;False;0;False;0,0;0.2,0.2;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector4Node;86;-593.2379,-1899.328;Inherit;False;Property;_DetailMultiplyChannel;Detail Multiply Channel;11;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;49;382.6752,-1380.28;Inherit;False;AlphaOverride;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;105;-231.1713,-1862.338;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;153;-13.35168,-1929.953;Inherit;False;Channel Picker;-1;;62;dc5f4cb24a8bdf448b40a1ec5866280e;0;2;5;FLOAT4;1,0,0,0;False;7;FLOAT4;0,0,0,1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;108;-228.1713,-1713.338;Inherit;False;Constant;_Float0;Float 0;15;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;22;-1683.138,-518.7701;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;27;-1773.575,-855.0742;Inherit;True;Property;_MainTex;Main Texture;1;0;Create;False;0;0;0;False;0;False;None;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.GetLocalVarNode;52;-304.4512,205.567;Inherit;False;49;AlphaOverride;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;10;-1419.139,-362.7703;Inherit;False;Property;_MainTextureChannel;Main Texture Channel;2;0;Create;True;0;0;0;False;0;False;1,1,1,0;1,1,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;150;-1508.96,-631.8519;Inherit;True;Property;_TextureSample0;Texture Sample 0;12;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;87;-153.2379,-2547.328;Inherit;False;Property;_DetailAdditiveChannel;Detail Additive Channel;12;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ConditionalIfNode;106;259.8287,-1914.338;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;64;31.54882,205.567;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;60;15.54882,285.5669;Inherit;False;0;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;159;-462.6389,-257.7749;Inherit;False;Property;_Desaturate;Desaturate?;13;0;Create;True;0;0;0;False;0;False;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;162;-382.7326,-134.1047;Inherit;False;0;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;155;132.2033,-2464.99;Inherit;False;Channel Picker;-1;;57;dc5f4cb24a8bdf448b40a1ec5866280e;0;2;5;FLOAT4;1,0,0,0;False;7;FLOAT4;0,0,0,1;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;156;-1069.835,-363.9704;Inherit;False;Channel Picker;-1;;60;dc5f4cb24a8bdf448b40a1ec5866280e;0;2;5;FLOAT4;1,0,0,0;False;7;FLOAT4;0,0,0,1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;92;431.6483,-1884.953;Inherit;False;MultiplyNoise;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;70;287.5488,253.5669;Inherit;False;Step Antialiasing;-1;;51;2a825e80dfb3290468194f83380797bd;0;2;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;37;-168.5735,-497.7309;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DesaturateOpNode;158;-165.6389,-328.7749;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;163;-149.0954,-220.5375;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;98;-292.8669,32.71658;Inherit;False;92;MultiplyNoise;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;91;400.6483,-2501.953;Inherit;False;AdditiveNoise;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;68;495.5488,285.5669;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;61;270.9582,23.08784;Inherit;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;164;170.5334,-373.1786;Inherit;False;4;4;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;101;136.5273,-200.7529;Inherit;False;91;AdditiveNoise;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;110;-1053,-596.252;Inherit;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;109;-1181,-596.252;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;40;627.2988,159.4486;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;111;-864.0005,-549.252;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleAddOpNode;102;408.6009,-362.8981;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;152;158.6752,-1380.28;Inherit;False;Channel Picker;-1;;64;dc5f4cb24a8bdf448b40a1ec5866280e;0;2;5;FLOAT4;1,0,0,0;False;7;FLOAT4;0,0,0,1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;165;787.0098,-573.7703;Float;False;True;-1;7;ASEMaterialInspector;0;0;Unlit;Piloto Studio/Screenspace Alpha;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;2;False;;0;False;;False;0;False;;0;False;;False;0;Custom;0.5;True;True;0;True;Transparent;;AlphaTest;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;2;5;False;;10;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;0;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;16;FLOAT4;0,0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;80;0;81;0
WireConnection;80;2;84;0
WireConnection;79;0;83;0
WireConnection;79;1;80;0
WireConnection;154;5;79;0
WireConnection;154;7;85;0
WireConnection;146;0;145;0
WireConnection;90;0;154;0
WireConnection;44;0;43;0
WireConnection;44;2;51;0
WireConnection;147;0;146;0
WireConnection;45;0;47;0
WireConnection;45;1;44;0
WireConnection;136;0;135;0
WireConnection;136;1;149;0
WireConnection;151;0;147;0
WireConnection;157;5;45;0
WireConnection;157;7;48;0
WireConnection;138;0;136;0
WireConnection;138;1;151;0
WireConnection;49;0;157;0
WireConnection;105;0;86;1
WireConnection;105;1;86;2
WireConnection;105;2;86;3
WireConnection;105;3;86;4
WireConnection;153;5;79;0
WireConnection;153;7;86;0
WireConnection;22;0;138;0
WireConnection;22;2;23;0
WireConnection;150;0;27;0
WireConnection;150;1;22;0
WireConnection;106;0;105;0
WireConnection;106;2;153;0
WireConnection;106;3;108;0
WireConnection;106;4;108;0
WireConnection;64;0;52;0
WireConnection;155;5;79;0
WireConnection;155;7;87;0
WireConnection;156;5;150;0
WireConnection;156;7;10;0
WireConnection;92;0;106;0
WireConnection;70;1;64;0
WireConnection;70;2;60;4
WireConnection;158;0;156;0
WireConnection;158;1;159;0
WireConnection;163;0;162;3
WireConnection;91;0;155;0
WireConnection;68;0;70;0
WireConnection;164;0;37;0
WireConnection;164;1;158;0
WireConnection;164;2;163;0
WireConnection;164;3;98;0
WireConnection;110;0;109;0
WireConnection;109;0;150;0
WireConnection;109;1;10;0
WireConnection;40;0;61;4
WireConnection;40;1;68;0
WireConnection;40;2;52;0
WireConnection;111;0;110;0
WireConnection;111;1;110;1
WireConnection;111;2;110;2
WireConnection;111;3;110;3
WireConnection;102;0;164;0
WireConnection;102;1;101;0
WireConnection;152;5;45;0
WireConnection;152;7;48;0
WireConnection;165;2;102;0
WireConnection;165;9;40;0
ASEEND*/
//CHKSM=213E0962811E2B826B32F67A67084B2A05C19799