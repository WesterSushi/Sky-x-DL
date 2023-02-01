using UnityEngine;
using UnityEngine.UI;
using MaxIceFlameTemplate.Basic;

namespace MaxIceFlameTemplate.UI
{
    public class GameOver : MonoBehaviour
    {
        private MainLine MainLine;
        public Text LevelNameText, DiamondText, PercentageText;
        public RawImage NormalCrown1, NormalCrown2, NormalCrown3, PerfectCrown1, PerfectCrown2, PerfectCrown3;
        public Image PerfectImage;
        public static bool IsPerfect = false;
        [HideInInspector] public Texture NormalCrown, NormalCrown_Grey, PerfectCrown, PerfectCrown_Grey;

        void Awake()
        {
            MainLine = FindObjectOfType<MainLine>();
            PerfectImage.gameObject.SetActive(false);
        }

        void Update()
        {
            if (MainLine.gUIObjects.LevelInformation.HasCrown)
            {
                switch (PlayerPrefs.GetInt(MainLine.gUIObjects.LevelInformation.LevelRecordId.ToString() + "_Perfect_HasCrown"))
                {
                    case 0://非完美
                        IsPerfect = false;
                        break;
                    case 1://完美
                        IsPerfect = true;
                        break;
                }
                if (IsPerfect)
                {
                    Crown(true);
                }
                else
                {
                    Crown(false);
                }
                if (MainLine.mainObjects.Percentage >= 100 && MainLine.DiamondCount >= MainLine.gUIObjects.LevelInformation.MaxDiamondCount && MainLine.CrownCount >= 3)
                {
                    PerfectImage.gameObject.SetActive(true);
                }
                else
                {
                    PerfectImage.gameObject.SetActive(false);
                }
            }
            else
            {
                PerfectCrown1.gameObject.SetActive(false);
                PerfectCrown2.gameObject.SetActive(false);
                PerfectCrown3.gameObject.SetActive(false);
                NormalCrown1.gameObject.SetActive(false);
                NormalCrown2.gameObject.SetActive(false);
                NormalCrown3.gameObject.SetActive(false);
                if (MainLine.mainObjects.Percentage >= 100 && MainLine.DiamondCount >= MainLine.gUIObjects.LevelInformation.MaxDiamondCount)
                {
                    PerfectImage.gameObject.SetActive(true);
                }
                else
                {
                    PerfectImage.gameObject.SetActive(false);
                }
            }
            PercentageText.text = MainLine.mainObjects.Percentage + "%";
            DiamondText.text = MainLine.DiamondCount + "/" + MainLine.gUIObjects.LevelInformation.MaxDiamondCount;
            LevelNameText.text = MainLine.gUIObjects.LevelInformation.LevelName;
        }

        public void Crown(bool Perfect)
        {
            if (Perfect)
            {
                PerfectCrown1.gameObject.SetActive(true);
                PerfectCrown2.gameObject.SetActive(true);
                PerfectCrown3.gameObject.SetActive(true);
                NormalCrown1.gameObject.SetActive(false);
                NormalCrown2.gameObject.SetActive(false);
                NormalCrown3.gameObject.SetActive(false);
                if (MainLine.CrownCount < 1)
                {
                    PerfectCrown1.texture = PerfectCrown_Grey;
                    PerfectCrown2.texture = PerfectCrown_Grey;
                    PerfectCrown3.texture = PerfectCrown_Grey;
                }
                if (MainLine.CrownCount == 1)
                {
                    PerfectCrown1.texture = PerfectCrown;
                    PerfectCrown2.texture = PerfectCrown_Grey;
                    PerfectCrown3.texture = PerfectCrown_Grey;
                }
                if (MainLine.CrownCount == 2)
                {
                    PerfectCrown1.texture = PerfectCrown;
                    PerfectCrown2.texture = PerfectCrown;
                    PerfectCrown3.texture = PerfectCrown_Grey;
                }
                if (MainLine.CrownCount >= 3)
                {
                    PerfectCrown1.texture = PerfectCrown;
                    PerfectCrown2.texture = PerfectCrown;
                    PerfectCrown3.texture = PerfectCrown;
                }
            }
            else
            {
                PerfectCrown1.gameObject.SetActive(false);
                PerfectCrown2.gameObject.SetActive(false);
                PerfectCrown3.gameObject.SetActive(false);
                NormalCrown1.gameObject.SetActive(true);
                NormalCrown2.gameObject.SetActive(true);
                NormalCrown3.gameObject.SetActive(true);
                if (MainLine.CrownCount < 1)
                {
                    NormalCrown1.texture = NormalCrown_Grey;
                    NormalCrown2.texture = NormalCrown_Grey;
                    NormalCrown3.texture = NormalCrown_Grey;
                }
                if (MainLine.CrownCount == 1)
                {
                    NormalCrown1.texture = NormalCrown;
                    NormalCrown2.texture = NormalCrown_Grey;
                    NormalCrown3.texture = NormalCrown_Grey;
                }
                if (MainLine.CrownCount == 2)
                {
                    NormalCrown1.texture = NormalCrown;
                    NormalCrown2.texture = NormalCrown;
                    NormalCrown3.texture = NormalCrown_Grey;
                }
                if (MainLine.CrownCount >= 3)
                {
                    NormalCrown1.texture = NormalCrown;
                    NormalCrown2.texture = NormalCrown;
                    NormalCrown3.texture = NormalCrown;
                }
            }
        }
    }
}