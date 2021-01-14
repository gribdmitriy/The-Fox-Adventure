// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "NEngine/Light/Shadow" {
    Properties{
        _LightPosition("Light position", Vector) = (0, 0, 0, 0)
        _ShadowLength("Shadow length", Range(0, 30)) = 0.1
        _ShadowColor("Shadow color", Color) = (0, 0, 0, 1)
    }
        SubShader{
            Pass {
                Tags {
                    "Queue" = "Transparent"
                    "IgnoreProjector" = "True"
                    "RenderType" = "Transparent"
                }

                Cull Off

                CGPROGRAM

                #pragma vertex vert
                #pragma fragment frag

                struct appdata {
                    fixed4 vertex : POSITION;
                    fixed4 normal : NORMAL;
                };

                struct v2f {
                    fixed4 vertex : SV_POSITION;
                    fixed4 color : COLOR;
                };

                fixed2 _LightPosition;
                fixed _ShadowLength;
                fixed4 _ShadowColor;

                v2f vert(appdata v) {
                    v2f o;

                    fixed2 normal = v.normal.xy;
                    fixed2 position = v.vertex.xy;

                    fixed2 delta = normalize(_LightPosition - position);

                    if (dot(delta, normal) > 0)
                    {
                        o.vertex = 0;
                        o.color = 0;
                        return o;
                    }

                    if (v.normal.z == 0)
                    {
                        o.color = _ShadowColor;
                        o.vertex = UnityObjectToClipPos(v.vertex);

                        return o;
                    }

                    o.color = _ShadowColor;
                    fixed2 direction = -delta * _ShadowLength;
                    fixed4 vertex = v.vertex + fixed4(direction.xy, 0, 0);

                    o.vertex = UnityObjectToClipPos(vertex);

                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    return i.color;
                }

                ENDCG
            }
    }
}