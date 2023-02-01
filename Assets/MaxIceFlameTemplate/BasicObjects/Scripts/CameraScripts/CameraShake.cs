using UnityEngine;
using System.Collections;

namespace MaxIceFlameTemplate.Camera
{
    public class CameraShake : MonoBehaviour
    {
        [HideInInspector] public bool startShake = false;  //camera是否开始震动
        [HideInInspector] public float seconds = 0f;    //震动持续秒数
        [HideInInspector] public bool started = false;    //是否已经开始震动
        [HideInInspector] public float quake = 0.2f;       //震动系数
        private Vector3 camPOS;  //camera的起始位置
        private Vector3 deltaPos = Vector3.zero;

        void LateUpdate()
        {
            if (startShake)
            {
                transform.localPosition -= deltaPos;
                deltaPos = Random.insideUnitSphere * (quake / 5);
                transform.localPosition += deltaPos;
            }
            if (started)
            {
                StartCoroutine(WaitForSecond(seconds));
                started = false;
            }
        }

        public void ShakeFor(float a, float b)
        {
            seconds = a;
            started = true;
            startShake = true;
            quake = b;
        }

        IEnumerator WaitForSecond(float a)
        {
            yield return new WaitForSeconds(a);
            startShake = false;
        }
    }
}