Shader "Custom/HillsShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)

        _Color1("Color1", Color) = (1,1,1,1)
        _Color2("Color2", Color) = (1,1,1,1)
        _Color3("Color3", Color) = (1,1,1,1)
        _Color4("Color4", Color) = (1,1,1,1)
        _Color5("Color5", Color) = (1,1,1,1)
        _Color6("Color6", Color) = (1,1,1,1)
        _Color7("Color7", Color) = (1,1,1,1)
        _Color8("Color8", Color) = (1,1,1,1)
        _Color9("Color9", Color) = (1,1,1,1)
        _Color10("Color10", Color) = (1,1,1,1)
        _Color11("Color11", Color) = (1,1,1,1)
        _Color12("Color12", Color) = (1,1,1,1)



        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 vertexP;
        };

        half _Glossiness;
        half _Metallic;

        fixed4 _Color;

        fixed4 _Color1;
        fixed4 _Color2;
        fixed4 _Color3;
        fixed4 _Color4;
        fixed4 _Color5;
        fixed4 _Color6;
        fixed4 _Color7;
        fixed4 _Color8;
        fixed4 _Color9;
        fixed4 _Color10;
        fixed4 _Color11;
        fixed4 _Color12;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)
        
            
        void vert(inout appdata_full v, out Input o) 
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.vertexP = v.vertex;
        }
        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

            float3 localVertexP = mul(unity_WorldToObject, IN.vertexP);

            //c = float4(IN.vertexP, 1.0);
            
            if (localVertexP.y >= 1250.0)
                c = _Color12;
            else if (localVertexP.y >= 1150.0)
                c = _Color11;
            else if (localVertexP.y >= 1050.0)
                c = _Color10;
            else if (localVertexP.y >= 950.0)
                c = _Color9;
            else if (localVertexP.y >= 850.0)
                c = _Color8;
            else if (localVertexP.y >= 750.0)
                c = _Color7;
            else if (localVertexP.y >= 650.0)
                c = _Color6;
            else if (localVertexP.y >= 550.0)
                c = _Color6;
            else if (localVertexP.y >= 450.0)
                c = _Color5;
            else if (localVertexP.y >= 350.0)
                c = _Color4;
            else if (localVertexP.y >= 250.0)
                c = _Color3;
            else if (localVertexP.y >= 150.0)
                c = _Color2;
            else if (localVertexP.y >= 50.0)
                c = _Color1;
                
            c *= tex2D(_MainTex, IN.uv_MainTex);

            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            //o.Metallic = _Metallic;
            //o.Smoothness = _Glossiness;
            o.Alpha = c.a;



        }
        ENDCG
    }
    FallBack "Diffuse"
}
