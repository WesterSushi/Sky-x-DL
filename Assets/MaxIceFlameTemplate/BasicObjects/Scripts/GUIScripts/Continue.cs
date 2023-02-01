using UnityEngine;
using MaxIceFlameTemplate.Basic;

namespace MaxIceFlameTemplate.UI
{
    public class Continue : MonoBehaviour
    {
        public GameObject ContinueUI;
        [HideInInspector] public MainLine MainLine;

        void Awake()
        {
            MainLine = FindObjectOfType<MainLine>();
        }

        public void Click()
        {
            MainLine.GetComponent<MainLine>().GameRevival();
            ContinueUI.SetActive(false);
            PlayerPrefs.SetInt(MainLine.gUIObjects.LevelInformation.LevelRecordId.ToString() + "DiamondCount", 0);
            MainLine.DiamondCount = 0;
        }
    }
}