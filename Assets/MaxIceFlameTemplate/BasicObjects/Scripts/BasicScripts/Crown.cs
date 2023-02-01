using System.Collections;
using UnityEngine;
using DG.Tweening;
using MaxIceFlameTemplate.Camera;

namespace MaxIceFlameTemplate.Basic
{
    public class Crown : MonoBehaviour
    {
        private MainLine MainLine;
        public CrownIcon CrownIcon;
        [HideInInspector] public GameObject CrownEffect;
        private GameObject EffectObject;
        public bool AutoRecord = true;
        public Vector3 RevivalForward = Vector3.zero;
        public float RevivalAudioTime = 0f;
        public int RevivalPercentage = 0;
        private bool Get = false;
        private bool Used = false;
        private GameObject[] Tails;
        private Diamond[] Diamonds;
        private bool ParticleRunning;
        private float DistanceToTarget = 0f;
        private float MoveSpeed = 0f;
        private float ShootAngle = 60f;

        void Start()
        {
            CrownIcon.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 0f);
            DistanceToTarget = Vector3.Distance(transform.position, CrownIcon.transform.position);
            MoveSpeed = DistanceToTarget * 1.28f;
        }

        void Update()
        {
            MainLine = FindObjectOfType<MainLine>();
            transform.Rotate(Vector3.up, Time.deltaTime * 45f);
            Tails = GameObject.FindGameObjectsWithTag("LineTail");
            Diamonds = FindObjectsOfType<Diamond>();
        }

        public void EnterTrigger()
        {
            MainLine.gameEvents.OnPickGem.Invoke();
            if (AutoRecord)
            {
                RevivalForward = MainLine.mainObjects.Forward;
                RevivalAudioTime = MainLine.start_audio.time;
                RevivalPercentage = MainLine.mainObjects.Percentage;
            }
            if (!Get)
            {
                if (MainLine.Crown1 == null)
                {
                    MainLine.Crown1 = gameObject;
                }
                else if (MainLine.Crown2 == null)
                {
                    MainLine.Crown2 = gameObject;
                }
                else
                {
                    MainLine.Crown3 = gameObject;
                }
                GetComponent<MeshRenderer>().enabled = false;
                MainLine.GetComponent<MainLine>().CrownCount += 1;
                EffectObject = Instantiate(CrownEffect, transform.position, Quaternion.Euler(Vector3.zero));
                ParticleRunning = true;
                StartCoroutine(EfectShoot());
                Get = true;
            }
        }

        public void Revival()
        {
            if (!Used)
            {
                MainLine.CrownCount -= 1;
                Used = true;
            }
            MainLine.DiamondCount = 0;
            CrownIcon.transform.GetChild(0).GetComponent<MeshRenderer>().material.DOFade(0f, 1f);
            for (int a = 0; a < Tails.Length; a++)
            {
                Destroy(Tails[a].gameObject);
            }
            MainLine.LineBody = null;
            MainLine.mainObjects.Percentage = RevivalPercentage;
            MainLine.transform.position = transform.position;
            MainLine.GetComponent<Rigidbody>().isKinematic = false;
            MainLine.mainObjects.Forward = RevivalForward;
            MainLine.transform.eulerAngles = RevivalForward;
            MainLine.Over = false;
            MainLine.Is_Stop = true;
            MainLine.start = false;
            MainLine.start_audio.time = RevivalAudioTime;
            if (MainLine.mainObjects.MainCamera.transform.parent.GetComponent<CameraFollower>() != null)
            {
                MainLine.mainObjects.MainCamera.transform.parent.GetComponent<CameraFollower>().Following = true;
            }
            for (int a = 0; a < Diamonds.Length; a++)
            {
                Diamonds[a].GetComponent<MeshRenderer>().enabled = true;
                Diamonds[a].GetComponent<SphereCollider>().enabled = true;
            }
        }

        public IEnumerator EfectShoot()
        {
            while (ParticleRunning)
            {
                EffectObject.transform.LookAt(CrownIcon.transform.position, Vector3.up);
                float num = Mathf.Min(1f, Vector3.Distance(EffectObject.transform.position, CrownIcon.transform.position) / DistanceToTarget) * ShootAngle;
                EffectObject.transform.rotation = EffectObject.transform.rotation * Quaternion.Euler(Mathf.Clamp(-num, -ShootAngle, ShootAngle), 0f, 0f);
                float num2 = Vector3.Distance(EffectObject.transform.position, CrownIcon.transform.position);
                EffectObject.transform.Translate(Vector3.forward * Mathf.Min(MoveSpeed * Time.deltaTime, num2));
                if (num2 <= 0.1f)
                {
                    ParticleRunning = false;
                    CrownIcon.transform.GetChild(0).GetComponent<MeshRenderer>().material.DOFade(1f, 1f);
                    Destroy(EffectObject, 10f);
                }
                yield return null;
            }
            yield break;
        }
    }
}