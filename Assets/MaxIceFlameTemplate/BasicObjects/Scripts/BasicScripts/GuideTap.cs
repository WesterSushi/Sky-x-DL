using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
    public class GuideTap : MonoBehaviour
    {
        public GameObject PlayEffect;
        public bool AutoPlay;
        private bool Done = false;

        void Update()
        {
            if (AutoPlay)
            {
                GetComponent<BoxCollider>().size = new Vector3(0.001f, 0.001f, 1.5f);
            }
            else
            {
                GetComponent<BoxCollider>().size = new Vector3(1f, 1f, 1.5f);
            }
        }

        void OnTriggerStay(Collider other)
        {
            if (other.GetComponent < MainLine>())
            {
                if (!AutoPlay)
                {
                    if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                    {
                        Instantiate(PlayEffect, transform.position, transform.rotation);
                        Destroy(gameObject);
                    }
                }
                else
                {
                    if (!Done)
                    {
                        FindObjectOfType<MainLine>().GetComponent<MainLine>().ChangeDirection();
                        Instantiate(PlayEffect, transform.position, transform.rotation);
                        Destroy(gameObject);
                        Done = true;
                    }
                }
            }
        }
    }
}