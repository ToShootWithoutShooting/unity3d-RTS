//#EditorFriendly
//#node13:posx=-299:posy=99:title=ParamFloat:title2=2:input0=1:input0type=float:
//#node12:posx=-480:posy=47:title=ParamColor:title2=ParamColor1:input0=(1,1,1,1):input0type=Color:
//#node11:posx=-316:posy=19:title=Multiply:input0=1:input0type=float:input0linkindexnode=12:input0linkindexoutput=0:input1=1:input1type=float:input1linkindexnode=6:input1linkindexoutput=0:
//#node10:posx=-91:posy=232:title=UnpackNormal:input0=0:input0type=float:input0linkindexnode=8:input0linkindexoutput=0:
//#node9:posx=-292:posy=210:title=Texture:title2=Gloss:input0=(0,0):input0type=Vector2:
//#node8:posx=-225.5:posy=270:title=Texture:title2=Normal:input0=(0,0):input0type=Vector2:
//#node7:posx=-182.5:posy=173:title=Texture:title2=Specular:input0=(0,0):input0type=Vector2:
//#node6:posx=-407.5:posy=145:title=Texture:title2=Emissive:input0=(0,0):input0type=Vector2:
//#node5:posx=-360.5:posy=-67:title=Texture:title2=Diffuse:input0=(0,0):input0type=Vector2:
//#node4:posx=0:posy=0:title=Lighting:title2=On:
//#node3:posx=0:posy=0:title=DoubleSided:title2=Back:
//#node2:posx=0:posy=0:title=FallbackInfo:title2=Transparent/Cutout/VertexLit:input0=1:input0type=float:
//#node1:posx=0:posy=0:title=LODInfo:title2=LODInfo1:input0=600:input0type=float:
//#masterNode:posx=0:posy=0:title=Master Node:input0linkindexnode=5:input0linkindexoutput=0:input1linkindexnode=11:input1linkindexoutput=0:input2linkindexnode=7:input2linkindexoutput=0:input3linkindexnode=9:input3linkindexoutput=0:input5linkindexnode=10:input5linkindexoutput=0:
//#sm=3.0
//#blending=Normal
//#ShaderName
Shader "ShaderFusion/DESGM" {
	Properties {
_Color ("Diffuse Color", Color) = (1.0, 1.0, 1.0, 1.0)
_SpecColor ("Specular Color", Color) = (1.0, 1.0, 1.0, 1.0)
_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
//#ShaderProperties
_Diffuse ("Diffuse", 2D) = "white" {}
_ParamColor1 ("ParamColor1", Color) = (1,1,1,1)
_Emissive ("Emissive", 2D) = "white" {}
_Specular ("Specular", 2D) = "white" {}
_Gloss ("Gloss", 2D) = "white" {}
_Normal ("Normal", 2D) = "white" {}
	}
	Category {
		SubShader { 
//#Blend
ZWrite On
//#CatTags
Tags { "RenderType"="Opaque" }
Lighting On
Cull Back
//#LOD
LOD 600
//#GrabPass
		CGPROGRAM
//#LightingModelTag
#pragma surface surf ShaderFusion vertex:vert alphatest:_Cutoff
 //use custom lighting functions
 
 //custom surface output structure
 struct SurfaceShaderFusion {
	half3 Albedo;
	half3 Normal;
	half3 Emission;
	half Specular;
	half3 GlossColor; //Gloss is now three-channel
	half Alpha;
 };
inline fixed4 LightingShaderFusion (SurfaceShaderFusion s, fixed3 lightDir, half3 viewDir, fixed atten)
{
	half3 h = normalize (lightDir + viewDir);
	
	fixed diff = max (0, dot (s.Normal, lightDir));
	
	float nh = max (0, dot (s.Normal, h));
	float3 spec = pow (nh, s.Specular*128.0) * s.GlossColor;
	
	fixed4 c;
	c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * _SpecColor.rgb * spec) * (atten * 2);
	c.a = s.Alpha + _LightColor0.a * _SpecColor.a * spec * atten;
	return c;
}
inline fixed4 LightingShaderFusion_PrePass (SurfaceShaderFusion s, half4 light)
{
	fixed3 spec = light.a * s.GlossColor;
	
	fixed4 c;
	c.rgb = (s.Albedo * light.rgb + light.rgb * _SpecColor.rgb * spec);
	c.a = s.Alpha + spec * _SpecColor.a;
	return c;
}
inline half4 LightingShaderFusion_DirLightmap (SurfaceShaderFusion s, fixed4 color, fixed4 scale, half3 viewDir, bool surfFuncWritesNormal, out half3 specColor)
{
	UNITY_DIRBASIS
	half3 scalePerBasisVector;
	
	half3 lm = DirLightmapDiffuse (unity_DirBasis, color, scale, s.Normal, surfFuncWritesNormal, scalePerBasisVector);
	
	half3 lightDir = normalize (scalePerBasisVector.x * unity_DirBasis[0] + scalePerBasisVector.y * unity_DirBasis[1] + scalePerBasisVector.z * unity_DirBasis[2]);
	half3 h = normalize (lightDir + viewDir);
	float nh = max (0, dot (s.Normal, h));
	float spec = pow (nh, s.Specular * 128.0);
	
	// specColor used outside in the forward path, compiled out in prepass
	specColor = lm * _SpecColor.rgb * s.GlossColor * spec;
	
	// spec from the alpha component is used to calculate specular
	// in the Lighting*_Prepass function, it's not used in forward
	return half4(lm, spec);
}
//#TargetSM
#pragma target 3.0
//#UnlitCGDefs
sampler2D _Diffuse;
float4 _ParamColor1;
sampler2D _Emissive;
sampler2D _Specular;
sampler2D _Gloss;
sampler2D _Normal;
float4 _Color;
		struct Input {
//#UVDefs
float2 sfuv1;
		INTERNAL_DATA
		};
		
		void vert (inout appdata_full v, out Input o) {
//#DeferredVertexBody
o.sfuv1 = v.texcoord.xy;
//#DeferredVertexEnd
		}
		void surf (Input IN, inout SurfaceShaderFusion o) {
			float4 normal = float4(0.0,0.0,1.0,0.0);
			float3 emissive = 0.0;
			float3 specular = 1.0;
			float gloss = 1.0;
			float3 diffuse = 1.0;
			float alpha = 1.0;
//#PreFragBody
float4 node5 = tex2D(_Diffuse,IN.sfuv1);
float4 node6 = tex2D(_Emissive,IN.sfuv1);
float4 node7 = tex2D(_Specular,IN.sfuv1);
float4 node9 = tex2D(_Gloss,IN.sfuv1);
float4 node8 = tex2D(_Normal,IN.sfuv1);
//#FragBody
normal = (float4(float3(UnpackNormal((node8))),0));
gloss = (node9);
specular = (node7);
emissive = ((_ParamColor1) * (node6));
diffuse = (node5);
			
			o.Albedo = diffuse.rgb*_Color;
			#ifdef SHADER_API_OPENGL
			o.Albedo = max(float3(0,0,0),o.Albedo);
			#endif
			
			o.Emission = emissive*_Color;
			#ifdef SHADER_API_OPENGL
			o.Emission = max(float3(0,0,0),o.Emission);
			#endif
			
			o.GlossColor = specular*_SpecColor;
			#ifdef SHADER_API_OPENGL
			o.GlossColor = max(float3(0,0,0),o.GlossColor);
			#endif
			
			o.Alpha = alpha*_Color.a;
			#ifdef SHADER_API_OPENGL
			o.Alpha = max(float3(0,0,0),o.Alpha);
			#endif
			
			o.Specular = gloss;
			#ifdef SHADER_API_OPENGL
			o.Specular = max(float3(0,0,0),o.Specular);
			#endif
			
			o.Normal = normal;
//#FragEnd
		}
		ENDCG
		}
	}
//#Fallback
Fallback "Transparent/Cutout/VertexLit"
}
