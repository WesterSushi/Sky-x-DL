using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using MaxIceFlameTemplate.UI;
using DG.Tweening;

namespace MaxIceFlameTemplate.Basic
{
    public class MainLine : MonoBehaviour
    {
        [Serializable]
        public class MainObjects//基础组件
        {
            public GameObject Tail;
            public GameObject DieCubes;
            public GameObject LandEffect;
            public AudioClip DieSound_Normal;
            public UnityEngine.Camera MainCamera;
            public Color LineColor = new Color(1f, 1f, 1f, 1f), DiamondColor = new Color(1f, 1f, 1f, 1f);
            public Vector3 Forward = Vector3.zero, TurnForward1 = new Vector3(0f, 90f, 0f), TurnForward2 = Vector3.zero;
            public Vector3 Gravity = new Vector3(0f, -9.8f, 0f);
            public float Speed = 2.4f;
            public int Percentage = 0;
            public bool EnableTurn = true;
            public bool EnableInvincible = false;
            public KeyCode ReplayKey = KeyCode.Escape;
            public bool EnableReplay = true;
        }
        [Serializable]
        public class LevelInfos//关卡信息
        {
            public int LevelRecordId = 0;
            public string LevelName = "标题";
            public int MaxDiamondCount = 10;
            public bool HasCrown = true;
        }
        [Serializable]
        public class GUIObjects//UI相关组件
        {
            public Text PercentageText;
            public GameOver GameOverInterface;
            public GameObject RevivalInterface;
            public LevelInfos LevelInformation;
        }
        [Serializable]
        public class GameEvents//事件
        {
            public UnityEvent OnGameStart, OnGameOver, OnGameWin, OnChangeDirection, OnTouchGround, OnLeaveGround, OnPickGem;
        }

        public MainObjects mainObjects;
        public GUIObjects gUIObjects;
        public GameEvents gameEvents;

        private GameObject[] LineTails;
        [HideInInspector] public AudioSource start_audio;
        [HideInInspector] public Material LineMaterial, DiamondMaterial;
        [HideInInspector] public bool Is_Stop = false;
        [HideInInspector] public bool start = false;
        [HideInInspector] public bool Over = true;
        [HideInInspector] public bool isFall = true;
        [HideInInspector] public bool Pause = false;
        [HideInInspector] public bool keydown = false;
        [HideInInspector] public bool Win = false;
        [HideInInspector] public GameObject LineBody;
        [HideInInspector] public GameObject Crown1;
        [HideInInspector] public GameObject Crown2;
        [HideInInspector] public GameObject Crown3;
        [HideInInspector] public int DiamondCount = 0, CrownCount = 0;

        void Awake()
        {
            start_audio = GetComponent<AudioSource>();
            LineBody = mainObjects.Tail;
            mainObjects.Forward = mainObjects.TurnForward1;
            transform.localEulerAngles = mainObjects.Forward;
            LineMaterial.color = mainObjects.LineColor;
            DiamondMaterial.color = mainObjects.DiamondColor;
            Physics.gravity = mainObjects.Gravity;
            DOTween.Clear();
        }

        void Start()
        {
            if (gUIObjects.PercentageText != null)
            {
                InvokeRepeating("SetPercentage", 0f, start_audio.GetComponent<AudioSource>().clip.length / 100);
            }
        }

        public void GameOver(bool win, bool stop)
        {
            if (!win)
            {
                gameEvents.OnGameOver.Invoke();
                Over = true;
                Instantiate(mainObjects.DieCubes, transform.position, transform.rotation);
                AudioSource.PlayClipAtPoint(mainObjects.DieSound_Normal, transform.position);
                if (start_audio)
                {
                    float Time = start_audio.time;
                    start_audio.Pause();
                    start_audio.time = Time;
                }
                Is_Stop = stop;
                GetComponent<Rigidbody>().isKinematic = stop;
                if (Crown1 != null)
                {
                    if (gUIObjects.RevivalInterface != null)
                    {
                        gUIObjects.RevivalInterface.GetComponent<CanvasGroup>().alpha = 0;
                        gUIObjects.RevivalInterface.SetActive(true);
                        DOTween.To(() => gUIObjects.RevivalInterface.GetComponent<CanvasGroup>().alpha, x => gUIObjects.RevivalInterface.GetComponent<CanvasGroup>().alpha = x, 1, 2);
                    }
                }
                else
                {
                    if (gUIObjects.GameOverInterface != null)
                    {
                        gUIObjects.GameOverInterface.GetComponent<CanvasGroup>().alpha = 0;
                        gUIObjects.GameOverInterface.gameObject.SetActive(true);
                        DOTween.To(() => gUIObjects.GameOverInterface.GetComponent<CanvasGroup>().alpha, x => gUIObjects.GameOverInterface.GetComponent<CanvasGroup>().alpha = x, 1, 2);
                    }
                }
            }
            else
            {
                if (PlayerPrefs.GetInt(gUIObjects.LevelInformation.LevelRecordId.ToString() + "CrownCount") < CrownCount)
                {
                    PlayerPrefs.SetInt(gUIObjects.LevelInformation.LevelRecordId.ToString() + "CrownCount", CrownCount);
                }
                if (PlayerPrefs.GetInt(gUIObjects.LevelInformation.LevelRecordId.ToString() + "Percentage") < mainObjects.Percentage)
                {
                    PlayerPrefs.SetInt(gUIObjects.LevelInformation.LevelRecordId.ToString() + "Percentage", mainObjects.Percentage);
                }
                if (PlayerPrefs.GetInt(gUIObjects.LevelInformation.LevelRecordId.ToString() + "DiamondCount") < DiamondCount)
                {
                    PlayerPrefs.SetInt(gUIObjects.LevelInformation.LevelRecordId.ToString() + "DiamondCount", DiamondCount);
                }
                if (gUIObjects.LevelInformation.HasCrown)
                {
                    if (mainObjects.Percentage >= 100 && DiamondCount >= gUIObjects.LevelInformation.MaxDiamondCount && CrownCount >= 3)
                    {
                        PlayerPrefs.SetInt(gUIObjects.LevelInformation.LevelRecordId.ToString() + "_Perfect_HasCrown", 1);
                    }
                }
                if (gUIObjects.GameOverInterface != null)
                {
                    gUIObjects.GameOverInterface.GetComponent<CanvasGroup>().alpha = 0;
                    gUIObjects.GameOverInterface.gameObject.SetActive(true);
                    DOTween.To(() => gUIObjects.GameOverInterface.GetComponent<CanvasGroup>().alpha, x => gUIObjects.GameOverInterface.GetComponent<CanvasGroup>().alpha = x, 1, 2);
                }
                Win = true;
                Is_Stop = true;
            }
        }

        void Update()
        {
            if (!Over && !Is_Stop && start)
            {
                if (IsGrounded())
                {
                    if (mainObjects.EnableTurn)
                    {
                        if ((Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Space)) && !keydown)
                        {
                            keydown = true;
                            ChangeDirection();
                        }
                        else if (!(Input.GetMouseButton(0) && keydown))
                        {
                            keydown = false;
                        }
                    }
                }
                else
                {
                    if (LineBody != null)
                    {
                        LineBody = null;
                    }
                }
                transform.Translate(Vector3.forward * mainObjects.Speed * 5f * Time.deltaTime, Space.Self);
                if (LineBody != null)
                {
                    LineBody.transform.localScale = new Vector3(LineBody.transform.localScale.x, LineBody.transform.localScale.y, LineBody.transform.localScale.z + 5f * mainObjects.Speed * Time.deltaTime);
                    LineBody.transform.Translate(Vector3.forward * 2.5f * mainObjects.Speed * Time.deltaTime, Space.Self);
                }
                LineTails = GameObject.FindGameObjectsWithTag("LineTail");
            }
            if (!start)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                {
                    gameEvents.OnGameStart.Invoke();
                    keydown = true;
                    start = true;
                    Over = false;
                    Is_Stop = false;
                    CreateLineBody();
                    if (start_audio)
                    {
                        start_audio.Play();
                    }
                }
            }
            if (Over && Is_Stop)
            {
                if (PlayerPrefs.GetInt(gUIObjects.LevelInformation.LevelRecordId.ToString() + "Percentage") < mainObjects.Percentage)
                {
                    PlayerPrefs.SetInt(gUIObjects.LevelInformation.LevelRecordId.ToString() + "Percentage", mainObjects.Percentage);
                }
                if (PlayerPrefs.GetInt(gUIObjects.LevelInformation.LevelRecordId.ToString() + "DiamondCount") < DiamondCount)
                {
                    PlayerPrefs.SetInt(gUIObjects.LevelInformation.LevelRecordId.ToString() + "DiamondCount", DiamondCount);
                }
            }
            if (mainObjects.EnableReplay)//重置游戏
            {
                if (Input.GetKeyDown(mainObjects.ReplayKey))
                {
                    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                }
            }
            if (mainObjects.Percentage > 100)//百分比上限
            {
                mainObjects.Percentage = 100;
            }
            if (DiamondCount > gUIObjects.LevelInformation.MaxDiamondCount)//钻石上限
            {
                DiamondCount = gUIObjects.LevelInformation.MaxDiamondCount;
            }
            if (CrownCount > 3)//皇冠上限
            {
                CrownCount = 3;
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (!Over)
            {
                if (mainObjects.EnableInvincible)
                {
                    if (mainObjects.LandEffect && isFall && start)
                    {
                        Destroy(Instantiate(mainObjects.LandEffect, new Vector3(transform.position.x, transform.position.y - transform.lossyScale.y / 2 + 0.2f, transform.position.z), Quaternion.Euler(90f, 0f, 0f)), 1f);
                        gameEvents.OnTouchGround.Invoke();
                    }
                }
                else
                {
                    if (collision.collider.tag == "Wall")
                    {
                        GameOver(false, true);
                    }
                    else
                    {
                        if (mainObjects.LandEffect && isFall && start)
                        {
                            Destroy(Instantiate(mainObjects.LandEffect, new Vector3(transform.position.x, transform.position.y - transform.lossyScale.y / 2 + 0.2f, transform.position.z), Quaternion.Euler(90f, 0f, 0f)), 1f);
                            gameEvents.OnTouchGround.Invoke();
                        }
                    }
                }
                if (start)
                {
                    CreateLineBody();
                }
            }
            isFall = false;
        }

        void OnCollisionExit(Collision collision)
        {
            isFall = !IsGrounded();
            gameEvents.OnLeaveGround.Invoke();
        }

        public void ChangeDirection()
        {
            if (mainObjects.Forward == mainObjects.TurnForward1)
            {
                mainObjects.Forward = mainObjects.TurnForward2;
            }
            else
            {
                mainObjects.Forward = mainObjects.TurnForward1;
            }
            transform.eulerAngles = mainObjects.Forward;
            CreateLineBody();
            gameEvents.OnChangeDirection.Invoke();
        }

        public void CreateLineBody()
        {
            LineBody = Instantiate(mainObjects.Tail, transform.position, transform.rotation);
        }

        public void SetPercentage()
        {
            gUIObjects.PercentageText.GetComponent<Text>().text = mainObjects.Percentage + "%";
            if (start && Over != true && mainObjects.Percentage < 100)
            {
                mainObjects.Percentage = mainObjects.Percentage + 1;
            }
            if (start && Win)
            {
                mainObjects.Percentage = 100;
            }
        }

        public bool IsGrounded()
        {
            return Physics.Raycast(transform.position, Vector3.down, transform.localScale.y / 2 + 0.1f);
        }

        public void GameRevival()
        {
            if (Crown3 != null)
            {
                Crown3.GetComponent<Crown>().Revival();
            }
            else if (Crown2 != null)
            {
                Crown2.GetComponent<Crown>().Revival();
            }
            else
            {
                Crown1.GetComponent<Crown>().Revival();
            }
        }

        public void EndTurn()
        {
            if (LineTails.Length - 2 >= 0)
            {
                LineTails[LineTails.Length - 1].transform.localScale = LineTails[LineTails.Length - 1].transform.localScale - new Vector3(0f, 0f, 0.585f);             
                LineTails[LineTails.Length - 2].transform.localScale = LineTails[LineTails.Length - 2].transform.localScale - new Vector3(0f, 0f, 0.585f);
            }
        }
    }
}