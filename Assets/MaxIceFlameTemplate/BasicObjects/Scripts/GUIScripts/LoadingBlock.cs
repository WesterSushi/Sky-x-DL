using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace MaxIceFlameTemplate.UI
{
    public class LoadingBlock : MonoBehaviour
    {
        public Color Color1;
        public Color Color2;
        public float Times;

        void Start()
        {
            frist();
        }

        void Update()
        {
            if (GetComponent<Image>().color == Color1)
            {
                second();
            }
            if (GetComponent<Image>().color == Color2)
            {
                frist();
            }
        }

        public void frist()
        {
            GetComponent<Image>().DOColor(Color1, Times);
        }

        public void second()
        {
            GetComponent<Image>().DOColor(Color2, Times);
        }
    }
}