using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
    public class GuideTapMaker : MonoBehaviour
    {
        public GameObject GuideTap;

        void Update()
        {
            if (FindObjectOfType<MainLine>().start && !FindObjectOfType<MainLine>().isFall)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                {
                    Instantiate(GuideTap, new Vector3(transform.position.x, transform.position.y - 0.45f, transform.position.z), Quaternion.Euler(90f, 0f, 0f));
                }
            }
        }
    }
}