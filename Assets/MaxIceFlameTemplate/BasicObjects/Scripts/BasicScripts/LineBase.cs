using UnityEngine;
using DG.Tweening;
using MaxIceFlameTemplate.Camera;

namespace MaxIceFlameTemplate.Basic
{
    public class LineBase : MonoBehaviour
    {
        private MainLine MainLine;
        private AudioSource Audio;
        private Vector3 DownPosition;
        public Vector3 DownRotation = Vector3.zero;
        public float DownTime = 1.5f;
        public float DownValue = 2f;
        public float LandEffectDeviation = 0.5f;
        public bool ControlledByUI = true;
        private bool Done = false;

        void Awake()
        {
            MainLine = FindObjectOfType<MainLine>();
            transform.DOKill();
            MainLine.transform.DOKill();
            DownPosition = new Vector3(0f, transform.localPosition.y - DownValue - 0.01f, 0f);
        }

        void Start()
        {
            MainLine.enabled = false;        
            MainLine.GetComponent<Rigidbody>().isKinematic = true;
            MainLine.transform.parent = transform;
            if (MainLine.mainObjects.MainCamera.transform.parent.GetComponent<CameraFollower>())
            {
                MainLine.mainObjects.MainCamera.transform.parent.GetComponent<CameraFollower>().enabled = false;
            }
        }

        void Update()
        {
            if (!Done && !ControlledByUI && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
            {
                transform.DOLocalMove(DownPosition, DownTime);
                MainLine.transform.DOLocalRotate(DownRotation, DownTime);
                Destroy(Instantiate(MainLine.mainObjects.LandEffect, new Vector3(MainLine.transform.localPosition.x, MainLine.transform.localPosition.y + LandEffectDeviation, MainLine.transform.localPosition.z), Quaternion.Euler(90f, 0f, 0f)), 3f);
                if (Audio != null)
                {
                    Destroy(Audio.gameObject);
                }
                Invoke("Starting", DownTime);
                Done = true;
            }
            if (GameObject.FindGameObjectWithTag("DontDestroyOnLoad") != null)
            {
                Audio = GameObject.FindGameObjectWithTag("DontDestroyOnLoad").GetComponent<AudioSource>();
            }
            else
            {
                Audio = null;
            }
        }

        void Starting()
        {
            transform.DOKill();
            MainLine.transform.DOKill();
            transform.DetachChildren();
            MainLine.transform.localEulerAngles = MainLine.mainObjects.TurnForward1;
            MainLine.enabled = true;
            MainLine.GetComponent<Rigidbody>().isKinematic = false;
            if (MainLine.mainObjects.MainCamera.transform.parent.GetComponent<CameraFollower>())
            {
                MainLine.mainObjects.MainCamera.transform.parent.GetComponent<CameraFollower>().enabled = true;
            }
        }

        public void LaunchBase()
        {
            if (!Done && ControlledByUI)
            {
                transform.DOLocalMove(DownPosition, DownTime);
                MainLine.transform.DOLocalRotate(DownRotation, DownTime);
                Destroy(Instantiate(MainLine.mainObjects.LandEffect, new Vector3(MainLine.transform.localPosition.x, MainLine.transform.localPosition.y + LandEffectDeviation, MainLine.transform.localPosition.z), Quaternion.Euler(90f, 0f, 0f)), 3f);
                if (Audio != null)
                {
                    Destroy(Audio.gameObject);
                }
                Invoke("Starting", DownTime);
                Done = true;
            }
        }
    }
}