using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
    public class SetActive : MonoBehaviour
    {
        public GameObject[] Objects;

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<MainLine>())
            {
                for (int i = 0; i < Objects.Length; i++)
                {
                    if (Objects[i].activeSelf)
                    {
                        Objects[i].SetActive(false);
                    }
                    else
                    {
                        Objects[i].SetActive(true);
                    }
                }
            }
        }
    }
}