Shader "Custom/CircleShader"
{
    Properties
    {
         [PerRendererData] _MainTex ("Texture", 2D) = "white" {}/*提高效率,暂时没啥用*/
          _Color("Tint",Color)=(1,1,1,1)

        //设置圆心点和半径
        _Center("Center",Vector)=(0,0,0,0)
        _Radius("Radius",Range(0,1500))=500

        _ColorMask ("Color Mask", Float) = 15

    }
    SubShader
    {
        Tags { "Queue"="Transparent"
                "IgnoreProjector"="True"
                "RenderType"="Transparent"
                "PreviewType"="Plane"
                "CanUseSpiteAtlas"="True"}
        LOD 100
        Cull Off
        Lighting Off
        
        ZWrite Off
        
        Blend SrcAlpha OneMinusSrcAlpha
        

        Pass
        {
            Name "Default"
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"
            #include "UnityCG.cginc"

            #pragma multi_compile __ UNITY_UI_CLIP_RECT
            #pragma multi_compile __ UNITY_UI_ALPHACLIP
            
            

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color:Color;
                float2 uv : TEXCOORD0;

                
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 wordPosition:TEXCOORD01;
                float4 color:Color;
                
            };

            
            

            fixed4 _Color;
            fixed4  _TextureSampleAdd;
            fixed4 _ClipRect;
            float _Radius;
            float4 _MainTex_ST;
            float2 _Center;
            v2f vert (appdata v)
            {
                v2f o;
                

                o.wordPosition=v.vertex;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv,_MainTex);
                o.color =v.color*_Color;//颜色叠加
               
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 color = (tex2D(_MainTex, i.uv)+_TextureSampleAdd) * i.color;
                #ifdef UNITY_UI_CLIP_RECT
                color.a*=UnityGet2DClipping(i.wordPosition.xy,_ClipRect);//区域裁剪
                // apply fog
                #endif
                #ifdef UNITY_UI_ALPHACLIP
                clip(color.a-0.001);//如果透明度为0则剔除
                #endif

                color.a*=(distance(i.wordPosition,_Center.xy)>_Radius);
                color.rbg*=color.a;
                return color;
            }
            ENDCG
        }
    }
}
