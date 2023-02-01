//Script by 橙之夏
using UnityEngine;
using DG.Tweening;
using System;
using MaxIceFlameTemplate.Basic;

namespace MaxIceFlameTemplate.Animations
{
    public class MovingPosMax : MonoBehaviour
    {
        public GameObject AnimationObject;
        [Serializable]
        public class Max
        {
            public Vector3 Pos;//位置
            public Ease Ease = Ease.InOutSine;
            public float PosTime;//移动时间
            public float WaitTime;//移动空隙延时间
        }
        public Max[] Position;

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<MainLine>())
            {
                Sequence sequence = DOTween.Sequence();
                for (int i = 0; i < Position.Length; i++)
                {
                    sequence.Append(AnimationObject.transform.DOMove(Position[i].Pos, Position[i].PosTime).SetEase(Position[i].Ease));
                    sequence.AppendInterval(Position[i].WaitTime);
                }
            }
        }
    }
}