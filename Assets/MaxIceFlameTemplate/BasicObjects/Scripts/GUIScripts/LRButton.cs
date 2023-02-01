using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace MaxIceFlameTemplate.UI
{
    public class LRButton : MonoBehaviour
    {
        public MenuSettings MenuSettings;
        [HideInInspector] public Trip Trip;
        [HideInInspector] public Setting Setting;
        public enum lr { Left, Right };
        public lr LeftOrRight;

        private void Start()
        {
            Trip = FindObjectOfType<Trip>();
            Setting = FindObjectOfType<Setting>();
        }
        public void click()
        {
            DOTween.Clear();
            if (LeftOrRight == lr.Left)
            {
                if (MenuSettings.NowLevelId > 0)
                {
                    MenuSettings.NowLevelId -= 1;
                    MenuSettings.LevelModelHolder.transform.DOMove(MenuSettings.LV3Last, 0.5f);
                }
            }
            else
            {
                if (MenuSettings.NowLevelId < MenuSettings.LevelInfos.Length - 1)
                {
                    MenuSettings.NowLevelId += 1;
                    MenuSettings.LevelModelHolder.transform.DOMove(MenuSettings.LV3Next, 0.5f);
                }
            }
            MenuSettings.PlayButton.GetComponent<Button>().enabled = false;
            StartCoroutine(clicks());
            MenuSettings.MainCamera.DOColor(MenuSettings.LevelInfos[MenuSettings.NowLevelId].ThisLevelCameraBackColor, 0.5f);
            Setting.GetComponent<Button>().enabled = false;
            Trip.GetComponent<Button>().enabled = false;
            MenuSettings.AudioPlayer.Stop();

        }
        IEnumerator clicks()
        {
            yield return new WaitForSeconds(0.5f);
            MenuSettings.PlayButton.GetComponent<Button>().enabled = true;
            Setting.GetComponent<Button>().enabled = true;
            Trip.GetComponent<Button>().enabled = true;
            if (!MenuSettings.AudioPlayer.isPlaying)
            {
                MenuSettings.AudioPlayer.Play();
            }
        }
    }
}