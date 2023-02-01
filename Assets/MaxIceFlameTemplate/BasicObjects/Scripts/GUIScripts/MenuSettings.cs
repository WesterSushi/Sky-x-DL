using UnityEngine;
using UnityEngine.UI;

namespace MaxIceFlameTemplate.UI
{
    public class MenuSettings : MonoBehaviour
    {
        public Text NameText;
        public Text DiamondText, PercentageText;
        public LRButton LeftButton, RightButton;
        public GameObject LevelModelHolder;
        public LevelInformation[] LevelInfos;

        [HideInInspector] public Vector3 LV3Next, LV3Last;
        [HideInInspector] public static int NowLevelId = 0;
        [HideInInspector] public string SceneName;
        [HideInInspector] public int NowPer, NowDia, NowCro;
        [HideInInspector] public int NowRecordId;
        [HideInInspector] public UnityEngine.Camera MainCamera;
        [HideInInspector] public PlayLevel PlayButton;
        [HideInInspector] public AudioSource AudioPlayer;
        private bool DontDestroyOnLoadDone = false;

        void Awake()
        {
            MainCamera.backgroundColor = LevelInfos[NowLevelId].ThisLevelCameraBackColor;
            MainCamera = FindObjectOfType<UnityEngine.Camera>();
            PlayButton = FindObjectOfType<PlayLevel>();
            AudioPlayer = FindObjectOfType<AudioSource>();
            AudioPlayer.clip = LevelInfos[NowLevelId].ThisLevelSound;
            MainCamera.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            AudioPlayer.Play();
            LevelModelHolder.transform.position = LevelInfos[0].ModelPosition;
        }

        void Start()
        {
            if (!DontDestroyOnLoadDone)
            {
                DontDestroyOnLoad(AudioPlayer);
                DontDestroyOnLoadDone = true;
            }
        }

        public void MassageSeter()
        {
            if (PlayerPrefs.GetInt(LevelInfos[NowLevelId].RecordId.ToString() + "Percentage") >= NowPer)
            {
                NowPer = PlayerPrefs.GetInt(LevelInfos[NowLevelId].RecordId.ToString() + "Percentage");
            }
            if (PlayerPrefs.GetInt(LevelInfos[NowLevelId].RecordId.ToString() + "DiamondCount") >= NowDia)
            {
                NowDia = PlayerPrefs.GetInt(LevelInfos[NowLevelId].RecordId.ToString() + "DiamondCount");
            }
            if (PlayerPrefs.GetInt(LevelInfos[NowLevelId].RecordId.ToString() + "CrownCount") >= NowCro)
            {
                NowCro = PlayerPrefs.GetInt(LevelInfos[NowLevelId].RecordId.ToString() + "CrownCount");
            }
        }

        public void MassageChanger()
        {
            AudioPlayer.clip = LevelInfos[NowLevelId].ThisLevelSound;
            NameText.text = LevelInfos[NowLevelId].LevelName;
            SceneName = LevelInfos[NowLevelId].SceneName;
            NowRecordId = LevelInfos[NowLevelId].RecordId;
            NowPer = PlayerPrefs.GetInt(LevelInfos[NowLevelId].RecordId.ToString() + "Percentage");
            NowDia = PlayerPrefs.GetInt(LevelInfos[NowLevelId].RecordId.ToString() + "DiamondCount");
            NowCro = PlayerPrefs.GetInt(LevelInfos[NowLevelId].RecordId.ToString() + "CrownCount");
        }

        public void PDC_TextChanger()
        {
            DiamondText.text = NowDia + "/" + LevelInfos[NowLevelId].MaxDiamondCount;
            PercentageText.text = NowPer + "%";
        }

        void Update()
        {
            MassageSeter();
            MassageChanger();
            PDC_TextChanger();

            if (NowLevelId <= 0)
            {
                LeftButton.GetComponent<Image>().color = new Color(0.5882353f, 0.5882353f, 0.5882353f, 1f);
                LeftButton.GetComponent<Button>().enabled = false;
            }
            else
            {
                LeftButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                LeftButton.GetComponent<Button>().enabled = true;
            }
            if (NowLevelId >= LevelInfos.Length - 1)
            {
                RightButton.GetComponent<Image>().color = new Color(0.5882353f, 0.5882353f, 0.5882353f, 1f);
                RightButton.GetComponent<Button>().enabled = false;
            }
            else
            {
                RightButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                RightButton.GetComponent<Button>().enabled = true;
            }

            if (NowCro < 1)
            {
                LevelInfos[NowLevelId].ThisLevelCrownModel1.SetActive(false);
                LevelInfos[NowLevelId].ThisLevelCrownModel2.SetActive(false);
                LevelInfos[NowLevelId].ThisLevelCrownModel3.SetActive(false);
            }
            if (NowCro == 1)
            {
                LevelInfos[NowLevelId].ThisLevelCrownModel1.SetActive(true);
                LevelInfos[NowLevelId].ThisLevelCrownModel2.SetActive(false);
                LevelInfos[NowLevelId].ThisLevelCrownModel3.SetActive(false);
            }
            if (NowCro == 2)
            {
                LevelInfos[NowLevelId].ThisLevelCrownModel1.SetActive(true);
                LevelInfos[NowLevelId].ThisLevelCrownModel2.SetActive(true);
                LevelInfos[NowLevelId].ThisLevelCrownModel3.SetActive(false);
            }
            if (NowCro >= 3)
            {
                LevelInfos[NowLevelId].ThisLevelCrownModel1.SetActive(true);
                LevelInfos[NowLevelId].ThisLevelCrownModel2.SetActive(true);
                LevelInfos[NowLevelId].ThisLevelCrownModel3.SetActive(true);
            }

            if (NowCro >= 3 && NowDia >= 10 && NowPer >= 100)
            {
                LevelInfos[NowLevelId].ThisLevelCrownModel1.SetActive(false);
                LevelInfos[NowLevelId].ThisLevelCrownModel2.SetActive(false);
                LevelInfos[NowLevelId].ThisLevelCrownModel3.SetActive(false);
                LevelInfos[NowLevelId].ThisLevelPerfectCrownHolder.SetActive(true);
            }
            else
            {
                LevelInfos[NowLevelId].ThisLevelPerfectCrownHolder.SetActive(false);
            }

            if (NowLevelId + 1 < LevelInfos.Length)
            {
                LV3Next = LevelInfos[NowLevelId + 1].ModelPosition;
            }
            if (NowLevelId - 1 >= 0)
            {
                LV3Last = LevelInfos[NowLevelId - 1].ModelPosition;
            }
        }
    }
}