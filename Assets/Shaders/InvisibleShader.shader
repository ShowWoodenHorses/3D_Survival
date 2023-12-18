Shader "Custom/InvisibleShader"
{
    Properties
    {
        _Color("Color", Color) = (1, 1, 1, 1)
        _IsInsideBuilding("Is Inside Building", Range(0, 1)) = 0
    }

    SubShader
    {
        Tags{ "Queue" = "Overlay" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : POSITION;
            };

            fixed4 _Color;
            float _IsInsideBuilding;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : COLOR
            {
                // Применяем прозрачность в зависимости от переменной _IsInsideBuilding
                float clipValue = smoothstep(0.5, 1.0, _IsInsideBuilding);
                clip(clipValue - 0.5);

                return _Color;
            }
            ENDCG
        }
    }
}
