using UnityEngine;
using DG.Tweening;
using MaxIceFlameTemplate.Basic;

namespace MaxIceFlameTemplate.UI
{
    public class ExitContinue : MonoBehaviour
    {
        [HideInInspector] public MainLine MainLine;
        public GameObject ContinueUI;

        void Start()
        {
            MainLine = FindObjectOfType<MainLine>();
        }

        public void click()
        {
            MainLine.gUIObjects.GameOverInterface.gameObject.SetActive(true);
            ContinueUI.SetActive(false);
            MainLine.CrownCount = 0;
            DOTween.Clear();
        }
    }
}