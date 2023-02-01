//Script by 橙之夏
using UnityEngine;
using DG.Tweening;
using System;
using MaxIceFlameTemplate.Basic;

namespace MaxIceFlameTemplate.Animations
{
    public class RatateMax : MonoBehaviour
    {
        public GameObject AnimationObject;//动画物体
        [Serializable]
        public class Max
        {
            public Vector3 Rot;//角度
            public float PosTime;//移动时间
            public Ease Ease = Ease.InOutSine;
            public float WaitTime;//移动空隙延时间
        }
        public Max[] Rotate;

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<MainLine>())
            {
                Sequence sequence = DOTween.Sequence();
                for (int i = 0; i < Rotate.Length; i++)
                {
                    sequence.Append(AnimationObject.transform.DORotate(Rotate[i].Rot, Rotate[i].PosTime).SetEase(Rotate[i].Ease));
                    sequence.AppendInterval(Rotate[i].WaitTime);
                }
            }
        }
    }
}