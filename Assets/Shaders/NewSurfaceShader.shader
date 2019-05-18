// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

     Shader "TrailWithZWrite" {
         Properties{
             _Color("Main Color", Color) = (0.9529412,0.5058824,0.5058824,1)
             _AlphaTex("Base (RGB) Trans (A)", 2D) = "white" {}
             //_ExtrusionAmount("Z Extrusion Amount", Float) = 0
         }
             SubShader{
                 Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
                 LOD 100
     
                 ZWrite On
                 ZTest Less
                 Blend SrcAlpha One
                 //Offset 0, -5000
     
             Pass{
     
                 CGPROGRAM
                 #pragma vertex vert
                 #pragma fragment frag
     
                 #include "UnityCG.cginc"
     
                 fixed4 _Color;
                 sampler2D _AlphaTex;
                 //float _ExtrusionAmount;
                 float4 _AlphaTex_ST;
     
                 struct v2f {
                     float4 pos : SV_POSITION;
                     half4 color : COLOR0;
                     float2 uv : TEXCOORD0;
                 };
     
                 v2f vert(appdata_full v)
                 {
                     v2f o;
     
                     //float3 camDirObjSpace = normalize(ObjSpaceViewDir(v.vertex));
                     //v.vertex.xyz += _ExtrusionAmount * camDirObjSpace;
                     o.color = v.color;
     
                     o.pos = UnityObjectToClipPos(v.vertex);
                     o.uv = TRANSFORM_TEX(v.texcoord, _AlphaTex);
                     return o;
                 }
     
                 fixed4 frag(v2f i) : SV_Target
                 {
                     fixed4 texcol = tex2D(_AlphaTex, i.uv) * i.color;
                     return texcol * _Color;
                 }
                 ENDCG
             }
         }
         FallBack "Unlit/Transparent Cutout"
     }