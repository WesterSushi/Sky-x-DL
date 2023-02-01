using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
    public class ObjectFollower : MonoBehaviour
    {
        public Transform FollowObject;
        public float SmoothTime = 0.5f;
        private Vector3 Velocity = Vector3.zero;
        public Vector3 Position = Vector3.zero;
        public Vector3 Rotation = Vector3.zero;

        void Start()
        {
            transform.Rotate(Rotation);
        }

        void Update()
        {
            transform.position = Vector3.SmoothDamp(transform.position, FollowObject.position + Position, ref Velocity, SmoothTime * (Time.deltaTime * 45f));
        }
    }
}