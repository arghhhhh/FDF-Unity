Shader "Custom/Flatten" {
    Properties{
        _Color("Color", Color) = (1,1,1,1)
        _Elevation("Elevation", Range(1, 0)) = 0
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float _Elevation;
            float4 _Color;

            struct MeshData
            {
                float4 vertex : POSITION;
            };

            struct Interpolators
            {
                float4 vertex : SV_POSITION;
            };

            Interpolators vert(MeshData v) {
                Interpolators o;
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.vertex.z = o.vertex.z - (1 + o.vertex.z) * _Elevation;
                
                return o;
            }

            float4 frag(Interpolators i) : SV_Target {
                return _Color;
            }

            ENDCG
        }
    }
}