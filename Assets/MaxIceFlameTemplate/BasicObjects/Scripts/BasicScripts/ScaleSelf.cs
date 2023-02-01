using UnityEngine;
using DG.Tweening;

namespace MaxIceFlameTemplate.Basic
{
    public class ScaleSelf : MonoBehaviour
    {
        private Vector3 scale = new Vector3(0, 0, 0);
        private float waittime = 3f, scaletime = 1.5f;

        void Start()
        {
            Invoke("Scale", waittime + Random.Range(-0.75f, 0.75f));
        }
        void Scale()
        {
            transform.DOScale(scale, scaletime + Random.Range(-0.75f, 0.75f));
        }
    }
}