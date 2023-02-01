using UnityEngine;
using DG.Tweening;
using MaxIceFlameTemplate.Basic;

namespace MaxIceFlameTemplate.Animations
{
    public class FogChanger : MonoBehaviour
    {
        public enum FogMode { Linear,Exponential, ExponentialSquared}
        public bool EnableFog = true;
        public FogMode Mode;
        public Color FogColor = Color.white;
        public float FogStart, FogEnd = 75f;
        public float FogDensity = 0.02f;
        public Ease Ease = Ease.InOutSine;
        public float Time;

        void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<MainLine>())
            {
                RenderSettings.fog = EnableFog;
                switch(Mode)
                {
                    case FogMode.Linear:
                        RenderSettings.fogMode = UnityEngine.FogMode.Linear;
                        break;
                    case FogMode.Exponential:
                        RenderSettings.fogMode = UnityEngine.FogMode.Exponential;
                        break;
                    case FogMode.ExponentialSquared:
                        RenderSettings.fogMode = UnityEngine.FogMode.ExponentialSquared;
                        break;
                }
                DOTween.To(() => RenderSettings.fogColor, a => RenderSettings.fogColor = a, FogColor, Time).SetEase(Ease);
                DOTween.To(() => RenderSettings.fogStartDistance, a => RenderSettings.fogStartDistance = a, FogStart, Time).SetEase(Ease);
                DOTween.To(() => RenderSettings.fogEndDistance, a => RenderSettings.fogEndDistance = a, FogEnd, Time).SetEase(Ease);
                DOTween.To(() => RenderSettings.fogDensity, a => RenderSettings.fogDensity = a, FogDensity, Time).SetEase(Ease);
            }
        }

        [ContextMenu("GetFogData")]
        void Get()
        {
            EnableFog = RenderSettings.fog;
            switch(RenderSettings.fogMode)
            {
                case UnityEngine.FogMode.Linear:
                    Mode = FogMode.Linear;
                    break;
                case UnityEngine.FogMode.Exponential:
                    Mode = FogMode.Exponential;
                    break;
                case UnityEngine.FogMode.ExponentialSquared:
                    Mode = FogMode.ExponentialSquared;
                    break;
            }
            FogColor = RenderSettings.fogColor;
            FogStart = RenderSettings.fogStartDistance;
            FogEnd = RenderSettings.fogEndDistance;
            FogDensity = RenderSettings.fogDensity;
        }

        [ContextMenu("GetFogColorFromCameraChanger")]
        void Get2()
        {
            if (GetComponent<CameraChanger>() != null)
            {
                FogColor = GetComponent<CameraChanger>().BackgroundColor;
            }
            else
            {
                Debug.LogError("无CameraChanger脚本！");
            }
        }
    }
}