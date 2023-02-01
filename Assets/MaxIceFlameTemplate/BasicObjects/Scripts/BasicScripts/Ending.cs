using UnityEngine;
using DG.Tweening;
using MaxIceFlameTemplate.Camera;

namespace MaxIceFlameTemplate.Basic
{
    public class Ending : MonoBehaviour
    {
        private MainLine MainLine;
        private EndingInvokeTrigger EndTrigger;
        public float Rate = 1;
        public float OpenNeedTime = 1;
        public float WinWaitTime = 1;
        private Transform Ending_Left;
        private Transform Ending_Right;

        public void Start()
        {
            Ending_Left = transform.Find("Ending_Left").GetComponent<Transform>();
            Ending_Right = transform.Find("Ending_Right").GetComponent<Transform>();
            EndTrigger = FindObjectOfType<EndingInvokeTrigger>();
            MainLine = FindObjectOfType<MainLine>();
        }

        public void InvokeWin()
        {
            MainLine.GetComponent<MainLine>().mainObjects.EnableTurn = false;
            if (MainLine.GetComponent<MainLine>().mainObjects.MainCamera.transform.parent.GetComponent<CameraFollower>().Following)
            {
                MainLine.GetComponent<MainLine>().mainObjects.MainCamera.transform.parent.GetComponent<CameraFollower>().Following = false;
            }
            Invoke("win", WinWaitTime);
        }
        public void doopen()
        {
            Ending_Left.DOLocalMoveZ(-0.1f * Rate, OpenNeedTime, false);
            Ending_Right.DOLocalMoveZ(0.1f * Rate, OpenNeedTime, false);
        }
        public void win()
        {
            MainLine.GetComponent<MainLine>().GameOver(true, true);
            EndTrigger.GetComponent<EndingInvokeTrigger>().playsound();
        }

    }
}