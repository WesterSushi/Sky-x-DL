using System;
using UnityEngine;
using DG.Tweening;
using MaxIceFlameTemplate.Basic;

namespace MaxIceFlameTemplate.Animations
{
    public class TransformAnimation : MonoBehaviour
    {
        [Serializable]
        public class Ani_Position
        {
            public Vector3 Position;
            public Ease Ease = Ease.InOutSine;
            public float Time = 0f;
            public bool SpaceWorld = true;
        }
        [Serializable]
        public class Ani_Rotation
        {
            public Vector3 Rotation;
            public Ease Ease = Ease.InOutSine;
            public float Time = 0f;
            public bool SpaceWorld = true;
        }
        [Serializable]
        public class Ani_Scale
        {
            public Vector3 Scale = Vector3.one;
            public Ease Ease = Ease.InOutSine;
            public float Time = 0f;
        }
        public bool EnablePosition = true;
        public Ani_Position Position;
        public bool EnableRotation = true;
        public Ani_Rotation Rotation;
        public bool EnableScale = true;
        public Ani_Scale Scale;
        [Tooltip("时间启动动画，若为false则是触发器启动")] public bool LaunchByTime = false;
        [Tooltip("启动动画等待时间，仅LaunchByTime为true时有效")] public float WaitTime = 0f;
        private bool Done_Pos = false, Done_Rot = false, Done_Sca = false;

        void Update()
        {
            AudioSource audio = FindObjectOfType<MainLine>().start_audio;
            bool Done = false;
            if(LaunchByTime && audio.time >= WaitTime && !Done)
            {
                LaunchAnimation();
                Done = true;
            }
        }

        public void LaunchAnimation()
        {
            if(EnablePosition && !Done_Pos)
            {
                if(Position.SpaceWorld)
                {
                    transform.DOMove(Position.Position, Position.Time).SetEase(Position.Ease);
                }
                else
                {
                    transform.DOLocalMove(Position.Position, Position.Time).SetEase(Position.Ease);
                }
                Done_Pos = true;
            }
            if (EnableRotation && !Done_Rot)
            {
                if (Rotation.SpaceWorld)
                {
                    transform.DORotate(Rotation.Rotation, Rotation.Time).SetEase(Rotation.Ease);
                }
                else
                {
                    transform.DOLocalRotate(Rotation.Rotation, Rotation.Time).SetEase(Rotation.Ease);
                }
                Done_Rot = true;
            }
            if(EnableScale && !Done_Sca)
            {
                transform.DOScale(Scale.Scale, Scale.Time).SetEase(Scale.Ease);
                Done_Sca = true;
            }
        }

        [ContextMenu("GetTransformData")]
        void Get()
        {
            if(EnablePosition)
            {
                if(Position.SpaceWorld)
                {
                    Position.Position = transform.position;
                }
                else
                {
                    Position.Position = transform.localPosition;
                }
            }
            if (EnableRotation)
            {
                if (Rotation.SpaceWorld)
                {
                    Rotation.Rotation = transform.eulerAngles;
                }
                else
                {
                    Rotation.Rotation = transform.localEulerAngles;
                }
            }
            if (EnableScale)
            {
                Scale.Scale = transform.localScale;
            }
        }
    }
}