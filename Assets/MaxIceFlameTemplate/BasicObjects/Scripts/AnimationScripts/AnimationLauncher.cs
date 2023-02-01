using UnityEngine;
using UnityEngine.Events;
using MaxIceFlameTemplate.Basic;

namespace MaxIceFlameTemplate.Animations
{
    public class AnimationLauncher : MonoBehaviour
    {
        public UnityEvent onTriggerEnter = new UnityEvent();
        private bool Done = false;

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<MainLine>() && !Done)
            {
                onTriggerEnter.Invoke();
                Done = true;
            }
        }
    }
}