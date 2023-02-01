using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using MaxIceFlameTemplate.Basic;

namespace MaxIceFlameTemplate.Animations
{
    public class ImageColorChanger : MonoBehaviour
    {
        public Image Image;
        public Color Color = Color.white;
        public Ease Ease = Ease.InOutSine;
        public float Time;

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<MainLine>())
            {
                Image.DOColor(Color, Time).SetEase(Ease);
            }
        }
    }
}