using UnityEngine;
using DG.Tweening;

namespace MaxIceFlameTemplate.UI
{
    public class CanvasAlpha : MonoBehaviour
    {
        public CanvasGroup firstCanvas, nextCanvas;

        public void to_first()
        {
            firstCanvas.gameObject.SetActive(true);
            DOTween.To(() => firstCanvas.alpha, x => firstCanvas.alpha = x, 1, 0.3f);
            DOTween.To(() => nextCanvas.alpha, x => nextCanvas.alpha = x, 0, 0.3f);
            Invoke("msfalse", 0.3f);
        }

        void msfalse()
        {
            nextCanvas.gameObject.SetActive(false);
        }

        public void to_next()
        {
            nextCanvas.gameObject.SetActive(true);
            DOTween.To(() => nextCanvas.alpha, x => nextCanvas.alpha = x, 1, 0.3f);
            DOTween.To(() => firstCanvas.alpha, x => firstCanvas.alpha = x, 0, 0.3f);
            Invoke("sfalse", 0.3f);
        }

        void sfalse()
        {
            firstCanvas.gameObject.SetActive(false);
        }
        
        public void hideFirst()
        {
            DOTween.To(() => firstCanvas.alpha, x => firstCanvas.alpha = x, 0, 0.3f);
            Invoke("sfalse", 0.3f);
        }
        
    }
}