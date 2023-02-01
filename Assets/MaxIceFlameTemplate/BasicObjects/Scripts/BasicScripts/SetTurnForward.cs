using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
    public class SetTurnForward : MonoBehaviour
    {
        public Vector3 NewTurnFoward1 = new Vector3(0f, 90f, 0f);
        public Vector3 NewTurnFoward2 = new Vector3(0f, 0f, 0f);

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<MainLine>())
            {
                FindObjectOfType<MainLine>().mainObjects.TurnForward1 = NewTurnFoward1;
                FindObjectOfType<MainLine>().mainObjects.TurnForward2 = NewTurnFoward2;
            }
        }
    }
}