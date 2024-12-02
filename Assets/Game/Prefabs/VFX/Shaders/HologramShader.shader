Shader "HologramShader/HologramShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}       
        _TintColor ("Tint Color", Color) = (1,1,1,1)
        _Transparency ("Transparency", Range(0.0, 0.5)) = 0.25
        _Cutoff ("Cutout", Range (0.0, 1.0)) = 0.2
        _Distance("Distance", float) = 1
        _Amplitude("Amplitude", float) = 1
        _Speed("Speed", float) = 1
        _MinBaseAmount ("Min Base Amount", Range(0.0, 1.0)) = 0.1
        _MaxBaseAmount ("Max Base Amount", Range(0.0, 1.0)) = 1.0
        _Interval ("Interval", float) = 5
        _SpikeDuration ("Spike Duration", float) = 0.5
        _MinSpikeChance ("Min Spike Chance", float) = 0.05
        _MaxSpikeChance ("Max Spike Chance", float) = 0.3
        _TextureScrollSpeed ("Texture Scroll Speed", float) = 1
        _GlowColor ("Glow Color", Color) = (1,1,1,1)
        _GlowIntensity ("Glow Intensity", Range(0.0, 10.0)) = 5
    }

    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _TintColor;
            float _Transparency;
            float _Cutoff;
            float _Distance;
            float _Amplitude;
            float _Speed;
            float _MinBaseAmount;
            float _MaxBaseAmount;
            float _Interval;
            float _SpikeDuration;
            float _MinSpikeChance;
            float _MaxSpikeChance;
            float _TextureScrollSpeed;
            float4 _GlowColor;
            float _GlowIntensity;

            float rand(float speed) {
                return frac(sin(speed) * 43758.5453);
            }

            float GetRandomSpikeChance(float speed) {
                return lerp(_MinSpikeChance, _MaxSpikeChance, rand(speed));
            }

            float GetRandomBaseAmount(float speed) {
                return lerp(_MinBaseAmount, _MaxBaseAmount, rand(speed));
            }

            float GetRandomAmount(float time){
                float randomTime = floor(time / _Interval) * _Interval;
                float spikeChance = GetRandomSpikeChance(randomTime);
                float spikeTrigger = rand(randomTime + 1.0);

                if(spikeTrigger < spikeChance){
                    float timeInInterval = frac(time / _Interval) * _Interval;
                    if (timeInInterval < _SpikeDuration){
                        return GetRandomBaseAmount(randomTime + 2.0);
                    }
                }

                return 0.0;
            }

            v2f vert (appdata v)
            {
                v2f o;

                float randomAmount = GetRandomAmount(_Time.y);

                v.vertex.x += sin(_Time.y * _Speed + v.vertex.y * _Amplitude) * _Distance * randomAmount;


                o.uv = v.uv;
                o.uv.y += _Time.y * _TextureScrollSpeed;
                

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) + _TintColor;
                col.a *= _Transparency;
                col.a = _Transparency;
                clip(col.rgb - _Cutoff);

                fixed4 glow = _GlowColor * _GlowIntensity;

                col.rgb += glow.rgb * col.a;
                return col;
            }
            ENDCG
        }
    }

    SubShader
    {
        tags { "RenderType" = "Opaque" }
        Pass
        {
            Name "EMISSION"
            Tags { "LightMode" = "Always"}

            CGPROGRAM
            #pragma vertex vertEmission
            #pragma fragment fragEmission
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            float4 _GlowColor;
            float _GlowIntensity;
            sampler2D _MainTex;

            v2f vertEmission (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 fragEmission (v2f i) : SV_Target
            {
                float alpha = tex2D(_MainTex, i.vertex.xy).a;
                return (_GlowColor * _GlowIntensity) * alpha;
            }
            ENDCG
        }
    }
}
