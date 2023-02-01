using UnityEngine;
using MaxIceFlameTemplate.Basic;

namespace MaxIceFlameTemplate.Animations
{
    public class AnimatorPlayer : MonoBehaviour
    {
        public Animator[] Animators;

        void Start()
        {
            for (int i = 0; i < Animators.Length; i++)
            {
                Animators[i].enabled = false;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<MainLine>())
            {
                for (int i = 0; i < Animators.Length; i++)
                {
                    Animators[i].enabled = true;
                }
            }
        }
    }
}