using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
    public class EndingOpenTrigger : MonoBehaviour
    {
        public Ending ending;

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<MainLine>())
            {
                ending.doopen();
            }
        }
    }
}