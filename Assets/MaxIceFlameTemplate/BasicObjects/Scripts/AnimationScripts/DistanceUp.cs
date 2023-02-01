using UnityEngine;
using DG.Tweening;
using MaxIceFlameTemplate.Basic;

namespace MaxIceFlameTemplate.Animations
{
    public class DistanceUp : MonoBehaviour
    {
        public Transform CheckObject;
        public float DownValue = 10f, CheckDistance = 20f;
        public Ease Ease = Ease.InOutSine;
        public float Time = 1f;
        private Vector3 OriginalPosition = Vector3.zero;

        void Awake()
        {
            OriginalPosition = transform.position;
        }

        void Start()
        {
            Vector3 vector = new Vector3(transform.position.x, transform.position.y - DownValue, transform.position.z);
            transform.position = vector;
        }

        void Update()
        {
            bool done = false;
            if (!done && FindObjectOfType<MainLine>().start)
            {
                float distance = Vector3.Distance(CheckObject.position, transform.position);
                if (distance <= CheckDistance)
                {
                    transform.DOMove(OriginalPosition, Time).SetEase(Ease);
                    done = true;
                }
            }
        }
    }
}