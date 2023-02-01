using UnityEngine;
using DG.Tweening;
using MaxIceFlameTemplate.Basic;

namespace MaxIceFlameTemplate.Animations
{
    public class CameraChanger : MonoBehaviour
    {
        public enum ClearFlags { Skybox, SolidColor}
        public enum Projection { Perspective, Orthographic }
        public UnityEngine.Camera Camera;
        public ClearFlags clearFlags = ClearFlags.SolidColor;
        public Color BackgroundColor = Color.white;
        public Projection projection = Projection.Perspective;
        [Tooltip("仅Perspective像机下可使用")][Range(0f, 179f)] public float FieldOfView = 60f;
        [Tooltip("仅Orthographic像机下可使用")] public float CameraSize = 17.5f;
        public Ease Ease = Ease.InOutSine;
        public float Time;

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<MainLine>())
            {
                switch (clearFlags)
                {
                    case ClearFlags.Skybox:
                        Camera.clearFlags = CameraClearFlags.Skybox;
                        break;
                    case ClearFlags.SolidColor:
                        Camera.clearFlags = CameraClearFlags.SolidColor;
                        break;
                }
                DOTween.To(() => Camera.backgroundColor, a => Camera.backgroundColor = a, BackgroundColor, Time).SetEase(Ease);
                switch (projection)
                {
                    case Projection.Perspective:
                        Camera.orthographic = false;
                        break;
                    case Projection.Orthographic:
                        Camera.orthographic = true;
                        break;
                }
                DOTween.To(() => Camera.fieldOfView, a => Camera.fieldOfView = a, FieldOfView, Time).SetEase(Ease);
                DOTween.To(() => Camera.orthographicSize, a => Camera.orthographicSize = a, CameraSize, Time).SetEase(Ease);
            }
        }

        [ContextMenu("GetDataFromCamera")]
        void Get()
        {
            if (Camera != null)
            {
                switch (Camera.clearFlags)
                {
                    case CameraClearFlags.Skybox:
                        clearFlags = ClearFlags.Skybox;
                        break;
                    case CameraClearFlags.SolidColor:
                        clearFlags = ClearFlags.SolidColor;
                        break;
                }
                BackgroundColor = Camera.backgroundColor;
                if (Camera.orthographic)
                {
                    projection = Projection.Orthographic;
                }
                else
                {
                    projection = Projection.Perspective;
                }
                FieldOfView = Camera.fieldOfView;
                CameraSize = Camera.orthographicSize;
            }
            else
            {
                Debug.LogError("未选择摄像机！");
            }
        }
    }
}