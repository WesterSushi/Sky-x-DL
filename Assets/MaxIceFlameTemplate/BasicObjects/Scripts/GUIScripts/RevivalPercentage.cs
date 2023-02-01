using UnityEngine;
using UnityEngine.UI;
using MaxIceFlameTemplate.Basic;

namespace MaxIceFlameTemplate.UI
{
    public class RevivalPercentage : MonoBehaviour
    {
        public Text PercentageText;
        public Image PercentageBar;
        private string a;
        private float b;

        void Update()
        {
            b = float.Parse(FindObjectOfType<MainLine>().mainObjects.Percentage.ToString());
            PercentageText.text = FindObjectOfType<MainLine>().mainObjects.Percentage.ToString() + "%";
            PercentageBar.fillAmount = b / 100f;
        }
    }
}