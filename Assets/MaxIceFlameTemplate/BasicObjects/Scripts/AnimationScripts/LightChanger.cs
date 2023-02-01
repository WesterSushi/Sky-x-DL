using UnityEngine;
using DG.Tweening;
using MaxIceFlameTemplate.Basic;

namespace MaxIceFlameTemplate.Animations
{
    public class LightChanger : MonoBehaviour
    {
        public enum LightType { Directional ,Point,Spot}
        public enum ShadowType { NoShadow,HardShadow,SoftShadow}
        public Light Light;
        public LightType Type = LightType.Point;
        public float LightRange = 10f;
        [Range(1f, 179f)] public float SpotAngle = 30f;
        public Color LightColor = Color.white;
        public float Intensity = 1f;
        public ShadowType shadowType = ShadowType.HardShadow;
        [Range(0f, 1f)] public float ShadowStrength = 0.5f;
        public Ease Ease = Ease.InOutSine;
        public float Time;

        void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<MainLine>())
            {
                switch(Type)
                {
                    case LightType.Directional:
                        Light.type = UnityEngine.LightType.Directional;
                        break;
                    case LightType.Point:
                        Light.type = UnityEngine.LightType.Point;
                        break;
                    case LightType.Spot:
                        Light.type = UnityEngine.LightType.Spot;
                        break;
                }
                DOTween.To(() => Light.range, a => Light.range = a, LightRange, Time).SetEase(Ease);
                DOTween.To(() => Light.spotAngle, a => Light.spotAngle = a, SpotAngle, Time).SetEase(Ease);
                DOTween.To(() => Light.color, a => Light.color = a, LightColor, Time).SetEase(Ease);
                DOTween.To(() => Light.intensity, a => Light.intensity = a, Intensity, Time).SetEase(Ease);
                switch(shadowType)
                {
                    case ShadowType.NoShadow:
                        Light.shadows = LightShadows.None;
                        break;
                    case ShadowType.HardShadow:
                        Light.shadows = LightShadows.Hard;
                        break;
                    case ShadowType.SoftShadow:
                        Light.shadows = LightShadows.Soft;
                        break;
                }
                DOTween.To(() => Light.shadowStrength, a => Light.shadowStrength = a, ShadowStrength, Time).SetEase(Ease);
            }
        }

        [ContextMenu("GetDataFromLight")]
        void Get()
        {
            if(Light != null)
            {
                switch(Light.type)
                {
                    case UnityEngine.LightType.Directional:
                        Type = LightType.Directional;
                        break;
                    case UnityEngine.LightType.Point:
                        Type = LightType.Point;
                        break;
                    case UnityEngine.LightType.Spot:
                        Type = LightType.Spot;
                        break;
                }
                LightRange = Light.range;
                SpotAngle = Light.spotAngle;
                LightColor = Light.color;
                Intensity = Light.intensity;
                switch (Light.shadows)
                {
                    case LightShadows.None:
                        shadowType = ShadowType.NoShadow;
                        break;
                    case LightShadows.Hard:
                        shadowType = ShadowType.HardShadow;
                        break;
                    case LightShadows.Soft:
                        shadowType = ShadowType.SoftShadow;
                        break;
                }
                ShadowStrength = Light.shadowStrength;
            }
            else
            {
                Debug.LogError("未选择光照！");
            }
        }
    }
}