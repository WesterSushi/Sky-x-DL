using UnityEngine;
using MaxIceFlameTemplate.Basic;

namespace MaxIceFlameTemplate.Camera
{
    public class CameraShakeTrigger : MonoBehaviour
    {
        public CameraShake CameraShake;
        public float seconds = 1f;
        public float quake = 2f;

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<MainLine>())
            {
                CameraShake.ShakeFor(seconds, quake);
            }
        }
    }
}