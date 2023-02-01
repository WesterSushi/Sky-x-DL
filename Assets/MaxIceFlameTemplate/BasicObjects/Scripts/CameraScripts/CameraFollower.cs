using UnityEngine;
using DG.Tweening;
using MaxIceFlameTemplate.Basic;

namespace MaxIceFlameTemplate.Camera
{
    public class CameraFollower : MonoBehaviour
    {
        public MainLine Player;
        private Transform Camera;
        public Vector3 AddPosition = Vector3.zero;
        public Vector3 Rotate = new Vector3(45f, 45f, 0f);
        public float DistanceFromObject = 25f;
        public float FollowSpeed = 1.2f;
        public bool Following = true;
        public Tween DoPos, DoRot, DoDis, DoSpe;

        void Start()
        {
            Camera = transform.GetChild(0);
        }

        void Update()
        {
            if (Following)
            {
                transform.eulerAngles = Rotate;
                Camera.localPosition = new Vector3(0f, 0f, -DistanceFromObject);
                Vector3 BaseTransform = Player.transform.position + AddPosition;
                transform.position = Vector3.Slerp(transform.position, BaseTransform, Mathf.Abs(FollowSpeed * Time.deltaTime));
            }
            if (Player.Is_Stop && Player.Over && Following)
            {
                Following = false;
                DoPos.Kill();
                DoRot.Kill();
                DoDis.Kill();
                DoSpe.Kill();
            }
        }
    }
}