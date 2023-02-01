using UnityEngine;

namespace MaxIceFlameTemplate.UI
{
    public class LevelInformation : MonoBehaviour
    {
        public GameObject ThisLevelCrownModel1, ThisLevelCrownModel2, ThisLevelCrownModel3;
        public GameObject ThisLevelCrownHolder;
        public GameObject ThisLevelPerfectCrownHolder;
        public AudioClip ThisLevelSound;
        public Color ThisLevelCameraBackColor = new Color(1f, 1f, 1f, 1f);
        public string SceneName;
        public string LevelName = "标题";
        [HideInInspector] public Vector3 ModelPosition;
        public int RecordId;
        public int MaxDiamondCount = 10;
        public bool HasCrown = true;

        void Awake()
        {
            ModelPosition = new Vector3(transform.position.x - (transform.position.x * 2), 0f, 0f);
        }

        void Update()
        {
            if (HasCrown)
            {
                ThisLevelCrownHolder.SetActive(true);
            }
            else
            {
                ThisLevelCrownHolder.SetActive(false);
            }
        }
    }
}