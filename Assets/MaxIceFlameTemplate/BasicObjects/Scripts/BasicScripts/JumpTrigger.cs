using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
    public class JumpTrigger : MonoBehaviour
    {
        public float JumpPower;

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<MainLine>())
            {
                other.GetComponent<Rigidbody>().AddForce(new Vector3(0f, JumpPower, 0f), ForceMode.VelocityChange);
            }
        }
    }
}