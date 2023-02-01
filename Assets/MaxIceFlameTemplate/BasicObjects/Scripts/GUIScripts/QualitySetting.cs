using UnityEngine;
using UnityEngine.UI;

namespace MaxIceFlameTemplate.UI
{
    public class QualitySetting : MonoBehaviour
    {
        public Text shower;
        [HideInInspector] public string vl = "极低", l = "低", m = "中", h = "高", vh = "极高", u = "极致";
        [HideInInspector] public int id;

        void Start()
        {
            id = QualitySettings.GetQualityLevel() + 1;
        }

        public void click()
        {
            id += 1;
        }

        void Update()
        {
            if (id <= 1)
            {
                QualitySettings.SetQualityLevel(0);
                shower.text = "画质：" + vl;
            }
            if (id == 2)
            {
                QualitySettings.SetQualityLevel(1);
                shower.text = "画质：" + l;
            }
            if (id == 3)
            {
                QualitySettings.SetQualityLevel(2);
                shower.text = "画质：" + m;
            }
            if (id == 4)
            {
                QualitySettings.SetQualityLevel(3);
                shower.text = "画质：" + h;
            }
            if (id == 5)
            {
                QualitySettings.SetQualityLevel(4);
                shower.text = "画质：" + vh;
            }
            if (id >= 6)
            {
                QualitySettings.SetQualityLevel(5);
                shower.text = "画质：" + u;
            }
            if (id > 6)
            {
                id = 1;
            }
            if (id < 1)
            {
                id = 1;
            }
        }
    }
}