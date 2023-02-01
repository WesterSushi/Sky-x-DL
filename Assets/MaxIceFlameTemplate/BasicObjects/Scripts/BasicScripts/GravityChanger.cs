using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
    public class GravityChanger : MonoBehaviour
    {
        public Vector3 Gravity = new Vector3(0f, -10f, 0f);

        public void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<MainLine>())
            {
                Physics.gravity = Gravity;
            }
        }
    }
}