// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.04 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.04;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,dith:2,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:33990,y:32542,varname:node_1,prsc:2|diff-11-RGB,emission-394-OUT,alpha-357-OUT;n:type:ShaderForge.SFN_Panner,id:4,x:32434,y:32631,varname:node_4,prsc:2,spu:0,spv:1.5;n:type:ShaderForge.SFN_Fresnel,id:5,x:32716,y:33113,varname:node_5,prsc:2|EXP-284-OUT;n:type:ShaderForge.SFN_Tex2d,id:6,x:32770,y:32672,ptovrint:False,ptlb:Noise Texture,ptin:_NoiseTexture,varname:node_3934,prsc:2,tex:3f5802bf8070604439bda876b7c70e89,ntxv:0,isnm:False|UVIN-4-UVOUT;n:type:ShaderForge.SFN_Color,id:11,x:32727,y:32297,ptovrint:False,ptlb:Main Color,ptin:_MainColor,varname:node_8859,prsc:2,glob:False,c1:0.608564,c2:0.9073499,c3:0.9852941,c4:1;n:type:ShaderForge.SFN_Multiply,id:54,x:33444,y:32642,varname:node_54,prsc:2|A-405-OUT,B-145-OUT;n:type:ShaderForge.SFN_ValueProperty,id:59,x:32613,y:32509,ptovrint:False,ptlb:Emissive Multiply,ptin:_EmissiveMultiply,varname:node_6097,prsc:2,glob:False,v1:2;n:type:ShaderForge.SFN_VertexColor,id:83,x:32941,y:32857,varname:node_83,prsc:2;n:type:ShaderForge.SFN_Multiply,id:84,x:33358,y:32909,varname:node_84,prsc:2|A-6-R,B-83-A,C-164-OUT;n:type:ShaderForge.SFN_Multiply,id:145,x:33185,y:32683,varname:node_145,prsc:2|A-11-RGB,B-6-R;n:type:ShaderForge.SFN_OneMinus,id:164,x:33138,y:33032,varname:node_164,prsc:2|IN-5-OUT;n:type:ShaderForge.SFN_Vector1,id:185,x:32279,y:33037,varname:node_185,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Exp,id:284,x:32517,y:33142,varname:node_284,prsc:2,et:1|IN-185-OUT;n:type:ShaderForge.SFN_Multiply,id:357,x:33637,y:32856,varname:node_357,prsc:2|A-84-OUT,B-164-OUT;n:type:ShaderForge.SFN_Color,id:393,x:33099,y:32389,ptovrint:False,ptlb:Mult Color,ptin:_MultColor,varname:node_3964,prsc:2,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:394,x:33679,y:32621,varname:node_394,prsc:2|A-393-RGB,B-54-OUT;n:type:ShaderForge.SFN_Exp,id:405,x:32830,y:32509,varname:node_405,prsc:2,et:0|IN-59-OUT;proporder:11-59-393-6;pass:END;sub:END;*/

Shader "Custom_Yoon/ShaderForge_Engine_Flame" {
    Properties {
        _MainColor ("Main Color", Color) = (0.608564,0.9073499,0.9852941,1)
        _EmissiveMultiply ("Emissive Multiply", Float ) = 2
        _MultColor ("Mult Color", Color) = (0.5,0.5,0.5,1)
        _NoiseTexture ("Noise Texture", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _NoiseTexture; uniform float4 _NoiseTexture_ST;
            uniform float4 _MainColor;
            uniform float _EmissiveMultiply;
            uniform float4 _MultColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = mul(unity_ObjectToWorld, float4(v.normal,0)).xyz;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 indirectDiffuse = float3(0,0,0);
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 diffuse = (directDiffuse + indirectDiffuse) * _MainColor.rgb;
////// Emissive:
                float4 node_4849 = _Time + _TimeEditor;
                float2 node_4 = (i.uv0+node_4849.g*float2(0,1.5));
                float4 _NoiseTexture_var = tex2D(_NoiseTexture,TRANSFORM_TEX(node_4, _NoiseTexture));
                float3 emissive = (_MultColor.rgb*(exp(_EmissiveMultiply)*(_MainColor.rgb*_NoiseTexture_var.r)));
/// Final Color:
                float3 finalColor = diffuse + emissive;
                float node_5 = pow(1.0-max(0,dot(normalDirection, viewDirection)),exp2(0.5));
                float node_164 = (1.0 - node_5);
                return fixed4(finalColor,((_NoiseTexture_var.r*i.vertexColor.a*node_164)*node_164));
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _NoiseTexture; uniform float4 _NoiseTexture_ST;
            uniform float4 _MainColor;
            uniform float _EmissiveMultiply;
            uniform float4 _MultColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = mul(unity_ObjectToWorld, float4(v.normal,0)).xyz;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 diffuse = directDiffuse * _MainColor.rgb;
/// Final Color:
                float3 finalColor = diffuse;
                float4 node_3909 = _Time + _TimeEditor;
                float2 node_4 = (i.uv0+node_3909.g*float2(0,1.5));
                float4 _NoiseTexture_var = tex2D(_NoiseTexture,TRANSFORM_TEX(node_4, _NoiseTexture));
                float node_5 = pow(1.0-max(0,dot(normalDirection, viewDirection)),exp2(0.5));
                float node_164 = (1.0 - node_5);
                return fixed4(finalColor * ((_NoiseTexture_var.r*i.vertexColor.a*node_164)*node_164),0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
