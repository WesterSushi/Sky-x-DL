using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
    public class SpeedChanger : MonoBehaviour
    {
        public float Speed = 2.4f;

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<MainLine>())
            {
                FindObjectOfType<MainLine>().mainObjects.Speed = Speed;
            }
        }
    }
}