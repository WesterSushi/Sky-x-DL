using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
    public class Diamond : MonoBehaviour
    {
        [HideInInspector] public GameObject GetEffect;

        void Update()
        {
            transform.Rotate(Vector3.up, Time.deltaTime * 45f);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<MainLine>() != null)
            {
                other.GetComponent<MainLine>().DiamondCount += 1;
                other.GetComponent<MainLine>().gameEvents.OnPickGem.Invoke();
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<SphereCollider>().enabled = false;
                Destroy(Instantiate(GetEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f)), 10f);
            }
        }
    }
}