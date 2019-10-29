//#EditorFriendly
//#node30:posx=-669:posy=87:title=Multiply:input0=1:input0type=float:input0linkindexnode=28:input0linkindexoutput=0:input1=0.7:input1type=float:
//#node29:posx=-836:posy=-74:title=Number:input0=1:input0type=float:
//#node28:posx=-733:posy=11:title=Subtract:input0=0:input0type=float:input0linkindexnode=29:input0linkindexoutput=0:input1=0:input1type=float:input1linkindexnode=27:input1linkindexoutput=0:
//#node27:posx=-872:posy=38:title=Vector.W:input0=(0,0,0,0):input0type=Vector4:input0linkindexnode=13:input0linkindexoutput=0:
//#node26:posx=-157:posy=-36:title=Add:input0=0:input0type=float:input0linkindexnode=23:input0linkindexoutput=0:input1=0:input1type=float:input1linkindexnode=30:input1linkindexoutput=0:
//#node25:posx=-64:posy=-112:title=Multiply:input0=1:input0type=float:input0linkindexnode=5:input0linkindexoutput=0:input1=1:input1type=float:input1linkindexnode=26:input1linkindexoutput=0:
//#node24:posx=-542:posy=-290:title=ParamColor:title2=Color1:input0=(1,1,1,1):input0type=Color:
//#node23:posx=-238:posy=-85:title=Add:input0=0:input0type=float:input0linkindexnode=18:input0linkindexoutput=0:input1=0:input1type=float:input1linkindexnode=16:input1linkindexoutput=0:
//#node22:posx=-614:posy=-362:title=Vector.Z:input0=(0,0,0,0):input0type=Vector4:input0linkindexnode=13:input0linkindexoutput=0:
//#node21:posx=-394:posy=-325:title=Multiply:input0=1:input0type=float:input0linkindexnode=22:input0linkindexoutput=0:input1=1:input1type=float:input1linkindexnode=24:input1linkindexoutput=0:
//#node20:posx=-385:posy=-168:title=Multiply:input0=1:input0type=float:input0linkindexnode=15:input0linkindexoutput=0:input1=1:input1type=float:input1linkindexnode=19:input1linkindexoutput=0:
//#node19:posx=-519:posy=-166:title=ParamColor:title2=Color2:input0=(1,1,1,1):input0type=Color:
//#node18:posx=-269:posy=-209:title=Add:input0=0:input0type=float:input0linkindexnode=21:input0linkindexoutput=0:input1=0:input1type=float:input1linkindexnode=20:input1linkindexoutput=0:
//#node17:posx=-552:posy=5:title=ParamColor:title2=Color3:input0=(1,1,1,1):input0type=Color:
//#node16:posx=-443:posy=-70:title=Multiply:input0=1:input0type=float:input0linkindexnode=14:input0linkindexoutput=0:input1=1:input1type=float:input1linkindexnode=17:input1linkindexoutput=0:
//#node15:posx=-603:posy=-239:title=Vector.Y:input0=(0,0,0,0):input0type=Vector4:input0linkindexnode=13:input0linkindexoutput=0:
//#node14:posx=-598:posy=-125:title=Vector.X:input0=(0,0,0,0):input0type=Vector4:input0linkindexnode=13:input0linkindexoutput=0:
//#node13:posx=-988:posy=-148:title=Texture:title2=Mask:input0=(0,0):input0type=Vector2:
//#node12:posx=-490:posy=71:title=ParamColor:title2=EmissiveColor:input0=(1,1,1,1):input0type=Color:
//#node11:posx=-305:posy=79:title=Multiply:input0=1:input0type=float:input0linkindexnode=12:input0linkindexoutput=0:input1=1:input1type=float:input1linkindexnode=6:input1linkindexoutput=0:
//#node10:posx=-42:posy=220:title=UnpackNormal:input0=0:input0type=float:input0linkindexnode=8:input0linkindexoutput=0:
//#node9:posx=-125:posy=125:title=Texture:title2=Gloss:input0=(0,0):input0type=Vector2:
//#node8:posx=-157.5:posy=222:title=Texture:title2=Normal:input0=(0,0):input0type=Vector2:
//#node7:posx=-124.5:posy=57:title=Texture:title2=Specular:input0=(0,0):input0type=Vector2:
//#node6:posx=-498.5:posy=153:title=Texture:title2=Emissive:input0=(0,0):input0type=Vector2:
//#node5:posx=-149.5:posy=-182:title=Texture:title2=Diffuse:input0=(0,0):input0type=Vector2:
//#node4:posx=0:posy=0:title=Lighting:title2=On:
//#node3:posx=0:posy=0:title=DoubleSided:title2=Back:
//#node2:posx=0:posy=0:title=FallbackInfo:title2=Transparent/Cutout/VertexLit:input0=1:input0type=float:
//#node1:posx=0:posy=0:title=LODInfo:title2=LODInfo1:input0=600:input0type=float:
//#masterNode:posx=0:posy=0:title=Master Node:input0linkindexnode=25:input0linkindexoutput=0:input1linkindexnode=11:input1linkindexoutput=0:input2linkindexnode=7:input2linkindexoutput=0:input3linkindexnode=9:input3linkindexoutput=0:input5linkindexnode=10:input5linkindexoutput=0:
//#sm=3.0
//#blending=Normal
//#ShaderName
Shader "ShaderFusion/Customizable_DESGM" {
	Properties {
_Color ("Diffuse Color", Color) = (1.0, 1.0, 1.0, 1.0)
_SpecColor ("Specular Color", Color) = (1.0, 1.0, 1.0, 1.0)
_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
//#ShaderProperties
_Diffuse ("Diffuse", 2D) = "white" {}
_Mask ("Mask", 2D) = "white" {}
_Color1 ("Color1", Color) = (1,1,1,1)
_Color2 ("Color2", Color) = (1,1,1,1)
_Color3 ("Color3", Color) = (1,1,1,1)
_EmissiveColor ("EmissiveColor", Color) = (1,1,1,1)
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
sampler2D _Mask;
float4 _Color1;
float4 _Color2;
float4 _Color3;
float4 _EmissiveColor;
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
float4 node13 = tex2D(_Mask,IN.sfuv1);
float4 node6 = tex2D(_Emissive,IN.sfuv1);
float4 node7 = tex2D(_Specular,IN.sfuv1);
float4 node9 = tex2D(_Gloss,IN.sfuv1);
float4 node8 = tex2D(_Normal,IN.sfuv1);
//#FragBody
normal = (float4(float3(UnpackNormal((node8))),0));
gloss = (node9);
specular = (node7);
emissive = ((_EmissiveColor) * (node6));
diffuse = ((node5) * ((((((node13).z) * (_Color1)) + (((node13).y) * (_Color2))) + (((node13).x) * (_Color3))) + (((1) - ((node13).w)) * 0.7)));
			
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
