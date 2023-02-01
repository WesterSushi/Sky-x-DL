//Script by 橙之夏
using UnityEngine;
using DG.Tweening;
using System;
using MaxIceFlameTemplate.Basic;

namespace MaxIceFlameTemplate.Animations
{
    public class ScaleMax : MonoBehaviour
    {
        public GameObject AnimationObject;//动画物体
        [Serializable]
        public class Max
        {
            public Vector3 Scale;//大小
            public float PosTime;//移动时间
            public Ease Ease = Ease.InOutSine;
            public float WaitTime;//移动空隙延时间
        }
        public Max[] Sca;

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<MainLine>())
            {
                Sequence sequence = DOTween.Sequence();
                for (int i = 0; i < Sca.Length; i++)
                {
                    sequence.Append(AnimationObject.transform.DOScale(Sca[i].Scale, Sca[i].PosTime).SetEase(Sca[i].Ease));
                    sequence.AppendInterval(Sca[i].WaitTime);
                }
            }
        }
    }
}