using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
    [RequireComponent(typeof(Collider))]
    public class CrownTrigger : MonoBehaviour
    {
        public Crown TargetCrown;

        void Update()
        {
            transform.position = TargetCrown.transform.position;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<MainLine>())
            {
                TargetCrown.EnterTrigger();
            }
        }
    }
}