using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
    public class Teleport : MonoBehaviour
    {
        public GameObject TeleportObject;
        public Vector3 TeleportPosition = Vector3.zero;
        public bool TeleportToObject = true;

        public void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<MainLine>())
            {
                if(TeleportToObject)
                {
                    other.transform.position = TeleportObject.transform.position;
                }
                else
                {
                    other.transform.position = TeleportPosition;
                }
            }
        }
    }
}