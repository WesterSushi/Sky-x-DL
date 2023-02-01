using UnityEngine;
using DG.Tweening;
using MaxIceFlameTemplate.Basic;

namespace MaxIceFlameTemplate.Animations
{
    public class MaterialColorChanger : MonoBehaviour
    {
        public Material Material;
        public Color OriginalColor = Color.white, Color = Color.white;
        public Ease Ease = Ease.InOutSine;
        public float Time;

        void Start()
        {
            Material.color = OriginalColor;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<MainLine>())
            {
                DOTween.To(() => Material.color, a => Material.color = a, Color, Time).SetEase(Ease);
            }
        }

        [ContextMenu("GetOriginalColor")]
        void Get()
        {
            if(Material != null)
            {
                OriginalColor = Material.color;
            }
            else
            {
                Debug.LogError("未选择材质球！");
            }
        }
    }
}