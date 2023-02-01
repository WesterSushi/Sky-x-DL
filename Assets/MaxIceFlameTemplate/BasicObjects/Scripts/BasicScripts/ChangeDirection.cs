using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
    public class ChangeDirection : MonoBehaviour
    {
        public bool Turn45 = false;
        private bool Done;

        void OnTriggerEnter(Collider other)
        {
            if (!Done && other.GetComponent<MainLine>() != null)
            {
                FindObjectOfType<MainLine>().ChangeDirection();
                if (Turn45)
                {
                    FindObjectOfType<MainLine>().Invoke("EndTurn", 0.005f);
                }
                Done = true;
            }
        }
    }
}